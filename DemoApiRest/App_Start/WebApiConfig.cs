using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using DemoApiRest.Extensiones;
using DemoApiRest.Models;

namespace DemoApiRest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //TODO MESSAGEHANDLERS 003. Registramos el Manejador de Mensajes que acabamos de crear.
            // Web API configuration and services
            config.MessageHandlers.Add(new LogHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

          // TODO ODATA 003. COPIAMOS LAS 4 lineas de Abajo en el Web Api Config

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Usuario>("Usuarios");
            builder.EntitySet<Mensaje>("Mensaje");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
