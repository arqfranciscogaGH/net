using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

using MeNet.Nucleo.Sesion;
using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.AdministradorBaseDatos;
using MeNet.Nucleo.Modelo;
using DRP.Modelo;
using MeNet.Nucleo.Configuracion;
using MeNet.Nucleo.Correo;
using MeNet.Nucleo.Utilerias;
using MeNet.Nucleo.Comun;
using MeNet.Nucleo.Negocio;



namespace MeNet.Seguridad.Administrador
{
    public class AdministradorSeguridad : AdministradorNegocioGeneral
    {
        #region // variables
        private ModeloSistema _contexto;
        private AdministradorConfiguracion _administradorConfiguracion;
        private List<Configuracion> _listaConfiguracion;
        AdministradorCorreo _administradorCorreo;

        private SesionSistema _sesionSistemaActual;
        private SesionUsuario _sesionUsuarioActual ;
        private Aplicacion _aplicacion;
        #endregion

        public ParametrosSeguridad ParametrosSeguridadActual = new ParametrosSeguridad();
        public AdministradorTerminologia AdministradorTerminologia ;

        #region // Propiedades

        //public int? IdIdiomaActual = null;
        
        Empleado EmpleadoUsuario = new Empleado();
        List<Perfil> ListaPerfil = new List<Perfil>();
        List<Grupo> ListaGrupo = new List<Grupo>();


        public int? IdIdiomaPorDefecto
        {
            get
            {
                return SesionSistemaActual.IdIdioma;
            }
            set
            {
   
            }
        }
        public int? IdIdioma
        {
            get
            {
                return SesionUsuarioActual.IdIdioma;
            }
            set
            {
                SesionUsuarioActual.IdIdioma = value;
            }
        }
        public string Cultura
        {
            get
            {
                return SesionSistemaActual.Cultura;
            }
            set
            {
                SesionSistemaActual.Cultura = value;
            }
        }




        #endregion


        #region // Metodos

        public AdministradorSeguridad()
        {
           
            _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
            this.Contexto = _contexto;
            AdministradorTerminologia = new AdministradorTerminologia();
            _administradorConfiguracion = new AdministradorConfiguracion();
            _listaConfiguracion = _administradorConfiguracion.ObtenerConfiguracionAplicacion(1);
            _administradorCorreo = new AdministradorCorreo();
            SesionSistema sesionSistema = SesionSistemaActual;
            _sesionUsuarioActual =ObtenerSesionUsuario();

        }

        public List<Perfil> ObtenerPerfilesUsuario()
        {
            return ListaPerfil;
        }

        public Aplicacion AplicacionActual
        {
            get
            {
                if (_aplicacion == null)
                {
                    _aplicacion = new Aplicacion();
                }
                return _aplicacion;
            }
            set { _aplicacion = value; }
        }
        public int ObtenerAplicacion(string Clave)
        {
            Aplicacion aplicacion = _contexto.Aplicacion.FirstOrDefault(c => c.Clave == Clave);
            if (aplicacion != null && aplicacion.IdAplicacion != null)
            {
                AplicacionActual = aplicacion;
            }
            return AplicacionActual.IdAplicacion;
        }
        List<ObtenerPrivilegios_Result> _privilegios;
        public List<ObtenerPrivilegios_Result> Privilegios
        {
            get
            {
                if (_privilegios == null)
                    _privilegios = ObtenerPrivilegios();

                return _privilegios;
            }
            set { _privilegios = value; }
        }
        public void ObtenerMenuPrincipal()
            {
            Perfil perfil = _contexto.Perfil.FirstOrDefault(c => c.IdPerfil.ToString() == ParametrosSeguridadActual.IdPerfil);
            if (perfil.IdMenu != null)
            {
                ParametrosSeguridadActual.IdMenu = perfil.IdMenu.ToString();
                ParametrosSeguridadActual.NombrePerfil = perfil.Nombre;
            }

        }

