using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sitio.Comun.Clases;

namespace Sitio.Controllers
{
    public class AutetificacionController : ApiController
    {
        public IHttpActionResult GetIniciar(string id)
        {
            var token = TokenGenerator.GenerateTokenJwt("paco");
            if (token != null && token != "")
                return Ok(token);
            else
                return Unauthorized();
        }
        public IHttpActionResult GetIniciarTres(string id, string llave)
        {
            var token = TokenGenerator.GenerateTokenJwt("paco");
            if (token != null && token != "")
                return Ok(token);
            else
                return Unauthorized();
        }
        public IHttpActionResult GetIniciarDos(string id, string llave, string filtro)
        {
            var token = TokenGenerator.GenerateTokenJwt("paco");
            if (token != null && token != "")
                return Ok(token);
            else
                return Unauthorized();
        }
    }
}
