
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
using MeNet.FlujoTrabajo.Administrador;
using DRP.Modelo;

namespace Sitio.AdministracionControles
{
    public partial class ConsultarEvaluaciones : System.Web.UI.Page
    {

        #region  paso  2  declaracion variables

        private static ModeloSistema _contexto;
        private string ClaveAplicacion = "ConsultarEvaluaciones";

        //  reglas  de megocio

        private static AdministradorConfiguracion administradorNegocio;
        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static object _entidad;
        private static IEnumerable<object> _lista;
        private static int IdElemento;

        //  controladores
        private static GeneradorControlesWeb generadorControles;

        // otros

        static private int IdModulo;

        static string ClaveFLujo;
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
                ObtenerParametros();
                IniciarControladores();
                ConfigurarAlCargarPaginaSoloInicialmente();
                DefinirCaptura();
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
            ActulizarElementos();
            ucWebBarraProgreso1.DesActivar();
        }

        #endregion

        #region  paso  5 Configurar

        // Obtener parametros

        public void ObtenerParametros()
        {
            string[] valores = null;
            string Parametros = Page.Request.QueryString.ToString();
            if (Parametros != null && Parametros != string.Empty)
            {
                valores = Parametros.Split('&');
                if (valores != null && valores.Length > 0)
                {
                    ClaveFLujo = valores[0].ToString();
                }
                else
                    ClaveFLujo = "SOLICITUD";

            }
            else
                ClaveFLujo = "SOLICITUD";

        }

        // Iniciar  contralores basicos en  evento preinit

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
            //ClaveAplicacion = "AdministracionConfiguracion";
            //captura = new Captura();
            //captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            //captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            //captura.IdPlaneacionCaptura = 5;
            //captura.IdClasificacionCaptura = 1;

            //captura.IdEstausCaptura = 1;
        }

        // definición de  bloques  de  captura  de  datos

        public void CargarControles()
        {
            //contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            //generadorControles.CargarControles(contenedor, captura, administrarSeguridad, Accion_Click);
        }

        // inicar controladores solo  una vez al cargar  la pagina

        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {

            _contexto = new ModeloSistema();
            _contexto = AdministradorContexto.Iniciar<ModeloSistema>();

            _entidad = new ConsultarPlaneaciones_Result();
            _tipoEntidad = _entidad.GetType();

            //administradorNegocio = new AdministradorConfiguracion();
            //_entidad = administradorNegocio.Instanciar();
            //_tipoEntidad = _entidad.GetType();
            //_lista = null;

            //  carga  modulos en  combobox
            //string campo = "IdModuloSistema";
            //string valor = "ModuloSistema1";
            //if (DropDownListAplicacion.Items.Count == 0)
            //{
            //    _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
            //    List<ModuloSistema> modulos = _contexto.ModuloSistema.ToList();
            //    cc.Cargar(DropDownListAplicacion, modulos, campo, valor);
            //    DropDownListAplicacion.AutoPostBack = true;
            //    DropDownListAplicacion.DataBind();
            //}
            //if (_entidad.IdModulo != 0)
            //{
            //    DropDownListAplicacion.SelectedIndex = DropDownListAplicacion.Items.IndexOf(DropDownListAplicacion.Items.FindByValue(_entidad.IdModulo.ToString()));
            //}
        }

        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 15;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 1000;
            ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "", "");

            //_entidad = new VerEstadisticasFlujo_Result();
            //_tipoEntidad = _entidad.GetType();
            //_lista = null;


            //ObjectResult<VerEstadisticasFlujo_Result> resultado = _contexto.VerEstadisticasFlujo(ClaveFLujo, 1);
            //_lista = resultado.ToList();
            //ucWebConsultorDinamico1.AsigarOrigenDatos(_lista, _tipoEntidad, "", "");

        }

        private void InscribirEventos()
        {
            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;


        }

        private void ConfigurarAlCargarPaginaSiempre()
        {
            //generadorControles.AplicarAcciones(contenedor, captura);
        }

        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        //public MeNet.Nucleo.Configuracion.Configuracion Instanciar()
        //{
        //    _entidad = administradorNegocio.Instanciar();
        //    return _entidad;
        //}

        //public MeNet.Nucleo.Configuracion.Configuracion Obtener()
        //{
        //    _entidad = administradorNegocio.Obtener(IdElemento);
        //    return _entidad;
        //}



        #endregion

        #region paso  7  metodos de seleccion de  registros


        // selecccion de captura uno
        public void SeleccionarCaptura1(object sender, ArgumentosConsulta argsConsulta)
        {
            IdElemento = 0;
            if (argsConsulta.Registro != null)
            {
                //IdElemento2 = 0;
                
                if (argsConsulta.Registro != null)
                {
                    IdElemento = short.Parse(argsConsulta.Registro.Cells[1].Text);
                    string IdCaptura = argsConsulta.Registro.Cells[3].Text;
                    Response.Redirect("Evaluacion.aspx?" + IdElemento.ToString() +"&"+ IdCaptura);

                }
            }
        }

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



        #endregion



        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActulizarElementos()
        {
            ActulizarElementosConsultaPrincipal(null, null);
            //ActulizarElementosConsultaSecundaria(null, null);
        }

        protected void ActulizarElementosConsultaPrincipal(object sender, EventArgs e)
        {

            _entidad = new ConsultarPlaneaciones_Result();
            _tipoEntidad = _entidad.GetType();
            _lista = null;
            if (_entidad != null)
            {
                ObjectResult<ConsultarPlaneaciones_Result> resultado = _contexto.ConsultarPlaneaciones("EVA", 5049, 1, 0, "1", "1", "1", "1", "1", 0);
                _lista = resultado.ToList();
                if (_lista != null)
                    ucWebConsultorDinamico1.AsigarOrigenDatos(_lista, _tipoEntidad, "", "");
            }


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
