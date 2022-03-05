using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
// solo para version anterior  entity 4
using System.Data.Objects;
using System.Data.EntityClient;


using MeNet.Nucleo.Sesion;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;


using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

//using System.Data.Entity.Core.Objects;
namespace MeNet.Nucleo.Contexto
{
    public class AdministradorContexto
    {
        private static object item;
        private static DbContext _contexto = null;
        private static Dictionary<string, DbContext> lista = new Dictionary<string, DbContext>();
        private static string _nombreCadenaConexion;
        private static string _cadenaConexion;
        public static Y Iniciar<Y>() where Y : class ,new()
        {
            item = new Y();
            string nombre = item.GetType().Name;
            _contexto = ObtenerContexto(nombre);
            if (_contexto == null)
            {
                _contexto = (DbContext)item;
                _contexto = Agregar(nombre, _contexto);
            }
            item = _contexto;
            return (Y)item;
        }
        public static object Iniciar()
        {
            _nombreCadenaConexion = ConfigurationManager.AppSettings["ModeloPorDefecto"].ToString();
            _contexto = Iniciar(_nombreCadenaConexion);
            return _contexto;
        }
        public static DbContext Iniciar(string nombre)
        {
            _contexto = ObtenerContexto(nombre);
            if (_contexto == null)
            {
                _contexto = Crear(nombre);
            }
            return _contexto;
        }
        public static DbContext ObtenerContexto(string nombre)
        {
            DbContext contexto = null;
            if (lista.ContainsKey(nombre))
                contexto = lista[nombre];
            return contexto;
        }
        public static object ObtenerContextoActual()
        {

            if (_contexto == null)
                _contexto = lista.Last().Value;
            if (_contexto == null)
                _contexto = Crear(AdministradorSesion.Obtener());
            return _contexto;
        }
        public static DbContext ObtenerContextoPorDefecto()
        {
            _nombreCadenaConexion = ConfigurationManager.AppSettings["ModeloPorDefecto"].ToString();
            _contexto = ObtenerContexto(_nombreCadenaConexion);
            if (_contexto == null)
                _contexto = Crear(_nombreCadenaConexion);
            if (_contexto == null)
                _contexto = Crear(AdministradorSesion.Obtener());

            return _contexto;
        }


        public static DbContext Crear(string nombreCadenaConexion)
        {
            _cadenaConexion = ConfigurationManager.ConnectionStrings[nombreCadenaConexion].ConnectionString;
            _contexto =  Crear(nombreCadenaConexion, _cadenaConexion);
            return _contexto;
        }
        public static DbContext Crear(string nombreCadenaConexion,string cadenaConexion)
        {

            _contexto = new DbContext(cadenaConexion);
            Agregar(nombreCadenaConexion, _contexto);
            return _contexto;
        }

        public static DbContext Crear(SesionSistema sesionSistema)
        {
            _contexto = new DbContext(sesionSistema.ObtenerCadenaConexion());
            AdministradorContexto.Agregar(_contexto.GetType().Name, _contexto);
            return _contexto;
        }
        public static object Crear(EntityConnection Conexion)
        {
            _contexto = new DbContext(Conexion.ConnectionString);
            AdministradorContexto.Agregar(_contexto.GetType().Name, _contexto);
            return _contexto;
        }
        public static DbContext Agregar(DbContext contexto)
        {
            _contexto = AdministradorContexto.Agregar(contexto.GetType().Name,contexto);
            return _contexto;
        }
        public static DbContext Agregar(string nombre, DbContext contexto)
        {
            if (!lista.ContainsKey(nombre))
                lista.Add(nombre, contexto);
            _contexto = contexto;
            return _contexto;
        }


        public static EntityConnection CrearConexion(SesionSistema sesionSistema)
        {
            EntityConnection conexion = new EntityConnection(sesionSistema.ObtenerCadenaConexion());
            return conexion;
        }
        public static EntityConnection ObtenerConexion()
        {
            return (EntityConnection)_contexto.Database.Connection;
        }

        public static void Limpiar()
        {
             lista.Clear();
        }
        public static void Cerrar()
        {
             Limpiar();
            String state = _contexto.Database.Connection.State.ToString();
            if (_contexto!=null )
            {
                _contexto.Database.Connection.Close();
                _contexto.Dispose();
            }
 

        }

    }
}
