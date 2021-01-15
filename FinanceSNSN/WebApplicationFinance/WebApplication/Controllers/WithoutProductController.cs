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
    public class WithoutProductController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        public int GetTotalCostWithout(int cartId) 
        {
            return (int)db.sp_TotalCostWithout(cartId).FirstOrDefault();
        }
    }
}
