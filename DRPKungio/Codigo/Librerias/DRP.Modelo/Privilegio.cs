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
    
    public partial class Privilegio
    {
        public int IdPrivilegio { get; set; }
        public Nullable<int> IdAplicacion { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Permiso { get; set; }
        public Nullable<int> IdSuscriptor { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string Nombre { get; set; }
    }
}
