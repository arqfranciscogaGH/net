using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRP.Modelo
{
    public class CapOpcionMenu
    {


            public int IdOpcionMenu { get; set; }
            public int IdMenu { get; set; }
            public Nullable<short> Orden { get; set; }
            public string Titulo { get; set; }
            public string Ayuda { get; set; }
            public string TipoComando { get; set; }
            public string Comando { get; set; }
            public string Caracteristicas { get; set; }
            public string Parametros { get; set; }
            public Nullable<int> IdPrivilegio { get; set; }
            public string Color { get; set; }
            public string Imagen { get; set; }
            public Nullable<bool> Activo { get; set; }
            public int? IdIdiomaActual;
            public int? IdIdiomaPorDefecto;



    }
}
