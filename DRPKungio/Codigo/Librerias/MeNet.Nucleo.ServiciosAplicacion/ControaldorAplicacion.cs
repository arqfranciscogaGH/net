using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MeNet.Nucleo.Sesion;
using MeNet.Seguridad.Administrador;
using MeNet.Nucleo.Eventos;
using MeNet.Nucleo.Variables;
using MeNet.Nucleo.Contexto;
namespace MeNet.Nucleo.ServiciosAplicacion
{
    public class ControaldorAplicacion
    {
        #region variables

        private ControaldorEventos _controladorEventos;
        private AdministradorVariables _administradorVariables;
        private AdministradorVariables _administradorVariablesGlobal;
        private AdministradorSeguridad _administradorSeguridad;

        #endregion

        #region propiedades
        public SesionSistema SesionSistemaActual
        {
            get { return AdministradorSesion.SesionSistemaActual; }
            set { AdministradorSesion.SesionSistemaActual = value; }
        }
        public ControaldorEventos ControaldorEventos
        {
            get
            {
                if (_controladorEventos == null)
                {
                    _controladorEventos = new ControaldorEventos();
                }
                return _controladorEventos;
            }
            set { _controladorEventos = value; }
        }

        public AdministradorVariables AdministradorVariablesGlobal
        {
            get
            {
                if (_administradorVariablesGlobal == null)
                {
                    _administradorVariablesGlobal = new AdministradorVariables();
                }
                return _administradorVariablesGlobal;
            }
            set { _administradorVariablesGlobal = value; }
        }

        public AdministradorVariables AdministradorVariablesSesion
        {
            get
            {
                //_administradorVariables = (AdministradorVariables)AdministradorVariablesGlobal.Obtener(Id);
                if (_administradorVariables == null)
                {
                    string Id = "VariablesSesion_" + SesionSistemaActual.LLaveSesion;
                    _administradorVariables = new AdministradorVariables();
                    AdministradorVariablesGlobal.Agregar(Id, _administradorVariables);
                }
                return _administradorVariables;
            }
            set { _administradorVariables = value; }
        }

        public AdministradorSeguridad AdministradorSeguridad
        {
            get
            {
                if (_administradorSeguridad == null)
                {
                    _administradorSeguridad = new AdministradorSeguridad();
                }
                return _administradorSeguridad;
            }
            set { _administradorSeguridad = value; }
        }


        #endregion

        #region // Metodos

        public  ControaldorAplicacion()
        {

            Limpiar();
        }
        public void Limpiar()
        {
            _controladorEventos = null;
            _administradorVariables = null;
            _administradorVariablesGlobal = null;
            _administradorSeguridad = null;

        }
        public void Cerrar()
        {
            Limpiar();
            AdministradorContexto.Cerrar();
        }
        #endregion
    }
}

