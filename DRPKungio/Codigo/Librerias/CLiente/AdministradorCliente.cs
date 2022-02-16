using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MeNet.Nucleo.AdministradorBaseDatos;
using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.Negocio;
namespace Entidad.Cliente
{
    public class AdministradorCliente : AdministradorNegocioEntidad<Cliente>
    {
        private ModeloCliente _contexto;

        private List<Cliente> _listaCliente;
        public AdministradorCliente()
        {
            _contexto = AdministradorContexto.Iniciar<ModeloCliente>();
            this.Contexto = _contexto;
        }

    }
}
