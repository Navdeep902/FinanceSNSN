using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EmailController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        [HttpPost]
        public IHttpActionResult sendmail(EmailClass ec)
        {
            var k = db.sp_EmailCheck(ec.To).FirstOrDefault();
            if (k != null)
            {
                string to = ec.To;
                string body = ec.Body;
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("financeproject34@gmail.com");
                mm.To.Add(to);
                mm.Subject = "OTP for FORGOT PASSWORD";
                mm.Body = body;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.UseDefaultCredentials = true;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("financeproject34@gmail.com", "Passwordsnsns");
                smtp.Send(mm);
                return Ok();
            }
            else
            {
                return BadRequest("No such User exists");
            }
        }
    }
}