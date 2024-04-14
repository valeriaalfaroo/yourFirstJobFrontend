using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobFront.Entidades.entities
{
    public class ArchivosOferta
    {

        public int idArchivosOferta { get; set; }
        public int idOferta { get; set; }
        public string nombreArchivo { get; set; }

        public byte [] archivo { get; set; }

        public string tipo { get; set; }


    }
}