        public SesionUsuario AsignarSesion(SesionSistema sesionSistema)
        {
            SesionUsuario sesionUsuario = new SesionUsuario();
            sesionUsuario.LlaveSesion = sesionSistema.LLaveSesion;
            sesionUsuario.CuentaUsuario = sesionSistema.Cuenta;
            sesionUsuario.IdIdioma = sesionSistema.IdIdioma;
            if (sesionUsuario.Tema == null || sesionUsuario.Tema == string.Empty)
                sesionUsuario.Tema = sesionSistema.Tema;
            sesionUsuario.AgenteSesion = sesionSistema.AgenteSesion;
            sesionUsuario.Dispositivo = sesionSistema.Dispositivo;
            sesionUsuario.Equipo = sesionSistema.Equipo;
            sesionUsuario.FechaIngreso = DateTime.Now;
            sesionUsuario.IdSuscriptor = sesionSistema.IdSuscriptor;
            sesionUsuario.IdEstatusRegistro = 1;

            return sesionUsuario;

        }
        public SesionUsuarioHistorial AsignarSeionUsuarioHistorial(SesionUsuarioHistorial sesionUsuarioHistorial, SesionUsuario sesionUsuario)
        {
            sesionUsuarioHistorial.CuentaUsuario = sesionUsuario.CuentaUsuario;
            sesionUsuarioHistorial.LlaveSesion = sesionUsuario.LlaveSesion;
            sesionUsuarioHistorial.AgenteSesion = sesionUsuario.AgenteSesion;
            sesionUsuarioHistorial.Dispositivo = sesionUsuario.Dispositivo;
            sesionUsuarioHistorial.Equipo = sesionUsuario.Equipo;
            sesionUsuarioHistorial.IdSuscriptor = sesionUsuario.IdSuscriptor;
            sesionUsuarioHistorial.CuentaUsuario = sesionUsuario.CuentaUsuario;
            sesionUsuarioHistorial.IdUsuario = sesionUsuario.IdUsuario;
            sesionUsuarioHistorial.IdSesion = sesionUsuario.IdSesion;
            sesionUsuarioHistorial.FechaIngreso = DateTime.Now;
            sesionUsuarioHistorial.ClaveAplicacion = SesionSistemaActual.ClaveAplicacion;

            return sesionUsuarioHistorial;
        }

        public Empleado ObtenerEmpleado(CuentaUsuario cuentaUsuario)
        {
            Empleado empleado;
            empleado = this.Obtener<Empleado>(s => s.IdUsuario == cuentaUsuario.IdUsuario);
            if (empleado == null)
                empleado = new Empleado();

            return empleado;
        }
        public List<Perfil> ObtenerPerfiles(CuentaUsuario cuentaUsuario)
        {
            List<Perfil> lista = new List<Perfil>();
            List<string> listaT = cuentaUsuario.Perfiles.Split(',').ToList();
            foreach (string elemento in listaT)
            {
                Perfil perfil = this.Obtener<Perfil>(s => s.IdPerfil.ToString() == elemento);
                lista.Add(perfil);
            }
            return lista;
        }
        public string AsignarPerfilPorDefecto()
        {
            string IdPerfil = string.Empty;
            Perfil perfil = ListaPerfil.FirstOrDefault();
            if (perfil != null)
                IdPerfil = perfil.IdPerfil.ToString();
            ParametrosSeguridadActual.IdPerfil = IdPerfil;
            return IdPerfil;
        }


        public List<Grupo> ObtenerGrupos(CuentaUsuario cuentaUsuario)
        {
            List<Grupo> lista = new List<Grupo>();
            List<string> listaT = cuentaUsuario.Grupos.Split(',').ToList();
            foreach (string elemento in listaT)
            {
                Grupo perfil = this.Obtener<Grupo>(s => s.IdGrupo.ToString() == elemento);
                lista.Add(perfil);
            }
            return lista;
        }

        public CuentaUsuario ObtenerCuentaUsuario(string cuentaUsuario)
        {
            CuentaUsuario cuenta = new CuentaUsuario();
            cuenta = this.Obtener<CuentaUsuario>(s => s.Cuenta == cuentaUsuario);
            return cuenta;
        }
        public Persona ObtenerPersona(CuentaUsuario cuentaUsuario)
        {
            Persona persona = new Persona();
            persona = this.Obtener<Persona>(s => s.IdUsuario == cuentaUsuario.IdUsuario);
            return persona;
        }

        public SesionUsuario AgregarSesionUsuario(SesionUsuario sesionUsuario)
        {
            this.Agregar<SesionUsuario>(sesionUsuario);
            return sesionUsuario;
        }
        public SesionUsuario ActualizarSesionUsuario(SesionUsuario sesionUsuario)
        {
            this.Actualizar<SesionUsuario>(sesionUsuario);
            return sesionUsuario;
        }

