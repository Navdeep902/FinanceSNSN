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
    public class LoginController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        public int GetUserLoginCheck(string username, string password)
        {
            return (int)db.sp_UserLoginCheck(username, password).FirstOrDefault();
        }

        public Nullable<int> GetCardNumberWithUsername(string username)
        {
            return db.sp_GetCardNumberWithUsername(username).FirstOrDefault();
        }
    }
}
