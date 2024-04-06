using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace yourFirstJobFront;

public partial class Aplicar : ContentPage
{
    String laURL = "https://localhost:44364/";
    Usuario usuario = new Usuario();
    int idOfertas; 
    public Aplicar(int idOfertas)
	{
		InitializeComponent();
        this.idOfertas = idOfertas;
        LoadUsuario(); 
	}

    private async void LoadUsuario()
    {
        try
        {
            var usuario = new Usuario();
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
                    usuario.nombreUsuario=res.usuario.nombreUsuario;
                    usuario.apellidos = res.usuario.apellidos;
                    usuario.correo = res.usuario.correo;
                    usuario.fechaNacimiento = res.usuario.fechaNacimiento;
                    usuario.sitioWeb = res.usuario.sitioWeb;
                    usuario.telefono = res.usuario.telefono;
                    usuario.estado = res.usuario.estado;
                    usuario.listaIdiomas = res.usuario.listaIdiomas;
                    usuario.listaHabilidades = res.usuario.listaHabilidades;
                    usuario.listaExperienciaLaboral = res.usuario.listaExperienciaLaboral;
                    usuario.listaArchivosUsuarios = res.usuario.listaArchivosUsuarios;

                    List<Usuario> usuarioList = new List<Usuario>();
                    usuarioList.Add(usuario);
                    usuarioListView.ItemsSource = usuarioList;
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

    private async void btnAplicar_Clicked(object sender, EventArgs e)
    {
        
            try
            {
                ReqIngresarAplicacion req = new ReqIngresarAplicacion();

            req.estadoAplicacion = "Enviado";
            req.idOfertaEmpleo = idOfertas;
            req.idUsuario = Sesion.usuarioSesion.idUsuario;

                var jsonContent= new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();

                var response = await httpClient.PostAsync(laURL + "api/aplicacion/ingresarAplicacion", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResIngresarAplicacion res = new ResIngresarAplicacion();

                    res = JsonConvert.DeserializeObject<ResIngresarAplicacion>(responseContent);

                    if (res.resultado) {

                   
                    await DisplayAlert("Aplicacion Ingresada", "¡Aplicacion ingresada con exito!", "Aceptar");
                    Navigation.PushAsync(new MainPage());

                }
                    else
                    {
                        await DisplayAlert("Error en el servidor", "No se pudo enviar la aplicacion", "Aceptar");
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

    private void Homebtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void Searchbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void Applicationbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void Profilebtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

   

   
}
