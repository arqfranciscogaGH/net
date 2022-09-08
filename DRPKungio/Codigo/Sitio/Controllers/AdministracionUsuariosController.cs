

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
    public class AdministracionUsuariosController : ApiController
    {
        //http://localhost:57022/api/AdministracionUsuarios/''/''/prueba
        private Modelo db = new Modelo();
        public IHttpActionResult Get(String id, String filtro, String llave)
        {
            dynamic resultado = null;
            if (id == "''" || id == "0")
                id = "";
            if (filtro == "''" || filtro == "0")
                filtro = "";
            if (AdminisradorLLaves.validar(llave))
            {
                resultado = db.AdministracionUsuarios(id, filtro).ToList();
                return Ok(resultado);
            }
            else
                return NotFound();
        }
    }
}