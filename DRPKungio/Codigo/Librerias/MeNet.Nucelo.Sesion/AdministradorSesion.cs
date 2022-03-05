using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;

namespace MeNet.Nucleo.Sesion
{
    public class AdministradorSesion
    {
        #region variables

        private static SesionSistema _sesionSistema;


        #endregion

        #region propiedades

        public static  SesionSistema SesionSistemaActual
        {
            get
            { 
                if   ( _sesionSistema==null)
                {
                    _sesionSistema = AdministradorSesion.Obtener();
                     //if  ( _sesionSistema.TipoSistema=="WEB" )
                     //    _sesionSistema = (SesionSistema)HttpContext.Current.Session["SesionSistema"];
                }
                return _sesionSistema;
            }
            set 
            {
                _sesionSistema =(SesionSistema)value;
                //if (_sesionSistema.TipoSistema == "WEB")
                //     HttpContext.Current.Session["SesionSistema"] = _sesionSistema;
            } 
        }
        #endregion

        #region metodos

        public static SesionSistema Obtener()
        {
            SesionSistema sesion = new SesionSistema();
            sesion = AdministradorSesion.ObtenerTipoAplicaion(sesion);
            sesion = AdministradorSesion.ObtenerInformacionSession(sesion);

            sesion = AdministradorSesion.ObtenerConfiguracion(sesion);
            sesion = AdministradorSesion.ObtenerInformacionAplicacion(sesion);
            _sesionSistema = sesion;
            return sesion;
        }
        public static SesionSistema Obtener(string cuenta, string contarsena)
        {
            _sesionSistema = Obtener();
            _sesionSistema.Cuenta = cuenta;
            _sesionSistema.Contrasena = contarsena;
            return _sesionSistema;
        }

