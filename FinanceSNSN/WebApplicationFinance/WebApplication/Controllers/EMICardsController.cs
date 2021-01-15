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
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EMICardsController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        // GET: api/EMICards
        public IQueryable<EMICard> GetEMICard()
        {
            return db.EMICard;
        }

        // GET: api/EMICards/5
        [ResponseType(typeof(EMICard))]
        public IHttpActionResult GetEMICard(int id)
        {
            EMICard eMICard = db.EMICard.Find(id);
            if (eMICard == null)
            {
                return NotFound();
            }

            return Ok(eMICard);
        }

        // PUT: api/EMICards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMICard(int id, EMICard eMICard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMICard.Card_Number)
            {
                return BadRequest();
            }

            db.Entry(eMICard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMICardExists(id))
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

        // POST: api/EMICards
        [ResponseType(typeof(EMICard))]
        public IHttpActionResult PostEMICard(EMICard eMICard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMICard.Add(eMICard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EMICardExists(eMICard.Card_Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eMICard.Card_Number }, eMICard);
        }

        // DELETE: api/EMICards/5
        [ResponseType(typeof(EMICard))]
        public IHttpActionResult DeleteEMICard(int id)
        {
            EMICard eMICard = db.EMICard.Find(id);
            if (eMICard == null)
            {
                return NotFound();
            }

            db.EMICard.Remove(eMICard);
            db.SaveChanges();

            return Ok(eMICard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMICardExists(int id)
        {
            return db.EMICard.Count(e => e.Card_Number == id) > 0;
        }
    }
}