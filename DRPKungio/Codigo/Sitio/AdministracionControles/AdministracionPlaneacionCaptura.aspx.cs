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
    public partial class AdministracionPlaneacionCaptura : System.Web.UI.Page
    {
        #region  paso  2  declaracion variables

        private ModeloSistema _contexto;
        private string ClaveAplicacion = "AdministracionPlaneacion";
        private string ClaveMensajeOperacionCompleta = "1";
        private string ClaveMensajePermiso = "2";
        //  reglas  de megocio
        private static AdministradorPlaneacion administradorNegocio;


        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static PlaneacionCaptura _entidad;
        private static IEnumerable<PlaneacionCaptura> _lista;
        private static int IdElemento;

        // segunda  captura

        private static Control contenedor2;
        private static Captura captura2;
        private static Type _tipoEntidad2;
        private static DefinicionCaptura _entidad2;
        private static object _lista2;
        private static int IdElemento2;

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
            if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ValidarPrivilegios(ClaveAplicacion, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.PermisoConsultar))
            {
                Configurar();
                InscribirEventos();
                ActualizarElementos(false);
                ConfigurarAlCargarPaginaSiempre();

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

            ClaveAplicacion = "AdministracionPlaneacion";
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IniciarSesionUsuario();

            generadorControles = new GeneradorControlesWeb();
            generadorControles.ControaldorAplicacionActual = AdministradorSistema.ControaldorAplicacion;
            generadorControles.Iniciar();

        }

        // definición de  planeación  y  aplicación  captura  de  datos
        private void DefinirCaptura()
        {
            ClaveAplicacion = "AdministracionPlaneacion";
            captura = new Captura();
            captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdPlaneacionCaptura = 1007;
            captura.IdClasificacionCaptura = 1;

            captura.IdEstausCaptura = 1;


            captura2 = new Captura();
            captura2.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura2.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura2.IdPlaneacionCaptura = 1008;
            captura2.IdClasificacionCaptura = 1;

            captura2.IdEstausCaptura = 1;

        }
        // definición de  bloques  de  captura  de  datos
        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            //contenedor = MeNet.Nucleo.ControlesDinamicos.Web.AdministradorControlesUI.ObtenerControl(BloqueCaptura, "BloqueCaptura");
            generadorControles.CargarControles(contenedor, captura, Accion_Click);

            contenedor2 = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura2");
            //contenedor = MeNet.Nucleo.ControlesDinamicos.Web.AdministradorControlesUI.ObtenerControl(BloqueCaptura, "BloqueCaptura");
            generadorControles.CargarControles(contenedor2, captura2, Accion_Click2);
        }

        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {
            administradorNegocio = new AdministradorPlaneacion();
            
            _entidad = administradorNegocio.Instanciar<PlaneacionCaptura>();
            _tipoEntidad = _entidad.GetType();

            _entidad2 = administradorNegocio.Instanciar<DefinicionCaptura>();
            _tipoEntidad2 = _entidad2.GetType();

        }
        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 5;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 1000;
            ucWebConsultorDinamico1.DefinirColumnasConsulta( _tipoEntidad, "IdPlaneacionCaptura,Descripcion,Activo", "");

            ucWebConsultorDinamico2.CrearControles(generadorControles, captura2);
            ucWebConsultorDinamico2.Paginacion = true;
            ucWebConsultorDinamico2.NumeroRegistrosPagina = 5;
            ucWebConsultorDinamico2.NumeroRegistrosConsulta = 1000;
            ucWebConsultorDinamico2.DefinirColumnasConsulta(_tipoEntidad2, "IdDefinicionCaptura,Orden,IdContenedor,Publico, Activo", "");

        }

        private void InscribirEventos()
        {

            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
            ucWebConsultorDinamico2.EventoElememtoSeleccionado = SeleccionarCaptura2;
        }
        private void ConfigurarAlCargarPaginaSiempre()
        {
            generadorControles.AplicarAcciones(contenedor, captura);
        }


        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        public PlaneacionCaptura Instanciar()
        {
            return administradorNegocio.Instanciar<PlaneacionCaptura>();
        }

        public PlaneacionCaptura Obtener()
        {
            _entidad = Instanciar();
            _entidad.IdPlaneacionCaptura = IdElemento;
            return administradorNegocio.ObtenerPlaneacion(_entidad);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DefinicionCaptura Instanciar2()
        {
            _entidad2 = administradorNegocio.Instanciar<DefinicionCaptura>();
            return _entidad2;
        }
        public DefinicionCaptura Obtener2()
        {
            _entidad2 = Instanciar2();
            _entidad2.IdDefinicionCaptura = IdElemento2;
            _entidad2 = administradorNegocio.ObtenerDefinicion(_entidad2);
            return _entidad2;
        }

        #endregion

        #region paso  7  metodos de seleccion de  registros

        public void SeleccionarCaptura1(object sender, ArgumentosConsulta argsConsulta)
        {
            IdElemento = 0;
            if (argsConsulta.Registro != null)
            {
                IdElemento2 = 0;
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
                    //  se limpia  captura secundaria
                    btnNuevo_Click2(null, null);
                    //  se actuliza  elementos secundarios
                    ActulizarElementosConsultaSecundaria(null, null,true);
                }
            }
        }
        public void SeleccionarCaptura2(object sender, ArgumentosConsulta argsConsulta)
        {
            IdElemento2 = 0;
            if (argsConsulta.Registro != null)
            {
                IdElemento2 = short.Parse(argsConsulta.Registro.Cells[1].Text);
                if (IdElemento2 != null)
                {
                    _entidad2 = Obtener2();
                    if (_entidad2 != null)
                    {
                        //  se asignas  datos  a  captura
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura2, _tipoEntidad2, _entidad2);
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
            //generadorControles.AsignaVaorResuestaYControl(contenedor, captura, "IdModulo", _entidad.IdModulo.ToString());
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
            else if (accion == "Vista  Previa")
            {
                Ver();
            }
            else if (accion == "Cancelar" || accion == "Salir")
            {
                Salir();
            }
            UcWebMensaje1.MostrarMensaje("Planeación cuestionarios, Acción:" + accion, "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
            _entidad = Instanciar();
            _entidad = (PlaneacionCaptura)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad = (PlaneacionCaptura)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            if (_entidad != null)
            {
                _entidad.IdPlaneacionCaptura = 0;
                _entidad.IdSuscriptor = (int)AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdSuscriptor;
                administradorNegocio.Agregar<PlaneacionCaptura>((PlaneacionCaptura)_entidad);
                //administradorNegocio.AgregarPlaneacion(_entidad);
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                IdElemento = _entidad.IdPlaneacionCaptura;
                ActualizarElementos(true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (IdElemento != null && IdElemento != 0)
            {
                _entidad = Obtener();
                if (_entidad != null)
                {
                    _entidad = (PlaneacionCaptura)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    administradorNegocio.Actualizar<PlaneacionCaptura>((PlaneacionCaptura)_entidad);
                    //administradorNegocio.ActualziarPlaneacion(_entidad);
                    administradorNegocio.GuardarCambios();
                    ActulizarElementosConsultaPrincipal(sender, e,true);
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
                        administradorNegocio.Eliminar<PlaneacionCaptura>(_entidad);
                        //administradorNegocio.EliminarPlaneacion(_entidad);

                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, Instanciar());
                    }
                }
            }
            catch (Exception err)
            {

            }
            ActulizarElementosConsultaPrincipal(sender, e,true);
        }

        #endregion

        #region acciones de  captura dos
        protected void Accion_Click2(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;

            //generadorControles.AsignaVaorResuestaYControl(contenedor, captura, "IdIdioma", administrarSeguridad.IdIdiomaActual.ToString());
            if (accion == "Nuevo")
            {
                btnNuevo_Click2(sender, e);
            }
            else if (accion == "Agregar")
            {
                btnAgregar_Click2(sender, e);
            }
            else if (accion == "Modificar")
            {
                btnActualizar_Click2(sender, e);
            }
            else if (accion == "Eliminar")
            {
                btnEliminar_Click2(sender, e);
            }
            else if (accion == "Cancelar" || accion == "Salir")
            {
                Salir();
            }
            UcWebMensaje1.MostrarMensaje("Iniciar sesión", "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);

        }

        protected void btnNuevo_Click2(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor2, captura2);
            _entidad2 = (DefinicionCaptura)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, Instanciar2());
        }

        protected void btnAgregar_Click2(object sender, EventArgs e)
        {
            _entidad2 = Instanciar2();

            if (_entidad2 != null)
            {
                _entidad2 = (DefinicionCaptura)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                _entidad2 = (DefinicionCaptura)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                _entidad2.IdDefinicionCaptura = 0;
                _entidad2.IdPlaneacionCaptura = IdElemento;
                administradorNegocio.Agregar<DefinicionCaptura>((DefinicionCaptura)_entidad2);
                //administradorNegocio.AgregarDefinicion(_entidad2);
                administradorNegocio.GuardarCambios();
                IdElemento2 = _entidad2.IdDefinicionCaptura;
                generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                ActulizarElementosConsultaSecundaria(sender, e,true);
            }
        }

        protected void btnActualizar_Click2(object sender, EventArgs e)
        {
            if (IdElemento2 != null && IdElemento2 != 0)
            {
                _entidad2 = Obtener2();
                if (_entidad2 != null)
                {
                    _entidad2 = (DefinicionCaptura)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);

                    administradorNegocio.Actualizar<DefinicionCaptura>((DefinicionCaptura)_entidad2);
                    administradorNegocio.ActualziarDefinicion(_entidad2);
                    administradorNegocio.GuardarCambios();
                    ActulizarElementosConsultaSecundaria(sender, e,true);
                }
            }
        }

        protected void btnEliminar_Click2(object sender, EventArgs e)
        {
            try
            {
                if (IdElemento2 != null && IdElemento2 != 0)
                {
                    _entidad2 = Obtener2();
                    if (_entidad2 != null)
                    {
                        //_entidad2 = (DefinicionCaptura)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                        administradorNegocio.Eliminar<DefinicionCaptura>((DefinicionCaptura)_entidad2);
                        //administradorNegocio.EliminarDefinicion(_entidad2);
                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, Instanciar2());
                    }
                }
            }
            catch (Exception error)
            {

            }
            ActulizarElementosConsultaSecundaria(sender, e,true);
        }

        #endregion

        #endregion

        #region  Paso  9 Métodos para   actualizar  grids


        public void ActualizarElementos(bool actualizar)
        {
            ActulizarElementosConsultaPrincipal(null, null, actualizar);
            ActulizarElementosConsultaSecundaria(null, null, actualizar);
        }

        protected void ActulizarElementosConsultaPrincipal(object sender, EventArgs e,bool actualizar)
        {
            if (_entidad != null)
            {
                _lista = null;
                _entidad = new PlaneacionCaptura();
                _entidad.IdSuscriptor = 1;
               _lista = administradorNegocio.ObtenerListaPlaneacion();
                ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            }
        }
        protected void ActulizarElementosConsultaSecundaria(object sender, EventArgs e, bool actualizar)
        {
            if (IdElemento != null && IdElemento != 0)
            {
                if (_entidad == null)
                    _entidad = new PlaneacionCaptura();
                _entidad.IdPlaneacionCaptura = IdElemento;
                _lista2 = administradorNegocio.ObtenerListaDefinicion(_entidad).ToList();
                if (_lista2 != null && _lista2.GetType().ToString() != "System.Data.DataSet")
                {
                    ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2);
                }
                else
                    ActulizarElementosConsultaSecundariaVacio();
            }
            else
            {
                _lista2 = null;
                ActulizarElementosConsultaSecundariaVacio();
            }
        }
        public void ActulizarElementosConsultaSecundariaVacio()
        {
            ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2);
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
            if (_entidad != null  && IdElemento != null && IdElemento > 0)
                Response.Redirect("/Controles/AdministracionControles/VerPlaneacion.aspx?" + _entidad.IdAplicacion.ToString() +"&"+ IdElemento.ToString());
        }

        #endregion
    }
}