using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sitio.Comun.Controles;
using Sitio.Comun.Clases;
using MeNet.Nucleo.AdministradorConsultas;
using MeNet.Nucleo.Controles;
using MeNet.Seguridad.Administrador;

namespace Sitio.AdministracionControles
{
    public partial class VerCuestionario : System.Web.UI.Page
    {
        //private static AdministradorConfiguracion administradorNegocio;

        #region variables
        private static AdministradorSeguridad administrarSeguridad;
        private static GeneradorControlesWeb generadorControles;
        private static Control contenedor;

        // primer   captura
        private string ClaveAplicacion = string.Empty;
        private static Captura captura;



        private int IdContendor;
        #endregion

        #region propiedades


        #endregion


        #region métodos de eventos

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ucWebBarraProgreso1.Activar();
            if (!IsPostBack)
            {
                administrarSeguridad = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad;
                //administrarSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
                administrarSeguridad.IniciarSesionUsuario();
                generadorControles = new GeneradorControlesWeb();
                generadorControles.Iniciar();
                DefinirCaptura();
            }
            Page.Theme = administrarSeguridad.SesionUsuarioActual.Tema;
            string Id = Page.Request.QueryString.ToString();
            if (Id != null && Id != string.Empty)
                IdContendor = int.Parse(Id);
            CargarControles();
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //ConfigurarIncial();
                //Configurar();

            }
            else
            {
                //Configurar();
            }

            ucWebBarraProgreso1.DesActivar();
        }
        private void DefinirCaptura()
        {
            ClaveAplicacion = "VerCuestionario";
            captura = new Captura();
            captura.IdAplicacion = administrarSeguridad.ObtenerAplicacion(ClaveAplicacion);

            captura.IdSuscriptor = administrarSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdEstausCaptura = 1;

        }
        public void CargarControles()
        {
            if (IdContendor != null && IdContendor != 0)
            {
                contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
                generadorControles.CrearContenedorVistaPrevia(contenedor, captura, administrarSeguridad, Accion_Click, IdContendor);
            }

        }
        protected void Accion_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}