        public static  SesionSistema ObtenerTipoAplicaion(SesionSistema sesion)
        {
            try
            {
                sesion.TipoSistema = "Escritorio";
                if (ConfigurationManager.AppSettings["TipoSistema"] != null)
                    sesion.TipoSistema = ConfigurationManager.AppSettings["TipoSistema"].ToString();
            }
            catch
            {

            }
            return sesion;
        }
        public static  SesionSistema ObtenerInformacionSession(SesionSistema sesion)
        {
            if (sesion.TipoSistema == "Escritorio")
            {
                if (_sesionSistema!=null )
                {
                    Guid Guia = Guid.NewGuid();
                    sesion.LLaveSesion = Guia.ToString();
                    sesion.Ruta = Environment.CurrentDirectory;
                    _sesionSistema = sesion;
                }
                else
                    sesion=_sesionSistema ;
            }
            else if (sesion.TipoSistema == "WEB")
            {
                sesion.LLaveSesion = System.Web.HttpContext.Current.Session.SessionID;
                HttpRequest request = System.Web.HttpContext.Current.Request;
                sesion.Ruta = request.PhysicalApplicationPath;

                System.Web.HttpBrowserCapabilities browser = request.Browser;

                if (browser.MobileDeviceModel == "Unknown")
                    sesion.Dispositivo = request.UserAgent.Substring(20, 20);
                else
                    sesion.Dispositivo = browser.MobileDeviceModel;

                sesion.AgenteSesion = request.UserAgent;
                sesion.Explorador = browser.Browser;
                sesion.VersionExplorador = "Version:" + browser.Version + "MinorVersion:" + browser.MinorVersion + "MajorVersion:" + browser.MajorVersion;

                //sesion.EquipoUsuario = request.UserHostName; // UserHostAddress
                string s = "Browser Capabilities\n"
                + "Type = " + browser.Type + "\n"
                + "Name = " + browser.Browser + "\n"
                + "Version = " + browser.Version + "\n"
                + "Major Version = " + browser.MajorVersion + "\n"
                + "Minor Version = " + browser.MinorVersion + "\n"
                + "Platform = " + browser.Platform + "\n"
                + "Is Beta = " + browser.Beta + "\n"
                + "Is Crawler = " + browser.Crawler + "\n"
                + "Is AOL = " + browser.AOL + "\n"
                + "Is Win16 = " + browser.Win16 + "\n"
                + "Is Win32 = " + browser.Win32 + "\n"
                + "Supports Frames = " + browser.Frames + "\n"
                + "Supports Tables = " + browser.Tables + "\n"
                + "Supports Cookies = " + browser.Cookies + "\n"
                + "Supports VBScript = " + browser.VBScript + "\n"
                + "Supports JavaScript = " +
                browser.EcmaScriptVersion.ToString() + "\n"
                + "Supports Java Applets = " + browser.JavaApplets + "\n"
                + "Supports ActiveX Controls = " + browser.ActiveXControls
                + "\n";
                s = request.UserAgent;
                s = request.UserHostAddress;
                s = request.UserHostName;
                s = request.ServerVariables["HTTP_USER_AGENT"];
            }
            return sesion;
        }
        public static SesionSistema ObtenerInformacionAplicacion(SesionSistema sesion)
        {
            if (sesion.TipoSistema == "Escritorio")
            {

            }
            sesion.SistemaOperativo = System.Environment.OSVersion.Platform.ToString();
            sesion.Equipo = System.Environment.MachineName;
            sesion.CuentaDominio = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            sesion.LineaComando = Environment.CommandLine;
            sesion.ParametrosEntrada = Environment.GetCommandLineArgs();
            sesion.FechaIngreso = DateTime.Now;
            return sesion;
        }
        public static SesionSistema ObtenerConfiguracion(SesionSistema sesion)
        {
            if (ConfigurationManager.AppSettings["ModeloPorDefecto"] != null)
                sesion.NombreCadenaConexion = ConfigurationManager.AppSettings["ModeloPorDefecto"].ToString();
            sesion.CadenaConexion  = ConfigurationManager.ConnectionStrings[sesion.NombreCadenaConexion].ConnectionString;
            if (ConfigurationManager.AppSettings["TipoCuenta"] != null)
                sesion.TipoCuenta = ConfigurationManager.AppSettings["TipoCuenta"].ToString();
            if (ConfigurationManager.AppSettings["TemaPorDefecto"] != null)
                sesion.Tema = ConfigurationManager.AppSettings["TemaPorDefecto"].ToString();
            if (ConfigurationManager.AppSettings["GuardarSesion"] != null)
                sesion.GuardarSesion = ConfigurationManager.AppSettings["GuardarSesion"].ToString();
            if (ConfigurationManager.AppSettings["Cultura"] != null)
                sesion.Cultura = ConfigurationManager.AppSettings["Cultura"].ToString();
            if (ConfigurationManager.AppSettings["IdIdioma"] != null)
            {
                sesion.IdIdioma = Int16.Parse(ConfigurationManager.AppSettings["IdIdioma"].ToString());
                sesion.IdIdioma = sesion.IdIdioma;
            }
    

            sesion.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
            sesion.BaseDatos = ConfigurationManager.AppSettings["BaseDatos"].ToString();
            sesion.Proveedor = ConfigurationManager.AppSettings["Proveedor"].ToString();
            sesion.MetaData = ConfigurationManager.AppSettings["MetaData"].ToString();

            sesion.IdSistema = ConfigurationManager.AppSettings["IdAplicacion"].ToString();
            sesion.TituloSistema = ConfigurationManager.AppSettings["TituloSistema"].ToString();
            sesion.UrlSistema = ConfigurationManager.AppSettings["UrlSistema"].ToString();

            sesion.PermisoAgregar = ConfigurationManager.AppSettings["PermisoAgregar"].ToString();
            sesion.PermisoModificar = ConfigurationManager.AppSettings["PermisoModificar"].ToString();
            sesion.PermisoEliminar = ConfigurationManager.AppSettings["PermisoEliminar"].ToString();
            sesion.PermisoConsultar = ConfigurationManager.AppSettings["PermisoConsultar"].ToString();
            sesion.Ambiente = ConfigurationManager.AppSettings["Ambiente"].ToString();
            sesion.PaginaInicio = ConfigurationManager.AppSettings["PaginaInicio"].ToString();
            sesion.PaginaAcceso = ConfigurationManager.AppSettings["PaginaAcceso"].ToString();
            sesion.PaginaMenu = ConfigurationManager.AppSettings["PaginaMenu"].ToString();
    
            return sesion;
        }
        #endregion
    }
}

