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
    public class FTAvanzarActividadController : ApiController
    {
        private Modelo db = new Modelo();

        // GET: api/FTAvanzarActividad
        public IQueryable<FTAvanzarActividad> GetFTAvanzarActividad(String llave)
        {
            return db.FTAvanzarActividad;
        }

        //GET: api/FTAvanzarActividad/5
        [ResponseType(typeof(FTAvanzarActividad))]
        public async Task<IHttpActionResult> GetFTAvanzarActividad(int id, String llave)
        {
            FTAvanzarActividad fTAvanzarActividad = await db.FTAvanzarActividad.FindAsync(id);
            if (fTAvanzarActividad == null)
            {
                return NotFound();
            }

            return Ok(fTAvanzarActividad);
        }

        // PUT: api/FTAvanzarActividad/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFTAvanzarActividad(int id, String llave, FTAvanzarActividad ac)
        {
            if (AdminisradorLLaves.validar(llave))
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != ac.id)
                {
                    return BadRequest();
                }

                db.AvanzarActividad(ac.clave, ac.identificador, ac.idAccion, ac.idTarea, ac.claveEstatus, ac.parametros, ac.variables).ToList();

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FTAvanzarActividadExists(id))
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

        // POST: api/FTAvanzarActividad
        [ResponseType(typeof(FTAvanzarActividad))]
        public async Task<IHttpActionResult> PostFTAvanzarActividad(String llave, FTAvanzarActividad ac)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.AvanzarActividad(ac.clave, ac.identificador, ac.idAccion, ac.idTarea, ac.claveEstatus, ac.parametros, ac.variables).ToList();

                await db.SaveChangesAsync();
            }
            return CreatedAtRoute("DefaultApi", new { id = ac.id }, ac);
        }

        //// DELETE: api/FTAvanzarActividad/5
        //[ResponseType(typeof(FTAvanzarActividad))]
        //public async Task<IHttpActionResult> DeleteFTAvanzarActividad(int id)
        //{
        //    FTAvanzarActividad fTAvanzarActividad = await db.FTAvanzarActividad.FindAsync(id);
        //    if (fTAvanzarActividad == null)
        //    {
        //        return NotFound();
        //    }

        //    db.FTAvanzarActividad.Remove(fTAvanzarActividad);
        //    await db.SaveChangesAsync();

        //    return Ok(fTAvanzarActividad);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FTAvanzarActividadExists(int id)
        {
            return db.FTAvanzarActividad.Count(e => e.id == id) > 0;
        }
    }
}