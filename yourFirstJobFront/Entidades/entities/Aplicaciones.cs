using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobFront.Entidades.entities
{
    public class Aplicaciones
    {
        public int idAplicacion { get; set; }
        public Usuario usuario { get; set; }
        public Empleo empleo { get; set; }

        public string estadoAplicacion { get; set; }

    }
}
