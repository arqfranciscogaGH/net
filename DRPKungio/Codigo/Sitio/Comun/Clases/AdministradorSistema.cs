using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Web.UI;
using System.Configuration;

using System.Reflection;

using MeNet.Nucleo.Sesion;
using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.AdministradorBaseDatos;
using MeNet.Seguridad.Administrador;
using MeNet.Nucleo.Eventos;
using MeNet.Nucleo.Variables;
//using DRP.Modelo;
//using MeNet.Nucleo.Modelo;
using MeNet.Nucleo.ServiciosAplicacion;
namespace Sitio.Comun.Clases
{
    static public class AdministradorSistema
    {

        #region // variables

        private static ControaldorAplicacion _controaldorAplicacion;
        public static ControaldorEventos _controladorEventos ;
        public static AdministradorVariables _administradorVariables ;
        public static AdministradorVariables _administradorVariablesGlobal;
        private static AdministradorSeguridad _administradorSeguridad ;

        #endregion

        #region // Propiedades

        static public SesionSistema SesionSistemaActual
        {
            get { return AdministradorSesion.SesionSistemaActual; }
            set { AdministradorSesion.SesionSistemaActual = value; }
        }
        static public ControaldorAplicacion ControaldorAplicacion
        {
            get
            {
                if (_controaldorAplicacion == null)
                {
                     string Id = "ControaldorAplicacion_" + SesionSistemaActual.LLaveSesion;
                    _controaldorAplicacion = new ControaldorAplicacion();
                    _controaldorAplicacion.AdministradorVariablesGlobal.Agregar(Id, _controaldorAplicacion);
                }
                return _controaldorAplicacion;
            }
            set { _controaldorAplicacion = value; }
        }
        static public ControaldorEventos ControaldorEventosActual
        {
            get
            {
                if (_controladorEventos == null)
                {
                    //_controladorEventos = new ControaldorEventos();
                    _controladorEventos = ControaldorAplicacion.ControaldorEventos;
                }
                return _controladorEventos;
            }
            set { _controladorEventos = value; }
        }


        static public AdministradorVariables AdministradorVariablesSesion
        {
            get
            {
                _administradorVariables = ControaldorAplicacion.AdministradorVariablesSesion;
                return _administradorVariables;
            }
            set { _administradorVariables = value; }
        }

        static public AdministradorVariables AdministradorVariablesGlobal
        {
            get
            {
                if (_administradorVariablesGlobal == null)
                {
                    //_administradorVariablesGlobal = new AdministradorVariables();
                    _administradorVariablesGlobal = ControaldorAplicacion.AdministradorVariablesGlobal;
                }
                return _administradorVariablesGlobal;
            }
            set { _administradorVariablesGlobal = value; }
        }
 

        
        static public AdministradorSeguridad AdministradorSeguridadActual
        {
            get {
                if (_administradorSeguridad == null)
                {
                    _administradorSeguridad =  AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad;
                  }
                return _administradorSeguridad; }
            set { _administradorSeguridad = value; }
        }

        #endregion

        #region // Metodos

        //static public AdministradorSeguridad ObtenerAdministradorSeguridadd()
        //{
        //    string Id = string.Empty; 
        //    SesionSistema _sesionSistema = AdministradorSesion.SesionSistemaActual;
        //    Id="AdministradorSeguridad_" + _sesionSistema.LLaveSesion;
        //    _administradorSeguridad = (AdministradorSeguridad)AdministradorVariablesGlobal.Obtener(Id);
        //    if   (_administradorSeguridad == null )
        //    {
        //        _administradorSeguridad = new AdministradorSeguridad();
        //        AdministradorVariablesGlobal.Agregar(Id, _administradorSeguridad);
        //    }
   
        //    return _administradorSeguridad;
        //}

        #endregion


    }
}