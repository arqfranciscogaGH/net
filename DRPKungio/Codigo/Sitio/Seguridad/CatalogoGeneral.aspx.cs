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
using DRP.Modelo;


namespace Sitio.Seguridad
{
    public partial class CatalogoGeneral : System.Web.UI.Page
    {
        #region variables
        private string ClaveAplicacion = "AdministracionCatalogo";


        private static GeneradorControlesWeb generadorControles;

        private static AdmistradorCatalogos administradorNegocio;

        // primer   captura
        private static Captura captura;
        private static Control contenedor;
        private static Type _tipoEntidad;
        private static Catalogo _entidad;
        private static IEnumerable<Catalogo> _lista;
        static private int IdElemento;

        static private int IdAplicacion;


        // segunda  captura

        private static Captura captura2;
        private static Control contenedor2;
        private static Type _tipoEntidad2;
        private static ElementoCatalogo _entidad2;
        private static object _lista2;
        static private int IdElemento2;
        private ModeloSistema _contexto;
        #endregion

        #region métodos de eventos

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
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
            CargarControles();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            Configurar();
            InscribirEventos();
            ActualizarElementos(false);
            ConfigurarAlCargarPaginaSiempre();
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

            ClaveAplicacion = "AdministracionCatalogo";
            captura = new Captura();
            captura.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;
            captura.IdPlaneacionCaptura = 6;
            captura.IdClasificacionCaptura = 1;

            captura.IdEstausCaptura = 1;

            ClaveAplicacion = "AdministracionEleCatalogo";
            captura2 = new Captura();
            captura2.IdAplicacion = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ObtenerAplicacion(ClaveAplicacion);
            captura2.IdSuscriptor = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor;

            captura2.IdPlaneacionCaptura = 7;
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
            administradorNegocio = new AdmistradorCatalogos();
            CargardorCatalogos cc = new CargardorCatalogos();

            IdAplicacion = 10;

            _entidad = administradorNegocio.Instanciar<Catalogo>();
            _entidad.IdAplicacion = IdAplicacion;
            _tipoEntidad = _entidad.GetType();
            _lista = administradorNegocio.ObtenerCatalogos(_entidad);

            _entidad2 = administradorNegocio.Instanciar<ElementoCatalogo>();
            _tipoEntidad2 = _entidad2.GetType();

        }

        private void Configurar()
        {
            ucWebConsultorDinamico1.CrearControles(generadorControles,captura);
            ucWebConsultorDinamico1.Paginacion = true;
            ucWebConsultorDinamico1.NumeroRegistrosPagina = 5;
            ucWebConsultorDinamico1.NumeroRegistrosConsulta = 100;

            ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "IdCatalogo,Nombre,Descripcion", "");

