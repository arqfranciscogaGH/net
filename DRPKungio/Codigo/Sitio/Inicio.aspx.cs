using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sitio.Comun.Controles;
using Sitio.Comun.Clases;
using MeNet.Seguridad.Administrador;


namespace Sitio
{
    public partial class Inicio : System.Web.UI.Page
    {
        #region variables
        private string ClaveAplicacion = "Inicio";
        private static AdministradorSeguridad administrarSeguridad;


        #endregion

        #region métodos de eventos
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ucWebBarraProgreso1.Activar();

            if (!IsPostBack)
            {
                administrarSeguridad = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad;
                administrarSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
                administrarSeguridad.IniciarSesionUsuario();
            }
            Page.Theme = administrarSeguridad.SesionUsuarioActual.Tema;

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Configurar();
            }
            else
            {
                Configurar();
            }

            ucWebBarraProgreso1.DesActivar();
        }
        #endregion

        #region Configurar


        private void Configurar()
        {

        }

        #endregion

        #region Métodos

        #endregion

        #region metodos de acciones

        #endregion

    }
}