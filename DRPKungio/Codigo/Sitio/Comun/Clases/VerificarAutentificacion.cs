using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitio.Controllers;
using Sitio.Models;

namespace Sitio.Comun.Clases
{
    public class VerificarAutentificacion:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuarioAutentificado = (Cuenta)HttpContext.Current.Session["usuarioAutentificado"];
            if(usuarioAutentificado==null)
            {
                if(filterContext.Controller is accederController ==false)
                {
                    filterContext.HttpContext.Response.Redirect("~/acceder/index");
                }
            }
            else
            {
                if (filterContext.Controller is accederController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/menus/index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}