using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DemoApiRest.Controllers
{
    public class SaludosController : ApiController
    {

        [ResponseType(typeof (string))]
        public IHttpActionResult Get(String nombre)
        {
            return Ok($"Hola {nombre} me debes pasta");
            

        }
    }
}
