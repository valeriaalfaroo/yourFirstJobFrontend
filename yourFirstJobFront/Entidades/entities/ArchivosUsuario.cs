using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO; //  include  for Path and File classes
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.PlatformConfiguration; // Include for FileSystem

namespace yourFirstJobFront.Entidades.entities
{
    public class ArchivosUsuario
    {
        public int idArchivosUsuarios { get; set; }
        public int idUsuario { get; set; }
        public string nombreArchivo { get; set; }
        public byte[] archivo { get; set; }
        public string tipo { get; set; }
        public ICommand DownloadCommand { get; private set; }

        public ArchivosUsuario()
        {
            DownloadCommand = new Command(() => DownloadPdf(this));
        }

        private static void DownloadPdf(ArchivosUsuario archivo)
        {
            // Use 'archivo' to access the properties for the PDF

            //direccion de descarga para computadora
            //  string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 


            //direccion de descarga para telefono
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

#if ANDROID
downloadsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
#endif

            string filePath = Path.Combine(downloadsPath, archivo.nombreArchivo);
            SaveFile(archivo.archivo, filePath);


            // Inform the user that the file has been downloaded
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("Descarga", "El archivo se ha descargado.", "OK");
            });
        }



        private static void SaveFile(byte[] fileContent, string filePath)
        {
            File.WriteAllBytes(filePath, fileContent);
        }
    }
}

