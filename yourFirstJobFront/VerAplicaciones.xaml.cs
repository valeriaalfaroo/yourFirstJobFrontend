using Newtonsoft.Json;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Utilitarios;
using System.Text;

namespace yourFirstJobFront;

public partial class VerAplicaciones : ContentPage
{
    String laURL = "https://yourfirstjobback.azurewebsites.net/";
    public List<Aplicaciones> Aplicaciones { get; set; }
    public VerAplicaciones()
	{
		InitializeComponent();
        LoadAplicaciones();
	}

    private async void LoadAplicaciones()
    {
        try
        {

            var aplicaciones = new List<Aplicaciones>();

            ReqObtenerAplicacion req= new ReqObtenerAplicacion();

            req.idUser = Sesion.usuarioSesion.idUsuario; 

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/aplicacion/obtenerAplicacionesUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResObtenerAplicaciones res = new ResObtenerAplicaciones();

                res = JsonConvert.DeserializeObject<ResObtenerAplicaciones>(responseContent);

                if (res.resultado)
                {

                    foreach (var aplicacion in res.aplicaciones)
                    {
                        
                        aplicaciones.Add(aplicacion);
                        
                    }
                    aplicacionesListView.ItemsSource = aplicaciones;
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

    private void Homebtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void Searchbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BuscarPorTitulo());
    }

    private void Applicationbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VerAplicaciones());
    }

    private void Profilebtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Perfil());
    }

}