        public CuentaUsuario ActualizarCuentaUsuario(CuentaUsuario cuentaUsuario)
        {

            this.Actualizar<CuentaUsuario>(cuentaUsuario);
            return cuentaUsuario;
        }
        public SesionUsuarioHistorial AgregarSesionUsuarioHistorial(SesionUsuario sesionUsuario)
        {
            //_contexto.SesionUsuario. =sesionUsuario.;
            SesionUsuarioHistorial sesionUsuarioHistorial = new SesionUsuarioHistorial();
            sesionUsuarioHistorial = AsignarSeionUsuarioHistorial(sesionUsuarioHistorial, sesionUsuario);
            this.Agregar<SesionUsuarioHistorial>(sesionUsuarioHistorial);
            return sesionUsuarioHistorial;
        }

        public void GuardarSesion(SesionUsuario sesionUsuario, CuentaUsuario cuentaUsuario)
        {
            System.IO.StringWriter writer = new System.IO.StringWriter();

            if (sesionUsuario.IdSesion == 0)
            {
                AgregarSesionUsuario(sesionUsuario);
            }
            else
            {
                ActualizarSesionUsuario(sesionUsuario);
            }
            _contexto.SaveChanges();
            if (cuentaUsuario != null)
            {
                ActualizarCuentaUsuario(cuentaUsuario);
            }
            _contexto.SaveChanges();
            if (SesionSistemaActual.GuardarSesion == "BaseDatos")
            {
                AgregarSesionUsuarioHistorial(sesionUsuario);
            }
            _contexto.SaveChanges();
        }

        public SesionUsuario IniciarSesionUsuario()
        {
            _sesionUsuarioActual = ObtenerSesionUsuario();
            RealizarTransaccionIniciarSesionUsuario(_sesionUsuarioActual, null);
            //if  (_sesionUsuarioActual!=null && _sesionUsuarioActual.IdUsuario!=0)
            //    Privilegios = ObtenerPrivilegios();
            return _sesionUsuarioActual;
        }


