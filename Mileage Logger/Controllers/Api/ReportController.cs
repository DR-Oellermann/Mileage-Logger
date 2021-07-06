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
using Mileage_Logger.Models;

namespace Mileage_Logger.Controllers.Api
{
    public class ReportController : ApiController
    {
        private milageTrackerEntities db = new milageTrackerEntities();

        // GET: api/Report
        public IQueryable<tblFillUp> GettblFillUps()
        {
            return db.tblFillUps;
        }

        // GET: api/Report/5
        [ResponseType(typeof(tblFillUp))]
        public IHttpActionResult GettblFillUp(int id)
        {
            tblFillUp tblFillUp = db.tblFillUps.Find(id);
            if (tblFillUp == null)
            {
                return NotFound();
            }

            return Ok(tblFillUp);
        }

        // PUT: api/Report/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblFillUp(int id, tblFillUp tblFillUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblFillUp.FillUp_ID)
            {
                return BadRequest();
            }

            db.Entry(tblFillUp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblFillUpExists(id))
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

        // POST: api/Report
        [ResponseType(typeof(tblFillUp))]
        public IHttpActionResult PosttblFillUp(tblFillUp tblFillUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblFillUps.Add(tblFillUp);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblFillUp.FillUp_ID }, tblFillUp);
        }

        // DELETE: api/Report/5
        [ResponseType(typeof(tblFillUp))]
        public IHttpActionResult DeletetblFillUp(int id)
        {
            tblFillUp tblFillUp = db.tblFillUps.Find(id);
            if (tblFillUp == null)
            {
                return NotFound();
            }

            db.tblFillUps.Remove(tblFillUp);
            db.SaveChanges();

            return Ok(tblFillUp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblFillUpExists(int id)
        {
            return db.tblFillUps.Count(e => e.FillUp_ID == id) > 0;
        }
    }
}