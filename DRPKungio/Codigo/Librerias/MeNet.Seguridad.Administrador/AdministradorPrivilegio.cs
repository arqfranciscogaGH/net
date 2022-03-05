using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using System.Data.Common;
using System.Data.SqlClient;

using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.Negocio;
using MeNet.Nucleo.Modelo;
using DRP.Modelo;
using System.Data.Entity.Core.Objects;

namespace MeNet.Seguridad.Administrador
{
    public class AdministradorPrivilegio : AdministradorNegocioEntidad<Privilegio>
    {
        private ModeloSistema _contexto;
        public AdministradorPrivilegio()
        {
            _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
            // se asigna contexto a clase base
            this.Contexto = _contexto;

        }
        public ObjectResult<ObtenerPrivilegiosPorTipo_Result> ObtenerPrivilegiosPorTipo(string TipoElemento, int? IdElemento)
        {
            ObjectResult<ObtenerPrivilegiosPorTipo_Result> privilegios = _contexto.ObtenerPrivilegiosPorTipo(TipoElemento, IdElemento);
            return privilegios;
        }
        public ObjectResult<ObtenerPrivilegios_Result> ObtenerPrivilegios(int? IdUsuario, int? IdPerfil, int? IdGrupo)
        {
            ObjectResult<ObtenerPrivilegios_Result> privilegios = _contexto.ObtenerPrivilegios(IdUsuario, IdPerfil, IdGrupo);
            return privilegios;
        }

    }
}
