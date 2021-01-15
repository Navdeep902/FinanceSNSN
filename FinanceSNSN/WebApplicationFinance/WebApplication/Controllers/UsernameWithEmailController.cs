using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UsernameWithEmailController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        public string GetUserWithEmail(string email) 
        {
            string temp = db.sp_UserWithEmail(email).FirstOrDefault();
            return temp; 
        }
    }
}
