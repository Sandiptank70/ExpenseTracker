using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseTracker.Controllers
{
    
    public class LimitController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult getlimit()
        {
            using (var context = new ExpenseTrackerEntities2())
            {
                //var data = context.Limits.Where(x=>x.UserId==1).FirstOrDefault();
                var data = context.Limits.Where(x => x.UserId == 1).FirstOrDefault();
                if (data != null)
                    return Ok(data.ExpLimit);
                else
                    return Ok();
                    }
        }
        [System.Web.Http.HttpGet]
        [Route("api/Limit/{id}")]
        public IHttpActionResult getlimit(int id)
        {
            using (var context = new ExpenseTrackerEntities2())
            {
                
                var data = context.Categories.Where(x => x.Id== id).FirstOrDefault();
                return Ok(data.CatAmount);
            }
        }
    }
}
