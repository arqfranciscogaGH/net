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
    public class ConsultarSociosController : ApiController
    {
        //http://localhost:50954/api/ConsultarSocios/1/''/prueba
        private Modelo db = new Modelo();
        public IHttpActionResult Get(int id, String filtro, String llave)
        {
            dynamic resultado = null;
            if (filtro == "''" || filtro == "0")
                filtro = "";
            if (AdminisradorLLaves.validar(llave))
            {
                resultado = db.ConsultarSocios(id, filtro).ToList();
                return Ok(resultado);
            }
            else
                return NotFound();
        }
    }
}
