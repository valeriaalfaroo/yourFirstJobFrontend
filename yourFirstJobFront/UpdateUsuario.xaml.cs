using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Text;
using archivos.Entidades.request;
using yourFirstJobFront.Entidades.response;
using yourFirstJobFront.Entidades.request;

namespace yourFirstJobFront;

public partial class UpdateUsuario : TabbedPage
{
    String laURL = "https://yourfirstjobback.azurewebsites.net/";
    string url= "https://localhost:44364/";

    public Usuario usuario { get; set; }
    public int SelectedIndexHabilidad { get; set; }


    public UpdateUsuario()
    {
        InitializeComponent();
        LoadUsuario();
    }

    //Carga el usuario
    private async void LoadUsuario()
    {
        try
        {

            ReqObtenerUsuario req = new ReqObtenerUsuario();

            req.idUser = Sesion.usuarioSesion.idUsuario;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/ObtenerUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResObtenerPerfilUsuario res = new ResObtenerPerfilUsuario();

                res = JsonConvert.DeserializeObject<ResObtenerPerfilUsuario>(responseContent);

                if (res.resultado)
                {
                    Usuario usuario = new Usuario();

                    usuario.idUsuario = res.usuario.idUsuario;
                    usuario.nombreUsuario = res.usuario.nombreUsuario;
                    usuario.apellidos = res.usuario.apellidos;
                    usuario.correo = res.usuario.correo;
                    usuario.telefono = res.usuario.telefono;
                    usuario.fechaNacimiento = res.usuario.fechaNacimiento;
                    usuario.idRegion = res.usuario.idRegion;
                    usuario.sitioWeb = res.usuario.sitioWeb;

                    displayUserInfo(usuario);

                    #region

                    //Valido idiomas existen
                    if (res.usuario.listaIdiomas.Any())
                    {
                        //Hay elementos

                        //Meto Idiomas
                        List<Idiomas> listaIdomas = new List<Idiomas>();

                        foreach (Idiomas item in res.usuario.listaIdiomas)
                        {
                            Idiomas idiomas = new Idiomas();

                            idiomas.idIdioma = item.idIdioma;
                            idiomas.idioma = item.idioma;
                            idiomas.nivel = item.nivel;

                            listaIdomas.Add(idiomas);

                        }

                        usuario.listaIdiomas = listaIdomas;

                        idiomasListView.ItemsSource = usuario.listaIdiomas;


                    }
                    else
                    {
                        //No hay elementos


                    }

                    //Valido habilidades existen
                    if (res.usuario.listaHabilidades.Any())
                    {
                        //Hay elementos

                        //Meto Habilidades
                        List<Habilidades> listaHabilidades = new List<Habilidades>();

                        foreach (Habilidades item in res.usuario.listaHabilidades)
                        {
                            Habilidades habilidad = new Habilidades();

                            habilidad.idHabilidades = item.idHabilidades;
                            habilidad.categoria = item.categoria;
                            habilidad.descripcion = item.descripcion;

                            listaHabilidades.Add(habilidad);

                        }

                        usuario.listaHabilidades = listaHabilidades;

                        habilidadesListView.ItemsSource = usuario.listaHabilidades;

                    }
                    else
                    {
                        //No hay elementos

                    }

                    //Valido estudios existen
                    if (res.usuario.listaEstudios.Any())
                    {
                        //Hay elementos

                        //Meto Estudios
                        List<Estudios> listaEstudios = new List<Estudios>();

                        foreach (Estudios item in res.usuario.listaEstudios)
                        {
                            Estudios estudio = new Estudios();

                            estudio.idEstudios = item.idEstudios;
                            estudio.nombreInstitucion = item.nombreInstitucion;
                            estudio.gradoAcademico = item.gradoAcademico;

                            Profesion profesion = new Profesion();

                            profesion.nombreProfesion = item.profesion.nombreProfesion;
                            profesion.descripcion = item.profesion.descripcion;

                            estudio.profesion = profesion;

                            estudio.fechaInicio = item.fechaInicio;
                            estudio.fechaFinalizacion = item.fechaFinalizacion;

                            listaEstudios.Add(estudio);

                        }

                        usuario.listaEstudios = listaEstudios;

                        estudiosListView.ItemsSource = usuario.listaEstudios;

                    }
                    else
                    {
                        //No hay elementos


                    }

                    //Valido experiencia existen
                    if (res.usuario.listaExperienciaLaboral.Any())
                    {
                        //Hay elementos

                        //Meto Experiencia Laboral
                        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();

                        foreach (ExperienciaLaboral item in res.usuario.listaExperienciaLaboral)
                        {

                            ExperienciaLaboral experiencia = new ExperienciaLaboral();

                            experiencia.idExperiencia = item.idExperiencia;

                            Profesion profesion = new Profesion();

                            profesion.nombreProfesion = item.profesion.nombreProfesion;
                            profesion.descripcion = item.profesion.descripcion;

                            experiencia.profesion = profesion;

                            experiencia.puesto = item.puesto;
                            experiencia.nombreEmpresa = item.nombreEmpresa;
                            experiencia.responsabilidades = item.responsabilidades;
                            experiencia.fechaInicio = item.fechaInicio;
                            experiencia.fechaFinalizacion = item.fechaFinalizacion;

                            listaExperiencia.Add(experiencia);

                        }

                        usuario.listaExperienciaLaboral = listaExperiencia;

                        experienciaListView.ItemsSource = usuario.listaExperienciaLaboral;

                    }
                    else
                    {
                        //No hay elementos

                    }

                    //valido si archvios existen
                    if (res.usuario.listaArchivosUsuarios.Any())
                    {
                        List<ArchivosUsuario> listaArchivos = res.usuario.listaArchivosUsuarios
                            .Where(item => item.tipo.ToLower() == "pdf" || item.tipo.ToLower() == "png") // Filter for PDF and png files only
                            .Select(item => new ArchivosUsuario
                            {
                                idArchivosUsuarios = item.idArchivosUsuarios,
                                nombreArchivo = item.nombreArchivo,
                                archivo = item.archivo,
                                //  tipo = item.tipo
                            }).ToList();

                        usuario.listaArchivosUsuarios = listaArchivos;
                        ArchivosListView.ItemsSource = usuario.listaArchivosUsuarios;
                    }

                    #endregion



                }


                else
                {
                    await DisplayAlert("Error", "Error en la conexion", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion" + ex.Message, "Aceptar");
        }
    }

    //carga los archivos --opcional
    //obtener los archivos
    private async void cargarArchivos()
    {

        try
        {
            ReqObtenerArchivosUsuario req = new ReqObtenerArchivosUsuario
            {
                idUsuario = Sesion.usuarioSesion.idUsuario //  ID del usuario correspondiente
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(laURL + "api/usuario/obtenerArchivoUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResObtenerArchviosUsuario>(responseContent);

                if (res.resultado)
                {
                    ArchivosListView.ItemsSource = res.listaArchivosUsuario;
                }
                else
                {
                    await DisplayAlert("Error", string.Join(Environment.NewLine, res.listaDeErrores), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicaci�n: " + ex.Message, "Aceptar");
        }
    }

    //Setea la info usuario
    private void displayUserInfo(Usuario usuario)
    {
        entryNombre.Text = usuario.nombreUsuario;
        entryApellidos.Text = usuario.apellidos;
        entryCorreo.Text = usuario.correo;
        entryTelefono.Text = usuario.telefono.ToString();
        pikerFecha.Date = usuario.fechaNacimiento;
        pickerRegion.SelectedIndex = usuario.idRegion - 1;

        if (usuario.sitioWeb != null)
        {
            entrySitioWeb.Text = usuario.sitioWeb;
        }

    }

    //Actualiza info de usuario
    private async void btn_Update_Usuario(object sender, EventArgs e)
    {


        try
        {


            ReqUpdateUsuario req = new ReqUpdateUsuario();

            Usuario user = new Usuario();

            user.idUsuario = Sesion.usuarioSesion.idUsuario;
            user.nombreUsuario = entryNombre.Text;
            user.apellidos = entryApellidos.Text;
            user.correo = entryCorreo.Text;
            user.telefono = int.Parse(entryTelefono.Text);
            user.fechaNacimiento = pikerFecha.Date;
            user.idRegion = pickerRegion.SelectedIndex + 1;
            user.sitioWeb = entrySitioWeb.Text;

            req.usuario = user;



            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/actualizarUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResUpdateUsuario res = new ResUpdateUsuario();

                res = JsonConvert.DeserializeObject<ResUpdateUsuario>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion", "Aceptar");
        }

    }

    //Actualizar idiomas de usuario
    private async void btn_Update_Idiomas(object sender, EventArgs e)
    {
        try
        {
            Dictionary<Tuple<string, string>, int> menuOptionsToIds = new Dictionary<Tuple<string, string>, int>
            {
            { Tuple.Create("ingles", "nativo"), 1 },
            { Tuple.Create("portugues", "nativo"), 2 },
            { Tuple.Create("espanol", "nativo"), 3 },
            { Tuple.Create("ingles", "avanzado"), 4 },
            { Tuple.Create("portugues", "avanzado"), 5 },
            { Tuple.Create("espanol", "avanzado"), 6 },
            { Tuple.Create("ingles", "basico"), 7 },
            { Tuple.Create("portugues", "basico"), 8 },
            { Tuple.Create("espanol", "basico"), 9 }
            };

            List<ReqUpdateUsuarioIdioma> lstReq = new List<ReqUpdateUsuarioIdioma>();

            foreach (Idiomas idioma in idiomasListView.ItemsSource)
            {
                ReqUpdateUsuarioIdioma req = new ReqUpdateUsuarioIdioma();

                Tuple<string, string> opcionSeleccionada = new Tuple<string, string>(idioma.idioma, idioma.nivel);

                req.idIdiomaNuevo = menuOptionsToIds[opcionSeleccionada];

                req.idIdioma = idioma.idIdioma;

                req.idUsuario = Sesion.usuarioSesion.idUsuario;

                lstReq.Add(req);

            }


            var jsonContent = new StringContent(JsonConvert.SerializeObject(lstReq), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/actualizarUsuarioIdioma", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResUpdateUsuarioIdioma res = new ResUpdateUsuarioIdioma();

                res = JsonConvert.DeserializeObject<ResUpdateUsuarioIdioma>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }

    }

    //Actualizar habilidades de usuario
    private async void btn_Update_Habilidades(object sender, EventArgs e)
    {
        try
        {
            Dictionary<Tuple<string>, int> menuOptionsToIds = new Dictionary<Tuple<string>, int>
            {
                { Tuple.Create("Trabajo en equipo"), 1 },
                { Tuple.Create("Comunicacion efectiva"), 2 },
                { Tuple.Create("Colaboración"), 3 },
                { Tuple.Create("Flexibilidad"), 4 },
                { Tuple.Create("Empatía"), 5 }
            };

            List<ReqUpdateUsuarioHabilidades> lstReq = new List<ReqUpdateUsuarioHabilidades>();

            foreach (Habilidades habilidad in habilidadesListView.ItemsSource)
            {
                ReqUpdateUsuarioHabilidades req = new ReqUpdateUsuarioHabilidades();

                Tuple<string> opcionSeleccionada = new Tuple<string>(habilidad.categoria);

                //Seteo
                req.idHabilidadNueva = menuOptionsToIds[opcionSeleccionada];
                req.idHabilidad = habilidad.idHabilidades;
                req.idUsuario = Sesion.usuarioSesion.idUsuario;

                lstReq.Add(req);

            }


            var jsonContent = new StringContent(JsonConvert.SerializeObject(lstReq), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/actualizarUsuarioHabilidad", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResUpdateUsuarioHabilidades res = new ResUpdateUsuarioHabilidades();

                res = JsonConvert.DeserializeObject<ResUpdateUsuarioHabilidades>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    //Actualizar estudios de usuario
    private async void btn_Update_Estudios(object sender, EventArgs e)
    {
        try
        {

            Dictionary<Tuple<string>, int> menuOptionsToIds = new Dictionary<Tuple<string>, int>
            {
                 { Tuple.Create("Software Engineer"), 1 },
                 { Tuple.Create("Electrical  Engineer"), 2 },
                 { Tuple.Create("Industrial Engineer"), 3 },
                 { Tuple.Create("Civil Engineer"), 4 },
                 { Tuple.Create("Doctor"), 5 }
            };

            List<ReqUpdateUsuarioEstudios> lstReq = new List<ReqUpdateUsuarioEstudios>();

            foreach (Estudios estudio in estudiosListView.ItemsSource)
            {
                ReqUpdateUsuarioEstudios req = new ReqUpdateUsuarioEstudios();

                //Seteo 
                Estudios estudios = new Estudios();

                req.idUsuario = Sesion.usuarioSesion.idUsuario;
                estudios.idEstudios = estudio.idEstudios;

                estudios.nombreInstitucion = estudio.nombreInstitucion;
                estudios.gradoAcademico = estudio.gradoAcademico;
                estudios.fechaInicio = estudio.fechaInicio;
                estudios.fechaFinalizacion = estudio.fechaFinalizacion;

                Profesion profesion = new Profesion();

                Tuple<string> opcionSeleccionada = new Tuple<string>(estudio.profesion.nombreProfesion);

                profesion.idProfesion = menuOptionsToIds[opcionSeleccionada];

                estudios.profesion = profesion;



                req.estudios = estudios;

                lstReq.Add(req);

            }


            var jsonContent = new StringContent(JsonConvert.SerializeObject(lstReq), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/actualizarUsuarioEstudio", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResUpdateUsuarioEstudios res = new ResUpdateUsuarioEstudios();

                res = JsonConvert.DeserializeObject<ResUpdateUsuarioEstudios>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    //Actualizar experiencia de usuario
    private async void btn_Update_Experiencia(object sender, EventArgs e)
    {
        try
        {

            List<ReqUpdateUsuarioExperiencia> lstReq = new List<ReqUpdateUsuarioExperiencia>();

            foreach (ExperienciaLaboral experiencia in experienciaListView.ItemsSource)
            {
                ReqUpdateUsuarioExperiencia req = new ReqUpdateUsuarioExperiencia();

                Dictionary<Tuple<string>, int> menuOptionsToIds = new Dictionary<Tuple<string>, int>
                {
                    { Tuple.Create("Software Engineer"), 1 },
                    { Tuple.Create("Electrical  Engineer"), 2 },
                    { Tuple.Create("Industrial Engineer"), 3 },
                    { Tuple.Create("Civil Engineer"), 4 },
                    { Tuple.Create("Doctor"), 5 }
                };

                //Seteo 
                ExperienciaLaboral experienciaLaboral = new ExperienciaLaboral();

                req.idUsuario = Sesion.usuarioSesion.idUsuario;
                experienciaLaboral.idExperiencia = experiencia.idExperiencia;

                experienciaLaboral.puesto = experiencia.puesto;
                experienciaLaboral.nombreEmpresa = experiencia.nombreEmpresa;
                experienciaLaboral.responsabilidades = experiencia.responsabilidades;
                experienciaLaboral.fechaInicio = experiencia.fechaInicio;
                experienciaLaboral.fechaFinalizacion = experiencia.fechaFinalizacion;

                Profesion profesion = new Profesion();

                Tuple<string> opcionSeleccionada = new Tuple<string>(experiencia.profesion.nombreProfesion);

                profesion.idProfesion = menuOptionsToIds[opcionSeleccionada];

                experienciaLaboral.profesion = profesion;



                req.experienciaLaboral = experienciaLaboral;

                lstReq.Add(req);

            }


            var jsonContent = new StringContent(JsonConvert.SerializeObject(lstReq), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/actualizarUsuarioExperiencia", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResUpdateUsuarioExperiencia res = new ResUpdateUsuarioExperiencia();

                res = JsonConvert.DeserializeObject<ResUpdateUsuarioExperiencia>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    //Borrrar idioma
    private async void btn_Borrar_Idioma(object sender, EventArgs e)
    {
        ReqEliminarIdiomaUsuario req = new ReqEliminarIdiomaUsuario();

        var selectedItem = (sender as Button).BindingContext as Idiomas;

        req.idIdioma = selectedItem.idIdioma;
        req.idUsuario = Sesion.usuarioSesion.idUsuario;

        try
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/eliminarIdiomaUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResEliminarIdiomaUsuario res = new ResEliminarIdiomaUsuario();

                res = JsonConvert.DeserializeObject<ResEliminarIdiomaUsuario>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    //Borrrar habilidad
    private async void btn_Borrar_Habilidad(object sender, EventArgs e)
    {
        ReqEliminarHabilidadUsuario req = new ReqEliminarHabilidadUsuario();

        var selectedItem = (sender as Button).BindingContext as Habilidades;

        req.idHabilidad = selectedItem.idHabilidades;
        req.idUsuario = Sesion.usuarioSesion.idUsuario;

        try
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/eliminarHabilidadUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResEliminarHabilidadUsuario res = new ResEliminarHabilidadUsuario();

                res = JsonConvert.DeserializeObject<ResEliminarHabilidadUsuario>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    //Borrrar estudio
    private async void btn_Borrar_Estudios(object sender, EventArgs e)
    {
        ReqEliminarEstudioUsuario req = new ReqEliminarEstudioUsuario();

        var selectedItem = (sender as Button).BindingContext as Estudios;

        req.idEstudio = selectedItem.idEstudios;
        req.idUsuario = Sesion.usuarioSesion.idUsuario;

        try
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/eliminarEstudiosUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResEliminarEstudioUsuario res = new ResEliminarEstudioUsuario();

                res = JsonConvert.DeserializeObject<ResEliminarEstudioUsuario>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    //Borrrar experiencia
    private async void btn_Borrar_Expereincia(object sender, EventArgs e)
    {
        ReqEliminarExperienciaUsuario req = new ReqEliminarExperienciaUsuario();

        var selectedItem = (sender as Button).BindingContext as ExperienciaLaboral;

        req.idExperiencia = selectedItem.idExperiencia;
        req.idUsuario = Sesion.usuarioSesion.idUsuario;

        try
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/eliminarExperienciaUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResEliminarExperienciaUsuario res = new ResEliminarExperienciaUsuario();

                res = JsonConvert.DeserializeObject<ResEliminarExperienciaUsuario>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        ReqEliminarArchivosUsuario req = new ReqEliminarArchivosUsuario();

        var selectedItem = (sender as Button).BindingContext as ArchivosUsuario;

        req.idArchivosUsuarios = selectedItem.idArchivosUsuarios;
        try
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, url + "api/usuario/borrarArchivoUsuario");
            request.Content = jsonContent;

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResEliminarArchivosUsuarios res = new ResEliminarArchivosUsuarios();

                res = JsonConvert.DeserializeObject<ResEliminarArchivosUsuarios>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El archivo se elimino correctamente!", "Aceptar");
                   // await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El archvio fallo al eliminar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
           else
            {
                var statusCode = response.StatusCode;
                var responseContent = await response.Content.ReadAsStringAsync();
                // Log or inspect statusCode and responseContent
                await DisplayAlert("Error", $"Server responded with status code: {statusCode}\nResponse content: {responseContent}", "Aceptar");
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }

    }

    
}