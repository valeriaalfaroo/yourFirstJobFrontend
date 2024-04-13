using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities;
using yourFirstJobFront.Entidades.Response;

namespace yourFirstJobFront.Entidades.response
{
    public class ResObtenerArchviosUsuario : ResBase
    {
        public List<ArchivosUsuario> listaArchivosUsuario { get; set; }

    }
}
