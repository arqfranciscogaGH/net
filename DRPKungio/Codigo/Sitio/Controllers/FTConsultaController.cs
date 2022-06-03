using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Sitio.Models;
using Sitio.Comun.Clases;
namespace Sitio.Controllers
{
    public class FTConsultaController : ApiController
    {
        private Modelo db = new Modelo();

        // http://localhost:57022/api/FTConsulta/LINEAIV/''/1/ConsultarMisPendientes/prueba
        // http://kungio.com/api/FTConsulta/LINEAIV/''/1/ConsultarMisPendientes/prueba


        // http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerTramite/prueba
        // http://kungio.com/api/FTConsulta/LINEAIV/''/1/VerTramite/prueba

        //http://localhost:57022/api/FTConsulta/''/30988020381/1/Seguimiento/prueba
        //http://kungio.com/api/FTConsulta/''/30988020381/1/Seguimiento/prueba

        // http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerEstadisticasFlujoPorVista/prueba
        // http://kungio.com/api/FTConsulta/LINEAIV/''/1/VerEstadisticasFlujoPorVista/prueba

        // http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerEstadisticasFlujoPorVistaDetallePorFiltro/prueba
        // http://kungio.com/api/FTConsulta/LINEAIV/''/1/VerEstadisticasFlujoPorVistaDetallePorFiltro/prueba

        // http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerProductividadVencidasdDetalle/prueba

        // http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerProductividadVencidas/prueba
        // http://kungio.com/api/FTConsulta/LINEAIV/''/1/VerProductividadVencidas/prueba

        // http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerProductividadVencidasdDetalle/prueba
        // http://kungio.com/api/FTConsulta/LINEAIV/''/1/VerProductividadVencidasdDetalle/prueba

        // http://localhost:57022/api/FTConsulta/LINEAIV/19/1/prueba/VerlujoTrabajoPorTareaDetalle/prueba
        // http://kungio.com/api/FTConsulta/LINEAIV/19/1/prueba/VerlujoTrabajoPorTareaDetalle/prueba

        //http://localhost:57022/api/FTConsulta/LINEAIV/004/1/ConsultarHistorial/prueba

        //http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerEstadisticasFlujoTrabajoPorTarea/prueba
        //http://localhost:57022/api/FTConsulta/LINEAIV/''/1/VerEstadisticasFlujoTrabajoPorEstatus/prueba



        //[ResponseType(typeof(ConsultarMisPendientes_Result))]
        public IHttpActionResult Get(String clave, String variables, int? idIdioma, String consulta, String llave)
        {
            dynamic resultado=null;
        
            if (AdminisradorLLaves.validar(llave))
            {
                //   cuando  un parametro url es '' se debe mandar vacio, es decir parametro = "";
                //   se envia  el parametro clave  ''  para ver  todos  los flujos
                if (clave == "''" || clave == "0")
                    clave = "";
                if (variables == "''" || variables == "0")
                    variables = "";
                if (idIdioma == null || idIdioma == 0)
                    idIdioma = 1;
                if (consulta == "ConsultarMisPendientes")
                {
                    //variables = variables != String.Empty ? "IdPerfil:" + variables : String.Empty;
                    resultado = db.ConsultarMisPendientes(clave, variables, idIdioma).ToList();
                }
                else if (consulta == "Seguimiento")
                    resultado = db.Seguimiento(clave, variables, idIdioma).ToList();
                else if (consulta == "VerTramite")
                    resultado = db.VerTramite(clave, variables, idIdioma).ToList();
                else if (consulta == "VerProductividadVencidas")
                    resultado = db.VerProductividadVencidas(clave, variables, idIdioma).ToList();
                else if (consulta == "VerProductividadVencidasdDetalle")
                    resultado = db.VerProductividadVencidasdDetalle(clave, variables, idIdioma).ToList();
                else if (consulta == "VerEstadisticasFlujoPorVista")
                    resultado = db.VerEstadisticasFlujoPorVista(clave, variables, idIdioma).ToList();
                else if (consulta == "VerEstadisticasFlujoPorVistaDetallePorFiltro")
                    resultado = db.VerEstadisticasFlujoPorVistaDetallePorFiltro(clave, variables, idIdioma).ToList();
                else if (consulta == "VerlujoTrabajoPorTareaDetalle")
                    resultado = db.VerlujoTrabajoPorTareaDetalle(clave, variables, idIdioma).ToList();

                //else if (consulta == "ConsultarHistorial")
                //    resultado = db.ConsultarHistorial(clave, variables, idIdioma).ToList();
                //else if (consulta == "VerEstadisticasFlujoTrabajoPorTarea")
                //    resultado = db.VerEstadisticasFlujoTrabajoPorTarea(clave, variables, idIdioma).ToList();

                //  revisar las sigiuientes
                //else if (consulta == "VerEstadisticasFlujoTrabajoPorEstatus")
                //    resultado = db.VerEstadisticasFlujoTrabajoPorEstatus(clave, idIdioma).ToList();
                //else if (consulta == "VerEstadisticasTrabajoPorEstatusHistorial")
                //    resultado = db.VerEstadisticasTrabajoPorEstatusHistorial(clave, idIdioma).ToList();
                //else if (consulta == "VerProductividadPorTarea")
                //    resultado = db.VerProductividadPorTarea(clave, variables, idIdioma).ToList();
                //else if (consulta == "VerEstadisticasFlujoPorVariable")
                //    resultado = db.VerEstadisticasFlujoPorVariable(clave, variables, idIdioma).ToList();
                return Ok(resultado);
            }
            else
                return NotFound();
        }



        // POST: api/FTConsultarMisPendientes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FTConsultarMisPendientes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FTConsultarMisPendientes/5
        public void Delete(int id)
        {
        }
    }
}
