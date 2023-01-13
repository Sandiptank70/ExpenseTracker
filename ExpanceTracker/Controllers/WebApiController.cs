using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    
    public class WebApiController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult Getdata()
        {
            using (var context = new ExpenseTrackerEntities2())
            {
                IList<ExpanceModel> expense = context.displayCategory().Select(x=>new ExpanceModel()
                {
                    CatName= x.CatName,
                    Category= x.Category,
                    ExpAmt= x.ExpAmt,
                    ExpDate= x.ExpDate,
                    ExpDescription= x.ExpDescription,
                    ExpName= x.ExpName,
                    Id=x.Id
                    
                }).ToList();
                
               
                return Ok(expense);
            }
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult InsertExplimit(ExpanceLimit li)
        {
            if (ModelState.IsValid)
            {

                using (var context = new ExpenseTrackerEntities2())
                {
                    var data = context.Limits.FirstOrDefault(l => l.UserId == 1);
                    if (data != null)
                    {

                        data.AvalibleAmt += li.ExpLimit;
                        data.ExpLimit += li.ExpLimit;
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        Limit limit = new Limit()
                        {
                            UserId = 1,
                            AvalibleAmt = li.ExpLimit,
                            ExpLimit = li.ExpLimit

                        };
                        context.Limits.Add(limit);
                        context.SaveChanges();
                        return Ok();
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateExpense(ExpanceModel ex)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ExpenseTrackerEntities2())
                {
                    var data = context.Expances.FirstOrDefault(x => x.Id == ex.Id);
                    if (data != null)
                    {
                        var catdata = context.Categories.Where(c => c.Id == data.Category).FirstOrDefault();
                        catdata.CatAvalibleAmt += data.ExpAmt;
                        catdata.CatAvalibleAmt -= ex.ExpAmt;
                        
                        data.ExpName = ex.ExpName;
                        data.ExpAmt = ex.ExpAmt;
                        data.Category = ex.Category;
                        data.ExpDate = ex.ExpDate;
                        data.ExpDescription = ex.ExpDescription;
                        context.SaveChanges();
                        return Ok("Update Successfully");
                        
                    }
                    else
                    {
                        return Ok("Data Not Found");
                    }
                }
            }
            else
            {
                return BadRequest("Fill All Data");
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/WebApi/{id}")]
        public IHttpActionResult Deletedata(int id)
        {
            using (var context = new ExpenseTrackerEntities2())
            {
                var data = context.Expances.Where(model => model.Id == id).FirstOrDefault();
                var catamt = context.Categories.Where(model => model.Id == data.Category).FirstOrDefault();
                catamt.CatAvalibleAmt += data.ExpAmt;
                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                IList<ExpanceModel> expense = context.displayCategory().Select(x => new ExpanceModel()
                {
                    CatName = x.CatName,
                    Category = x.Category,
                    ExpAmt = x.ExpAmt,
                    ExpDate = x.ExpDate,
                    ExpDescription = x.ExpDescription,
                    ExpName = x.ExpName,
                    Id = x.Id

                }).ToList();
                return Ok(expense);
            }
        }
    
    [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/WebApi/{id}")]
        public IHttpActionResult filter(int id)
        {

            using (var context=new ExpenseTrackerEntities2())
            {
                IList<ExpanceModel> expense = context.FilterRecord(id).Select(x => new ExpanceModel()
                {
                    CatName = x.CatName,
                    Category = x.Category,
                    ExpAmt = x.ExpAmt,
                    ExpDate = x.ExpDate,
                    ExpDescription = x.ExpDescription,
                    ExpName = x.ExpName,
                    Id = x.Id

                }).ToList();
                return Ok(expense);
            }
                
        }

        
    } 
}
