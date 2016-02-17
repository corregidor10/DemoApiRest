using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoApiRest.Extensiones;

namespace DemoApiRest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //TODO 002. Registramos el Manejador de Mensajes que acabamos de crear.
            // Web API configuration and services
            config.MessageHandlers.Add(new LogHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
