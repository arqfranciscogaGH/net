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
    public class FTVerEstatusTareaController : ApiController
    {
        private Modelo db = new Modelo();

        // http://localhost:57022/api/FTVerEstatusTarea/1/prueba
        public async Task<IHttpActionResult> GetLista(String llave)
        {
            dynamic resultado = null;
            if (AdminisradorLLaves.validar(llave))
            {
                resultado = db.VerEstatusTarea(0).ToList();
                return Ok(resultado);
            }
            else
                return null;
        }
        public async Task<IHttpActionResult> Get(int id, String llave)
        {
            dynamic resultado = null;
            if (AdminisradorLLaves.validar(llave))
            {
                resultado = db.VerEstatusTarea(id).ToList();
                if (resultado == null)
                {
                    return NotFound();
                }
                return Ok(resultado);
            }
            else
                return NotFound();
        }
    }
}
