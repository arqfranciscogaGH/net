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
    public class SuscripcionsController : ApiController
    {
        private Modelo db = new Modelo();

        // http://localhost:57022/api/Suscripcions/prueba
        // http://kungio.com/api/Suscripcions/prueba
        public IHttpActionResult GetSuscripcion(String llave)
        {
            if (AdminisradorLLaves.validar(llave))
                return Ok(db.Suscripcion.ToList());
            else
                return NotFound();
        }
        // GET: api/Suscripciones/5
        [ResponseType(typeof(Suscripcion))]
        public async Task<IHttpActionResult> GetSuscripcion(int id, String llave)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                Suscripcion suscripcion = await db.Suscripcion.FindAsync(id);
                if (suscripcion == null)
                {
                    return NotFound();
                }
                return Ok(suscripcion);
            }
            else
                return NotFound();
        }

        // PUT: api/Suscripciones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSuscripcion(int id, String llave, Suscripcion suscripcion)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != suscripcion.IdSuscriptor)
                {
                    return BadRequest();
                }

                db.Entry(suscripcion).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuscripcionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Suscripciones
        [ResponseType(typeof(Suscripcion))]
        public async Task<IHttpActionResult> PostSuscripcion( String llave,Suscripcion suscripcion)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                db.Suscripcion.Add(suscripcion);
                await db.SaveChangesAsync();
            }
            return CreatedAtRoute("DefaultApi", new { id = suscripcion.IdSuscriptor }, suscripcion);
        }

        // DELETE: api/Suscripciones/5
        [ResponseType(typeof(Suscripcion))]
        public async Task<IHttpActionResult> DeleteSuscripcion(int id,String llave)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                Suscripcion suscripcion = await db.Suscripcion.FindAsync(id);
                if (suscripcion == null)
                {
                    return NotFound();
                }

                db.Suscripcion.Remove(suscripcion);
                await db.SaveChangesAsync();

                return Ok(suscripcion);
            }
            else
                return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SuscripcionExists(int id)
        {
            return db.Suscripcion.Count(e => e.IdSuscriptor == id) > 0;
        }
    }
}