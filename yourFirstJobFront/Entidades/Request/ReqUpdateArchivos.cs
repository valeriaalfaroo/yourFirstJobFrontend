using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobFront.Entidades.request
{
    public class ReqUpdateArchivos
    {
        public int idArchivosUsuarios { get; set; }
        public string nombreArchivo { get; set; }

        public byte[] archivo { get; set; }

        public string tipo { get; set; }

        public int idUsuario { get; set; }
    }
}
