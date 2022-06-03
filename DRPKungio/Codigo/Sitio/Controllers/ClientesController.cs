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
    public class ClientesController : ApiController
    {
        private Modelo db = new Modelo();
        // http://localhost:57022/api/Clientes/prueba
        // http://kungio.com/api/Clientes/prueba

        // GET: api/Clientes
        public IQueryable<Cliente> GetCliente(String llave)
        {
            if (AdminisradorLLaves.validar(llave))
                return db.Cliente;
            else
                return null;
        }
        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> GetCliente(int id, String llave)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                Cliente cliente = await db.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return Ok(cliente);
            }
            else
                return NotFound();
        }
        public async Task<IHttpActionResult> GetCliente(int id, String filtro, String llave)
        {
            dynamic resultado = null;
            if (AdminisradorLLaves.validar(llave))
            {
                if (filtro == "id")
                    resultado = db.Cliente.Where(s => s.id == id).ToList();
                else if (filtro == "idSocio")
                    resultado = db.Cliente.Where(s => s.idSocio == id).ToList();
                else if (filtro == "IdSuscriptor")
                    resultado = db.Cliente.Where(s => s.idSuscriptor == id).ToList();
                else
                    resultado = db.Cliente;
                return Ok(resultado);
            }
            else
                return NotFound();
        }


        // PUT: api/Personas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCliente(int id, String llave, Cliente cliente)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != cliente.id)
                {
                    return BadRequest();
                }

                db.Entry(cliente).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(id))
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

        // POST: api/Personas
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> PostCliente(String llave, Cliente cliente)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Cliente.Add(cliente);
                await db.SaveChangesAsync();
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.id }, cliente);
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> DeleteCliente(int id, String llave)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                Cliente cliente = await db.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                db.Cliente.Remove(cliente);
                return Ok(cliente);
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

        private bool ClienteExists(int id)
        {
            return db.Cliente.Count(e => e.id == id) > 0;
        }
    }
}
