using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;

using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity;
using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.AdministradorBaseDatos;

namespace MeNet.Nucleo.Negocio
{
    public class AdministradorNegocioGeneral
    {
        private DbContext _contexto = null;
        private AccesoBDContextGeneral abd;
        private object _entidad = null;
        public DbContext Contexto
        {
            get { return _contexto; }
            set { _contexto = value; abd = new AccesoBDContextGeneral(_contexto); }
        }
        public AdministradorNegocioGeneral()
        {
            _contexto = AdministradorContexto.ObtenerContextoPorDefecto();
            abd = new AccesoBDContextGeneral(_contexto);
        }
        public AdministradorNegocioGeneral(DbContext contexto)
        {
            _contexto = contexto;
            abd = new AccesoBDContextGeneral(contexto);
        }

        public E Instanciar<E>() where E : class, new()
        {
            _entidad =(E) new E();
            return (E)_entidad;
        }
        public E Agregar<E>(E elemento) where E : class, new()
        {
           return abd.Agregar<E>(elemento );
        }

        public void Actualizar<E>(E elemento) where E : class, new()
        {
            abd.Actualizar<E>(elemento );
        }

        public void Eliminar<E>(E elemento) where E : class, new()
        {
            abd.Eliminar<E>(elemento );
        }
        public IEnumerable<E> Consultar<E>(Expression<Func<E, bool>> expresion) where E : class, new()
        {
            return abd.Consultar<E>(expresion);
        }

        public E Obtener<E>( Expression<Func<E, bool>> expresion) where E : class, new()
        {

            return abd.Obtener<E>(expresion);
        }

        public List<E> ObtenerLista<E>() where E : class, new()
        {
            return abd.ObtenerLista<E>();
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
