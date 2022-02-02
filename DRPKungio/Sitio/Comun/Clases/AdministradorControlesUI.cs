using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitio.Comun.Clases
{
     public class BuscadorControlesUI
    {
        // se usa la pagina  y el id del control
        static public Control ObtenerControl(Page pagina, string nombre)
        {
            
            Control control;
            control = pagina.FindControl(nombre);
            return control;
        }
        // se usa la pagina  y el contendorPlaceHolderID para paginas maestra  y el id del control
        static public Control ObtenerControl(MasterPage pagina, string contendorPlaceHolderID, string nombre)
        {
            Control control;
            
            control = pagina.FindControl(contendorPlaceHolderID).FindControl(nombre);
            return control;
        }
        // se usa el control padre  y el id del control
        static  public Control ObtenerControl(Control controlPadre, string nombre)
        {
            Control control;
            control = controlPadre.FindControl(nombre);
            return control;
        }

    }
}