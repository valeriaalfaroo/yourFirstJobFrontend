using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities; 

namespace yourFirstJobFront.Utilitarios
{
    public class Sesion
    {
        public static Usuario usuarioSesion = new Usuario();


        public static void CerrarSeccion()
        {
            Sesion.usuarioSesion.idUsuario = 0;
            Sesion.usuarioSesion.nombreUsuario = "";
            Sesion.usuarioSesion.apellidos = "";
            Sesion.usuarioSesion.correo = "";
            Sesion.usuarioSesion.telefono = 0;
            Sesion.usuarioSesion.fechaNacimiento = DateTime.MinValue;
            Sesion.usuarioSesion.idRegion = 0;
            Sesion.usuarioSesion.contrasena = "";
            Sesion.usuarioSesion.sitioWeb = "";
            Sesion.usuarioSesion.fechaRegistro = DateTime.MinValue;
            Sesion.usuarioSesion.estado = false;
            Sesion.usuarioSesion.region = new Region1();
            Sesion.usuarioSesion.listaIdiomas = new List<Idiomas>();
            Sesion.usuarioSesion.listaHabilidades = new List<Habilidades>();
            Sesion.usuarioSesion.listaEstudios = new List<Estudios>();
            Sesion.usuarioSesion.listaArchivosUsuarios = new List<ArchivosUsuario>();
            Sesion.usuarioSesion.listaExperienciaLaboral = new List<ExperienciaLaboral>();
        }
    }

    
}
