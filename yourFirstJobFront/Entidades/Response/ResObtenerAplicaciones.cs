using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities; 
namespace yourFirstJobFront.Entidades.Response
{
    public class ResObtenerAplicaciones : ResBase
    {
        public List<Aplicaciones> aplicaciones { get; set; }
    }
}