            ucWebConsultorDinamico2.CrearControles(generadorControles,captura2);
            ucWebConsultorDinamico2.Paginacion = true;
            ucWebConsultorDinamico2.NumeroRegistrosPagina = 5;
            ucWebConsultorDinamico2.NumeroRegistrosConsulta = 100;
            ucWebConsultorDinamico2.DefinirColumnasConsulta(_tipoEntidad2, "IdElemento,Id,Valor, Orden", "");


            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
            ucWebConsultorDinamico2.EventoElememtoSeleccionado = SeleccionarCaptura2;

        }

        private void InscribirEventos()
        {
            ucWebConsultorDinamico1.EventoElememtoSeleccionado = SeleccionarCaptura1;
            DropDownListAplicacion.SelectedIndexChanged += DropDownListAplicacion_SelectedIndexChanged;

        }

        private void ConfigurarAlCargarPaginaSiempre()
        {
            CargarListas();
            generadorControles.AplicarAcciones(contenedor, captura);
        }

        #endregion

        #region Paso  6 Métodos de acciones en entidades

        public Catalogo Instanciar()
        {
            return administradorNegocio.Instanciar<Catalogo>();
        }

        public ElementoCatalogo Instanciar2()
        {
            return administradorNegocio.Instanciar<ElementoCatalogo>();
        }

        public Catalogo Obtener()
        {
            return administradorNegocio.Obtener<Catalogo>(s => s.IdCatalogo == IdElemento);
        }

        public ElementoCatalogo Obtener2()
        {
            return administradorNegocio.ObtenerElementoCatalogo(IdElemento2, (Int16)AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma);
            //return administradorNegocio.Obtener<ElementoCatalogo>(s => s.IdElemento == IdElemento2);
        }

        #endregion

        #region paso  7  metodos de seleccion de  registros
        protected void DropDownListAplicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList listaaplicacion = (DropDownList)sender;
            IdAplicacion = int.Parse(listaaplicacion.SelectedItem.Value);
            if (IdAplicacion != 0)
            {

                btnNuevo_Click(null, null);
                btnNuevo_Click2(null, null);
                generadorControles.AsignaValorResuestaYControl(contenedor, captura,"IdAplicacion",IdAplicacion.ToString());
                   _entidad.IdAplicacion = IdAplicacion;
                //  se actuliza  elementos 
                ActualizarElementos(true);
                //ScriptManager.RegisterStartupScript(this, Page.GetType(), "doPostBack", "Recargar('', '');", true);
            }

        }

        protected void SeleccionOpcionMenu(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandArgument;
            if (accion != null && accion != string.Empty)
                AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma = int.Parse(accion);


            ActualizarElementos(true);
            //ConsultarElementosDeElementoPrincipalSeleccionado();
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "doPostBack", "Recargar('', '');", true);
            //Response.Write("<script>document.forms[0].submit();</script>");
        }

        public void SeleccionarCaptura1(object sender, ArgumentosConsulta argsConsulta)
        {
            IdElemento = 0;
            if (argsConsulta.Registro != null)
            {
                if (argsConsulta.TipoEntidad.Name.Contains("Catalogo"))
                {
                    IdElemento = int.Parse(argsConsulta.Registro.Cells[1].Text);
                    if (IdElemento != null)
                    {
                        _entidad = Obtener();
                        if (_entidad != null)
                        {
                            generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
                        }
                        //ConsultarElementosDeElementoPrincipalSeleccionado();
                        btnNuevo_Click2(null, null);
                        ActualizarElementosConsultaSecundaria(sender, argsConsulta, true);
                        //ScriptManager.RegisterStartupScript(this, Page.GetType(), "doPostBack", "Recargar('', '');", true);
                    }
                }
            }
        }

        public void SeleccionarCaptura2(object sender, ArgumentosConsulta argsConsulta)
        {
            IdElemento2 = 0;
            if (argsConsulta.Registro != null)
            {

                if (argsConsulta.TipoEntidad.Name == "ElementoCatalogo")
                {
                    IdElemento2 = int.Parse(argsConsulta.Registro.Cells[1].Text);
                    //IdElemento2 = int.Parse(ucWebConsultorDinamico2.Argumentos.Registro.Cells[1].Text);
                    if (IdElemento2 != null)
                    {

                        _entidad2 = Obtener2();
                        if (_entidad2 != null)
                        {
                            generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura2, _tipoEntidad2, _entidad2);
                        }
                        //ConsultarElementoSeleccionado();
                        //ScriptManager.RegisterStartupScript(this, Page.GetType(), "doPostBack", "Recargar('', '');", true);
                    }
                }
            }
        }


        #endregion

        # region paso  8  acciones de  captura

        #region acciones de captura uno
        protected void Accion_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;
            //generadorControles.AsignaVaorResuestaYControl(contenedor, captura, "IdAplicacion", _entidad.IdAplicacion.ToString());
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
            else if (accion == "IDIOMA" || accion == "Ingles" || accion == "Español")
                SeleccionOpcionMenu(sender, e);
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
            _entidad = (Catalogo)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
            _entidad.IdAplicacion = IdAplicacion;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad =(Catalogo) generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            if (_entidad != null)
            {
                _entidad.IdCatalogo = 0;
                _entidad.IdAplicacion = IdAplicacion;
                administradorNegocio.Agregar<Catalogo>((Catalogo)_entidad);
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                IdElemento = _entidad.IdCatalogo;

            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (IdElemento != null && IdElemento != 0)
            {
                _entidad = Obtener();
                if (_entidad != null)
                {
                    _entidad = (Catalogo)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    administradorNegocio.Actualizar<Catalogo>((Catalogo)_entidad);
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
                        //_entidad = (Catalogo)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

                        administradorNegocio.Eliminar<Catalogo>((Catalogo)_entidad);

                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, Instanciar());
                    }
                }
            }
            catch (Exception err)
            {

            }
        
        }

        #  endregion 

        # region acciones de captura uno
        protected void Accion_Click2(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;

            generadorControles.AsignaValorResuestaYControl(contenedor, captura, "IdIdioma", AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma.ToString());
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
            else if (accion == "IDIOMA" || accion == "Ingles" || accion == "Español")
                SeleccionOpcionMenu(sender, e);
            if (accion == "Agregar" || accion == "Modificar" || accion == "Eliminar")
            {
                ActualizarElementosConsultaSecundaria(sender, e,true);
                UcWebMensaje1.MostrarMensaje("Acción:" + accion, "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
            }
        }

        protected void btnNuevo_Click2(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor2, captura2);
            _entidad2 = (ElementoCatalogo)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, Instanciar2());
        }

        protected void btnAgregar_Click2(object sender, EventArgs e)
        {
            _entidad2 = Instanciar2();

            if (_entidad2 != null)
           {

                _entidad2 = (ElementoCatalogo)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                _entidad2.IdElemento = 0;
                _entidad2.IdCatalogo = IdElemento;
                _entidad2.IdIdioma = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma;
                administradorNegocio.Agregar<ElementoCatalogo>((ElementoCatalogo)_entidad2);
                administradorNegocio.GuardarCambios();
                IdElemento2 = _entidad2.IdElemento;
                generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);

            }

        }

        protected void btnActualizar_Click2(object sender, EventArgs e)
        {
            if (IdElemento2 != null && IdElemento2 != 0)
            {
                _entidad2 = Obtener2();
                if (_entidad2 != null)
                {
                    _entidad2 = (ElementoCatalogo)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                    administradorNegocio.Actualizar<ElementoCatalogo>((ElementoCatalogo)_entidad2);
                    administradorNegocio.GuardarCambios();

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
                        //_entidad2 = (ElementoCatalogo)generadorControles.GuardarEntidadPorAplicacion(contenedor2, captura2, _tipoEntidad2, _entidad2);
                        administradorNegocio.Eliminar<ElementoCatalogo>((ElementoCatalogo)_entidad2);
                        administradorNegocio.GuardarCambios();
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor2, captura2, _tipoEntidad2, Instanciar2());
                    }
                }
            }
           catch ( Exception error)
            {

            }

        }

        #endregion 

        #endregion

        #region  Paso  9 Métodos para   actualizar  grids

        public void ActualizarElementos(bool actualizar)
        {
            ActualizarElementosConsultaPrincipal(null, null, actualizar);
            ActualizarElementosConsultaSecundaria(null, null, actualizar);
        }

        protected void ActualizarElementosConsultaPrincipal(object sender, EventArgs e,bool actualizar)
        {
            if (_entidad != null)
            {
                if (_lista == null || actualizar)
                    _lista = administradorNegocio.ObtenerCatalogos(_entidad);

            }
            ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
        }
        //public void ConsultarElementosDeElementoPrincipalSeleccionado()
        //{
        //    if (_entidad.Tipo == "C")
        //    {
        //        _lista = administradorNegocio.ObtenerCatalogos(_entidad);
        //        ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
        //        if (IdElemento != null && IdElemento != 0)
        //        {
        //            //IdElemento = int.Parse(ucWebConsultorDinamico1.Argumentos.Registro.Cells[1].Text);
        //            _lista2 = administradorNegocio.ObtenerListaElementosCatalogo(IdElemento, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma);
        //            if (_lista2 != null && _lista2.GetType().ToString() != "System.Data.DataSet")
        //            {
        //                ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2);
        //            }
        //            //else
        //            //    ConsultarElementoSeleccionadoVacio();
        //        }
        //    }
        //}

        protected void ActualizarElementosConsultaSecundaria(object sender, EventArgs e, bool actualizar)
        {
            if (_entidad.Tipo == "C" && IdElemento != null && IdElemento != 0)
            {
                if (_lista2 == null || actualizar)
                    _lista2 = administradorNegocio.ObtenerListaElementosCatalogo(IdElemento, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma);
                if (_lista2 != null && _lista2.GetType().ToString() != "System.Data.DataSet")
                {
                    ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2, _tipoEntidad2);
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
            _lista2 = administradorNegocio.ObtenerListaElementosCatalogo(0, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma);
            ucWebConsultorDinamico2.AsigarOrigenDatos((IEnumerable<object>)_lista2, _tipoEntidad2);
        }


        #endregion

        #region Métodos comunes



        public void ObtenerRespuesta(object respuesta)
        {
            respuesta = respuesta.ToString();
        }
        public void CargarListas()
        {

            // carga aplicaciones  en combobox

            string campo = "IdAplicacion";
            string valor = "Descripcion";
            CargardorCatalogos cc = new CargardorCatalogos();
            if (DropDownListAplicacion.Items.Count == 0)
            {
                _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();

                cc.Cargar(DropDownListAplicacion, _contexto.Aplicacion.ToList(), campo, valor);

                DropDownListAplicacion.SelectedIndex = DropDownListAplicacion.Items.IndexOf(DropDownListAplicacion.Items.FindByValue(_entidad.IdAplicacion.ToString()));
                DropDownListAplicacion.AutoPostBack = true;
                DropDownListAplicacion.DataBind();
            }
        }
        #endregion   
    }
}