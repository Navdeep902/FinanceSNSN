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
    public class RegisterBanksController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        // GET: api/RegisterBanks
        public IQueryable<RegisterBank> GetRegisterBank()
        {
            return db.RegisterBank;
        }

        // GET: api/RegisterBanks/5
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

        // PUT: api/RegisterBanks/5
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

            db.sp_updatepassword(id, registerBank.Password);

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

        // POST: api/RegisterBanks
        [ResponseType(typeof(RegisterBank))]
        public IHttpActionResult PostRegisterBank(RegisterBank registerBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int temp = (int)db.sp_check(registerBank.username).FirstOrDefault();

            if (temp==0) 
            {
                db.sp_InsertEMICard(registerBank.username);
            }

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

        // DELETE: api/RegisterBanks/5
        [ResponseType(typeof(RegisterBank))]
        public IHttpActionResult DeleteRegisterBank(string id)
        {
            RegisterBank registerBank = db.RegisterBank.Find(id);
            if (registerBank == null)
            {
                return NotFound();
            }

            db.Delete_EMIDetails(id);
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