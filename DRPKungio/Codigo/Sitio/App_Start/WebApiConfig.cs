using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Headers;


namespace Sitio
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.MapHttpAttributeRoutes();

            // Otras  Rutas de API web
  
            config.Routes.MapHttpRoute(
                name: "DefaultApillave",
                routeTemplate: "api/{controller}/{llave}",
                defaults: new { llave = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );
            config.Routes.MapHttpRoute(
                name: "DefaultApiidllave",
                routeTemplate: "api/{controller}/{id}/{llave}",
                defaults: new { id = RouteParameter.Optional, llave = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                 name: "DefaultApiidFiltroLlave",
                 routeTemplate: "api/{controller}/{id}/{filtro}/{llave}",
                 defaults: new { id = RouteParameter.Optional, filtro = RouteParameter.Optional, llave = RouteParameter.Optional }
             );
            config.Routes.MapHttpRoute(
                name: "FlujoTrabajoSeguimiento",
                routeTemplate: "api/{controller}/{clave}/{identificador}/{idIdioma}/{llave}",
                defaults: new { clave = RouteParameter.Optional, identificador = RouteParameter.Optional, idIdioma = RouteParameter.Optional, llave = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApiFTConsulta",
                routeTemplate: "api/{controller}/{clave}/{variables}/{idIdioma}/{consulta}/{llave}",
                defaults: new { clave = RouteParameter.Optional, variables = RouteParameter.Optional, idIdioma = RouteParameter.Optional, consulta = RouteParameter.Optional, llave = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
