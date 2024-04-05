using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities; 

namespace yourFirstJobFront.Entidades.Response
{
    public class ResObtenerUnEmpleo : ResBase
    {
      public Empleo empleo {  get; set; }

    }
}
