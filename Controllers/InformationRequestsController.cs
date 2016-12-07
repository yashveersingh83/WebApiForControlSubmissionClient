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
    public class InformationRequestsController : ApiController
    {
        private ControlSubmissionDbContext db = new ControlSubmissionDbContext();

        // GET: api/InformationRequests
        [ResponseType(typeof(List< InformationRequest>))]
        public IHttpActionResult GetInformationRequest()
        {
           var t = User.Identity;
          return  Ok(db.InformationRequest.Include("MileStone").Include("Recepient").ToList());
        }

        // GET: api/InformationRequests/5
        [ResponseType(typeof(InformationRequest))]
        public IHttpActionResult GetInformationRequest(int id)
        {
            InformationRequest informationRequest = db.InformationRequest.Find(id);
            if (informationRequest == null)
            {
                return NotFound();
            }

            return Ok(informationRequest);
        }

        [ResponseType(typeof(List<InformationRequest>))]
        [Route("api/InformationRequests/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public PagedResponse<InformationRequest> Get(int pageIndex, int pageSize)
        {
            var data = db.InformationRequest.AsEnumerable();
            return new PagedResponse<InformationRequest>(data, pageIndex, pageSize);
        }

        // PUT: api/InformationRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInformationRequest(int id, InformationRequest informationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != informationRequest.Id)
            {
                return BadRequest();
            }

            db.Entry(informationRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationRequestExists(id))
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

        // POST: api/InformationRequests
        [ResponseType(typeof(InformationRequest))]
        public IHttpActionResult PostInformationRequest([FromBody]InformationRequest informationRequest)
        {
        // //    if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

           db.InformationRequest.Add(informationRequest);
           db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = informationRequest.Id }, informationRequest);
        }

        // DELETE: api/InformationRequests/5
        [ResponseType(typeof(InformationRequest))]
        public IHttpActionResult DeleteInformationRequest(int id)
        {
            InformationRequest informationRequest = db.InformationRequest.Find(id);
            if (informationRequest == null)
            {
                return NotFound();
            }

            db.InformationRequest.Remove(informationRequest);
            db.SaveChanges();

            return Ok(informationRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InformationRequestExists(int id)
        {
            return db.InformationRequest.Count(e => e.Id == id) > 0;
        }
    }
}