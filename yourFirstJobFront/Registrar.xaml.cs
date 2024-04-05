using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Text;
using yourFirstJobFront.Entidades.entities;
namespace yourFirstJobFront
{
    public partial class Registrar : ContentPage
    {
        String laURL = "https://localhost:44364/";
        int selectedRadio = -1;
        public Registrar()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ReqIngresarUsuario req = new ReqIngresarUsuario();

                req.nombreUsuario = txtUsername.Text;
                req.apellidos = txtApellidos.Text;
                req.correo = txtCorreo.Text;
                req.telefono = int.Parse(txtTelefono.Text);
                req.fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                req.idRegion = selectedRadio;
                req.contrasena = txtPassword.Text;


                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();

                var response = await httpClient.PostAsync(laURL + "api/usuario/ingresarUsuario", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResIngresarUsuario res = new ResIngresarUsuario();

                    res = JsonConvert.DeserializeObject<ResIngresarUsuario>(responseContent);

                    if (res.resultado)
                    {

                        Navigation.PushAsync(new MainPage());

                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo ingresar la informacion en el registrar", "Aceptar");
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


        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            selectedRadio = Convert.ToInt32(radioButton.Value);
        }


    }
}


