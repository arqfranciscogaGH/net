
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
using Entidad.Cliente;


namespace Sitio.Consultas
    {
        public partial class Comisiones : System.Web.UI.Page
        {

            #region  paso  2  declaracion variables



            private string ClaveAplicacion = "AdministracionCliente";

            //  reglas  de megocio

            private static AdministradorCliente administradorNegocio;
            //  captura de etidades  y  listas 

            // primer   captura
            private static Control contenedor;
            private static Captura captura;
            private static Type _tipoEntidad;
            private static Cliente _entidad;
            private static List<Cliente> _lista;
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
                ActualizarElementos(false);
                ConfigurarAlCargarPaginaSiempre();
                ucWebBarraProgreso1.DesActivar();
            }

            #endregion

            #region  paso  5 Configurar

            public void IniciarControladores()
            {
                ClaveAplicacion = "AdministracionCliente";
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

                captura.IdPlaneacionCaptura = 5500;
                captura.IdClasificacionCaptura = 1;

                captura.IdEstausCaptura = 1;
            }

            // definición de  bloques  de  captura  de  datos
            public void CargarControles()
            {
                contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "BloqueCaptura");
                //generadorControles.CargarControles(contenedor, captura, Accion_Click);
            }

            private void ConfigurarAlCargarPaginaSoloInicialmente()
            {
                administradorNegocio = new AdministradorCliente();
                _entidad = administradorNegocio.Instanciar();
                _tipoEntidad = _entidad.GetType();
               // _lista = obtenerLista();

            }



            private void Configurar()
            {
                ucWebConsultorDinamico1.CrearControles(generadorControles, captura);
                ucWebConsultorDinamico1.Paginacion = true;
                ucWebConsultorDinamico1.NumeroRegistrosPagina = 10;
                ucWebConsultorDinamico1.NumeroRegistrosConsulta = 5000;
                ucWebConsultorDinamico1.DefinirColumnasConsulta(_tipoEntidad, "id,nombre,curp,referencia,telefonoMovil", "");
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

            public Cliente Instanciar()
            {
                _entidad = administradorNegocio.Instanciar();
                _entidad.estatus = 1;
                _entidad = asignar(_entidad);
                return _entidad;

            }

            public Cliente Obtener()
            {
                _entidad = administradorNegocio.Obtener(s => s.id == IdElemento);
                _entidad = asignar(_entidad);

                return _entidad;
            }

            public Cliente asignar(Cliente _entidad)
            {
                String IdUsuario = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.IdUsuario;
                _entidad.idUsuario = int.Parse(IdUsuario == null || IdUsuario == "" ? "2" : IdUsuario);
                if (_entidad.idSocio == null || _entidad.idSocio == 0)
                    _entidad.idSocio = _entidad.idUsuario;
                if (_entidad.idSuscriptor == null || _entidad.idSuscriptor == 0)
                {
                    if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.IdPerfil == "5")
                        _entidad.idSuscriptor = 3;
                    else
                        _entidad.idSuscriptor = 2;
                }

                if (_entidad.fechaRegistro == null || _entidad.fechaRegistro == "")
                    _entidad.fechaRegistro = DateTime.Now.ToString("dd-MM-yyyy");
                _entidad.fechaEstatus = DateTime.Now.ToString("dd-MM-yyyy");
                if (_entidad.cuentaBanco == null || _entidad.cuentaBanco == "")
                    _entidad.cuentaBanco = DateTime.Now.Hour.ToString();
                if (_entidad.idOperacion == null || _entidad.idOperacion == 0)
                {
                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 14)
                        _entidad.idOperacion = 1;
                    else
                        _entidad.idOperacion = 2;
                }
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
                _entidad = (Cliente)generadorControles.AsignarEntidadAControlesPorAplicacion(contenedor, captura, _tipoEntidad, _entidad);
            }

            protected void btnAgregar_Click(object sender, EventArgs e)
            {
                _entidad = Instanciar();
                _entidad = (Cliente)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);

                if (_entidad != null)
                {
                    _entidad.id = 0;
                    //_entidad.IdSuscriptor = int.Parse(AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionUsuarioActual.IdSuscriptor.ToString());
                    administradorNegocio.Agregar((Cliente)_entidad);
                    administradorNegocio.GuardarCambios();
                    generadorControles.AsignarEntidadAControlesPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                    IdElemento = _entidad.id;
                }
            }

            protected void btnActualizar_Click(object sender, EventArgs e)
            {
                if (IdElemento != null && IdElemento != 0)
                {
                    _entidad = Obtener();
                    if (_entidad != null)
                    {
                        _entidad = (Cliente)generadorControles.GuardarEntidadPorAplicacion(BloqueCaptura, captura, _tipoEntidad, _entidad);
                        administradorNegocio.Actualizar((Cliente)_entidad);
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
                            administradorNegocio.Eliminar((Cliente)_entidad);
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
                    if (_lista == null || actualizar)
                        _lista = obtenerLista();
                }
                ucWebConsultorDinamico1.AsigarOrigenDatos(_lista);
            }

            private List<Cliente> obtenerLista()
            {
                String fechaActual = DateTime.Now.ToString("dd-MM-yyyy");
                int idOperacion = 1;
                if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 14)
                    idOperacion = 1;
                else
                    idOperacion = 2;

                if (AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.IdPerfil == "5")
                    _lista = administradorNegocio.Consultar(s => s.idSuscriptor == 3 && s.fechaRegistro == fechaActual && s.idOperacion == idOperacion).ToList();
                else
                    _lista = administradorNegocio.Consultar(s => s.estatus == 1 || s.estatus == null).ToList();
                return _lista;
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