using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace yourFirstJobFront;

public partial class VerEmpleo : ContentPage
{
    String laURL = "https://localhost:44364/";
    Empleo empleo = new Empleo();
    public VerEmpleo(int idOfertas)
	{
		InitializeComponent();
        LoadEmpleo(idOfertas); 
	}

    private async void LoadEmpleo(int idOfertas)
    {
        try
        {
            var empleo=new Empleo();
            ReqObtenerUnEmpleo req = new ReqObtenerUnEmpleo();

            req.idOferta = idOfertas; 

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/empleo/obtenerUnEmpleo",jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
               ResObtenerUnEmpleo res = new ResObtenerUnEmpleo();

                res = JsonConvert.DeserializeObject<ResObtenerUnEmpleo>(responseContent);

                if (res.resultado)
                {
                    empleo.idOfertas = res.empleo.idOfertas;
                    empleo.tituloEmpleo = res.empleo.tituloEmpleo;
                    empleo.descripcionEmpleo = res.empleo.descripcionEmpleo;
                    empleo.ubicacionEmpleo = res.empleo.ubicacionEmpleo;
                    empleo.tipoEmpleo = res.empleo.tipoEmpleo;
                    empleo.experiencia = res.empleo.experiencia;
                    empleo.fechaPublicacion = res.empleo.fechaPublicacion;
                    empleo.estado = res.empleo.estado;

                    // Map relationships
                    empleo.empresa = res.empleo.empresa;
                    empleo.lstIdiomas = res.empleo.lstIdiomas;
                    empleo.lstHabilidades = res.empleo.lstHabilidades;
                    empleo.lstProfesiones = res.empleo.lstProfesiones;
                    empleo.lstArchivos = res.empleo.lstArchivos;

                    List<Empleo> empleosList = new List<Empleo>(); 
                    empleosList.Add(empleo); 
                    empleosListView.ItemsSource = empleosList;
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

    private void btnAplicar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Aplicar()); 
    }
}