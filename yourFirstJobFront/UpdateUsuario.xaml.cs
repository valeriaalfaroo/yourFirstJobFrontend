using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Text;

namespace yourFirstJobFront;

public partial class UpdateUsuario : TabbedPage
{
    String laURL = "https://localhost:44364/";

    public Usuario usuario { get; set; }

    public UpdateUsuario()
	{
		InitializeComponent();
        LoadUsuario();
	}

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


                    ////Meto Region
                    //yourFirstJobFront.Entidades.entities.Region region = new yourFirstJobFront.Entidades.entities.Region();

                    //region.idRegion = res.usuario.region.idRegion;
                    //region.nombreRegion = res.usuario.region.nombreRegion;

                    //usuario.region = region;

                    displayUserInfo(usuario);


                    //**********Temporalmente comentado******************

                    #region

                    //Valido idiomas existen
                    //if (res.usuario.listaIdiomas.Any())
                    //{
                    //    //Hay elementos

                    //    //Meto Idiomas
                    //    List<Idiomas> listaIdomas = new List<Idiomas>();

                    //    foreach (Idiomas item in res.usuario.listaIdiomas)
                    //    {
                    //        Idiomas idiomas = new Idiomas();

                    //        idiomas.idIdioma = item.idIdioma;
                    //        idiomas.idioma = item.idioma;
                    //        idiomas.nivel = item.nivel;

                    //        listaIdomas.Add(idiomas);

                    //    }

                    //    usuario.listaIdiomas = listaIdomas;

                    //    idiomasListView.ItemsSource = usuario.listaIdiomas;

                    //}
                    //else
                    //{
                    //    //No hay elementos
                    //    lblIdiomas.IsVisible = false;
                    //    lineIdiomas.IsVisible = false;

                    //}

                    ////Valido habilidades existen
                    //if (res.usuario.listaHabilidades.Any())
                    //{
                    //    //Hay elementos

                    //    //Meto Habilidades
                    //    List<Habilidades> listaHabilidades = new List<Habilidades>();

                    //    foreach (Habilidades item in res.usuario.listaHabilidades)
                    //    {
                    //        Habilidades habilidad = new Habilidades();

                    //        habilidad.idHabilidades = item.idHabilidades;
                    //        habilidad.categoria = item.categoria;
                    //        habilidad.descripcion = item.descripcion;

                    //        listaHabilidades.Add(habilidad);

                    //    }

                    //    usuario.listaHabilidades = listaHabilidades;

                    //    habilidadesListView.ItemsSource = usuario.listaHabilidades;

                    //}
                    //else
                    //{
                    //    //No hay elementos
                    //    lblExperiencia.IsVisible = false;
                    //    lineExperiencia.IsVisible = false;

                    //}

                    ////Valido estudios existen
                    //if (res.usuario.listaEstudios.Any())
                    //{
                    //    //Hay elementos

                    //    //Meto Estudios
                    //    List<Estudios> listaEstudios = new List<Estudios>();

                    //    foreach (Estudios item in res.usuario.listaEstudios)
                    //    {
                    //        Estudios estudio = new Estudios();

                    //        estudio.idEstudios = item.idEstudios;
                    //        estudio.nombreInstitucion = item.nombreInstitucion;
                    //        estudio.gradoAcademico = item.gradoAcademico;

                    //        Profesion profesion = new Profesion();

                    //        profesion.nombreProfesion = item.profesion.nombreProfesion;
                    //        profesion.descripcion = item.profesion.descripcion;

                    //        estudio.profesion = profesion;

                    //        estudio.fechaInicio = item.fechaInicio;
                    //        estudio.fechaFinalizacion = item.fechaFinalizacion;

                    //        listaEstudios.Add(estudio);

                    //    }

                    //    usuario.listaEstudios = listaEstudios;

                    //    estudiosListView.ItemsSource = usuario.listaEstudios;

                    //}
                    //else
                    //{
                    //    //No hay elementos
                    //    lblEstudios.IsVisible = false;
                    //    lineEstudios.IsVisible = true;

                    //}

                    ////Valido experiencia existen
                    //if (res.usuario.listaExperienciaLaboral.Any())
                    //{
                    //    //Hay elementos

                    //    //Meto Experiencia Laboral
                    //    List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();

                    //    foreach (ExperienciaLaboral item in res.usuario.listaExperienciaLaboral)
                    //    {
                    //        ExperienciaLaboral experiencia = new ExperienciaLaboral();


                    //        Profesion profesion = new Profesion();

                    //        profesion.nombreProfesion = item.profesion.nombreProfesion;
                    //        profesion.descripcion = item.profesion.descripcion;

                    //        experiencia.profesion = profesion;

                    //        experiencia.puesto = item.puesto;
                    //        experiencia.nombreEmpresa = item.nombreEmpresa;
                    //        experiencia.responsabilidades = item.responsabilidades;
                    //        experiencia.fechaInicio = item.fechaInicio;
                    //        experiencia.fechaFinalizacion = item.fechaFinalizacion;

                    //        listaExperiencia.Add(experiencia);

                    //    }

                    //    usuario.listaExperienciaLaboral = listaExperiencia;

                    //    experienciaListView.ItemsSource = usuario.listaExperienciaLaboral;

                    //}
                    //else
                    //{
                    //    //No hay elementos

                    //}

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
}