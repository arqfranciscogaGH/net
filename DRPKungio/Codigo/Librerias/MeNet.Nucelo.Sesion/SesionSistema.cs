using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

using System.Data.EntityClient;
namespace MeNet.Nucleo.Sesion
{
    public class SesionSistema
    {
        public string IdSistema = string.Empty;
        public string TituloSistema = string.Empty;
        public string UrlSistema = string.Empty;
        public string Servidor = string.Empty;
        public string BaseDatos = string.Empty;
        public string MetaData = string.Empty;
        public string Proveedor = string.Empty;
        public string InfoSeguridad = string.Empty;

        public string LLaveSesion = string.Empty;
        public string TipoCuenta = string.Empty;
        public string TipoSistema = string.Empty;
        public string CuentaDominio = string.Empty;
        public string Dominio = string.Empty;

        public string NombreCadenaConexion = string.Empty;
        public string CadenaConexion = string.Empty;

        public string Ruta = string.Empty;
        public string Tema = string.Empty;
        public string Equipo = string.Empty;
        public string AgenteSesion = string.Empty;
        public string Explorador = string.Empty;
        public string VersionExplorador = string.Empty;
        public string Dispositivo = string.Empty;
        public string SistemaOperativo = string.Empty;
        public string LineaComando = string.Empty;
        public string[] ParametrosEntrada;
        public string Cuenta = string.Empty;
        public string Contrasena = string.Empty;
        public DateTime FechaIngreso = DateTime.Now;
        public string Cultura = string.Empty;
        public int? IdIdioma = 1;
        
        public Int16 IdPerfil=0;
        public Int16 IdArea = 0;
        public Int16 IdGrupo = 0;
        public int NumeroSesionesRealizadas = 0;
        public int NumeroSesionesPermitidas = 3;
        public bool Bloqueado = false;
        public bool Activo = true;
        public bool Autentificacion;
        public string GuardarSesion = string.Empty;
        public int? IdSuscriptor =1;
        public string ClaveAplicacion = string.Empty;
        public string PermisoAgregar = string.Empty;
        public string PermisoModificar = string.Empty;
        public string PermisoEliminar = string.Empty;
        public string PermisoConsultar = string.Empty;
        public string Ambiente = string.Empty;
        public string PaginaInicio = string.Empty;
        public string PaginaAcceso = string.Empty;
        public string PaginaMenu = string.Empty;

      //public SesionSistema Iniciar(string llaveSesion)
        //{
        //    SesionSistema sesion = new SesionSistema();
        //    sesion = sesion.Iniciar();
        //    sesion.LLaveSesion = llaveSesion;
        //    return sesion;
        //}


        public string ObtenerCadenaConexion()
        {
            EntityConnectionStringBuilder _EntCadenaConexion = new EntityConnectionStringBuilder();
            _EntCadenaConexion.Provider = Proveedor;
            _EntCadenaConexion.Metadata = MetaData;
            _EntCadenaConexion.ProviderConnectionString = "Data Source=" + Servidor + " ;Initial Catalog=" + BaseDatos + ";";

            if (Cuenta != string.Empty && TipoCuenta == "CuentaBaseDatos")
            {
                _EntCadenaConexion.ProviderConnectionString = _EntCadenaConexion.ProviderConnectionString + " user id= " + Cuenta + ";   password= " + Contrasena + ";";
                if (InfoSeguridad != string.Empty || InfoSeguridad != null)
                {
                    InfoSeguridad = " Integrated Security = false;";
                }
                _EntCadenaConexion.ProviderConnectionString = _EntCadenaConexion.ProviderConnectionString + " " + InfoSeguridad;
            }

            else if (Cuenta != string.Empty && TipoCuenta == "CuentaDominio")
            {
                if (InfoSeguridad != string.Empty || InfoSeguridad != null)
                {
                    InfoSeguridad = " Integrated Security = true;";
                }
                _EntCadenaConexion.ProviderConnectionString = _EntCadenaConexion.ProviderConnectionString + " " + InfoSeguridad;
            }
            else // CuentaSistema
            {
                _EntCadenaConexion.ConnectionString = CadenaConexion;
            }
            CadenaConexion = _EntCadenaConexion.ConnectionString;
            return _EntCadenaConexion.ConnectionString;
        }
    }
}
