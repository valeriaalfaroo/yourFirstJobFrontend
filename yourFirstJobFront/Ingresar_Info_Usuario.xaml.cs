using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Maui.Storage;
using yourFirstJobFront.Entidades.request; //implementar para archivos del cel


namespace yourFirstJobFront;

public partial class Ingresar_Info_Usuario : TabbedPage
{
    String laURL = "https://yourfirstjobback.azurewebsites.net/";
   // String laURL = "https://localhost:44364/";


    public Ingresar_Info_Usuario()
	{
		InitializeComponent();
	}

    private bool archivoSeleccionado = false; //para saber si cargo un archivo 


    public async Task<byte[]> ConvertirArchivoABase64(FileResult archivo)
    {
        using (var stream = await archivo.OpenReadAsync())
        {
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes);
            return bytes;
        }
    }


    private async void btn_Ingresar_Idioma(object sender, EventArgs e)
    {
        try
        {
            Dictionary<Tuple<string, string>, int> menuOptionsToIds = new Dictionary<Tuple<string, string>, int>
            {
            { Tuple.Create("ingles", "nativo"), 1 },
            { Tuple.Create("portugues", "nativo"), 2 },
            { Tuple.Create("espanol", "nativo"), 3 },
            { Tuple.Create("ingles", "avanzado"), 4 },
            { Tuple.Create("portugues", "avanzado"), 5 },
            { Tuple.Create("espanol", "avanzado"), 6 },
            { Tuple.Create("ingles", "basico"), 7 },
            { Tuple.Create("portugues", "basico"), 8 },
            { Tuple.Create("espanol", "basico"), 9 }
            };


            ReqEliminarIdiomaUsuario req = new ReqEliminarIdiomaUsuario();

            Tuple<string, string> opcionSeleccionada = new Tuple<string, string>(pickerIdioma.SelectedItem.ToString(), pickerNivel.SelectedItem.ToString());

            req.idIdioma = menuOptionsToIds[opcionSeleccionada];

            req.idUsuario = Sesion.usuarioSesion.idUsuario;


            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/insertIdiomaUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResIngresarIdiomaUsuario res = new ResIngresarIdiomaUsuario();

                res = JsonConvert.DeserializeObject<ResIngresarIdiomaUsuario>(responseContent);

                if (res.resultado)
                {

                    await DisplayAlert("Exito", "¡El usuario se actualizo correctamente!", "Aceptar");
                    await Navigation.PushAsync(new Perfil());

                }
                else
                {
                    await DisplayAlert("Error", "El usuario fallo al actualizar: " + res.listaDeErrores.FirstOrDefault(), "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error en el servidor", "Aceptar");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    private async void btnEnviarArchivos_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Codificar los archivos en base64
            FileResult archivoSeleccionado = await FilePicker.Default.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.image", "public.data" } },
                { DevicePlatform.Android, new[] { "image/*", "application/pdf", "application/msword" } },
                { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx" } }
            }),
                PickerTitle = "Seleccionar archivo"
            });

            if (archivoSeleccionado != null)
            {

                ReqIngresarArchivoUsuario req = new ReqIngresarArchivoUsuario();
                byte[] archivoBase64 = await ConvertirArchivoABase64(archivoSeleccionado);
                 req.idUsuario = Sesion.usuarioSesion.idUsuario;
                req.nombreArchivo = entryNombre.Text;
                req.tipo = pickerTipo.SelectedItem.ToString();
                req.archivo = archivoBase64;


                var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient();

                // Enviar la solicitud al servidor
                var response = await httpClient.PostAsync(laURL + "api/usuario/ingresarArchivoUsuario", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Manejar la respuesta del servidor
                    await DisplayAlert("Exitoso", "Archivo enviado correctamente", "Aceptar");

                }
                else
                {
                    await DisplayAlert("Error", "Error en el servidor", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Advertencia", "No se seleccion� ning�n archivo", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex, "Aceptar");
        }
    }

    
}