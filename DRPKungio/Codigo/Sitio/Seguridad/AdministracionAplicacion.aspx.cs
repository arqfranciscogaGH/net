﻿

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

//  clases  para  captura 
using MeNet.Nucleo.Controles;
//  clases  para  consultas 
using MeNet.Nucleo.AdministradorConsultas;
//  clases  para  modelo  de base de datos 
using DRP.Modelo;


namespace Sitio.Seguridad
{
    public partial class AdministracionAplicacion : System.Web.UI.Page
    {

        #region  paso  2  declaracion variables


        private string ClaveAplicacion = "AdministracionAplicacion";
        private string ClaveMensajeOperacionCompleta = "1";
        private string ClaveMensajePermiso = "2";
        //  reglas  de megocio

        private static AdministradorAplicacion administradorNegocio;
        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static Aplicacion _entidad;
        private static List<Aplicacion> _lista;
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
            ClaveAplicacion = "AdministracionAplicacion";
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IniciarSesionUsuario();

            generadorControles = new GeneradorControlesWeb();
            generadorControles.ControaldorAplicacionActual = AdministradorSistema.ControaldorAplicacion;
            generadorControles.Iniciar();

        }

        // definición de  planeación  y  aplicación  captura  de  datos
        private void DefinirCaptura()
        {
            ClaveAplicacion = "AdministracionAplicacion";
            captura = new Captura();
            captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdPlaneacionCaptura = 1014;
            captura.IdClasificacionCaptura = 1;
            captura.IdEstausCaptura = 1;
        }

        // definición de  bloques  de  captura  de  datos
        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            generadorControles.CargarControles(contenedor, captura,  Accion_Click);
        }

        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {
            administradorNegocio = new AdministradorAplicacion();
            _entidad = administradorNegocio.Instanciar();
            _tipoEntidad = _entidad.GetType();
            //_lista = administradorNegocio.ObtenerLista();
            _lista = administradorNegocio.Consultar(s => s.Activo == true).ToList();
        }

        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 8;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 1000;
            ucWebConsultorDinamico1.DefinirColumnasConsulta( _tipoEntidad, "IdAplicacion,Clave,Nombre,Descripcion,Tipo,IdModuloSistema,Activo", "IdAplicacion");
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

        public Aplicacion Instanciar()
        {
            _entidad = administradorNegocio.Instanciar();
            return _entidad;
        }

        public Aplicacion Obtener()
        {
            _entidad = administradorNegocio.Obtener(s=>s.IdAplicacion == IdElemento);
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
            if (accion == "Agregar" || accion == "Modificar" || accion == "Eliminar")
            {
                ActualizarElementos(true);
                UcWebMensaje1.MostrarMensaje("Acción:" + accion, "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
            _entidad = Instanciar();
            _entidad = (Aplicacion)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad = (Aplicacion)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            if (_entidad != null)
            {
                _entidad.IdAplicacion = 0;
                administradorNegocio.Agregar((Aplicacion)_entidad);
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                IdElemento = _entidad.IdAplicacion;
             }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (IdElemento != null && IdElemento != 0)
            {
                _entidad = Obtener();
                if (_entidad != null)
                {
                    _entidad = (Aplicacion)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    administradorNegocio.Actualizar((Aplicacion)_entidad);
                    administradorNegocio.GuardarCambios();
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
                        administradorNegocio.Eliminar((Aplicacion)_entidad);
                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, Instanciar());
                    }
                }
            }
            catch (Exception err)
            {

            }

        }

        #endregion



        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActualizarElementos(bool actualizar)
        {
            ActualizarElementosConsultaPrincipal(null, null, actualizar);
            //ActualizarElementosConsultaSecundaria(null, null);
        }

        protected void ActualizarElementosConsultaPrincipal(object sender, EventArgs e, bool actualizar)
        {
            if (_entidad != null)
            {

                //_lista = administradorNegocio.Consultar(s =>  s.Activo != null).ToList();
                if (_lista == null || actualizar)
                    _lista = administradorNegocio.ObtenerLista().ToList();

            }
            ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
        }

        #endregion

        #region  paso  10 Métodos comunes

        public void ObtenerRespuesta(object respuesta)
        {
            respuesta = respuesta.ToString();
        }

        #endregion

    }
}