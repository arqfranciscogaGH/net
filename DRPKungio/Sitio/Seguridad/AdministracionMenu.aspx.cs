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

//  clases  para  modelo  de base de datos 
using DRP.Modelo;

namespace Sitio.Seguridad
{
    public partial class AdministracionMenu : System.Web.UI.Page
    {
        #region  paso  2  declaracion variables


        private ModeloSistema _contexto;
        private string ClaveAplicacion = "";

        //  reglas  de megocio
        private static AdministradorMenus administradorNegocio;

        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static DRP.Modelo.Menu _entidad;
        private static IEnumerable<DRP.Modelo.Menu> _lista;
        private static  int IdElemento;

        // segunda  captura

        private static Control contenedor2;
        private static Captura captura2;
        private static Type _tipoEntidad2;
        private static CapOpcionMenu _entidad2;
        private static object _lista2;
        private static  int IdElemento2;

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
            //UcWebMenuFuncionalidad2.DefinirMenuPrincipal();
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
            ActualizarElementos();
            ucWebBarraProgreso1.DesActivar();
        }

        #endregion

        #region  paso  5 Configurar

        public void IniciarControladores()
        {
            ClaveAplicacion = "AdministracionMenu";
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IniciarSesionUsuario();

            generadorControles = new GeneradorControlesWeb();
            generadorControles.ControaldorAplicacionActual = AdministradorSistema.ControaldorAplicacion;
            generadorControles.Iniciar();

        }

        // definición de  planeación  y  aplicación  captura  de  datos
        private void DefinirCaptura()
        {
            ClaveAplicacion = "AdministracionMenu";
            captura = new Captura();
            captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdPlaneacionCaptura = 1004;
            captura.IdClasificacionCaptura = 1;
            captura.IdEstausCaptura = 1;

            ClaveAplicacion = "AdministraciónOpciones";
            captura2 = new Captura();
            captura2.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura2.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura2.IdPlaneacionCaptura = 1006;
            captura2.IdClasificacionCaptura = 1;

            captura2.IdEstausCaptura = 1;

        }
        // definición de  bloques  de  captura  de  datos
        public void CargarControles()
        {
            contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
            //contenedor = MeNet.Nucleo.ControlesDinamicos.Web.AdministradorControlesUI.ObtenerControl(BloqueCaptura, "BloqueCaptura");
            generadorControles.CargarControles(contenedor, captura,  Accion_Click);

            contenedor2 = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura2");
            //contenedor = MeNet.Nucleo.ControlesDinamicos.Web.AdministradorControlesUI.ObtenerControl(BloqueCaptura, "BloqueCaptura");
            generadorControles.CargarControles(contenedor2, captura2,  Accion_Click2);
        }

