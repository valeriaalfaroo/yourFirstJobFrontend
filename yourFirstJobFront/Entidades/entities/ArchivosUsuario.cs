using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobFront.Entidades.entities
{
    public class ArchivosUsuario
    {
        public int idArchivosUsuarios { get; set; }
        public int idUsuario { get; set; }
        public string nombreArchivo { get; set; }

        public BinaryReader archivo { get; set; }

        public string tipo { get; set; }



    }
}
