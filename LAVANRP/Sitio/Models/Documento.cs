using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitio.Models
{

    public class Documento
    {
        private int documento_ID;
        private string documento_Nombre;
        private string documento_Tipo;

        public int Documento_ID { get => documento_ID; set => documento_ID = value; }
        public string Documento_Nombre { get => documento_Nombre; set => documento_Nombre = value; }
        public string Documento_Tipo { get => documento_Tipo; set => documento_Tipo = value; }
    }
  
}