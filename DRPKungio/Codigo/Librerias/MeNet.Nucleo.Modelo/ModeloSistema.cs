using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.EntityClient;
//using System.Data.Objects;
using DRP.Modelo;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;

namespace  MeNet.Nucleo.Modelo
{

    public class ModeloSistema : ModeloDRP
    {
        #region Constructors

        /// <summary>
        /// Initializes a new ModeloDRP object using the connection string found in the 'ModeloDRP' section of the application configuration file.
        /// </summary>
        public ModeloSistema()
            //: base("name=ModeloDRP")
        {
            //this.ContextOptions.LazyLoadingEnabled = true;
            //this.OnContextCreated();
        }

        /// <summary>
        /// Initialize a new ModeloDRP object.
        ///// </summary>
        public ModeloSistema(string connectionString)
            //: base(connectionString)
        {
            //base.ContextOptions.LazyLoadingEnabled = true;
            //OnContextCreated();
            base.Database.Connection.ConnectionString = connectionString;
        }

        ///// <summary>
        ///// Initialize a new ModeloDRP object.
        ///// </summary>
        public ModeloSistema(EntityConnection connection)
        {
            this.Database.Connection.ConnectionString = connection.ConnectionString;
            //this.ContextOptions.LazyLoadingEnabled = true;
            //base.OnContextCreated();
        }

        public void Iniciar (string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
        }
        #endregion
    }

}
