using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MeNet.Nucleo.Comun;
using MeNet.Nucleo.Correo;

namespace Sitio.Comun.Servicios.Correo
{
    /// <summary>
    /// Descripción breve de ServicioCorreo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioCorreo : System.Web.Services.WebService
    {


        // obtiene toda la informacion de configuracion
        [WebMethod]
        public RespuestaServicio EnviarPorConfiguracion(string IdAplicacion, string VariablesDinamicas, string imagenes)
        {
            RespuestaServicio  respuesta = new RespuestaServicio();
            try
            {

                AdministradorCorreo administradorCorreo = new AdministradorCorreo(IdAplicacion);
                administradorCorreo.Correo.VariablesDinamicas = VariablesDinamicas;
                administradorCorreo.Correo.Imagenes = imagenes;
                
                //respuesta = administradorCorreo.Enviar(IdAplicacion, "Comun/Xml/Configuracion.xml");
                respuesta = administradorCorreo.Enviar();
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 1;
                respuesta.Mensaje = ex.ToString();
            }

            return respuesta;
        }

        // obtiene toda la informacion de configuracion, excepto el correo destinario y variables dinamicas
        [WebMethod]
        public RespuestaServicio EnviarPorParametros(string idAplicacion = null, string variablesDinamicas = null, string imagenes = null, string servidor = null, string puerto = null, string usuario = null, string contrasena = null, string correoDestino = null, string asunto = null, string mensaje = null, string plantilla = null, string documentos = null)
        { 
            RespuestaServicio respuesta = new RespuestaServicio();
            try
            {
                AdministradorCorreo administradorCorreo = new AdministradorCorreo();
                respuesta = administradorCorreo.Enviar(idAplicacion, variablesDinamicas, imagenes, servidor, puerto, usuario, contrasena, correoDestino, asunto, mensaje, plantilla, documentos);
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 1;
                respuesta.Mensaje = ex.ToString();
            }
            return respuesta;
        }

    }
}
