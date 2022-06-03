using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitio.Models;
namespace Sitio.Controllers
{
    public class menusController : Controller
    {
        // GET: menu
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

            return Json(new { success = "OK", error = "" });

            //}
            //catch (Exception e)
            //{
            //    return Content("Ocurrioun error: " + e.Message);
            //}
        }
        [HttpPost]
        public JsonResult Guardar(Documento doc)
        {
            int valor = doc.Documento_ID;
            string nombre = doc.Documento_Nombre;
            string tipo = doc.Documento_Tipo;
            nombre.ToUpper();
            tipo.ToUpper();
            return Json(new { success = "OK", error = "" });
        }
    }
}