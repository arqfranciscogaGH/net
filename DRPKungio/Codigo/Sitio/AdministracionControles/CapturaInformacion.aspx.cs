

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



// paso  1 indicar los  espacios de nombre  usados

//  administrador  de aplicaciones  y  seguridad
using MeNet.Seguridad.Administrador;
//  clases  comunes
using Sitio.Comun.Clases;
using Sitio.Comun.Controles;
//  clases  para  contesxto  y modelo comun 
using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.Modelo;
//  clases  para  captura 
using MeNet.Nucleo.Controles;
//  clases  para  consultas 
using MeNet.Nucleo.AdministradorConsultas;
//  clases  para  manejo de menus 
using MeNet.Seguridad.GeneradorMenus;
//  clases  para  manejo de catalogos genericos 
using MeNet.Nucleo.Catalogo.Usr;
using MeNet.Nucleo.Catalogo;
//Configuracion
using MeNet.Nucleo.Configuracion;
//  clases  para  modelo  de base de datos 

using MeNet.FlujoTrabajo.Administrador;


namespace Sitio.AdministracionControles
{
    public partial class CapturaInformacionaspx : System.Web.UI.Page
    {

        #region  paso  2  declaracion variables

        private static AdministradorSeguridad administrarSeguridad;

        private string ClaveAplicacion = "CAPINF";

        //  reglas  de megocio

        private static AdministradorFlujoTrabajo administradorNegocioFlujo;

        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static object _entidad;
        private static List<object> _lista;
        private static string IdPlaneacionCaptura;

        //  controladores
        private static GeneradorControlesWeb generadorControles;

        // otros

        private static string Identificador = string.Empty;

        private static bool _obtener = true;

        private string IdVariableIdPlaneacionCaptura = "IdVariableIdPlaneacionCaptura";
        private string IdVariableIdentificadordCaptura = "IdVariableIdentificadordCaptura";

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
                ObtenerParametros();
            }

            Page.Theme = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.Tema;
            CargarControles();

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
            AsignarDatosCaptura();
            ObtenerDatosCaptura();
            ActualizarElementos();

