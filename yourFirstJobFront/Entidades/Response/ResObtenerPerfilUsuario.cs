﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yourFirstJobFront.Entidades.entities; 
namespace yourFirstJobFront.Entidades.Response
{
    public class ResObtenerPerfilUsuario : ResBase
    {

        public Usuario usuario { get; set; }
        //public List<Idiomas> listaIdiomas { get; set; }


    }
}
