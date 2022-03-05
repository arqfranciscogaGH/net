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
        private static AdministradorVariables _administradorVariablesSistema;

        #endregion

        #region // Propiedades
        static public SesionSistema SesionSistemaActual
        {
            get { return AdministradorSesion.SesionSistemaActual; }
            set { AdministradorSesion.SesionSistemaActual = value; }
        }
        static public AdministradorVariables AdministradorVariablesSistema
        {
            get
            {
                if (_administradorVariablesSistema == null)
                {
                    _administradorVariablesSistema = new AdministradorVariables();
                }
                return _administradorVariablesSistema;
            }
            set { _administradorVariablesSistema = value; }
        }


        static public ControaldorAplicacion ControaldorAplicacion
        {
            get
            {

                string Id = "ControaldorAplicacion_" + SesionSistemaActual.LLaveSesion;
                _controaldorAplicacion=(ControaldorAplicacion)AdministradorVariablesSistema.Obtener(Id);
                if (_controaldorAplicacion == null)
                {
                    _controaldorAplicacion = new ControaldorAplicacion();
                    AdministradorVariablesSistema.Agregar(Id, _controaldorAplicacion);
                }
                return _controaldorAplicacion;
            }
            set {
                string Id = "ControaldorAplicacion_" + SesionSistemaActual.LLaveSesion;
                _controaldorAplicacion = (ControaldorAplicacion)AdministradorVariablesSistema.Obtener(Id);
                if (_controaldorAplicacion != null)
                    _controaldorAplicacion = value;
            }
        }


        static public void CerrarSesion()
        {
            ControaldorAplicacion.AdministradorSeguridad.CerrarSesion();
            ControaldorAplicacion.Cerrar();
            string Id = "ControaldorAplicacion_" + SesionSistemaActual.LLaveSesion;
            AdministradorVariablesSistema.Eliminar(Id);
        }

        static public ControaldorEventos ControaldorEventosActual
        {
            get { return ControaldorAplicacion.ControaldorEventos; }
            set { ControaldorAplicacion.ControaldorEventos = value; }
        }
        static public AdministradorSeguridad AdministradorSeguridadActual
        {
            get { return AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad; }
            set { AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad = value; }
        }

        //static public AdministradorVariables AdministradorVariablesSesion
        //{
        //    get  {  return ControaldorAplicacion.AdministradorVariablesSesion; }
        //    set { ControaldorAplicacion.AdministradorVariablesSesion = value; }
        //}

        //static public AdministradorVariables AdministradorVariablesGlobal
        //{
        //    get { return ControaldorAplicacion.AdministradorVariablesGlobal; }
        //    set { ControaldorAplicacion.AdministradorVariablesGlobal = value; }
        //}





        #endregion

        #region // Metodos



        #endregion


    }
}