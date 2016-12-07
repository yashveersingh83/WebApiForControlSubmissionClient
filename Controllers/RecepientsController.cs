using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:5012", headers: "*", methods: "*")]
    public class RecepientsController : ApiController
    {
        private ControlSubmissionDbContext db = new ControlSubmissionDbContext();

        // GET: api/Recepients
        public IQueryable<Recepient> GetRecepients()
        {
            return db.Recepients;
        }

        // GET: api/Recepients/5
        [ResponseType(typeof(Recepient))]
        public IHttpActionResult GetRecepient(int id)
        {
            Recepient recepient = db.Recepients.Find(id);
            if (recepient == null)
            {
                return NotFound();
            }

            return Ok(recepient);
        }

        // PUT: api/Recepients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecepient(int id, Recepient recepient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recepient.Id)
            {
                return BadRequest();
            }

            db.Entry(recepient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecepientExists(id))
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

        // POST: api/Recepients
        [ResponseType(typeof(Recepient))]
        public IHttpActionResult PostRecepient([FromBody]Recepient recepient)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Recepients.Add(recepient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recepient.Id }, recepient);
        }

        // DELETE: api/Recepients/5
        [ResponseType(typeof(Recepient))]
        public IHttpActionResult DeleteRecepient(int id)
        {
            Recepient recepient = db.Recepients.Find(id);
            if (recepient == null)
            {
                return NotFound();
            }

            db.Recepients.Remove(recepient);
            db.SaveChanges();

            return Ok(recepient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecepientExists(int id)
        {
            return db.Recepients.Count(e => e.Id == id) > 0;
        }
    }
}