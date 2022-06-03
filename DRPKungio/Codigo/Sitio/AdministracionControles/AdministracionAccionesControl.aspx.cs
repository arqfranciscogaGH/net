

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
//using DRP.Modelo;


namespace Sitio.AdministracionControles
    {
        public partial class AdministracionAccionesControl : System.Web.UI.Page
        {
            #region  paso  2  declaracion variables

            private ModeloSistema _contexto;
            private string ClaveAplicacion = "AdministracionAccionesControl";
            private string ClaveMensajeOperacionCompleta = "1";
            private string ClaveMensajePermiso = "2";

        //  reglas  de megocio
        private static AdministradorControles administradorNegocio;


            // primer   captura
            private static Control contenedor;
            private static Captura captura;
            private static Type _tipoEntidad;
            private static AccionControl _entidad;
            private static IEnumerable<AccionControl> _lista;
            private static int IdElemento;

            //  controladores
            private static GeneradorControlesWeb generadorControles;

            // otros
            private static string IdCuestionarioOrigen = string.Empty;
            private static string IduestionarioAfectado = "1";
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
                Configurar();
                InscribirEventos();
                ConfigurarAlCargarPaginaSiempre();

                ActulizarElementos();
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
                captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
                captura.IdPlaneacionCaptura = 1012;
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

            private void ConfigurarAlCargarPaginaSoloInicialmente()
            {
                administradorNegocio = new AdministradorControles();
                 CargardorCatalogos cc = new CargardorCatalogos();

                _entidad = administradorNegocio.Instanciar<AccionControl>();
                _tipoEntidad = _entidad.GetType();


             }
            private void Configurar()
            {
                ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
                ucWebConsultorDinamico1.Paginacion = true;
                ucWebConsultorDinamico1.NumeroRegistrosPagina = 15;
                ucWebConsultorDinamico1.NumeroRegistrosConsulta = 1000;
                //ucWebConsultorDinamico1.Campos = "IdAccionControl,IdPropiedadControlOrigen,IdPropiedadControlAfectado ,Accion,Activo";
                ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "IdAccionControl,IdPropiedadControlOrigen,IdPropiedadControlAfectado ,Accion,Activo", "");
        }

            private void InscribirEventos()
            {
                ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
             }

             private void ConfigurarAlCargarPaginaSiempre()
             {
                 generadorControles.AplicarAcciones(contenedor, captura);
              }


        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        public AccionControl Instanciar()
            {
                return administradorNegocio.Instanciar<AccionControl>();
            }

            public AccionControl Obtener()
            {
                //_entidad.IdAccionControl = IdElemento;
                return administradorNegocio.Obtener<AccionControl>(s=>s.IdAccionControl== IdElemento);
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

        #endregion

        #region paso  8  acciones de  captura 

        #region acciones de  captura uno

        protected void Accion_Click(object sender, EventArgs e)
            {
                LinkButton boton = (LinkButton)sender;
                string accion = boton.CommandName;
                if (accion == "Nuevo")
                {
                    btnNuevo_Click(sender, e);
                }
                else if (accion == "Agregar")
                {
                    btnAgregar_Click(sender, e);
                }
                else if (accion == "Modificar")
                {
                    btnActualizar_Click(sender, e);
                }
                else if (accion == "Eliminar")
                {
                    btnEliminar_Click(sender, e);
                }
                else if (accion == "Cancelar" || accion == "Salir")
                {
                    Salir();
                }
                UcWebMensaje1.MostrarMensaje("Acción pregunta, Acción:" + accion, "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }

            protected void btnNuevo_Click(object sender, EventArgs e)
            {
                Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
                valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
                _entidad = Instanciar();
                _entidad = (AccionControl)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);

            }

            protected void btnAgregar_Click(object sender, EventArgs e)
            {
                _entidad = Instanciar();
                _entidad = (AccionControl)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

                if (_entidad != null)
                {
                    _entidad.IdAccionControl = 0;
                     administradorNegocio.Agregar<AccionControl>((AccionControl)_entidad);
                    //administradorNegocio.AgregarPlaneacion(_entidad);
                    administradorNegocio.GuardarCambios();
                    generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    IdElemento = _entidad.IdAccionControl;
                    ActulizarElementos();
                }
            }

            protected void btnActualizar_Click(object sender, EventArgs e)
            {
                if (IdElemento != null && IdElemento != 0)
                {
                    _entidad = Obtener();
                    if (_entidad != null)
                    {
                        _entidad = (AccionControl)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                        administradorNegocio.Actualizar<AccionControl>((AccionControl)_entidad);
                        //administradorNegocio.ActualziarPlaneacion(_entidad);
                        administradorNegocio.GuardarCambios();
                        ActulizarElementosConsultaPrincipal(sender, e);
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
                            administradorNegocio.Eliminar<AccionControl>(_entidad);
                            //administradorNegocio.EliminarPlaneacion(_entidad);

                            administradorNegocio.GuardarCambios();
                            generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, Instanciar());
                        }
                    }
                }
                catch (Exception err)
                {

                }
                ActulizarElementosConsultaPrincipal(sender, e);
            }

            #endregion
        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActulizarElementos()
        {
            ActulizarElementosConsultaPrincipal(null, null);

        }

        protected void ActulizarElementosConsultaPrincipal(object sender, EventArgs e)
        {
            //if (_entidad != null)
            //{
                _lista = null;
                if  (IdCuestionarioOrigen!=null && IdCuestionarioOrigen==string.Empty)
                     _lista = administradorNegocio.Consultar<AccionControl>(s=>s.Activo==true).ToList();
                else
                     _lista = administradorNegocio.Consultar<AccionControl>(s => s.Activo == true && s.IdCuestionarioOrigen.ToString() == IdCuestionarioOrigen).ToList();
            ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            //}
        }

        #endregion

        #region  paso  10 Métodos comunes
        public void ObtenerParametros()
        {
            string Id = Page.Request.QueryString.ToString();
            if (Id != null && Id != string.Empty)
                IdCuestionarioOrigen = Id;
            //IdCuestionarioOrigen = int.Parse(Id);
        }
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