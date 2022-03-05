using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.Negocio;
using MeNet.Nucleo.Modelo;
using DRP.Modelo;
using System.Data.Entity.Core.Objects;

namespace MeNet.Seguridad.Administrador
{
    public  class AdministradorTerminologia : AdministradorNegocioEntidad<AdministradorTerminologia>
    {
        private  ModeloSistema _contexto;
        public AdministradorTerminologia()
        {
            _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
            // se asigna contexto a clase base
            this.Contexto = _contexto;

        }
        public ObjectResult<ObtenerTerminologia_Result> ObtenerTerminologia(string clave, int? idIdioma)
        {
            ObjectResult<ObtenerTerminologia_Result> terminologia = _contexto.ObtenerTerminologia(clave, idIdioma);
            return terminologia;
        }
        public  ObjectResult<ObtenerTerminologias_Result> ObtenerTerminologias(int? idAplicacion, int? idIdioma)
        {
            ObjectResult<ObtenerTerminologias_Result> terminologias = _contexto.ObtenerTerminologias(idAplicacion, idIdioma);
            return terminologias;
        }

    }
}
