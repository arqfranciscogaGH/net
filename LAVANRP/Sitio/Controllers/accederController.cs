using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitio.Models;

namespace Sitio.Controllers
{
    public class accederController : Controller
    {
        // GET: acceder
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Ingresar(Cuenta cuenta)
        {
            //try
            //{
            //    /*return Content("Inicio sesion ");*/
            if (cuenta.Usuario == "fga")
                Session["usuarioAutentificado"] = cuenta;
            return Json(new { success = "OK", error = "" });

            //}
            //catch (Exception e)
            //{
            //    return Content("Ocurrioun error: " + e.Message);
            //}
        }
        public ActionResult Enter(string usuario,string contrasena)
        {
            try
            {
               
                if (usuario == "fga")
                {
                    Cuenta cuenta = new Cuenta();
                    cuenta.Usuario = usuario;
                    cuenta.Contrasena = contrasena;
                    Session["usuarioAutentificado"] = cuenta;
                }
                   
                return Content("1");

            }
            catch (Exception e)
            {
                return Content("Ocurrioun error: " + e.Message);
            }
        }
    }
}