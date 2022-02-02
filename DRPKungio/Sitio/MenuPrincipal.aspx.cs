using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sitio.Comun.Controles;
using Sitio.Comun.Clases;
using MeNet.Nucleo.Eventos;
using MeNet.Nucleo.Variables;

using MeNet.Seguridad.Administrador;
using MeNet.Nucleo.AdministradorConsultas;
using MeNet.Nucleo.Controles;
using MeNet.Seguridad.GeneradorMenus;

namespace Sitio
{
    public partial class MenuPrincipal : System.Web.UI.Page
    {

        #region  paso  2  declaracion variables

        private string ClaveAplicacion = "MenuPrincipal";

        //  reglas  de megocio


        //  captura de etidades  y  listas 

        // primer   captura


        //  controladores


        // otros



        #endregion

        #region  paso  3  declaracion propiedades

        #endregion

        #region  paso  4  métodos de eventos de página


        //  metodo   para  permitir  la  exportar  datos de grid  

        public override void VerifyRenderingInServerForm(Control control)
        {
       
        }

        //  metodo   inicial  de  página  

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ucWebBarraProgreso1.Activar();
            if (!IsPostBack)
            {
                IniciarControladores();
                ConfigurarAlCargarPaginaSoloInicialmente();
                DefinirCaptura();
            }
            Page.Theme = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.Tema;
            UcWebMenuFuncionalidad2.DefinirMenuPrincipal();
            UcWebEncabezadoPagina1.Usuario = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.NombreUsuario;
            UcWebEncabezadoPagina1.Perfil = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.NombrePerfil;
   
            CargarControles();
            Control con = LoadControl("Comun/Controles/ListaPersonalizada.ascx");
        }

        //  metodo   carga  de  página  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
            Configurar();
            InscribirEventos();
            ConfigurarAlCargarPaginaSiempre();
            //ActulizarElementos();
            ucWebBarraProgreso1.DesActivar();
        }

        #endregion

        #region  paso  5 Configurar

        public void IniciarControladores()
        {
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IniciarSesionUsuario();

        }

        // definición de  planeación  y  aplicación  captura  de  datos

        private void DefinirCaptura()
        {


        }

        // definición de  bloques  de  captura  de  datos

        public void CargarControles()
        {

        }

        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {


        }

        private void Configurar()
        {

        }

        private void InscribirEventos()
        {

            UcWebMenuFuncionalidad2.AccionMenu += SeleccionOpcionMenu;

            UcWebMenuFuncionalidad4.AccionMenu += SeleccionOpcionMenu;
            UcWebMenuFuncionalidad5.AccionMenu += SeleccionOpcionMenu;
            UcWebMenuFuncionalidad6.AccionMenu += SeleccionOpcionMenu;
            UcWebMenuFuncionalidad7.AccionMenu += SeleccionOpcionMenu;
            ListaPersonalizada1.AdminisradorActual.EventoElememtoSeleccionado = Seleccionar;
        }

        private void ConfigurarAlCargarPaginaSiempre()
        {

        }

        #endregion

        #region Paso  6 Métodos de acciones  en entidades



        #endregion

           
        #region Métodos

        protected void SeleccionOpcionMenu(object sender, EventArgs e)
        {

        }
        public void Seleccionar(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string Valor = boton.Text;
            string Argumento = boton.CommandArgument ;
            string Comando = boton.CommandName;
        }

        #endregion

     }
}