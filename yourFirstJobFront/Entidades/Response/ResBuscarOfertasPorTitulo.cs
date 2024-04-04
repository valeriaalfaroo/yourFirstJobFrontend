using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities; 

namespace yourFirstJobFront.Entidades.Response
{
    public class ResBuscarOfertasPorTitulo : ResBase
    {
        public List<Empleo> empleos { get; set; }
    }
}
