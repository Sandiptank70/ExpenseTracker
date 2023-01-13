using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseTracker.Controllers
{
    public class ExpanceController : ApiController
    {
        [HttpGet]
        public IHttpActionResult dropdown()
        {
            using (var context=new ExpenseTrackerEntities2())
            {
                IList<Category> drop = context.Categories.ToList();
                return Ok(drop);
            }
                
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult addExpense(ExpanceModel model)
        {
            if (ModelState.IsValid)
            {
                using (var contex = new ExpenseTrackerEntities2())
                {
                    var data = contex.Categories.Where(x => x.Id == model.Category).FirstOrDefault();
                    Expance cat = new Expance()
                    {
                        Category = model.Category,
                        ExpAmt = model.ExpAmt,
                        ExpDate = model.ExpDate,
                        ExpDescription= model.ExpDescription,
                        ExpName= model.ExpName,
                        
                    };
                    data.CatAvalibleAmt -= model.ExpAmt;
                    contex.Expances.Add(cat);
                    contex.SaveChanges();
                    return Ok();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
