using Newtonsoft.Json;
using System.Text;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
namespace yourFirstJobFront;

public partial class Login : ContentPage
{
    String laURL = "https://localhost:44364/";
    public Login()
	{
		InitializeComponent();
	}

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        try
        {
            ReqLogin req = new ReqLogin();

            req.username = txtUsername.Text;
            req.password = txtPassword.Text;

            //Mock
            //req.username = "Encripto";
            //req.password = "pato";

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "/api/usuario/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResLogin res = new ResLogin();

                res = JsonConvert.DeserializeObject<ResLogin>(responseContent);

                if (res.resultado)
                {

                    

                    Sesion.usuarioSesion.idUsuario = res.usuario.idUsuario;
                    Sesion.usuarioSesion.nombreUsuario = res.usuario.nombreUsuario;
                    Sesion.usuarioSesion.apellidos = res.usuario.apellidos;
                    Sesion.usuarioSesion.correo = res.usuario.correo;
                    

                    Navigation.PushAsync(new MainPage());

                }
                else
                {
                    await DisplayAlert("Incorrecto", "¡Usuario o contraseña incorrecto!", "Aceptar");
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
    private void OnRegisterClicked(object sender, EventArgs e)
    {
        // Navigate to the registration page
        Navigation.PushAsync(new Registrar());
    }

}