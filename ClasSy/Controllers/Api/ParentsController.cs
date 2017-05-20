using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClasSy.Models;

namespace ClasSy.Controllers.Api
{
    public class ParentsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Parents
        public IQueryable<Parent> GetUsers()
        {
            return db.Parents;
        }

        // GET: api/Parents/5
        [ResponseType(typeof(Parent))]
        public IHttpActionResult GetParent(string id)
        {
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }

        // PUT: api/Parents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParent(string id, Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parent.Id)
            {
                return BadRequest();
            }

            db.Entry(parent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Parents
        [ResponseType(typeof(Parent))]
        public IHttpActionResult PostParent(Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(parent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ParentExists(parent.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parent.Id }, parent);
        }

        // DELETE: api/Parents/5
        [ResponseType(typeof(Parent))]
        public IHttpActionResult DeleteParent(string id)
        {
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return NotFound();
            }

            db.Users.Remove(parent);
            db.SaveChanges();

            return Ok(parent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParentExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}