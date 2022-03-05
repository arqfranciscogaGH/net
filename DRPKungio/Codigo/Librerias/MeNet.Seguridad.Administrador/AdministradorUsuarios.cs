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

namespace MeNet.Seguridad.Administrador
{
    public class AdministradorUsuarios : AdministradorNegocioGeneral
    {
        private ModeloDRP _contexto;
        public AdministradorUsuarios()
        {
            _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
            
            // se asigna contexto a clase base
            this.Contexto = _contexto;
            
        }
    }

}
