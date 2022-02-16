using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sitio.Comun.Clases;
using MeNet.Nucleo.Catalogo.Usr;

namespace Sitio.Comun.Controles
{
    public partial class ucWebCambiarIdioma : System.Web.UI.UserControl
    {
        # region Variables locales de clase

        private static CargardorCatalogos cargardorCatalogos;
        private static int? _IdIdioma;
        private static string _cultura;
        # endregion

        # region propiedades



        # endregion

        # region métodosde eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargardorCatalogos = new CargardorCatalogos();

                _IdIdioma = AdministradorSistema.AdministradorSeguridadActual.SesionUsuarioActual.IdIdioma;

                cargardorCatalogos.Cargar(lisIdioma, "2", 1);

                lisIdioma.SelectedIndex = lisIdioma.Items.IndexOf(lisIdioma.Items.FindByValue(_IdIdioma.ToString()));
            }

        }
        protected void lisIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdIdioma   = lisIdioma.SelectedValue.ToString();
            _IdIdioma = Int16.Parse(IdIdioma);
            if (_IdIdioma != null && _IdIdioma != 0)
            {
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma = _IdIdioma;
                // Actualizar  cultura
                _cultura = cargardorCatalogos.AdmistradorCatalogos.ObtenerElementoCatalogo(25, IdIdioma, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma).Valor;

                if (_cultura != null && _cultura != string.Empty)
                {
                    AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.Cultura = _cultura;
                }
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.Actualizar(AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual, false);
                string url = Page.Request.Url.AbsoluteUri;
                Response.Redirect(url);
            }

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
               AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.Actualizar(AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual,true );
        }
         # endregion

        # region otros  métodos

        # endregion

    }
}