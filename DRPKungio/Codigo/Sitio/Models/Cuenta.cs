using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitio.Models
{
    public class Cuenta
    {
        private string _Usuario;
        private string _Contrasena;


        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Contrasena { get => _Contrasena; set => _Contrasena = value; }

    }
}