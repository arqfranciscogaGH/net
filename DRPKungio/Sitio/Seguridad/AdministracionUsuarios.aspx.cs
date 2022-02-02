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
    public partial class AdministracionUsuarios : System.Web.UI.Page
    {

        #region  paso  2  declaracion variables

        private ModeloSistema _contexto;
        private string ClaveAplicacion = "AdministracionUsuarios";

        //  reglas  de megocio

        private static AdministradorUsuarios  administradorNegocio;
        //  captura de etidades  y  listas 

        // primer   captura
        private static Control contenedor;
        private static Captura captura;
        private static Type _tipoEntidad;
        private static CuentaUsuario _entidad;
        private static List<CuentaUsuario> _lista;
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
            captura.IdPlaneacionCaptura = 1016;
            captura.IdClasificacionCaptura = 1;
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
            administradorNegocio = new AdministradorUsuarios();
            _entidad = administradorNegocio.Instanciar<CuentaUsuario>();
            _tipoEntidad = _entidad.GetType();
            _lista = administradorNegocio.ObtenerLista<CuentaUsuario>();

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
            ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "IdUsuario,Cuenta,Activo", "");
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

        public CuentaUsuario Instanciar()
        {
            _entidad = administradorNegocio.Instanciar<CuentaUsuario>();
            return _entidad;
        }

        public CuentaUsuario Obtener()
        {
            _entidad = administradorNegocio.Obtener<CuentaUsuario>(s => s.IdUsuario == IdElemento);
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


                        //  Persona

                        Persona persona = new Persona();
                        persona = administradorNegocio.Obtener<Persona>(s => s.IdUsuario == _entidad.IdUsuario);
                        if (persona == null)
                        {
                            persona = new Persona();
                            persona.IdUsuario = _entidad.IdUsuario;
                        }
                           
                        generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, persona.GetType(), persona);


                        //  Domicilio
                        Domicilio domicilio = new Domicilio();
                        domicilio = administradorNegocio.Obtener<Domicilio>(s => s.IdUsuario == _entidad.IdUsuario);
                        if (domicilio == null)
                        {
                            domicilio = new Domicilio();
                            domicilio.IdUsuario = _entidad.IdUsuario;
                            domicilio.IdPersona = persona.IdPersona;
                        }
                        generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, domicilio.GetType(), domicilio);


                        //  Empleado
                        Empleado empleado = new Empleado();
                        empleado = administradorNegocio.Obtener<Empleado>(s => s.IdUsuario == _entidad.IdUsuario);
                        if (empleado == null)
                        {
                            empleado = new Empleado();
                            empleado.IdUsuario = _entidad.IdUsuario;
                        }
                        generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, empleado.GetType(), empleado);

                        //  Cuenta Usuario
                        //  se asigna  datos  a  captura
                        generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);

                        generadorControles.ParametrosDinamicos = "@TipoElemento=U" + ";@IdElemento=" + _entidad.IdUsuario.ToString();

                        generadorControles.AplicarAcciones(contenedor, captura);
                        UcWebMensaje1.MostrarMensaje("3", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
                    }
                    ////  se limpia  captura secundaria
                    //btnNuevo_Click2(null, null);
                    ////  se actuliza  elementos secundarios
                    //ActulizarElementosConsultaSecundaria(null, null);
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
            //else if (accion == "Vista  Previa")
            //{
            //    Ver();
            //}
            else if (accion == "Cancelar" || accion == "Salir")
            {
                Salir();
            }
            UcWebMensaje1.MostrarMensaje("Cuenta Usuario, Acción:" + accion, "Se realizó la  operación completa", UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, ObtenerRespuesta);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Dictionary<string, AtributoControl> valoresControles = new Dictionary<string, AtributoControl>();
            valoresControles = generadorControles.ObtenerValoresControlesPorAplicacion(contenedor, captura);
            _entidad = Instanciar();
            _entidad = (CuentaUsuario)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
            //_entidad.IdModulo = IdModulo;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _entidad = Instanciar();
            _entidad = (CuentaUsuario)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

            if (_entidad != null)
            {
                //   cuenta  de usuario
                _entidad.IdUsuario = 0;
                _entidad.IdSuscriptor = int.Parse(AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdSuscriptor.ToString());

                administradorNegocio.Agregar((CuentaUsuario)_entidad);
                administradorNegocio.GuardarCambios();
                IdElemento = _entidad.IdUsuario;
                string sql = "exec  ActualizarPrivilegios 'I','U'," + _entidad.IdUsuario + ",'" + _entidad.Privilegios + "'";
                administradorNegocio.ExecutarSql(sql);
                administradorNegocio.GuardarCambios();
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                //  Persona
                Persona persona = GuardarPersona("AGREGAR", _entidad);

                //  Domicilio
                Domicilio domicilio =GuardarDomicilio("AGREGAR", _entidad, persona);
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, domicilio.GetType(), domicilio);
                //  Empleado
                Empleado empleado =GuardarEmpleado("AGREGAR", _entidad);
                generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, empleado.GetType(), empleado);

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
 
                    //  Persona
                    Persona persona =GuardarPersona("ACTULIZAR", _entidad);

                    //  Domicilio
                    GuardarDomicilio("ACTULIZAR", _entidad, persona);

                    //  Empleado
                    GuardarEmpleado("ACTULIZAR", _entidad);

                    //   cuenta  de usuario

                    _entidad = (CuentaUsuario)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    administradorNegocio.Actualizar((CuentaUsuario)_entidad);
                    string sql = "exec  ActualizarPrivilegios 'A','U'," + _entidad.IdUsuario + ",'" + _entidad.Privilegios + "'";
                    administradorNegocio.ExecutarSql(sql);
                    administradorNegocio.GuardarCambios();

                    ActualizarElementosConsultaPrincipal(sender, e);
                }
            }
        }
        public Persona  GuardarPersona (string accion, CuentaUsuario _entidad )
        {
            Persona persona=null;
            persona = administradorNegocio.Obtener<Persona>(s => s.IdUsuario == _entidad.IdUsuario);
            if (accion.ToUpper() == "AGREGAR" || persona == null)
            {
                persona = new Persona();
                generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, persona.GetType(), persona);
                persona.IdPersona = 0;
                persona.IdUsuario = _entidad.IdUsuario;
                persona.IdSuscriptor = int.Parse(AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdSuscriptor.ToString());
                administradorNegocio.Agregar((Persona)persona);

            }
            else if (accion.ToUpper() == "ACTULIZAR" || persona != null)
            {
                generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, persona.GetType(), persona);
                administradorNegocio.Actualizar((Persona)persona); administradorNegocio.Actualizar((Persona)persona);
            }

            administradorNegocio.GuardarCambios();
            return persona;
        }

        
        public Domicilio GuardarDomicilio(string accion, CuentaUsuario _entidad, Persona entidad)
        {
            Domicilio domicilio= administradorNegocio.Instanciar<Domicilio>();
            domicilio = administradorNegocio.Obtener<Domicilio>(s => s.IdUsuario == _entidad.IdUsuario);
            if (domicilio == null || accion.ToUpper() == "AGREGAR"  )
            {
                domicilio =  administradorNegocio.Instanciar<Domicilio>();

                generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, domicilio.GetType(), domicilio);
                domicilio.IdDomicilio = 0;
                domicilio.IdUsuario = _entidad.IdUsuario;
                domicilio.IdPersona = entidad.IdPersona;
                domicilio.IdSuscriptor = int.Parse(AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdSuscriptor.ToString());
                administradorNegocio.Agregar((Domicilio)domicilio);
            }
            else if (accion.ToUpper() == "ACTULIZAR" || domicilio != null)
            {
                generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, domicilio.GetType(), domicilio);
                administradorNegocio.Actualizar((Domicilio)domicilio); administradorNegocio.Actualizar((Domicilio)domicilio);
            }

            administradorNegocio.GuardarCambios();
            return domicilio;
        }
        public Empleado GuardarEmpleado(string accion, CuentaUsuario _entidad)
        {
            Empleado empleado = null;
            empleado = administradorNegocio.Obtener<Empleado>(s => s.IdUsuario == _entidad.IdUsuario);
            if (accion.ToUpper() == "AGREGAR" || empleado == null)
            {
                empleado = new Empleado();
                generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, empleado.GetType(), empleado);
                empleado.IdEmpleado = 0;
                empleado.IdUsuario = _entidad.IdUsuario;
                administradorNegocio.Agregar((Empleado)empleado);
            }
            else if (accion.ToUpper() == "ACTULIZAR" || empleado != null)
            {
                generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, empleado.GetType(), empleado);
                administradorNegocio.Actualizar((Empleado)empleado); administradorNegocio.Actualizar((Empleado)empleado);
            }

            administradorNegocio.GuardarCambios();
            return empleado;
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
                        //  Domicilio

                        Domicilio domicilio = administradorNegocio.Obtener<Domicilio>(s => s.IdUsuario == _entidad.IdUsuario);
                        if (domicilio != null)
                            administradorNegocio.Eliminar((Domicilio)domicilio);

                        //  Empleado

                        Empleado empleado = administradorNegocio.Obtener<Empleado>(s => s.IdUsuario == _entidad.IdUsuario);
                        if (empleado != null)
                            administradorNegocio.Eliminar((Empleado)empleado);
                        //  Persona

                        Persona persona = administradorNegocio.Obtener<Persona>(s => s.IdUsuario == _entidad.IdUsuario);
                        if (persona != null)
                            administradorNegocio.Eliminar((Persona)persona);

                        administradorNegocio.GuardarCambios();

                        administradorNegocio.Eliminar((CuentaUsuario)_entidad);
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
                _lista = administradorNegocio.Consultar<CuentaUsuario>(s => s.IdSuscriptor == AdministradorSistema.SesionSistemaActual.IdSuscriptor).ToList();
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
