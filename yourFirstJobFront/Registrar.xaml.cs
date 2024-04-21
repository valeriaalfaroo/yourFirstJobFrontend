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
        String laURL = "https://yourfirstjobback.azurewebsites.net/";
       // string url = "https://localhost:44364/";
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

                Usuario usuario = new Usuario();

                usuario.nombreUsuario = txtUsername.Text;
                usuario.apellidos = txtApellidos.Text;
                usuario.correo = txtCorreo.Text;
                usuario.telefono = int.Parse(txtTelefono.Text);
                usuario.fechaNacimiento = pikerFecha.Date;
                usuario.idRegion = pickerRegion.SelectedIndex + 1;
                usuario.contrasena = txtPassword.Text;

                req.usuario = usuario;


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
                        await DisplayAlert("Exito", "El usuario se registro con exito", "Aceptar");
                        Navigation.PushAsync(new Login());

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


