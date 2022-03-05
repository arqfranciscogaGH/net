using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sitio.Comun.Clases;
using Sitio.Comun.Controles;

using MeNet.Nucleo.Catalogo.Usr;
using System.Threading;
using MeNet.Seguridad.Administrador;
using MeNet.Nucleo.Controles;

namespace Sitio
{
    public partial class Acceso : System.Web.UI.Page
    {
        #region variables
          
        private static GeneradorControlesWeb generadorControles;
        private static Captura captura;
        private string ClaveAplicacion = "Acceso";
        private static Control contenedor;
        private string nombreBoton = "Ingresar";
        private string Cuenta = string.Empty;
        private string Contrasena = string.Empty;
        private string idControlPerfil = string.Empty;



        #endregion

        #region propiedades

        #endregion

        #region métodos de eventos
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
            CargarControles();

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
            }
            Configurar();
            InscribirEventos();
            ConfigurarAlCargarPaginaSiempre();
            ucWebBarraProgreso1.DesActivar();
        }

        #endregion

        #region Configurar

        public void IniciarControladores()
        {
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IniciarSesionUsuario();

            generadorControles = new GeneradorControlesWeb();
            generadorControles.ControaldorAplicacionActual = AdministradorSistema.ControaldorAplicacion;
            generadorControles.Iniciar();

        }
        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {

        }
        private void InscribirEventos()
        {

        }
        private void ConfigurarAlCargarPaginaSiempre()
        {

        }
 
        private void DefinirCaptura()
        {
            captura = new Captura();
            captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdPlaneacionCaptura = 1;
            captura.IdClasificacionCaptura = 1;
            captura.IdEstausCaptura = 1;
        }
        private void Configurar()
        {

        }
        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            generadorControles.CargarControles(contenedor, captura, Accion_Click);

        }

        // metodo para evitar el error al exportar y tambien establer EnableEventValidation = "false" en el encanbezado del aspx
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        #endregion

        #region Métodos de acciones

        protected void Accion_Click(object sender, EventArgs e)
        {
            Button boton;
            DropDownList lista;
            string accion=string.Empty;
            if (sender.GetType().Name == "Button")
            {
                boton = (Button)sender;
                accion = boton.CommandName;
            }
            else if (sender.GetType().Name == "DropDownList")
            {
                lista = (DropDownList)sender;
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.IdPerfil= lista.SelectedValue.ToString();
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerMenuPrincipal();
            }
            if (accion == "Recuperar contraseña")
            {
                btnRecuperar_Click(sender, e);
            }
            else if (accion == "Entrar")
            {
                btnIngresar_Click(sender,e);
            }
            else if (accion == "Continuar")
            {
                ucWebBarraProgreso1.DesActivar();
                Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
                valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);

                AtributoControl perfil = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "Perfil").Value;
                if (perfil!=null)
                {
                    string IdPerfil = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "Perfil").Value.Respuesta;
                    idControlPerfil = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "Perfil").Value.IdControl;
                    AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.IdPerfil = IdPerfil;

                    AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerMenuPrincipal();
                   // AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.Privilegios = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerPrivilegios();
                }
  
                ucWebBarraProgreso1.DesActivar();
                VerMenu();

            }
            else if (accion == "Regresar")
            {
                nombreBoton = "Ingresar";
                generadorControles.RegresarVista(sender);
            }
           
            else if (accion == "Cambiar Contraseña ")
            {
                //btnActualizar_Click(sender, e);
            }
 

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                ucWebBarraProgreso1.Activar();
                ObtenerDatos();
                string Mensaje = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarSesionUsuario(Cuenta, Contrasena);

                ucWebBarraProgreso1.DesActivar();
                if (Mensaje!=string.Empty)
                      UcWebMensaje1.MostrarMensaje("Iniciar sesión", Mensaje, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
                else
                {
                    if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerPerfilesUsuario().Count > 1)
                    {
                        DropDownList controlPerfil = (DropDownList)contenedor.FindControl(idControlPerfil);
                        CargardorCatalogos cc = new CargardorCatalogos();
                        string campo = "IdPerfil";
                        string valor = "Nombre";

                        cc.Cargar(controlPerfil, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerPerfilesUsuario(), campo, valor);
                        generadorControles.AvanzarVista(sender);
                    }
                    else
                    {
                        VerMenu();
                    }
                }
            }
            catch (Exception Error)
            {
                ucWebBarraProgreso1.DesActivar();
                UcWebMensaje1.MostrarMensaje("Iniciar sesión", Error.Message, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);

            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                ucWebBarraProgreso1.Activar();
                ObtenerDatos();
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.Actualizar(AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual);

                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.OperacionCambiarContrasena(Cuenta, Contrasena);

                //AdministradorSistema.AdministradorSeguridadSistema.OperacionCambiarContrasena(txtCuenta.Text, txtContrasena.Text);
                //UcWebMensaje1.MostrarMensaje("Cambiar contraseña", "La operación se realizó correctamente", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar);
                ucWebBarraProgreso1.DesActivar();
            }
            catch (Exception Error)
            {
                ucWebBarraProgreso1.DesActivar();
                UcWebMensaje1.MostrarMensaje("Cambiar contraseña", Error.Message, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }
        }
        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                ucWebBarraProgreso1.Activar();
                ObtenerDatos();
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.OperacionRecuperarContrasena(Cuenta);
                //UcWebMensaje1.MostrarMensaje("Recuperar  contraseña", "La operación se realizó correctamente", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar);
                ucWebBarraProgreso1.DesActivar();
            }
            catch (Exception Error)
            {
                ucWebBarraProgreso1.DesActivar();
                UcWebMensaje1.MostrarMensaje("Recuperar contraseña", Error.Message, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);

            }
        }

        #endregion

        #region Métodos


        public void ObtenerDatos()
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
            Cuenta = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "Cuenta").Value.Respuesta;
            Contrasena = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "Contraseña").Value.Respuesta;
            idControlPerfil = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "Perfil").Value.IdControl;


        }
        private void Ejecutar()
        {
            Thread.Sleep(3000);
            //Response.Redirect("../MenuPrincipal.aspx");
        }
        public void ObtenerRespuesta(object respuesta)
        {
            respuesta = respuesta.ToString();
        }

        private void VerMenu()
        {
      
            String PaginaMenu = System.Configuration.ConfigurationManager.AppSettings["PaginaMenu"].ToString();
            Response.Redirect(PaginaMenu);
        }
    }

        #endregion

}