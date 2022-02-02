using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Core.Objects;


// paso  1 indicar los  espacios de nombre  usados

//  administrador  de aplicaciones  y  seguridad
using MeNet.Seguridad.Administrador;
//  clases  comunes
using Sitio.Comun.Clases;
using Sitio.Comun.Controles;
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
using DRP.Modelo;

namespace Sitio.AdministracionControles
{
    public partial class AdministrarRespuestas : System.Web.UI.Page
    {
        #region  paso  2  declaracion variables

        private ModeloSistema _contexto;
        private static string ClaveAplicacion = "";



        private string ClaveMensajeOperacionCompleta = "1";
        private string ClaveMensajePermiso = "2";
        //  reglas  de megocio
        private static AdministradorControles administradorNegocio;

        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static RespuestaPregunta _entidad;
        private static IEnumerable<RespuestaPregunta> _lista;
        private static int IdElemento;

        // segunda  captura

        //private static Control contenedor2;
        //private static Captura captura2;
        //private static Type _tipoEntidad2;
        //private static CapPropiedadControl _entidad2;
        //private static List<PropiedadControl> _lista2;
        //private static int IdElemento2;

        //  controladores
        private static GeneradorControlesWeb generadorControles;

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
                ObtenerParametros();
                DefinirCaptura();
            }

            Page.Theme = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.Tema;
            if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.PermisoConsultar))
            {
                CargarControles();
                // esta linea  solo cuando se requiere actulziar los parametros en las variables de captura
                btnNuevo_Click(null, null);
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
        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {
            CargardorCatalogos cc = new CargardorCatalogos();
            // regla  de negocio
            administradorNegocio = new AdministradorControles();

            // elemento  principal
            _entidad = Instanciar();
            _tipoEntidad = _entidad.GetType();
            _lista = administradorNegocio.ObtenerLista<RespuestaPregunta>();

        }
        public void ObtenerParametros()
        {
            //  Parametros de entrada
            string[] valores = null;
            string Parametros = Page.Request.QueryString.ToString();

            if (Parametros != null && Parametros != string.Empty)
            {
                valores = Parametros.Split('&');
            }
            else
            {
                //IdPlaneacionCaptura = (string)AdministradorSistema.AdministradorVariablesGlobal.Obtener(IdVariableIdPlaneacionCaptura);
                //Identificador = (string)AdministradorSistema.AdministradorVariablesGlobal.Obtener(IdVariableIdentificadordCaptura);
            }
            if (valores != null && valores.Length >= 2)
            { 
                if (_entidad==null)
                    _entidad = Instanciar();
                if (valores[0] != null && valores[0] != string.Empty)
                    _entidad.IdCuestionario = int.Parse(valores[0].ToString());
                if (valores[1] != null && valores[1] != string.Empty)
                    _entidad.IdPropiedadControl = int.Parse(valores[1].ToString());
            }
        }
        // definición de  planeación  y  aplicación  captura  de  datos
        private void DefinirCaptura()
        {
            ClaveAplicacion = "AdministracionRespuestas";
            captura = new Captura();
            captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdPlaneacionCaptura = 5025;
            captura.IdClasificacionCaptura = 1;

            captura.IdEstausCaptura = 1;


        }
        // definición de  bloques  de  captura  de  datos
        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            //contenedor = MeNet.Nucleo.ControlesDinamicos.Web.AdministradorControlesUI.ObtenerControl(BloqueCaptura, "BloqueCaptura");
            generadorControles.CargarControles(contenedor, captura, Accion_Click);
        }


        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 5;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 10000;

            ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "IdRespuestaPregunta,Descripcion,Activo", "IdRespuestaPregunta");


        }

        private void InscribirEventos()
        {
            //DropDownListAplicacion.SelectedIndexChanged += DropDownListAplicacion_SelectedIndexChanged;
            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;

        }
        private void ConfigurarAlCargarPaginaSiempre()
        {
            generadorControles.AplicarAcciones(contenedor, captura);
        }


        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        public RespuestaPregunta Instanciar()
        {
            _entidad = administradorNegocio.Instanciar<RespuestaPregunta>();
             return _entidad;
        }
        public RespuestaPregunta Asignar()
        {
            ObtenerParametros();
            return _entidad;
        }
        public RespuestaPregunta Obtener()
        {
            return administradorNegocio.Obtener<RespuestaPregunta>(s => s.IdRespuestaPregunta == IdElemento);
        }


        public void ObtenerDatosCaptura()
        {
            //_entidad2 = Obtener2();
            //if (_entidad2 != null)
            //{
            //    generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura2, _tipoEntidad2, _entidad2);
            //}
        }

        #endregion

        #region paso  7  metodos de seleccion de  registros


        public void SeleccionarCaptura1(object sender, ArgumentosConsulta argsConsulta)
        {
            IdElemento = 0;
            if (argsConsulta.Registro != null)
            {

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


        protected void SeleccionOpcionMenu(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandArgument;
            if (accion != null && accion != string.Empty)
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma = int.Parse(accion);


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
                        permitirAccion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, permisoAccion);
                        if (permitirAccion)
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
            _entidad = Asignar();
            _entidad = (RespuestaPregunta)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad = Asignar();
            _entidad = (RespuestaPregunta)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
            if (_entidad != null)
            {
                _entidad.IdRespuestaPregunta = 0;
                administradorNegocio.Agregar<RespuestaPregunta>((RespuestaPregunta)_entidad);
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                IdElemento = _entidad.IdRespuestaPregunta;
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
                    _entidad = (RespuestaPregunta)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    _entidad = Asignar();
                    administradorNegocio.Actualizar<RespuestaPregunta>((RespuestaPregunta)_entidad);
                    administradorNegocio.GuardarCambios();
                    ActualizarElementosConsultaPrincipal(sender, e);
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
                        administradorNegocio.Eliminar<RespuestaPregunta>((RespuestaPregunta)_entidad);

                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, Instanciar());
                    }
                }
            }
            catch (Exception err)
            {

            }
            ActualizarElementosConsultaPrincipal(sender, e);
        }

        #endregion

        #region acciones de  captura dos



        #endregion

        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActualizarElementos()
        {
            ActualizarElementosConsultaPrincipal(null, null);

        }

        protected void ActualizarElementosConsultaPrincipal(object sender, EventArgs e)
        {
            if (_entidad != null)
            {
                _lista = null;
                _lista = administradorNegocio.Consultar<RespuestaPregunta>( s=> s.IdPropiedadControl == _entidad.IdPropiedadControl ).ToList();

                ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            }
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
        private void Ver()
        {
            if (IdElemento != null && IdElemento > 0)
                Response.Redirect("/AdministracionControles/VerCuestionario.aspx?" + IdElemento.ToString());
        }

        private void VerAccionesVariables()
        {
            if (IdElemento != null && IdElemento > 0)
                Response.Redirect("/Controles/AdministracionControles/AdministracionAccionesControl.aspx?" + IdElemento.ToString());
        }
        #endregion
    }
}