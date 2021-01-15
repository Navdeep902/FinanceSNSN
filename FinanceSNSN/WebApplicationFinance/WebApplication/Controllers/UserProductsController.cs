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
    public class UserProductsController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        public List<sp_UserProducts_Result> GetUserProducts(string username)
        {
            return db.sp_UserProducts(username).ToList<sp_UserProducts_Result>();
        }

        public int GetLastTransactionDate(string username, int productId)
        {
            return (int)db.sp_LastTransactionDate(username, productId).FirstOrDefault();
        }
    }
}
