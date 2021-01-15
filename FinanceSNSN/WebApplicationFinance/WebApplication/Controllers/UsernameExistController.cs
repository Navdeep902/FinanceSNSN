using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UsernameExistController : ApiController
    {
        private dbfinanceEntities entities = new dbfinanceEntities();
        public IHttpActionResult checkuser(string username)
        {
            var result = entities.RegisterBank.ToList().Exists(x => x.username.Equals(username, StringComparison.CurrentCultureIgnoreCase));
            return Ok(result);
        }
    }
}
