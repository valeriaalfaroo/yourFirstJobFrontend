using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Text;

namespace yourFirstJobFront;

public partial class Perfil : ContentPage
{
    String laURL = "https://localhost:44364/";
	public Usuario usuario {  get; set; }

    public Perfil()
	{
		InitializeComponent();

		lblNombreCompleto.Text = Sesion.usuarioSesion.nombreUsuario + " " + Sesion.usuarioSesion.apellidos;

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
                        lblIdiomas.IsVisible = false;
                        lineIdiomas.IsVisible = false;

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
                        lblHabilidades.IsVisible = false;
                        lineHabilidades.IsVisible = false;

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
                        lblEstudios.IsVisible = false;
                        lineEstudios.IsVisible = true;

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
                        lblExperiencia.IsVisible = false;
                        lineExperiencia.IsVisible = false;

                    }

                    

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
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex.Message, "Aceptar");
        }
    }

    private void displayUserInfo(Usuario usuario)
    {
        lblCorreo.Text = "Correo: " + usuario.correo;
        lblTelefono.Text = "Telefono: " + usuario.telefono;
        lblFechaDeNacimiento.Text = "Fecha de nacimiento: " + usuario.fechaNacimiento;
        lblRegion.Text = "Region: " + usuario.idRegion;

        if (usuario.sitioWeb != null)
        {
            lblSitioWeb.Text = "Sito web: " + usuario.sitioWeb;
        }
        else
        {
            lblSitioWeb.IsVisible = false;
        }
        
    }

    private void Button_Clicked_EditarUsuario(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UpdateUsuario());
    }

    private void Button_Clicked_CerrarSeccion(object sender, EventArgs e)
    {
        Sesion.CerrarSeccion();
        Navigation.PushAsync(new Login());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Ingresar_Info_Usuario());
    }
}