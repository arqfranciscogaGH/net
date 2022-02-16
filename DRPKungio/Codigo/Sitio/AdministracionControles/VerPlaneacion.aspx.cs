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
    public partial class VerPlaneacion : System.Web.UI.Page
    {
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
            string[] valores = null;
            ClaveAplicacion = "VerCuestionario";
            captura = new Captura();
            captura.IdSuscriptor = administrarSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdEstausCaptura = 1;
            string Parametros = Page.Request.QueryString.ToString();
            if (Parametros != null && Parametros != string.Empty)
            { 
                valores = Parametros.Split('&');
                if (valores != null && valores.Length>1)
                {
                    captura.IdAplicacion = int.Parse(valores[0].ToString());
                    captura.IdPlaneacionCaptura = int.Parse(valores[1].ToString());
                }
            }
   
        }
        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            generadorControles.CargarControles(contenedor, captura, administrarSeguridad, Accion_Click);

        }
        protected void Accion_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}