        private void ConfigurarAlCargarPaginaSoloInicialmente()
        {
            administradorNegocio = new AdministradorMenus();

            IdModulo = 1;
            _entidad = administradorNegocio.Instanciar<DRP.Modelo.Menu>();
            _entidad.IdModulo = IdModulo;
            _tipoEntidad = _entidad.GetType();

            _entidad2 = administradorNegocio.Instanciar<CapOpcionMenu>();
            _tipoEntidad2 = _entidad2.GetType();
      
        }
        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 5;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 100;
            ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "IdMenu,Titulo,Activo", "");

            ucWebConsultorDinamico2.CrearControles(generadorControles, captura2);
            ucWebConsultorDinamico2.Paginacion = true;
            ucWebConsultorDinamico2.NumeroRegistrosPagina = 5;
            ucWebConsultorDinamico2.NumeroRegistrosConsulta = 100;
            ucWebConsultorDinamico2.DefinirColumnasConsulta(_tipoEntidad2, "IdOpcionMenu,Orden,Titulo, Activo", "");
        }

        private void InscribirEventos()
        {
            DropDownListAplicacion.SelectedIndexChanged += DropDownListAplicacion_SelectedIndexChanged;
            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
            ucWebConsultorDinamico2.EventoElememtoSeleccionado = SeleccionarCaptura2;
        }
        private void ConfigurarAlCargarPaginaSiempre()
        {
            CargarListas();
            generadorControles.AplicarAcciones(contenedor, captura);
        }


        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        public DRP.Modelo.Menu Instanciar()
        {
            return administradorNegocio.Instanciar<DRP.Modelo.Menu>();
        }

        public DRP.Modelo.Menu Obtener()
        {
            return administradorNegocio.Obtener<DRP.Modelo.Menu>(s => s.IdMenu == IdElemento);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CapOpcionMenu Instanciar2()
        {
            _entidad2 = administradorNegocio.Instanciar<CapOpcionMenu>();
            _entidad2.IdIdiomaActual = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma;
            _entidad2.IdIdiomaPorDefecto = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdiomaPorDefecto;

            return _entidad2;
        }
        public CapOpcionMenu Obtener2()
        {
            _entidad2 = Instanciar2();
            _entidad2.IdOpcionMenu = IdElemento2;
            _entidad2 = administradorNegocio.ObtenerOpcionMenu(_entidad2);
            return _entidad2;
        }

        #endregion

        #region paso  7  metodos de seleccion de  registros

        protected void DropDownListAplicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList listaaplicacion = (DropDownList)sender;
            IdModulo = int.Parse(listaaplicacion.SelectedItem.Value);
            IdElemento = 0;
            if (IdModulo != 0)
            {
                btnNuevo_Click(null, null);
                btnNuevo_Click2(null, null);
                IdModulo = int.Parse(listaaplicacion.SelectedItem.Value);
                _entidad.IdModulo = IdModulo;
                //  se actuliza  elementos 
                ActualizarElementos();
            }
        }
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
                    ActualizarElementosConsultaSecundaria(null, null);
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

        protected void SeleccionOpcionMenu(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandArgument;
            if (accion!=null  && accion != string.Empty)
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma= int.Parse(accion);
            _entidad2= Obtener2();
            generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
            ActualizarElementosConsultaSecundaria(null, null);
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
            UcWebMensaje1.MostrarMensaje("Menú, Acción:" + accion, "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
            _entidad = Instanciar();
            _entidad = (DRP.Modelo.Menu)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
            _entidad.IdModulo = IdModulo;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad = (DRP.Modelo.Menu)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            if (_entidad != null)
            {
                _entidad.IdModulo = IdModulo;
                administradorNegocio.Agregar<DRP.Modelo.Menu>((DRP.Modelo.Menu)_entidad);
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                IdElemento = _entidad.IdMenu;
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
                    _entidad = (DRP.Modelo.Menu)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    administradorNegocio.Actualizar<DRP.Modelo.Menu>((DRP.Modelo.Menu)_entidad);
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
                        //_entidad = (DRP.Modelo.Menu )generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

                        administradorNegocio.Eliminar<DRP.Modelo.Menu>((DRP.Modelo.Menu)_entidad);

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
        protected void Accion_Click2(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;
            bool mostrar = false;
            //generadorControles.AsignaVaorResuestaYControl(contenedor, captura, "IdIdioma", administrarSeguridad.IdIdioma.ToString());
            if (accion == "Nuevo")
            {
                btnNuevo_Click2(sender, e);
                mostrar = true;
            }
            else if (accion == "Agregar")
            {
                btnAgregar_Click2(sender, e);
                mostrar = true;
            }
            else if (accion == "Modificar")
            {
                btnActualizar_Click2(sender, e);
                mostrar = true;
            }
            else if (accion == "Eliminar")
            {
                btnEliminar_Click2(sender, e);
                mostrar = true;
            }
            else if (accion == "IDIOMA" || accion == "Ingles" || accion == "Español")
            {
                SeleccionOpcionMenu(sender, e);
                mostrar = true;
            }
               
            else if (accion == "Cancelar" || accion == "Salir")
            {
                Salir();
            }
            if (mostrar == true)
                 UcWebMensaje1.MostrarMensaje("Opcion nmenú, Acción:" + accion, "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);

        }

        protected void btnNuevo_Click2(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor2, captura2);
            _entidad2 = (CapOpcionMenu)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, Instanciar2());
        }

        protected void btnAgregar_Click2(object sender, EventArgs e)
        {
            _entidad2 = Instanciar2();

            if (_entidad2 != null)
            {
                _entidad2 = (CapOpcionMenu)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                _entidad2.IdMenu = IdElemento;
                _entidad2.IdOpcionMenu = 0;
                //_entidad2.IdIdioma = administrarSeguridad.IdIdiomaActual;
                _entidad2=administradorNegocio.AgregarOpcionMenu(_entidad2 );
                administradorNegocio.GuardarCambios();
                IdElemento2 = _entidad2.IdOpcionMenu;
                generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                ActualizarElementosConsultaSecundaria(sender, e);
            }
        }

        protected void btnActualizar_Click2(object sender, EventArgs e)
        {
            if (IdElemento2 != null && IdElemento2 != 0)
            {
                _entidad2 = Obtener2();
                if (_entidad2 != null)
                {
                    _entidad2 = (CapOpcionMenu)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                    //administradorNegocio.Actualizar<ElementoDRP.Modelo.Menu >((ElementoDRP.Modelo.Menu )_entidad2);
                    administradorNegocio.ActualizarOpcionMenu(_entidad2) ;
                    administradorNegocio.GuardarCambios();
                    ActualizarElementosConsultaSecundaria(sender, e);
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
                        //_entidad2 = (ElementoDRP.Modelo.Menu )generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                        administradorNegocio.EliminarOpcionMenu(_entidad2);
                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, Instanciar2());
                    }
                }
            }
            catch (Exception error)
            {

            }
            ActualizarElementosConsultaSecundaria(sender, e);
        }

        #endregion

        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActualizarElementos()
        {
            ActualizarElementosConsultaPrincipal(null,null);
            ActualizarElementosConsultaSecundaria(null, null);
        }

        protected void ActualizarElementosConsultaPrincipal(object sender, EventArgs e)
        {
            if (_entidad != null)
            {
                _lista = null;
                _lista = administradorNegocio.ObtenerMenus(_entidad);
                ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            }
        }
        protected void ActualizarElementosConsultaSecundaria(object sender, EventArgs e)
        {
            if (IdElemento != null && IdElemento != 0)
            {
                _entidad2.IdMenu = IdElemento;
                _lista2 = administradorNegocio.ObtenerOpcionesMenu(_entidad2);
                if (_lista2 != null && _lista2.GetType().ToString() != "System.Data.DataSet")
                {
                    ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2, _tipoEntidad2);
                }
                else
                    ActualizarElementosConsultaSecundariaVacio();
            }
            else
            {
                _lista2 = null;
                ActualizarElementosConsultaSecundariaVacio();
            }
        }
        public void ActualizarElementosConsultaSecundariaVacio()
        {
            ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2, _tipoEntidad2);
        }

        #endregion

        #region  paso  10 Métodos comunes
        public void CargarListas()
        {
  
            //  carga  modulos en  combobox
            string campo = "IdModuloSistema";
            string valor = "Nombre";
            CargardorCatalogos cc = new CargardorCatalogos();
            if (DropDownListAplicacion.Items.Count == 0)
            {
                _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
                List<ModuloSistema> modulos = _contexto.ModuloSistema.ToList();
                cc.Cargar(DropDownListAplicacion, modulos, campo, valor);
                DropDownListAplicacion.SelectedIndex = DropDownListAplicacion.Items.IndexOf(DropDownListAplicacion.Items.FindByValue(_entidad.IdModulo.ToString()));
                DropDownListAplicacion.AutoPostBack = true;
                DropDownListAplicacion.DataBind();
            }
            //if (_entidad.IdModulo != 0)
            //{
            //    DropDownListAplicacion.SelectedIndex = DropDownListAplicacion.Items.IndexOf(DropDownListAplicacion.Items.FindByValue(_entidad.IdModulo.ToString()));
            //}
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
        private void Ver()
        {
            if (IdElemento != null && IdElemento > 0)
                Response.Redirect("/Seguridad/VerMenu.aspx?" + IdElemento.ToString());
        }

        #endregion

    }
}