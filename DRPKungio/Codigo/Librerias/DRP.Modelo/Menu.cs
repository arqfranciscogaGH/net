//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DRP.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public int IdMenu { get; set; }
        public string Titulo { get; set; }
        public string Caracteristicas { get; set; }
        public Nullable<int> IdModulo { get; set; }
        public Nullable<int> IdSuscriptor { get; set; }
        public string Tipo { get; set; }
        public string MenuPrincipal { get; set; }
        public string Marco { get; set; }
        public string Tamano { get; set; }
        public string Borde { get; set; }
        public string Sombra { get; set; }
        public string Efecto { get; set; }
        public string MostrarTitulo { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string ColorMenu { get; set; }
        public string ColorMarco { get; set; }
    }
}