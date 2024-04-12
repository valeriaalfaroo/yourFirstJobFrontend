using Newtonsoft.Json;
using System.Text;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Utilitarios;

namespace yourFirstJobFront;

public partial class BuscarPorTitulo : ContentPage
{
    String laURL = "https://yourfirstjobback.azurewebsites.net/";
    public List<Empleo> Empleos { get; set; }
    public BuscarPorTitulo()
	{
		InitializeComponent();
        LoadEmpleos();
	}

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        // Perform action here, such as searching for the entered text
        string searchText = txtTitulo.Text;
        SearchForJobs(searchText);
    }

    private async void SearchForJobs(string searchText)
    {
        try
        {
            var Empleos = new List<Empleo>();
            ReqBuscarOfertasPorTitulo req = new ReqBuscarOfertasPorTitulo();

            req.titulo = searchText;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/empleo/obtenerEmpleosTitulo", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResBuscarOfertasPorTitulo res = new ResBuscarOfertasPorTitulo();

                res = JsonConvert.DeserializeObject<ResBuscarOfertasPorTitulo>(responseContent);

                if (res.resultado)
                {
                    foreach (var empleo in res.empleos)
                    {
                        Empleos.Add(empleo);
                    }
                    empleosListView.ItemsSource = Empleos;
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
    private async void LoadEmpleos()
    {
        try
        {

            var Empleos = new List<Empleo>();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(laURL + "api/empleo/obtenerTodosLosEmpleos");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResObtenerTodosLosEmpleos res = new ResObtenerTodosLosEmpleos();

                res = JsonConvert.DeserializeObject<ResObtenerTodosLosEmpleos>(responseContent);

                if (res.resultado)
                {
                    foreach (var empleo in res.empleos)
                    {
                        Empleos.Add(empleo);
                    }
                    empleosListView.ItemsSource = Empleos;
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


    private void btnIngresarOfertaEmpleo_Clicked(object sender, EventArgs e)

    {
        var selectedItem = (sender as Button).BindingContext as Empleo;
        int idOfertas = selectedItem.idOfertas;
        Navigation.PushAsync(new VerEmpleo(idOfertas));
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
