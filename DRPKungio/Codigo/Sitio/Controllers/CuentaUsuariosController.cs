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
    // http://localhost:57022/api/CuentaUsuarios/prueba
    // http://kungio.com/api/CuentaUsuarios/prueba
    public class CuentaUsuariosController : ApiController
    {
        private Modelo db = new Modelo();

        // GET: api/Usuarios
        public async Task<IHttpActionResult> GetUsuario(String llave)
        {
            dynamic resultado = null;
            if (AdminisradorLLaves.validar(llave))
            {
                resultado = db.CuentaUsuario.ToList();
                return Ok(resultado);
            }
  
            else
                return NotFound(); 
        }
        public async Task<IHttpActionResult> GetUsuarios(int id, String filtro, String llave)
        {
            dynamic resultado = null;
            if (AdminisradorLLaves.validar(llave))
            {
                if (filtro == "IdUsuario")
                    resultado = db.CuentaUsuario.Where(s => s.IdUsuario == id).ToList();
                else if (filtro == "IdUsuarioSuperior")
                    resultado = db.CuentaUsuario.Where(s => s.IdUsuarioSuperior == id).ToList();
                else if (filtro == "IdSuscriptor")
                    resultado = db.CuentaUsuario.Where(s => s.IdSuscriptor == id).ToList();
                else
                    resultado = db.CuentaUsuario;
                return Ok(resultado);
            }
            else
                return NotFound();
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(CuentaUsuario))]
        public async Task<IHttpActionResult> GetUsuario(int id, String llave)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                CuentaUsuario usuario = await db.CuentaUsuario.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                return Ok(usuario);
            }
            else
                return NotFound();
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsuario(int id, String llave, CuentaUsuario usuario)
        {
            if (AdminisradorLLaves.validar(llave))
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != usuario.IdUsuario)
                {
                    return BadRequest();
                }

                db.Entry(usuario).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(CuentaUsuario))]
        public async Task<IHttpActionResult> PostUsuario(String llave, CuentaUsuario usuario)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.CuentaUsuario.Add(usuario);
                await db.SaveChangesAsync();
            }
            return CreatedAtRoute("DefaultApi", new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(CuentaUsuario))]
        public async Task<IHttpActionResult> DeleteUsuario(int id, String llave)
        {
            if (AdminisradorLLaves.validar(llave))
            {
                CuentaUsuario usuario = await db.CuentaUsuario.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                db.CuentaUsuario.Remove(usuario);
                await db.SaveChangesAsync();
                return Ok(usuario);
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

        private bool UsuarioExists(int id)
        {
            return db.CuentaUsuario.Count(e => e.IdUsuario == id) > 0;
        }
    }
}
