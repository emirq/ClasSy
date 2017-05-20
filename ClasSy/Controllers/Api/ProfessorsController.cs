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
    public class ProfessorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Professors
        public IQueryable<Professor> GetUsers()
        {
            return db.Professors;
        }

        // GET: api/Professors/5
        [ResponseType(typeof(Professor))]
        public IHttpActionResult GetProfessor(string id)
        {
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        // PUT: api/Professors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProfessor(string id, Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != professor.Id)
            {
                return BadRequest();
            }

            db.Entry(professor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(id))
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

        // POST: api/Professors
        [ResponseType(typeof(Professor))]
        public IHttpActionResult PostProfessor(Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(professor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProfessorExists(professor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = professor.Id }, professor);
        }

        // DELETE: api/Professors/5
        [ResponseType(typeof(Professor))]
        public IHttpActionResult DeleteProfessor(string id)
        {
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return NotFound();
            }

            db.Users.Remove(professor);
            db.SaveChanges();

            return Ok(professor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfessorExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}