        public void RealizarTransaccionIniciarSesionUsuario(SesionUsuario sesionUsuario, CuentaUsuario cuentaUsuario)
        {
            try
            {
                using (TransactionScope transaccion = new TransactionScope())
                {
                    GuardarSesion(sesionUsuario, cuentaUsuario);
                    transaccion.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                //writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }
            catch (ApplicationException ex)
            {
                //writer.WriteLine("ApplicationException Message: {0}", ex.Message);
            }
        }

        public void Actualizar(SesionUsuario sesionUsuario, bool actualizaCuenta)
        {
            // obtener información  de cuenta 
            CuentaUsuario cuentaUsuario = null;
            if (actualizaCuenta)
            {
                cuentaUsuario = ObtenerCuentaUsuario(sesionUsuario.CuentaUsuario);
                if (cuentaUsuario != null)
                {
                    cuentaUsuario.Tema = sesionUsuario.Tema;
                    cuentaUsuario.IdIdioma = sesionUsuario.IdIdioma;
                }
            }

            if (SesionSistemaActual.GuardarSesion == "BaseDatos")
            {
                RealizarTransaccionActualizar(_sesionUsuarioActual, cuentaUsuario);
            }
        }
        public void RealizarTransaccionActualizar(SesionUsuario sesionUsuario, CuentaUsuario cuentaUsuario)
        {
            try
            {
                using (TransactionScope transaccion = new TransactionScope())
                {
                    GuardarSesion(sesionUsuario, cuentaUsuario);
                    transaccion.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                //writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }
            catch (ApplicationException ex)
            {
                //writer.WriteLine("ApplicationException Message: {0}", ex.Message);
            }
        }
        public SesionUsuario SesionUsuarioActual
        {
            get
            {
                if (_sesionUsuarioActual == null)
                  _sesionUsuarioActual = ObtenerSesionUsuario();

                return _sesionUsuarioActual;
            }
            set { _sesionUsuarioActual = value; }
        }
        public SesionUsuario ObtenerSesionUsuario()
        {
            SesionUsuario sesionUsuario;
            string llave = SesionSistemaActual.LLaveSesion;
            sesionUsuario=this.Obtener<SesionUsuario>(s => s.LlaveSesion == llave);
            //sesionUsuario = _contexto.SesionUsuario.FirstOrDefault(s => s.LlaveSesion == llave);

            if (sesionUsuario == null)
                sesionUsuario = AsignarSesion(SesionSistemaActual);

            return sesionUsuario;
        }

        public SesionSistema SesionSistemaActual
        {
            get { return AdministradorSesion.SesionSistemaActual; }
            set { AdministradorSesion.SesionSistemaActual = value; }
        }

        public int? IdSuscriptor
        {
            get
            {
                // return SesionUsuarioActual.IdSuscriptor;
                return AdministradorSesion.SesionSistemaActual.IdSuscriptor;
            }
            set
            {
                AdministradorSesion.SesionSistemaActual.IdSuscriptor = value;
                //SesionUsuarioActual.IdSuscriptor = value;
            }
        }
        public string ValidarSesionUsuario(string cuenta, string contrasena)
        {
            string mansaje = string.Empty;
  
            // inicia    la sesion  
            SesionUsuario sesionUsuario = ObtenerSesionUsuario();

            SesionSistemaActual.Cuenta = cuenta;
            SesionSistemaActual.Contrasena = contrasena;


            //  inicia contexto
            if (SesionSistemaActual.TipoCuenta == "CuentaBaseDatos")
            {
                ModeloSistema _contexto = new ModeloSistema(SesionSistemaActual.ObtenerCadenaConexion());
                AdministradorContexto.Agregar(_contexto);
            }
            //_contexto = AdministradorContexto.Iniciar<ModeloSistema>(sesionSistema.ObtenerCadenaConexion());

            // obtener información  de cuenta 
            CuentaUsuario cuentaUsuario = new CuentaUsuario();
            cuentaUsuario = ObtenerCuentaUsuario(SesionSistemaActual.Cuenta);
            if (cuentaUsuario != null)
            {
                if (cuentaUsuario.Activo!=true )
                {
                    mansaje = "La cuenta no esta activa";

                }
                if (cuentaUsuario.IdEstatusUsuario == 1)
                    mansaje = "La cuenta es bloqueada";
                //int DiasParaExpirar = 0;
                //DiasParaExpirar= (int)_administradorConfiguracion.Convertir("DiasParaExpirar");
                //int DiasAntesExpirar = (int)_administradorConfiguracion.Convertir("DiasAntesExpirar");

                //DateTime fv = cuentaUsuario.FechaVigencia;

                //DateTime fv = DateTime.Parse(cuentaUsuario.FechaVigencia.ToString());

                //TimeSpan difDias = DateTime.Today.Subtract(fv);
                
                if (cuentaUsuario.Expira != null && cuentaUsuario.Expira == true)
                {
                    mansaje = "La cuenta ya expiró ";
                           
                }
                //else if (cuentaUsuario.FechaVigencia != null && difDias.Days<3 )
                //         mansaje = "La cuenta está expirar ,  debe  cambiar su contraseña ";

                int Sesiones = 0;
                Sesiones = (int)_administradorConfiguracion.Convertir("Sesiones");

                if (cuentaUsuario.Sesiones > Sesiones)  // definir Sesiones en parametro
                {
                    mansaje = "Ha rebasado el numero de sesiones";
                }
                int Intentos = (int)_administradorConfiguracion.Convertir("Intentos");
                if (cuentaUsuario.Intentos > Intentos)  // definir intentos en parametro
                {
                    cuentaUsuario.IdEstatusUsuario = 1;  // se bloquea 

                    mansaje = "Ha rebasado el numero de intentos";

                }
                if (SesionSistemaActual.TipoCuenta != "CuentaBaseDatos" && cuentaUsuario.Contrasena != SesionSistemaActual.Contrasena)
                {
                    if (cuentaUsuario.Intentos == null)
                        cuentaUsuario.Intentos = 0;
                    cuentaUsuario.Intentos = cuentaUsuario.Intentos + 1;
                    //GuardarSesion( sesionUsuario, cuentaUsuario);
                    mansaje = "La contraseña es incorrecta";
                }
                if (mansaje == string.Empty)
                {
                    if (cuentaUsuario.Tema == null || cuentaUsuario.Tema == string.Empty)
                        cuentaUsuario.Tema = SesionSistemaActual.Tema;
                    if (cuentaUsuario.IdIdioma == null)
                        cuentaUsuario.IdIdioma = SesionSistemaActual.IdIdioma;

         
                    if (cuentaUsuario.Sesiones == null)
                        cuentaUsuario.Sesiones = 0;
                    cuentaUsuario.Sesiones = cuentaUsuario.Sesiones + 1;
                    cuentaUsuario.Intentos = 0;
                    cuentaUsuario.FechaCambioEstatus = DateTime.Now;

                    sesionUsuario.Tema = cuentaUsuario.Tema;
                    sesionUsuario.IdIdioma = cuentaUsuario.IdIdioma;

                    sesionUsuario.IdUsuario = cuentaUsuario.IdUsuario;
                    sesionUsuario.CuentaUsuario = cuentaUsuario.Cuenta;

                    sesionUsuario.IdSuscriptor = cuentaUsuario.IdSuscriptor;
                    SesionSistemaActual.IdSuscriptor = cuentaUsuario.IdSuscriptor;

                    EmpleadoUsuario = ObtenerEmpleado(cuentaUsuario);
                    ListaPerfil = ObtenerPerfiles(cuentaUsuario);
                    ListaGrupo = ObtenerGrupos(cuentaUsuario);

                    ParametrosSeguridadActual = new ParametrosSeguridad();
                    ParametrosSeguridadActual.IdUsuario = cuentaUsuario.IdUsuario.ToString();
                    ParametrosSeguridadActual.IdPerfil = AsignarPerfilPorDefecto();
                    ParametrosSeguridadActual.IdGrupo = cuentaUsuario.Grupos;
                    ParametrosSeguridadActual.IdPuesto = EmpleadoUsuario.IdPuesto.ToString();
                    ParametrosSeguridadActual.IdArea = EmpleadoUsuario.IdArea.ToString();
                    //AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdSuscriptor = cuentaUsuario.IdSuscriptor;
                    if (ListaPerfil != null && ListaPerfil.Count == 1)
                        ObtenerPrivilegios();
                    Persona persona = _contexto.Persona.FirstOrDefault(s => s.IdUsuario == cuentaUsuario.IdUsuario);
                    if (persona != null)
                    {
                        ParametrosSeguridadActual.NombreUsuario = persona.Nombre;
                    }
                }
            }
            else
            {
                mansaje = "La cuenta  No existe";
            }
            // guardar 
            RealizarTransaccionValidarSesionUsuario(sesionUsuario, cuentaUsuario);
            if (mansaje != string.Empty)
            {
                Privilegios = ObtenerPrivilegios();
            }
            return mansaje;
        }
        public string AsignarParametrosDinamicos(string cadena)
        {
            cadena=ParametrosSeguridadActual.IdUsuario != string.Empty ?cadena.Replace("@IdUsuario", ParametrosSeguridadActual.IdUsuario): cadena.Replace("@IdUsuario", "0");
            cadena = ParametrosSeguridadActual.IdPerfil != string.Empty ? cadena.Replace("@IdPerfil", ParametrosSeguridadActual.IdPerfil) : cadena.Replace("@IdPerfil", "0");
            cadena = ParametrosSeguridadActual.IdGrupo != string.Empty ? cadena.Replace("@IdGrupo", ParametrosSeguridadActual.IdGrupo) : cadena.Replace("@IdGrupo", "0");
            cadena = ParametrosSeguridadActual.IdArea != string.Empty ? cadena.Replace("@IdArea", ParametrosSeguridadActual.IdArea) : cadena.Replace("@IdArea", "0");
            cadena = ParametrosSeguridadActual.IdPuesto != string.Empty ? cadena.Replace("@IdPuesto", ParametrosSeguridadActual.IdPuesto) : cadena.Replace("@IdPuesto", "0");
            return cadena;
        }
        public bool ValidarPrivilegios(string clave, string permiso)
        {
            bool existe = false;
            if ((SesionSistemaActual.Ambiente.ToUpper() == "DESARROLLO" || SesionSistemaActual.Ambiente == string.Empty))
                existe = true;
            else
            {
                List<ObtenerPrivilegios_Result> privilegios = Privilegios.Where(s => s.ClaveAplicacion == clave && s.IdPrivilegiAsignado != null).ToList();
                if (privilegios != null)
                {
                    foreach (ObtenerPrivilegios_Result privilegio in privilegios)
                    {

                        if (privilegio.IdPrivilegiAsignado!=null && privilegio.Permiso != null && privilegio.Permiso != string.Empty)
                        {
                            existe = permiso.ToUpper() == "N" ? true : privilegio.Permiso.Contains(permiso);
                            if (existe )
                                return existe;
                        }
                    }
                }
            }
            return existe;
        }
        public bool ValidarExistenciaPrivilegio(string clave)
        {
            bool existe = false;
            if ( clave == string.Empty || clave == "0" || (SesionSistemaActual.Ambiente.ToUpper() == "DESARROLLO" || SesionSistemaActual.Ambiente == string.Empty))
            {
                existe = true;
   
            }
            else
            {
                ObtenerPrivilegios_Result privilegio = Privilegios.FirstOrDefault(s => s.Clave == clave && s.IdPrivilegiAsignado != null);
                if (privilegio != null )
                {
                    existe = true;
                }
            }
            return existe;
        }
        public List<ObtenerPrivilegios_Result> ObtenerPrivilegios()
        {
            AdministradorPrivilegio administradorPrivilegio = new AdministradorPrivilegio();
            int? IdUsuario = ParametrosSeguridadActual.IdUsuario != string.Empty ? int.Parse(ParametrosSeguridadActual.IdUsuario) : 0;
            int? IdPerfil = ParametrosSeguridadActual.IdPerfil != string.Empty ? int.Parse(ParametrosSeguridadActual.IdPerfil) : 0;
            int? IdGrupo = ParametrosSeguridadActual.IdGrupo != string.Empty ? int.Parse(ParametrosSeguridadActual.IdGrupo) : 0;
            Privilegios = administradorPrivilegio.ObtenerPrivilegios(IdUsuario, IdPerfil, IdGrupo).ToList();
            return Privilegios;
        }

        public void RealizarTransaccionValidarSesionUsuario(SesionUsuario sesionUsuario, CuentaUsuario cuentaUsuario)
        {
            try
            {
                using (TransactionScope transaccion = new TransactionScope())
                {
                    GuardarSesion(sesionUsuario, cuentaUsuario);
                    transaccion.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                //writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }
            catch (ApplicationException ex)
            {
                //writer.WriteLine("ApplicationException Message: {0}", ex.Message);
            }
        }


        public void OperacionCambiarContrasena(string cuenta, string contrasena)
        {
            CuentaUsuario cuentaUsuario = new CuentaUsuario();
            string idAplicacion = "CambioContrasena";
            string variablesDinamicas = string.Empty;
            try
            {
                SesionSistema sesionSistema = SesionSistemaActual;
                RespuestaServicio RespuestaCorreo = new RespuestaServicio();
                Persona persona = ObtenerPersona(cuentaUsuario);
                cuentaUsuario = RealizarTransaccionContrasena(cuenta, contrasena);

                if (persona != null)
                {
                    variablesDinamicas = "@Sistema=" + sesionSistema.TituloSistema + " | @Nombre=" + persona.Nombre + "| @Usuario=" + cuentaUsuario.Cuenta + " | @Contrasena=" + cuentaUsuario.Contrasena + "  | @DireccionSitio=" + sesionSistema.UrlSistema;
                    string correos = "arqfranciscoga@live.com; arqfranciscoga@gmail.com";
                    string imagenes = "id=@imagen1|formato=image/jpg|url=~/Promociones/logo.jpg; id=@imagen2|formato=image/jpg|url=~/Promociones/logo2.jpg ; id=@logo|formato=image/png |url=~/Promociones/logo.png;";
                    AdministradorUtilerias au = new AdministradorUtilerias();
                    string anexos = au.ObtenerDirectorioDeUrl("/Documentos/prueba.txt");
                    anexos = anexos + ";" + au.ObtenerDirectorioDeUrl("/Documentos/prueba.docx");
                    RespuestaCorreo = _administradorCorreo.Enviar("FGAGOOGLE_CCD", variablesDinamicas, imagenes, null, null, null, null, correos, "ASUNTO FGA", "MENSAJE xxx", "/Comun/Servicios/Correo/Plantillas/NotificacionMensaje.html", anexos);

                }

            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public CuentaUsuario RealizarTransaccionContrasena(string cuenta, string contrasena)
        {
            CuentaUsuario cuentaUsuario = new CuentaUsuario();
            try
            {
                cuentaUsuario = ObtenerCuentaUsuario(cuenta);
                using (TransactionScope transaccion = new TransactionScope())
                {
                    if (cuentaUsuario != null)
                    {
                        cuentaUsuario.Contrasena = contrasena;
                        ActualizarCuentaUsuario(cuentaUsuario);
                    }
                    _contexto.SaveChanges();
                    transaccion.Complete();
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            return cuentaUsuario;
        }


        public void RealizarTransaccionCerrarSesion(SesionUsuario sesionUsuario, CuentaUsuario cuentaUsuario)
        {
            try
            { 
                using (TransactionScope transaccion = new TransactionScope())
                {
                    if (cuentaUsuario != null)
                    {
                        cuentaUsuario.Sesiones--;
                        SesionSistemaActual.ClaveAplicacion = "CerrarSesion";
                        GuardarSesion(sesionUsuario, cuentaUsuario);
                        sesionUsuario.CuentaUsuario = String.Empty;
                        sesionUsuario.IdUsuario = 0;
                        SesionSistemaActual.Cuenta = String.Empty;
                    }
                    if (sesionUsuario != null)
                    {
                        this.Eliminar<SesionUsuario>(sesionUsuario);
                        _contexto.SaveChanges();
                    }
                    //int numero = 0;
                    //int numero2 = numero / numero;
                    transaccion.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                //writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }
            catch (ApplicationException ex)
            {
                //writer.WriteLine("ApplicationException Message: {0}", ex.Message);
            }
        }

        public void OperacionRecuperarContrasena(string cuenta)
        {
            SesionUsuario sesionUsuario = ObtenerSesionUsuario();
            CuentaUsuario cuentaUsuario = new CuentaUsuario();
            string idAplicacion = "RecuperarContrasena";
            string variablesDinamicas = string.Empty;
            try
            {
                RespuestaServicio RespuestaCorreo = new RespuestaServicio();
                cuentaUsuario = ObtenerCuentaUsuario(cuenta);
                Persona persona = ObtenerPersona(cuentaUsuario);
                if (persona != null)
                {
                    SesionSistema sesionSistema = AdministradorSesion.Obtener();
                    variablesDinamicas = "@Sistema=" + sesionSistema.TituloSistema + " | @Nombre=" + persona.Nombre + "| @Usuario=" + cuentaUsuario.Cuenta + " | @Contrasena=" + cuentaUsuario.Contrasena + "  | @DireccionSitio=" + sesionSistema.UrlSistema;

                    string correos = "arqfranciscoga@live.com; arqfranciscoga@gmail.com";
                    string imagenes = "id=@imagen1|formato=image/jpg|url=~/Promociones/logo.jpg; id=@imagen2|formato=image/jpg|url=~/Promociones/logo2.jpg ; id=@logo|formato=image/png |url=~/Promociones/logo.png;";
                     AdministradorUtilerias au = new AdministradorUtilerias();
                     string anexos = au.ObtenerDirectorioDeUrl("/Documentos/prueba.txt");
                    // anexos = anexos+ ";" + au.ObtenerDirectorioDeUrl("/Documentos/prueba.docx");
                    //   RespuestaCorreo = _administradorCorreo.EnviarPorParametros("FGAGOOGLE_CCD", variablesDinamicas, imagenes, null, null, null, null, correos, "ASUNTO FGA", "MENSAJE xxx", "/Comun/Servicios/Correo/Plantillas/RecuperacionContrasena.html", anexos);
                    RespuestaCorreo = _administradorCorreo.Enviar("FGAGOOGLE_CCD", variablesDinamicas, imagenes, null, null, null, null, correos, "ASUNTO FGA", "MENSAJE xxx", "/Comun/Servicios/Correo/Plantillas/NotificacionMensaje.html", anexos);
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public void CerrarSesion()
        {
            SesionUsuario sesionUsuario = ObtenerSesionUsuario();
            CuentaUsuario cuentaUsuario = ObtenerCuentaUsuario(sesionUsuario.CuentaUsuario);
            RealizarTransaccionCerrarSesion(sesionUsuario, cuentaUsuario);
            ParametrosSeguridadActual = new ParametrosSeguridad();
            this.Contexto.Dispose();
            _contexto = null;
        }

        #endregion
    }
}
