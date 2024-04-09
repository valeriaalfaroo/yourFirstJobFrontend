using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities;

namespace yourFirstJobFront.Entidades.Request
{
    class ReqUpdateUsuarioEstudios
    {
        public int idUsuario { get; set; }
        public Estudios estudios { get; set; }
    }
}
