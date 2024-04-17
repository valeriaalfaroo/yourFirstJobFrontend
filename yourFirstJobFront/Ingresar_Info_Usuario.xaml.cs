using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Maui.Storage; //implementar para archivos del cel
using yourFirstJobFront.Entidades.request; 


namespace yourFirstJobFront;

public partial class Ingresar_Info_Usuario : TabbedPage
{
    String laURL = "https://yourfirstjobback.azurewebsites.net/";
    String url = "https://localhost:44364/";


    public Ingresar_Info_Usuario()
	{
		InitializeComponent();
	}

    private bool archivoSeleccionado = false; //para saber si cargo un archivo 


    //realizar la conversion para envio a la base de datos
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


            ReqIngresarIdiomaUsuario req = new ReqIngresarIdiomaUsuario();

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

    private async void btn_Ingresar_Habilidad(object sender, EventArgs e)
    {
        try
        {
            
            ReqIngresarHabilidadUsuario req = new ReqIngresarHabilidadUsuario();

            req.idUsuario = Sesion.usuarioSesion.idUsuario;
            
            Habilidades habilidad = new Habilidades();

            habilidad.idHabilidades = pickerHabilidad.SelectedIndex + 1;

            req.habilidades = habilidad;


            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/insertHabilidadUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResIngresarHabilidadUsuario res = new ResIngresarHabilidadUsuario();

                res = JsonConvert.DeserializeObject<ResIngresarHabilidadUsuario>(responseContent);

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

    private async void btn_Ingresar_Estudio(object sender, EventArgs e)
    {
        try
        {

            ReqIngresarEstudioUsuario req = new ReqIngresarEstudioUsuario();

            req.idUsuario = Sesion.usuarioSesion.idUsuario;

            Estudios estudio = new Estudios();

            estudio.nombreInstitucion = entryInstitucion.Text;

            estudio.gradoAcademico = pickerGrado.SelectedItem.ToString();

            Profesion profesion = new Profesion();

            profesion.idProfesion = pickerProfesion.SelectedIndex + 1;

            estudio.profesion = profesion;

            estudio.fechaInicio = pikerFechaInicio.Date;
            estudio.fechaFinalizacion = pikerFechaFinal.Date;

            req.estudio = estudio;


            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/usuario/insertEstudioUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResIngresarEstudioUsuario res = new ResIngresarEstudioUsuario();

                res = JsonConvert.DeserializeObject<ResIngresarEstudioUsuario>(responseContent);

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

    private async void btn_Ingresar_Experiencia(object sender, EventArgs e)
    {
        try
        {

            ReqIngresarExperienciaUsuario req = new ReqIngresarExperienciaUsuario();

            req.idUsuario = Sesion.usuarioSesion.idUsuario;

            ExperienciaLaboral experiencia = new ExperienciaLaboral();

            Profesion profesion = new Profesion();

            profesion.idProfesion = pickerProfesionExp.SelectedIndex + 1;

            experiencia.profesion = profesion;

            experiencia.puesto = entryPuesto.Text;

            experiencia.nombreEmpresa = entryNombreEmpresa.Text;

            experiencia.responsabilidades = entryResponsabilidades.Text;

            experiencia.fechaInicio = pikerFechaInicioExp.Date;
            experiencia.fechaFinalizacion = pikerFechaFinalExp.Date;

            req.experiencia = experiencia;


            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(url + "api/usuario/insertExperienciaUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResIngresarExperienciaUsuario res = new ResIngresarExperienciaUsuario();

                res = JsonConvert.DeserializeObject<ResIngresarExperienciaUsuario>(responseContent);

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
            // Show options to pick file type
            string[] options = { "Seleccionar imagen", "Seleccionar PDF" };
            string selectedOption = await DisplayActionSheet("Seleccionar tipo de archivo", "Cancelar", null, options);

            if (selectedOption == "Seleccionar imagen")
            {
                // Code for selecting image file
                FileResult archivoSeleccionado = await FilePicker.Default.PickAsync(new PickOptions
                {
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.image" } },
                    { DevicePlatform.Android, new[] { "image/*" } },
                    { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png" } }
                }),
                    PickerTitle = "Seleccionar imagen"
                });

                if (archivoSeleccionado != null)
                {
                    await EnviarArchivo(archivoSeleccionado, "imagen");
                }
                else
                {
                    await DisplayAlert("Advertencia", "No se seleccionó ninguna imagen", "Aceptar");
                }
            }
            else if (selectedOption == "Seleccionar PDF")
            {
                // Code for selecting PDF file
                FileResult archivoSeleccionado = await FilePicker.Default.PickAsync(new PickOptions
                {
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.data" } },
                    { DevicePlatform.Android, new[] { "application/pdf" } },
                    { DevicePlatform.WinUI, new[] { ".pdf" } }
                }),
                    PickerTitle = "Seleccionar PDF"
                });

                if (archivoSeleccionado != null)
                {
                    await EnviarArchivo(archivoSeleccionado, "pdf");
                }
                else
                {
                    await DisplayAlert("Advertencia", "No se seleccionó ningún archivo PDF", "Aceptar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Grave", "Elimine la aplicación: " + ex, "Aceptar");
        }
    }

    private async Task EnviarArchivo(FileResult archivoSeleccionado, string tipoArchivo)
    {
        ReqIngresarArchivoUsuario req = new ReqIngresarArchivoUsuario();
        byte[] archivoBase64 = await ConvertirArchivoABase64(archivoSeleccionado);
        req.idUsuario = Sesion.usuarioSesion.idUsuario;
        string nombreArchivo = entryNombre.Text;
        if (tipoArchivo == "pdf" && !nombreArchivo.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            nombreArchivo += ".pdf";
        }
        req.nombreArchivo = nombreArchivo;

        req.tipo = tipoArchivo; // Set the file type as "png" for images or "pdf" for PDF files
        req.archivo = archivoBase64;

        var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
        HttpClient httpClient = new HttpClient();

        // Enviar la solicitud al servidor
        var response = await httpClient.PostAsync(laURL + "api/usuario/ingresarArchivoUsuario", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Exitoso", "Archivo enviado correctamente", "Aceptar");
        }
        else
        {
            await DisplayAlert("Error", "Error en el servidor", "Aceptar");
        }
    }

    
}