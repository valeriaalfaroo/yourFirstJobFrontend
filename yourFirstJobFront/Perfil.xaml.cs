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


                    //Meto Region(***No esta sirviendo***)

                    //Region region = new Region();

                    //region.idRegion = res.usuario.region.idRegion;
                    //region.nombre = res.usuario.region.nombreRegion;

                    //usuario.region = region;

                    List<Idiomas>listaIdomas = new List<Idiomas>();

                    foreach (Idiomas item in res.usuario.listaIdiomas)
                    {
                        Idiomas idiomas = new Idiomas();

                        idiomas.idIdioma = item.idIdioma;
                        idiomas.idioma = item.idioma;
                        idiomas.nivel = item.nivel;

                        listaIdomas.Add(idiomas);

                    }

                    usuario.listaIdiomas = listaIdomas;

                    //Test
                    lblNombreCompleto.Text = "Cambio: " + res.usuario.sitioWeb;

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
}