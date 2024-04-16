using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities;

namespace yourFirstJobFront.Entidades.Request
{
    public class ReqIngresarHabilidadUsuario
    {
        public int idUsuario { get; set; }
        public Habilidades habilidades { get; set; }
    }
}
