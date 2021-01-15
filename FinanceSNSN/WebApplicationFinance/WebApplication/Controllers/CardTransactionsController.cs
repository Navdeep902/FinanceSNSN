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
    public class CardTransactionsController : ApiController
    {
        private dbfinanceEntities db = new dbfinanceEntities();

        public HttpResponseMessage GetCreditDetails(int cardNumber) 
        {
            return Request.CreateResponse(db.sp_CreditDetails(cardNumber).FirstOrDefault());
        }

        public List<sp_UserTransactions_Result> GetCardTransactions(string username) 
        {
            return db.sp_UserTransactions(username).ToList<sp_UserTransactions_Result>();
        }
    }
}
