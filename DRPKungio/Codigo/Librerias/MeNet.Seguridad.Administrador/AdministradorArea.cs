using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;


using System.Data.Common;

using System.Data;

using System.Data.SqlClient;

using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.Negocio;
using MeNet.Nucleo.Modelo;
using DRP.Modelo;


namespace MeNet.Seguridad.Administrador
{
    public class AdministradorArea:  AdministradorNegocioEntidad<Area>
    {
        private ModeloSistema _contexto;
        public AdministradorArea()
        {
            _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
            // se asigna contexto a clase base
            this.Contexto = _contexto;
   
        }
        //public Area Obtener(Area entidad)
        //{
        //    return this.Obtener(s => s.IdArea == entidad.IdArea);
        //}
        //public List<Area> ObtenerLista(Area entidad)
        //{
        //    return this.Consultar(s => s.Activo == true).ToList();
        //}

    }

}
