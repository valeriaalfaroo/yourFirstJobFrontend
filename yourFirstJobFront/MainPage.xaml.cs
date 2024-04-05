using Newtonsoft.Json;
using System.Text;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Utilitarios;

namespace yourFirstJobFront
{
    public partial class MainPage : ContentPage
    {
        String laURL = "https://localhost:44364/";
        public List<Empleo> Empleos { get; set; }

        public MainPage()
        {
            InitializeComponent();

            LoadEmpleos();
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
                await DisplayAlert("Error Grave", "Elimine la aplicacion"+ex.Message, "Aceptar");
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

}
