using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Entidades.Request;
using yourFirstJobFront.Entidades.Response;
using yourFirstJobFront.Utilitarios;
using Newtonsoft.Json;
using System.Text;
using System.Windows.Input;
using yourFirstJobFront.Entidades.request;

namespace yourFirstJobFront;

public partial class Perfil : ContentPage
{
    String url = "https://yourfirstjobback.azurewebsites.net/";
    string newU = "https://localhost:44364/";
    
	public Usuario usuario {  get; set; }


    public Perfil()
	{
		InitializeComponent();

		lblNombreCompleto.Text = Sesion.usuarioSesion.nombreUsuario + " " + Sesion.usuarioSesion.apellidos;

        LoadUsuario();

    }

    //realizar la conversion para envio a la base de datos de imagen
    public async Task<byte[]> ConvertirArchivoABase64(FileResult archivo)
    {
        using (var stream = await archivo.OpenReadAsync())
        {
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes);
            return bytes;
        }
    }
    public int selectedImageId; // Variable to store the ID of the selected image


    private async void LoadUsuario()
	{
        try
        {


            ReqObtenerUsuario req = new ReqObtenerUsuario();

            req.idUser = Sesion.usuarioSesion.idUsuario;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(url + "api/usuario/ObtenerUsuario", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResObtenerPerfilUsuario res = new ResObtenerPerfilUsuario();

                res = JsonConvert.DeserializeObject<ResObtenerPerfilUsuario>(responseContent);

                if (res.resultado)
                {
                    Usuario usuario = new Usuario();

                    usuario.idUsuario = res.usuario.idUsuario;
                    usuario.nombreUsuario = res.usuario.nombreUsuario;
                    usuario.apellidos = res.usuario.apellidos;
                    usuario.correo = res.usuario.correo;
                    usuario.telefono = res.usuario.telefono;
                    usuario.fechaNacimiento = res.usuario.fechaNacimiento;
                    usuario.idRegion = res.usuario.idRegion;
                    usuario.sitioWeb = res.usuario.sitioWeb;


                    //Meto Region
                    Region1 region = new Region1();

                    region.idRegion = res.usuario.region.idRegion;
                    region.nombreRegion = res.usuario.region.nombreRegion;

                    usuario.region = region;

                    displayUserInfo(usuario);

                    //Valido idiomas existen
                    if (res.usuario.listaIdiomas.Any())
                    {
                        //Hay elementos

                        //Meto Idiomas
                        List<Idiomas> listaIdomas = new List<Idiomas>();

                        foreach (Idiomas item in res.usuario.listaIdiomas)
                        {
                            Idiomas idiomas = new Idiomas();

                            idiomas.idIdioma = item.idIdioma;
                            idiomas.idioma = item.idioma;
                            idiomas.nivel = item.nivel;

                            listaIdomas.Add(idiomas);

                        }

                        usuario.listaIdiomas = listaIdomas;

                        idiomasListView.ItemsSource = usuario.listaIdiomas;

                    }
                    else
                    {
                        //No hay elementos
                        lblIdiomas.IsVisible = false;
                        lineIdiomas.IsVisible = false;

                    }

                    //Valido habilidades existen
                    if (res.usuario.listaHabilidades.Any())
                    {
                        //Hay elementos

                        //Meto Habilidades
                        List<Habilidades> listaHabilidades = new List<Habilidades>();

                        foreach (Habilidades item in res.usuario.listaHabilidades)
                        {
                            Habilidades habilidad = new Habilidades();

                            habilidad.idHabilidades = item.idHabilidades;
                            habilidad.categoria = item.categoria;
                            habilidad.descripcion = item.descripcion;

                            listaHabilidades.Add(habilidad);

                        }

                        usuario.listaHabilidades = listaHabilidades;

                        habilidadesListView.ItemsSource = usuario.listaHabilidades;

                    }
                    else
                    {
                        //No hay elementos
                        lblHabilidades.IsVisible = false;
                        lineHabilidades.IsVisible = false;

                    }

                    //Valido estudios existen
                    if (res.usuario.listaEstudios.Any())
                    {
                        //Hay elementos

                        //Meto Estudios
                        List<Estudios> listaEstudios = new List<Estudios>();

                        foreach (Estudios item in res.usuario.listaEstudios)
                        {
                            Estudios estudio = new Estudios();

                            estudio.idEstudios = item.idEstudios;
                            estudio.nombreInstitucion = item.nombreInstitucion;
                            estudio.gradoAcademico = item.gradoAcademico;

                            Profesion profesion = new Profesion();

                            profesion.nombreProfesion = item.profesion.nombreProfesion;
                            profesion.descripcion = item.profesion.descripcion;

                            estudio.profesion = profesion;

                            estudio.fechaInicio = item.fechaInicio;
                            estudio.fechaFinalizacion = item.fechaFinalizacion;

                            listaEstudios.Add(estudio);

                        }

                        usuario.listaEstudios = listaEstudios;

                        estudiosListView.ItemsSource = usuario.listaEstudios;

                    }
                    else
                    {
                        //No hay elementos
                        lblEstudios.IsVisible = false;
                        lineEstudios.IsVisible = true;

                    }

                    //Valido experiencia existen
                    if (res.usuario.listaExperienciaLaboral.Any())
                    {
                        //Hay elementos

                        //Meto Experiencia Laboral
                        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();

                        foreach (ExperienciaLaboral item in res.usuario.listaExperienciaLaboral)
                        {
                            ExperienciaLaboral experiencia = new ExperienciaLaboral();


                            Profesion profesion = new Profesion();

                            profesion.nombreProfesion = item.profesion.nombreProfesion;
                            profesion.descripcion = item.profesion.descripcion;

                            experiencia.profesion = profesion;

                            experiencia.puesto = item.puesto;
                            experiencia.nombreEmpresa = item.nombreEmpresa;
                            experiencia.responsabilidades = item.responsabilidades;
                            experiencia.fechaInicio = item.fechaInicio;
                            experiencia.fechaFinalizacion = item.fechaFinalizacion;

                            listaExperiencia.Add(experiencia);

                        }

                        usuario.listaExperienciaLaboral = listaExperiencia;

                        experienciaListView.ItemsSource = usuario.listaExperienciaLaboral;

                    }
                    else
                    {
                        //No hay elementos
                        lblExperiencia.IsVisible = false;
                        lineExperiencia.IsVisible = false;

                    }

                    //mostrar archvios pdf

                    if (res.usuario.listaArchivosUsuarios.Any())
                    {
                        List<ArchivosUsuario> listaArchivos = res.usuario.listaArchivosUsuarios
                            .Where(item => item.tipo.ToLower() == "pdf") // Filter for PDF files only
                            .Select(item => new ArchivosUsuario
                            {
                                idArchivosUsuarios = item.idArchivosUsuarios,
                                nombreArchivo = item.nombreArchivo,
                                archivo = item.archivo,
                                tipo = item.tipo
                            }).ToList();

                        usuario.listaArchivosUsuarios = listaArchivos;
                        archivosListView.ItemsSource = usuario.listaArchivosUsuarios; 
                    }

                    
                    //mostrar foto de perfil
                    if (res.usuario.listaArchivosUsuarios.Any())
                    {
                        List<ArchivosUsuario> listaArchivos = res.usuario.listaArchivosUsuarios
                            .Where(item => item.tipo.ToLower() == "perfil") // Filter for fotoperfil file only
                            .Select(item =>
                            {
                                selectedImageId = item.idArchivosUsuarios; // Set the selectedImageId to the idArchivosUsuarios value
                                return new ArchivosUsuario
                                {
                                    idArchivosUsuarios = item.idArchivosUsuarios, // idArchivo here
                                    nombreArchivo = item.nombreArchivo,
                                    archivo = item.archivo,
                                    tipo = item.tipo
                                };
                            }).ToList();

                        usuario.listaArchivosUsuarios = listaArchivos;
                        if (usuario.listaArchivosUsuarios.Count > 0)
                        {
                            var Archivo = usuario.listaArchivosUsuarios[0]; // Access the element to display

                            // Convert the byte array to an ImageSource
                            ImageSource imagenSource = ImageSource.FromStream(() => new MemoryStream(Archivo.archivo));
                            ImagenImageButton.Source = imagenSource; // Assign the image to the ImageButton Source
                        }
                    }

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
            await DisplayAlert("Error Grave", "Elimine la aplicacion: " + ex.Message, "Aceptar");
        }
    }

    private void displayUserInfo(Usuario usuario)
    {
        lblCorreo.Text = "Correo: " + usuario.correo;
        lblTelefono.Text = "Telefono: " + usuario.telefono;
        lblFechaDeNacimiento.Text = "Fecha de nacimiento: " + usuario.fechaNacimiento;
        lblRegion.Text = "Region: " + usuario.region.nombreRegion;

        if (usuario.sitioWeb != null)
        {
            lblSitioWeb.Text = "Sito web: " + usuario.sitioWeb;
        }
        else
        {
            lblSitioWeb.IsVisible = false;
        }
        
    }


    public static void SaveFile(byte[] fileContent, string filePath)
    {
        File.WriteAllBytes(filePath, fileContent);
    }

    private void Button_Clicked_EditarUsuario(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UpdateUsuario());
    }

    private void Button_Clicked_CerrarSeccion(object sender, EventArgs e)
    {
        Sesion.CerrarSeccion();
        Navigation.PushAsync(new Login());
    }

    private void Button_Clicked_IngresarInfo(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Ingresar_Info_Usuario());
    }

   

    //buton para la foto perfil
    private async void ImageButton_Clicked(object sender, EventArgs e)
     {

         try
         {         
             // Show options to pick file type
             string[] options = { "Seleccionar imagen"};
             string selectedOption = await DisplayActionSheet("Foto de perfil", "Cancelar", null, options);

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
                   //  idImagen = usuario.listaArchivosUsuarios[0].idArchivosUsuarios; 

                     await EnviarArchivo( archivoSeleccionado, selectedImageId); 
                 }
                 else
                 {
                     await DisplayAlert("Advertencia", "No se seleccionó ninguna imagen", "Aceptar");
                 }
             }

             }
         catch(Exception) { }
     }



    private async Task EnviarArchivo( FileResult archivoSeleccionado, int electedImageId)
    {
        ReqActualizarFotoPerfil req = new ReqActualizarFotoPerfil();
        byte[] archivoBase64 = await ConvertirArchivoABase64(archivoSeleccionado);
        req.idUsuario = Sesion.usuarioSesion.idUsuario;
        //  req.idArchivo = req.idArchivo;
        req.idArchivo = electedImageId;  
        req.archivo = archivoBase64;

        var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
        HttpClient httpClient = new HttpClient();

        // Enviar la solicitud al servidor
        var response = await httpClient.PostAsync(newU + "api/usuario/actualizarFotoPerfil", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Exitoso", "Foto actualizada correctamente", "Aceptar");
            await Navigation.PushAsync(new MainPage());

        }
        else
        {
            await DisplayAlert("Error", "Error en el servidor", "Aceptar");
        }
    }
}