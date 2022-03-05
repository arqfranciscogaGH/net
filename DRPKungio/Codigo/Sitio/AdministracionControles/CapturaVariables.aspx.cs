

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
    public partial class CapturaVariables : System.Web.UI.Page
    {

        #region  paso  2  declaracion variables

        private static AdministradorSeguridad administrarSeguridad;

        private string ClaveAplicacion = "CapturaVariables";

        //  reglas  de megocio

        private static AdministradorFlujoTrabajo administradorNegocioFlujo;
        
        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static object _entidad;
        private static List<object> _lista;
        private static int IdElemento;
  
        //  controladores
        private static GeneradorControlesWeb generadorControles;

        // otros

        private static string Identificador = string.Empty;
        private string ClaveFLujo = string.Empty;
        private string IdFlujoTrabajo = string.Empty;
        private static FlujoTrabajo _flujo;
        private static string ClaveActividad = string.Empty;
        private static string ClaveEstatus = string.Empty;
        private static int Version = 1;

        private static string IdTipoAsignacion = string.Empty;
        private static string IdResponsable = string.Empty;
        private static string IdResponsableTexto = string.Empty;

        private static string Operacion = string.Empty;

        private static bool _obtener = true;

        private static Captura capturaResponsable;
        private static Control contenedorResponsable;



        private  string IdVariableIdFlujoTrabajo = "FlujoTrabajo_IdFlujoTrabajo";
        private string IdVariableIdentificadorFlujoTrabajo = "FlujoTrabajo_Identificador";
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

            Page.Theme = administrarSeguridad.SesionUsuarioActual.Tema;
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
            ActulizarElementos();

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
            generadorControles.AdministradorVariablesActual = AdministradorSistema.AdministradorVariablesSistema;
            generadorControles.Iniciar();

        }

        // definición de  planeación  y  aplicación  captura  de  datos
        private void DefinirCaptura()
        {
            capturaResponsable = new Captura();

            capturaResponsable.IdAplicacion = administrarSeguridad.ObtenerAplicacion(ClaveAplicacion);
            capturaResponsable.IdPlaneacionCaptura = 3022;
            capturaResponsable.IdClasificacionCaptura = 1;
            capturaResponsable.IdSuscriptor = administrarSeguridad.SesionUsuarioActual.IdSuscriptor;
            capturaResponsable.IdEstausCaptura = 1;

            captura = new Captura();
            captura.IdAplicacion = administrarSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdPlaneacionCaptura = null;
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
            if (captura.IdPlaneacionCaptura!=null && captura.IdPlaneacionCaptura != 0)
            {
                contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
                generadorControles.CargarControles(contenedor, captura, administrarSeguridad, Accion_Click);
                contenedorResponsable = generadorControles.CargarControles(contenedor, capturaResponsable, administrarSeguridad, Accion_Click, false);
            }
        }


        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {
            administradorNegocioFlujo = new AdministradorFlujoTrabajo();
            administradorNegocioFlujo.Iniciar(administrarSeguridad);
       
        }

        private void Configurar()
        {

            //ucWebConsultorDinamico1.CrearControles();
            //ucWebConsultorDinamico1.Paginacion = true;
            //ucWebConsultorDinamico1.NumeroRegistrosPagina = 15;
            //ucWebConsultorDinamico1.NumeroRegistrosConsulta = 1000;
            //ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "IdArea,Nombre,Descripcion,Activo", "IdArea");
         }



        private void InscribirEventos()
        {
            //ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
           
        }

        private void ConfigurarAlCargarPaginaSiempre()
        {
            generadorControles.AplicarAcciones(contenedor, captura);
            generadorControles.AplicarAcciones(contenedorResponsable, capturaResponsable);
        }
        public void ObtenerParametros()
        {
            //  Parametros de entrada
            string[] valores = null;
            string Parametros = Page.Request.QueryString.ToString();
            Identificador = string.Empty;
            IdFlujoTrabajo = string.Empty;
            if (Parametros != null && Parametros != string.Empty)
            {
                valores = Parametros.Split('&');
            }
            else
            {
                IdFlujoTrabajo = (string)AdministradorSistema.AdministradorVariablesSistema.Obtener(IdVariableIdFlujoTrabajo);
                Identificador = (string)AdministradorSistema.AdministradorVariablesSistema.Obtener(IdVariableIdentificadorFlujoTrabajo);
            }
            //  IdFlujoTrabajo
            if (valores != null && valores.Length > 0)
            {
                IdFlujoTrabajo = valores[0].ToString();
            }
            if (IdFlujoTrabajo != null && IdFlujoTrabajo != string.Empty && IdFlujoTrabajo != "0")
            {
                AdministradorSistema.AdministradorVariablesSistema.Agregar(IdVariableIdFlujoTrabajo, IdFlujoTrabajo);
                ObtenerFlujo(IdFlujoTrabajo);
            }
        
            // Identificador
            if (valores != null && valores.Length >= 2)
            {
                Identificador = valores[1].ToString();
            }
            if (Identificador != null && Identificador != string.Empty && Identificador != "0")
            {
                AdministradorSistema.AdministradorVariablesSistema.Agregar(IdVariableIdentificadorFlujoTrabajo, Identificador);
                captura.Identificador = Identificador;
                _obtener = true;
            }
            // Acción de flujo de trabajo
            if (valores != null && valores.Length >= 3)
                Operacion = valores[2].ToString();
            else
                Operacion = "INICIAR";
            // Id Aplicación
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
            DropDownList controlEstatus = null;
            string IdContenedor = string.Empty;
            string idControl = string.Empty;

            controlIDE = (TextBox)generadorControles.AsignarValorAControl(contenedor, captura, "Identificador", captura.Identificador, false);
            if (controlIDE != null && controlIDE.Text != string.Empty)
            {
                if (captura.Identificador != controlIDE.Text)
                {
                    Identificador = controlIDE.Text;
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
                if (_flujo != null && Operacion == "INICIAR")
                {
                    // iniciar  actividad
                    FlujoTrabajoInstancia instancia = null;
                    instancia = administradorNegocioFlujo.AvanzarTarea("INICIAR", _flujo.IdFlujoTrabajo, captura.Identificador, administrarSeguridad.SesionUsuarioActual.CuentaUsuario, administrarSeguridad.ParametrosSeguridadActual.IdPerfil, administrarSeguridad.SesionUsuarioActual.Equipo, "", "", "");
                    if (instancia != null)
                    {
                        // asignar  actividad  siguiente de  captura  variables
                        controlEstatus = null;

                        // asignar estatus siguiente de  captura  variables
                        controlEstatus = null;
                        controlEstatus = (DropDownList)generadorControles.AsignarValorAControl(contenedor, captura, "EstatusTarea", "", true);
                        if (controlEstatus != null)
                            CargarListaEstatus(controlEstatus, instancia);
                    }
                }
            }
        }
        #endregion

        #region paso  7  metodos de seleccion de  registros


        // selecccion  de captura uno
        //public void SeleccionarCaptura1(object sender, ArgumentosConsulta argsConsulta)
        //{
        //    IdElemento = 0;
        //    if (argsConsulta.Registro != null)
        //    {
        //        //IdElemento2 = 0;
        //        IdElemento = short.Parse(argsConsulta.Registro.Cells[1].Text);
        //        if (IdElemento != null)
        //        {
        //            _entidad = Obtener();
        //            if (_entidad != null)
        //            {
        //                //  se asigna  datos  a  captura
        //                generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
        //                generadorControles.AplicarAcciones(contenedor, captura);
        //            }
        //            ////  se limpia  captura secundaria
        //            //btnNuevo_Click2(null, null);
        //            ////  se actuliza  elementos secundarios
        //            //ActulizarElementosConsultaSecundaria(null, null);
        //        }
        //    }
        //}

        //protected void DropDownListAplicacion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList listaaplicacion = (DropDownList)sender;
        //    IdModulo = int.Parse(listaaplicacion.SelectedItem.Value);
        //    IdElemento = 0;
        //    if (IdModulo != 0)
        //    {
        //        btnNuevo_Click(null, null);
        //        btnNuevo_Click2(null, null);
        //        IdModulo = int.Parse(listaaplicacion.SelectedItem.Value);
        //        _entidad.IdModulo = IdModulo;
        //        //  se actuliza  elementos 
        //        ActulizarElementos();
        //    }
        //}

        #endregion

        #region paso  8  acciones de  captura 

        #region acciones de  captura uno



        protected void Accion_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;

            if (accion.ToUpper().Contains("INICIAR"))
            {
                administradorNegocioFlujo.AvanzarTarea("INICIAR", _flujo.IdFlujoTrabajo, captura.Identificador, administrarSeguridad.SesionUsuarioActual.CuentaUsuario, administrarSeguridad.ParametrosSeguridadActual.IdPerfil, administrarSeguridad.SesionUsuarioActual.Equipo, ClaveEstatus, "observacion", "resultado");
            }

            else if (accion.ToUpper().Contains("TERMINAR"))
            {

                if (captura.Identificador != null & captura.Identificador != string.Empty)
                {
                    ObtenerEstado();
                    administradorNegocioFlujo.AvanzarTarea("TERMINAR", _flujo.IdFlujoTrabajo, captura.Identificador, administrarSeguridad.SesionUsuarioActual.CuentaUsuario, administrarSeguridad.ParametrosSeguridadActual.IdPerfil, administrarSeguridad.SesionUsuarioActual.Equipo, ClaveEstatus, "observacion", "resultado");
                    UcWebMensaje1.MostrarMensaje("Captura  de  variables", "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
                }
            }
            else if (accion.ToUpper().Contains("GUARDAR"))
            {
                btnGuardar_Click(sender, e);
                //Operacion = "Guardar";
                UcWebMensaje1.MostrarMensaje("Captura  de  variables", "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }
            else if (accion.ToUpper().Contains("GuardarResponsable"))
            {
                //  guardar responsabele
                contenedorResponsable.Visible = false;

            }
            else if (accion.ToUpper().Contains("ASIGNAR"))
            {
                if (contenedor != null)
                    contenedor.Visible = false;
                if (contenedorResponsable != null)
                    contenedorResponsable.Visible = true;
            }
            else if (accion.ToUpper().Contains("CAPTURAR"))
            {
                if (contenedor != null)
                    contenedor.Visible = true;
                if (contenedorResponsable != null)
                    contenedorResponsable.Visible = false;

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
            captura =generadorControles.GuardarRespuestasPorAplicacion(captura, contenedor);
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

        public void ReasignarResponsable()
        {
            string IdContenedor = string.Empty;
            string idControl = string.Empty;

            DropDownList controlTipoResponsable = null;
            DropDownList controlResponsable = null;

            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");

            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedorResponsable, capturaResponsable);
            AtributoControl atributoTipo = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "TipoResponsableAsignado").Value;
            AtributoControl atributoResponsable = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "IdResponsableAsignado").Value;

            IdTipoAsignacion = string.Empty;
            IdResponsable = string.Empty;
            IdResponsableTexto = string.Empty;
            if (atributoTipo != null)
                IdTipoAsignacion = atributoTipo.Respuesta;
            if (atributoResponsable != null)
            {
                IdResponsable = atributoResponsable.Respuesta;
                IdResponsableTexto = atributoResponsable.RespuestaTextoLista;
            }
            if (atributoTipo != null && atributoResponsable != null)
            {
                administradorNegocioFlujo.AsignarResponsable(_flujo.IdFlujoTrabajo, captura.Identificador, administrarSeguridad.SesionUsuarioActual.CuentaUsuario, IdTipoAsignacion, IdResponsable, IdResponsableTexto);
            }

        }

        public void ObtenerFlujo(string IdFlujoTrabajo)
        {
            if (IdFlujoTrabajo != string.Empty)
            {
                int IdFlujoTrabajoI = int.Parse(IdFlujoTrabajo);
                _flujo = administradorNegocioFlujo.Obtener<FlujoTrabajo>(s => s.IdFlujoTrabajo == IdFlujoTrabajoI && s.Activo == true);

                if (_flujo != null)
                {
                    ClaveFLujo = _flujo.Clave;
                    captura.IdPlaneacionCaptura = _flujo.IdPlaneacionCaptura;
                }

            }

        }





        public void ObtenerEstado()
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);

            ClaveEstatus = valoresControles.FirstOrDefault(c => c.Value.NombrePropiedad == "EstatusTarea").Value.Respuesta;
            //if (ClaveEstatus != string.Empty)
            //    ClaveEstatus = administradorNegocioFlujo.Obtener<FlujoTrabajoEstatus>(s => s.IdEstatus.ToString() == ClaveEstatus).Clave;

        }

        private void CargarListaEstatus(DropDownList controlEstatus, FlujoTrabajoInstancia instancia)
        {
            controlEstatus.Visible = true;
            CargardorCatalogos cc = new CargardorCatalogos();
            string campo = "Clave";
            string valor = "Nombre";
            ObtenerEstado();
            List<FlujoTrabajoTareaEstatus> listaTareaEstatus = administradorNegocioFlujo.Consultar<FlujoTrabajoTareaEstatus>(s => s.IdTarea == instancia.IdTarea && s.Activo == true).ToList();
            List<FlujoTrabajoEstatus> listaEstatus = administradorNegocioFlujo.Consultar<FlujoTrabajoEstatus>(s => s.Activo == true).ToList();
            listaEstatus = listaEstatus.Where(s => listaTareaEstatus.Any(a => a.IdEstatus == s.IdEstatus)).ToList();
            cc.Cargar(controlEstatus, listaEstatus, campo, valor);
            if (listaEstatus.Count > 0 && ClaveEstatus != String.Empty)
                cc.SeleccionarElemento(controlEstatus, ClaveEstatus);

        }
        private void CargarListaActividades(DropDownList controlEstatus, FlujoTrabajoInstancia instancia)
        {
            controlEstatus.Visible = true;
            CargardorCatalogos cc = new CargardorCatalogos();
            string campo = "IdActividad";
            string valor = "Nombre";

            List<FlujoTrabajoTarea> listaTareaEstatus = administradorNegocioFlujo.Consultar<FlujoTrabajoTarea>(s => s.IdTarea == instancia.IdTarea && s.Activo == true).ToList();
            List<FlujoTrabajoActividad> listaActividad = administradorNegocioFlujo.Consultar<FlujoTrabajoActividad>(s => s.Activo == true).ToList();
            listaActividad = listaActividad.Where(s => listaTareaEstatus.Any(a => a.IdActividad == s.IdActividad)).ToList();
            string Clave = administradorNegocioFlujo.Obtener<FlujoTrabajoActividad>(s => s.IdActividad == instancia.IdActividad).IdActividad.ToString();
            cc.Cargar(controlEstatus, listaActividad, campo, valor);
            if (listaActividad.Count > 0 && Clave != String.Empty)
                cc.SeleccionarElemento(controlEstatus, Clave);

        }

    #endregion


    #endregion

        #region  Paso  9 Métodos para   actualizar  grids

    public void ActulizarElementos()
        {
            //ActulizarElementosConsultaPrincipal(null, null);
            //ActulizarElementosConsultaSecundaria(null, null);
        }

        protected void ActulizarElementosConsultaPrincipal(object sender, EventArgs e)
        {
            //if (_entidad != null)
            //{
            //    _lista = null;
            //    _lista = administradorNegocio.Consultar(s => s.Activo == true).ToList();
            //    ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            //}
        }
        ////protected void ActulizarElementosConsultaSecundaria(object sender, EventArgs e)
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
