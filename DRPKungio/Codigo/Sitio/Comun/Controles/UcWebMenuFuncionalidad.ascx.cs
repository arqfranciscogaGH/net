using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeNet.Seguridad.GeneradorMenus;
using System.Web.UI.HtmlControls;

using MeNet.Nucleo.Controles;
using Sitio.Comun.Clases;
using MeNet.Seguridad.Administrador;

namespace Sitio.Comun.Controles
{
    public partial class UcWebMenuFuncionalidad : System.Web.UI.UserControl
    {
        private string _TipoMenu = string.Empty;
        public string TipoMenu { get { return _TipoMenu; } set { _TipoMenu = value; } }

        private string _IdMenu = string.Empty;
        public string IdMenu { get { return _IdMenu; } set { _IdMenu = value;  } }
        public event EventHandler AccionMenu;
        public AdministradorSeguridad administradorSeguridad;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ContenedorMenuPrincipal.Attributes["class"] = "Marco";
            GeneradorMenus generador = new GeneradorMenus();

            administradorSeguridad = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad;

            generador.ObtenerMenu(IdMenu);

            if  ( ! generador.Tipo.ToUpper().Contains( "BOTON")  && ! generador.Tipo.ToUpper().Contains("ICONO") )
            {
                 string HtmlCadena = "";
                //HtmlGenericControl div = new HtmlGenericControl("Style");
                Int16 IdMenuNumero = Int16.Parse(IdMenu);
                HtmlCadena = generador.GenerarMenu(IdMenuNumero, administradorSeguridad, 0, ContenedorMenuPrincipal, AccionMenu);
                //ContenedorMenuPrincipal.Attributes["class"] = Tipo;
                ContenedorMenuPrincipal.InnerHtml = HtmlCadena;
            }
            else
            {

                GeneradorControlesWeb generadorControles = new GeneradorControlesWeb();
                generadorControles.Iniciar();

                ContenedorMenuPrincipal.Attributes["class"] = generadorControles.AsignarClase(generador.TipoMarco, "");
                generador.TipoBotonCss = generadorControles.AsignarClase(generador.Tipo, "");
                generadorControles = null;

                generador.GenerarMenuEnPanel(ContenedorMenuPrincipal, AccionMenu, administradorSeguridad,this.ID );
            }
            generador = null;
        }
        public void DefinirMenuPrincipal()
        {
            string IdMenu = string.Empty;
            IdMenu = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.IdMenu;
            if (IdMenu == null || IdMenu == string.Empty)
            {
                if (this.IdMenu== string.Empty)
                    IdMenu = "1";
                else
                    IdMenu = this.IdMenu;
            }
            this.IdMenu= IdMenu;
        }
        public void Mostrar(Control contenedor)
        {

            administradorSeguridad = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad;
            GeneradorMenus generador = new GeneradorMenus();
            List<int> listaPrivilegios = new List<int>();
            generador.ObtenerMenu(IdMenu);
            generador.GenerarMenuEnPanel (contenedor, AccionMenu,administradorSeguridad,  this.ID );
        }

    }
}