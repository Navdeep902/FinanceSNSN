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
    public class RegisterController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        // GET: api/Register
        public IQueryable<RegisterBank> GetRegisterBank()
        {
            return db.RegisterBank;
        }

        // GET: api/Register/5
        [ResponseType(typeof(RegisterBank))]
        public IHttpActionResult GetRegisterBank(string id)
        {
            RegisterBank registerBank = db.RegisterBank.Find(id);
            if (registerBank == null)
            {
                return NotFound();
            }

            return Ok(registerBank);
        }

        // PUT: api/Register/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegisterBank(string id, RegisterBank registerBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registerBank.username)
            {
                return BadRequest();
            }

            db.Entry(registerBank).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterBankExists(id))
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

        // POST: api/Register
        [ResponseType(typeof(RegisterBank))]
        public IHttpActionResult PostRegisterBank(RegisterBank registerBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegisterBank.Add(registerBank);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RegisterBankExists(registerBank.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = registerBank.username }, registerBank);
        }

        // DELETE: api/Register/5
        [ResponseType(typeof(RegisterBank))]
        public IHttpActionResult DeleteRegisterBank(string id)
        {
            RegisterBank registerBank = db.RegisterBank.Find(id);
            if (registerBank == null)
            {
                return NotFound();
            }

            db.RegisterBank.Remove(registerBank);
            db.SaveChanges();

            return Ok(registerBank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegisterBankExists(string id)
        {
            return db.RegisterBank.Count(e => e.username == id) > 0;
        }
    }
}