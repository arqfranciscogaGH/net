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
    public partial class UcWebCambiarTema : System.Web.UI.UserControl
    {
        #region Variables locales de clase
        private static CargardorCatalogos cargardorCatalogos;
        private static string _tema = string.Empty;

        # endregion

        # region propiedades



        # endregion

        # region métodosde eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (_tema == string.Empty)
                    _tema = AdministradorSistema.AdministradorSeguridadActual.SesionUsuarioActual.Tema;

                cargardorCatalogos = new CargardorCatalogos();
                cargardorCatalogos.Cargar(lisTema, "27", 1);
                if (_tema != string.Empty)
                    lisTema.SelectedIndex = lisTema.Items.IndexOf(lisTema.Items.FindByValue(_tema));
            }

        }
        protected void lisTema_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tema = lisTema.SelectedItem.Value;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.Tema = _tema;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.Actualizar(AdministradorSistema.AdministradorSeguridadActual.SesionUsuarioActual, false);
            string url = Page.Request.Url.AbsoluteUri;
            Response.Redirect(url);
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.Actualizar(AdministradorSistema.AdministradorSeguridadActual.SesionUsuarioActual, true);
        }
        # endregion

        # region otros  métodos



        # endregion
    }
}