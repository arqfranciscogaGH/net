using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;


using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.AdministradorBaseDatos;

namespace MeNet.Nucleo.Negocio
{
    public class AdministradorNegocioEntidad<T> : IAdministradorNegocioEntidad<T> where T : class, new()
    {
        private DbContext _contexto = null;
        private AccesoBDContext<T> abd;
        private T _entidad = null;
        public DbContext Contexto
        {
            get { return _contexto; }
            set { _contexto = value; abd = new AccesoBDContext<T>(_contexto); }
        }
        public  AdministradorNegocioEntidad()
        {
            _contexto = AdministradorContexto.ObtenerContextoPorDefecto();
            abd = new AccesoBDContext<T>(_contexto);
        }
        public AdministradorNegocioEntidad(DbContext contexto)
        {
            _contexto = contexto;
            abd = new AccesoBDContext<T>(contexto);
            
        }

        public T Instanciar()
        {
            _entidad = new T();
            return _entidad;
        }
        public T Agregar(T elemento)
        {
            return abd.Agregar(elemento);
        }

        public object Agregar(object elemento)
        {
            return abd.Agregar((T)elemento);
        }
        public void Actualizar(T elemento)
        {
            abd.Actualizar(elemento);
        }
        public void Actualizar(object elemento)
        {
            abd.Actualizar((T)elemento);
        }
        public void Eliminar(T elemento)
        {
            abd.Eliminar(elemento);
        }
        public void Eliminar(object elemento)
        {
            abd.Eliminar((T)elemento);
        }
        public IEnumerable<T> Consultar(Expression<Func<T, bool>> expresion)
        {
            return abd.Consultar(expresion);
        }

        public T Obtener(Expression<Func<T, bool>> expresion)
        {
            return abd.Obtener(expresion);
        }

        public List<T> ObtenerLista()
        {
            return abd.ObtenerLista();
        }
        public System.Data.DataSet ExecutarSqlDataset(string sql)
        {
            return abd.ExecutarSqlDataset(sql);
        }
        public void ExecutarSql(string sql)
        {
            abd.ExecutarSql(sql);
        }
        public void GuardarCambios()
        {
           abd.GuardarCambios();
        }

        public void IniciarTransaccion()
        {
            abd.IniciarTransaccion();
        }
        public void TerminarTransaccion()
        {
            abd.TerminarTransaccion();
        }
        public void DeshacerTransaccion()
        {
            abd.DeshacerTransaccion();
        }
        public IDbConnection ObtenerConexion()
        {
            return abd.ObtenerConexion();
        }
        public void Dispose()
        {
            _contexto.Dispose();
            abd.Dispose();
        }
    }
}