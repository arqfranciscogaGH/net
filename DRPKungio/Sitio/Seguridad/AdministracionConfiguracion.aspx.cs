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

//Configuracion
using MeNet.Nucleo.Configuracion;
//  clases  para  modelo  de base de datos 



namespace Sitio.Seguridad
{
    public partial class AdministracionConfiguracion : System.Web.UI.Page
    {

        #region  paso  2  declaracion variables

        static private string ClaveAplicacion = "AdministracionConfiguracion";

        
        private string ClaveMensajeOperacionCompleta = "1";
        private string ClaveMensajePermiso = "2";

        //  reglas  de megocio

        private static AdministradorConfiguracion administradorNegocio;
        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static MeNet.Nucleo.Configuracion.Configuracion _entidad;
        private static List<MeNet.Nucleo.Configuracion.Configuracion> _lista;
        private static int IdElemento;

        //  controladores
        private static GeneradorControlesWeb generadorControles;

        // otros

        static private int IdModulo;

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
            if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.PermisoConsultar))
            {
                CargarControles();
            }
            else
            { 
                UcWebMensaje1.MostrarMensaje(ClaveMensajePermiso, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }

        }

        //  metodo   carga  de  página  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.PermisoConsultar))
            {
                Configurar();
                InscribirEventos();
                ConfigurarAlCargarPaginaSiempre();
                ActualizarElementos();
            }
            else
            {
                
                UcWebMensaje1.MostrarMensaje(ClaveMensajePermiso, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }
            ucWebBarraProgreso1.DesActivar();
        }

        #endregion

        #region  paso  5 Configurar

        public void IniciarControladores()
        {

            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IniciarSesionUsuario();

            generadorControles = new GeneradorControlesWeb();
            generadorControles.ControaldorAplicacionActual = AdministradorSistema.ControaldorAplicacion;
            generadorControles.Iniciar();

        }

        // definición de  planeación  y  aplicación  captura  de  datos
        private void DefinirCaptura()
        {

            captura = new Captura();
            captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdPlaneacionCaptura = 5;
            captura.IdClasificacionCaptura = 1;
            captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdEstausCaptura = 1;
        }

        // definición de  bloques  de  captura  de  datos
        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            generadorControles.CargarControles(contenedor, captura, Accion_Click);
        }

        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {
            administradorNegocio = new AdministradorConfiguracion();
            _entidad = administradorNegocio.Instanciar();
            _tipoEntidad = _entidad.GetType();
            _lista = administradorNegocio.ObtenerLista();

        }

        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles,captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 10;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 1000;
            ucWebConsultorDinamico1.DefinirColumnasConsulta( _tipoEntidad, "IdConfiguracion,Id,IdAplicacion,Descripcion,Activo", "IdConfiguracion");
        }

        private void InscribirEventos()
        {
            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
            //DropDownListAplicacion.SelectedIndexChanged += DropDownListAplicacion_SelectedIndexChanged;
            
        }

        private void ConfigurarAlCargarPaginaSiempre()
        {
            generadorControles.AplicarAcciones(contenedor, captura);
        }

        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        public MeNet.Nucleo.Configuracion.Configuracion Instanciar()
        {
            _entidad = administradorNegocio.Instanciar();
            return _entidad;
        }

        public MeNet.Nucleo.Configuracion.Configuracion Obtener()
        {
            _entidad = administradorNegocio.Obtener(IdElemento);
            return _entidad;
        }



        #endregion

        #region paso  7  metodos de seleccion de  registros
   

        // selecccion  de captura uno
        public void SeleccionarCaptura1(object sender, ArgumentosConsulta argsConsulta)
        {
            IdElemento = 0;
            if (argsConsulta.Registro != null)
            {
                //IdElemento2 = 0;
                IdElemento = short.Parse(argsConsulta.Registro.Cells[1].Text);
                if (IdElemento != null)
                {
                    _entidad = Obtener();
                    if (_entidad != null)
                    {
                        //  se asigna  datos  a  captura
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
                        generadorControles.AplicarAcciones(contenedor, captura);
                    }

                }
            }
        }


        #endregion

        #region paso  8  acciones de  captura 

        #region acciones de  captura uno

        protected void Accion_Click(object sender, EventArgs e)
        {
            bool permitirAccion = false;
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;
            string permisoAccion = accion.Substring(0, 1);
            //generadorControles.AsignaVaorResuestaYControl(contenedor, captura, "IdModulo", _entidad.IdModulo.ToString());

            if (accion == "Agregar" || accion == "Modificar" || accion == "Eliminar")
            {
                if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, permisoAccion))
                {
                     if (accion == "Agregar")
                     {
                        permitirAccion=AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, permisoAccion);
                        if  (permitirAccion)
                            btnAgregar_Click(sender, e); 

                     }
                     else if (accion == "Modificar")
                     {
                        permitirAccion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, permisoAccion);
                        if (permitirAccion)
                            btnActualizar_Click(sender, e);
                     }

                     else if (accion == "Eliminar")
                     {
                        permitirAccion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, permisoAccion);
                        if (permitirAccion)
                            btnEliminar_Click(sender, e);
                    }

                }
                if (permitirAccion)
                    UcWebMensaje1.MostrarMensaje(ClaveMensajeOperacionCompleta, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
                else
                    UcWebMensaje1.MostrarMensaje(ClaveMensajePermiso, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }
            if (accion == "Nuevo")
            {
                btnNuevo_Click(sender, e);
            }

            //else if (accion == "Vista  Previa")
            //{
            //    Ver();
            //}
            else if (accion == "Cancelar" || accion == "Salir")
            {
                Salir();
            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
            _entidad = Instanciar();
            _entidad = (MeNet.Nucleo.Configuracion.Configuracion)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
            //_entidad.IdModulo = IdModulo;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad = (MeNet.Nucleo.Configuracion.Configuracion)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            if (_entidad != null)
            {
                _entidad.IdConfiguracion = 0;
                administradorNegocio.Agregar((MeNet.Nucleo.Configuracion.Configuracion)_entidad);
                IdElemento = _entidad.IdConfiguracion;
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                //IdElemento = _entidad.IdMenu;
                ActualizarElementos();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (IdElemento != null && IdElemento != 0)
            {
                _entidad = Obtener();
                if (_entidad != null)
                {
                    _entidad = (MeNet.Nucleo.Configuracion.Configuracion)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    administradorNegocio.Actualizar( ( MeNet.Nucleo.Configuracion.Configuracion)_entidad);
                    administradorNegocio.GuardarCambios();
                    ActualizarElementos();
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (IdElemento != null && IdElemento != 0)
                {
                    _entidad = Obtener();

                    if (_entidad != null)
                    {
                        //_entidad = (DRP.Modelo.Menu )generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

                        administradorNegocio.Eliminar((MeNet.Nucleo.Configuracion.Configuracion)_entidad);

                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, Instanciar());
                    }
                }
            }
            catch (Exception err)
            {

            }
            ActualizarElementos();
        }

        #endregion



        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActualizarElementos()
        {
            ActualizarElementosConsultaPrincipal(null, null);
            //ActualizarElementosConsultaSecundaria(null, null);
        }

        protected void ActualizarElementosConsultaPrincipal(object sender, EventArgs e)
        {
            if (_entidad != null)
            {
                _lista = null;
                _lista = administradorNegocio.Consultar(s => s.Activo == true).ToList();
                ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            }
        }
        ////protected void ActualizarElementosConsultaSecundaria(object sender, EventArgs e)
        ////{
        ////    if (IdElemento != null && IdElemento != 0)
        ////    {
        ////        _entidad2.IdMenu = IdElemento;
        ////        _lista2 = administradorNegocio.ObtenerOpcionesMenu(_entidad2);
        ////        if (_lista2 != null && _lista2.GetType().ToString() != "System.Data.DataSet")
        ////        {
        ////            ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2, _tipoEntidad2);
        ////        }
        ////        else
        ////            ActulizarElementosConsultaSecundariaVacio();
        ////    }
        ////    else
        ////    {
        ////        _lista2 = null;
        ////        ActulizarElementosConsultaSecundariaVacio();
        ////    }
        ////}
        ////public void ActulizarElementosConsultaSecundariaVacio()
        ////{
        ////    ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2, _tipoEntidad2);
        ////}

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