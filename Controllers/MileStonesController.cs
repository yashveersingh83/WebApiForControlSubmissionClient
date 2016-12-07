using Newtonsoft.Json;
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
    public class MileStonesController : ApiController
    {
        private ControlSubmissionDbContext db = new ControlSubmissionDbContext();

        // GET: api/MileStones
        [ResponseType(typeof( List< MileStone>))]
        public IHttpActionResult GetMileStones()
        {
           List<MileStone> r =  db.MileStones.ToList();
            return Ok(r);
        }

        // GET: api/MileStones/5
        [ResponseType(typeof(MileStone))]
        public IHttpActionResult GetMileStone(int id)
        {
            MileStone mileStone = db.MileStones.Find(id);
            if (mileStone == null)
            {
                return NotFound();
            }

            return Ok(mileStone);
        }

        // PUT: api/MileStones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMileStone(int id, MileStone mileStone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mileStone.Id)
            {
                return BadRequest();
            }

            db.Entry(mileStone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MileStoneExists(id))
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

        // POST: api/MileStones
        [ResponseType(typeof(MileStone))]
        public IHttpActionResult PostMileStone([FromBody] MileStone mileStone)
        {
          //  JsonConvert.DeserializeObject<MileStone>(mileStone);
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.MileStones.Add(mileStone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mileStone.Id }, mileStone);
        }

       // [ResponseType(typeof(List<MileStone>))]
        [Route("api/MileStones/search/{name}")]
        [HttpGet]
        public IHttpActionResult SearchMileStone(string name)
        {
            var r =  db.MileStones.Where(x => x.Name.Contains(name)).AsEnumerable();
            return Ok( new PagedResponse<MileStone>(r, 1, 10));
        }

        [ResponseType(typeof(List<MileStone>))]
        [Route("api/MileStones/{pageIndex:int}/{pageSize:int}")]
        [HttpGet]
        public PagedResponse<MileStone> Get(int pageIndex, int pageSize)
        {
            var data = db.MileStones.AsEnumerable();
            return new PagedResponse<MileStone>(data, pageIndex, pageSize);
        }

        // DELETE: api/MileStones/5
        [ResponseType(typeof(MileStone))]
        public IHttpActionResult DeleteMileStone(int id)
        {
            MileStone mileStone = db.MileStones.Find(id);
            if (mileStone == null)
            {
                return NotFound();
            }

            db.MileStones.Remove(mileStone);
            db.SaveChanges();

            return Ok(mileStone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MileStoneExists(int id)
        {
            return db.MileStones.Count(e => e.Id == id) > 0;
        }
    }

    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> data, int pageIndex, int pageSize)
        {
            Data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            Total = data.Count();
        }

        public int Total { get; set; }
        public ICollection<T> Data { get; set; }
    }
    public class DataModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}