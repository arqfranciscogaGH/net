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
    public partial class AdministracionPrivilegios : System.Web.UI.Page
    {
        #region  paso  2  declaracion variables


        private ModeloSistema _contexto;
        private string ClaveAplicacion = "";

        //  reglas  de megocio
        private static AdministradorPrivilegio administradorNegocio;

        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static  Privilegio _entidad;
        private static IEnumerable<Privilegio> _lista;
        private static int IdElemento;

        // segunda  captura


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
            ClaveAplicacion = "Privilegios";
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
            captura.IdPlaneacionCaptura = 5023;
            //APLICACIÓN 6049   cuentionario 13057  planeacion 5023
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
            administradorNegocio = new AdministradorPrivilegio();
            administradorNegocio.Instanciar();
            //IdModulo = 1;

            _entidad = Instanciar();
            _tipoEntidad = _entidad.GetType();

        }
        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 20;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 1000;
            ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "IdPrivilegio,Clave,Nombre,Activo", "");

         }

        private void InscribirEventos()
        {
            DropDownListAplicacion.SelectedIndexChanged += DropDownListAplicacion_SelectedIndexChanged;
            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
   
        }
        private void ConfigurarAlCargarPaginaSiempre()
        {
            CargarListas();
            generadorControles.AplicarAcciones(contenedor, captura);
        }


        #endregion

        #region Paso  6 Métodos de acciones  en entidades

        public Privilegio Instanciar()
        {
            _entidad= administradorNegocio.Instanciar();
            _entidad.Activo = true;
            _entidad.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            if (DropDownListAplicacion!=null && DropDownListAplicacion.Items.Count>0)
            {
                IdModulo = int.Parse(DropDownListAplicacion.SelectedItem.Value);
            }

            return _entidad;
        }

        public Privilegio Obtener()
        {
 
            return administradorNegocio.Obtener(s => s.IdPrivilegio == IdElemento);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

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
                IdModulo = int.Parse(DropDownListAplicacion.SelectedItem.Value);
                //_entidad.IdModulo = IdModulo;
                //  se actuliza  elementos 
                ActualizarElementos();
            }
        }
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
            _entidad = (Privilegio)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
            //_entidad.IdModulo = IdModulo;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad = (Privilegio)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            if (_entidad != null)
            {
                //_entidad.IdModulo = IdModulo;
                administradorNegocio.Agregar((Privilegio)_entidad);
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                IdElemento = _entidad.IdPrivilegio;
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
                    _entidad = (Privilegio)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    administradorNegocio.Actualizar((Privilegio)_entidad);
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
                        _entidad = (Privilegio)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

                        administradorNegocio.Eliminar((Privilegio)_entidad);

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
                 //if  (_entidad.IdModulo!=null && _entidad.IdModulo > 0)
                 //   _lista = administradorNegocio.Consultar(s => s.IdModulo==_entidad.IdModulo && s.Activo == true).ToList();
                 //else
                    _lista = administradorNegocio.ObtenerLista().ToList();

                ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            }
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
                ////DropDownListAplicacion.SelectedIndex = DropDownListAplicacion.Items.IndexOf(DropDownListAplicacion.Items.FindByValue(_entidad.IdModulo.ToString()));
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
                Response.Redirect("/Controles/Seguridad/VerMenu.aspx?" + IdElemento.ToString());
        }

        #endregion

    }
}