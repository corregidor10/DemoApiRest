using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using DemoApiRest.Models;

namespace DemoApiRest.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.


        //TODO ODATA 002. Agregamos un Controller de Tipo Web Api, ODATA. Nos indica unas Instrucciones de configuracion (VER ABAJO)
    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DemoApiRest.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Usuario>("Usuarios");
    builder.EntitySet<Mensaje>("Mensaje"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        */


        // TODO ODATA 004, Observar que los controles de tipo Odata Heredan del ODataController
    public class UsuariosController : ODataController
    {
        private RedContactosDaniEntities db = new RedContactosDaniEntities();

        // GET: odata/Usuarios

//TODO ODATA 005. Los metodos accesibles con Odata tienen que tener el atributo EnableQuery
        [EnableQuery]
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuario;
        }

        // GET: odata/Usuarios(5)
        [EnableQuery]
        public SingleResult<Usuario> GetUsuario([FromODataUri] int key)
        {
            return SingleResult.Create(db.Usuario.Where(usuario => usuario.idUsuario == key));
        }

        // PUT: odata/Usuarios(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Usuario> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuario = db.Usuario.Find(key);
            if (usuario == null)
            {
                return NotFound();
            }

            patch.Put(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(usuario);
        }

        // POST: odata/Usuarios
        public IHttpActionResult Post(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuario.Add(usuario);
            db.SaveChanges();

            return Created(usuario);
        }

        // PATCH: odata/Usuarios(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Usuario> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuario = db.Usuario.Find(key);
            if (usuario == null)
            {
                return NotFound();
            }

            patch.Patch(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(usuario);
        }

        // DELETE: odata/Usuarios(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Usuario usuario = db.Usuario.Find(key);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Usuarios(5)/Mensaje
        [EnableQuery]
        public IQueryable<Mensaje> GetMensaje([FromODataUri] int key)
        {
            return db.Usuario.Where(m => m.idUsuario == key).SelectMany(m => m.Mensaje);
        }

        // GET: odata/Usuarios(5)/Mensaje1
        [EnableQuery]
        public IQueryable<Mensaje> GetMensaje1([FromODataUri] int key)
        {
            return db.Usuario.Where(m => m.idUsuario == key).SelectMany(m => m.Mensaje1);
        }

        // GET: odata/Usuarios(5)/Usuario1
        [EnableQuery]
        public IQueryable<Usuario> GetUsuario1([FromODataUri] int key)
        {
            return db.Usuario.Where(m => m.idUsuario == key).SelectMany(m => m.Usuario1);
        }

        // GET: odata/Usuarios(5)/Usuario2
        [EnableQuery]
        public IQueryable<Usuario> GetUsuario2([FromODataUri] int key)
        {
            return db.Usuario.Where(m => m.idUsuario == key).SelectMany(m => m.Usuario2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int key)
        {
            return db.Usuario.Count(e => e.idUsuario == key) > 0;
        }
    }
}