            ucWebBarraProgreso1.DesActivar();
        }

        #endregion

        #region  paso  5 Configurar

        public void IniciarControladores()
        {
            administrarSeguridad = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad;
            administrarSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            administrarSeguridad.IniciarSesionUsuario();

            generadorControles = new GeneradorControlesWeb();
            generadorControles.AdministradorVariablesActual = AdministradorSistema.ControaldorAplicacion.AdministradorVariablesSesion;
            generadorControles.Iniciar();

        }

        // definición de  planeación  y  aplicación  captura  de  datos
        private void DefinirCaptura()
        {

            captura = new Captura();
            captura.IdAplicacion = administrarSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdPlaneacionCaptura = 4022;
            captura.IdClasificacionCaptura = 1;
            captura.IdSuscriptor = administrarSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdEstausCaptura = 1;
            captura.Identificador = string.Empty;
            captura.IdEstausCaptura = 1;

        }

        // definición de  bloques  de  captura  de  datos

        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            if (captura.IdPlaneacionCaptura != null && captura.IdPlaneacionCaptura != 0)
            {
                contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
                generadorControles.CargarControles(contenedor, captura, administrarSeguridad, Accion_Click);
            }
        }


        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {
 

        }

        private void Configurar()
        {


        }



        private void InscribirEventos()
        {
            //ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;

        }

        private void ConfigurarAlCargarPaginaSiempre()
        {
            generadorControles.AplicarAcciones(contenedor, captura);
        }
        public void ObtenerParametros()
        {
            //  Parametros de entrada
            string[] valores = null;
            string Parametros = Page.Request.QueryString.ToString();
            Identificador = string.Empty;
            if (Parametros != null && Parametros != string.Empty)
            {
                valores = Parametros.Split('&');
            }
            else
            {

                IdPlaneacionCaptura = (string)AdministradorSistema.AdministradorVariablesGlobal.Obtener(IdVariableIdPlaneacionCaptura);
                Identificador = (string)AdministradorSistema.AdministradorVariablesGlobal.Obtener(IdVariableIdentificadordCaptura);
            }
            // IdPlaneacionCaptura
            if (valores != null && valores.Length >= 0)
            {
                IdPlaneacionCaptura = valores[0].ToString();
                }
            if (IdPlaneacionCaptura != null && IdPlaneacionCaptura != string.Empty && IdPlaneacionCaptura != "0")
            {
                AdministradorSistema.AdministradorVariablesGlobal.Agregar(IdVariableIdentificadordCaptura, IdPlaneacionCaptura);
                captura.IdPlaneacionCaptura = int.Parse(IdPlaneacionCaptura);
            }
            // Identificador
            if (valores != null && valores.Length >= 2)
            {
                Identificador = valores[1].ToString();
            }
            if (Identificador != null && Identificador != string.Empty && Identificador != "0")
            {
                AdministradorSistema.AdministradorVariablesGlobal.Agregar(IdVariableIdentificadordCaptura, Identificador);
                captura.Identificador = Identificador;
                _obtener = true;
            }
            // IdAplicacion
            if (valores != null && valores.Length >= 4)
                captura.IdAplicacion = int.Parse(valores[3].ToString());

        }


        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        //public Area Instanciar()
        //{
        //    _entidad = administradorNegocio.Instanciar();
        //    return _entidad;
        //}

        //public Area Obtener()
        //{
        //    _entidad = administradorNegocio.Obtener(s => s.IdArea == IdElemento);
        //    return _entidad;
        //}
        public void AsignarDatosCaptura()
        {
            if (!IsPostBack)
            {
                generadorControles.AsignarValorAControl(contenedor, captura, "Identificador", captura.Identificador, true);
            }
        }
        public void ObtenerDatosCaptura()
        {
            TextBox controlIDE = null;
            controlIDE = (TextBox)generadorControles.AsignarValorAControl(contenedor, captura, "Identificador", captura.Identificador, false);
            if (controlIDE != null && controlIDE.Text != string.Empty)
            {
                if (captura.Identificador != controlIDE.Text)
                {
                    Identificador = controlIDE.Text;
                    captura.Identificador = controlIDE.Text;
                    captura.Identificador = controlIDE.Text;
                    generadorControles.AsignarValorAControl(contenedor, captura, "Identificador", captura.Identificador, true);
                    _obtener = true;
                }
            }
            if (captura.Identificador != null && captura.Identificador != string.Empty)
            {
                if (_obtener)
                {
                    //  Obtener  variables  de  captura
                    captura = (Captura)generadorControles.ObtenerCaptura(contenedor, captura, "Identificador", true);
                    _obtener = false;
                }

            }
        }
        #endregion

        #region paso  7  metodos de seleccion de  registros



        #endregion

        #region paso  8  acciones de  captura 

        #region acciones de  captura uno



        protected void Accion_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;


            if (accion.ToUpper().Contains("AGREGAR") || accion.ToUpper().Contains("MODIFICAR"))
            {
                btnGuardar_Click(sender, e);
                //Operacion = "Guardar";
                UcWebMensaje1.MostrarMensaje("Captura  de  información", "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }
            else if (accion == "Cancelar" || accion == "Salir")
            {
                Salir();
            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            //Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            //valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
            //_entidad = Instanciar();
            //_entidad = (Area)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            captura = generadorControles.GuardarRespuestasPorAplicacion(captura, contenedor);
            captura = (Captura)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, captura);
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //_entidad = Instanciar();
            //_entidad = (Area)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            //if (_entidad != null)
            //{
            //    _entidad.IdArea = 0;
            //    _entidad.IdSuscriptor = int.Parse(administrarSeguridad.IdSuscriptor.ToString());
            //    administradorNegocio.Agregar((Area)_entidad);
            //    administradorNegocio.GuardarCambios();
            //    generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
            //    IdElemento = _entidad.IdArea;
            //    ActulizarElementos();
            //}
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            //if (IdElemento != null && IdElemento != 0)
            //{
            //    _entidad = Obtener();
            //    if (_entidad != null)
            //    {
            //        _entidad = (Area)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
            //        administradorNegocio.Actualizar((Area)_entidad);
            //        administradorNegocio.GuardarCambios();
            //        ActulizarElementosConsultaPrincipal(sender, e);
            //    }
            //}
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (IdElemento != null && IdElemento != 0)
            //    {
            //        _entidad = Obtener();

            //        if (_entidad != null)
            //        {
            //            administradorNegocio.Eliminar((Area)_entidad);
            //            administradorNegocio.GuardarCambios();
            //            generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, Instanciar());
            //        }
            //    }
            //}
            //catch (Exception err)
            //{

            //}
            //ActulizarElementosConsultaPrincipal(sender, e);
        }




        #endregion


        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActualizarElementos()
        {
            //ActualizarElementosConsultaPrincipal(null, null);
            //ActualizarElementosConsultaSecundaria(null, null);
        }

    

        #endregion

        #region  paso  10 Métodos comunes

        public void ObtenerRespuesta(object respuesta)
        {
            respuesta = respuesta.ToString();
        }
        private void Salir()
        {
            generadorControles = null;
            Response.Redirect("MenuPrincipal.aspx");
        }

        #endregion
    }
}