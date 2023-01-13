using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseTracker.Controllers
{
    public class CategoryController : ApiController
    {
        [System.Web.Http.HttpPost]
        public IHttpActionResult addCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                using (var contex = new ExpenseTrackerEntities2())
                {
                    var data = contex.Limits.Where(e => e.UserId == 1).FirstOrDefault();
                    if (data.AvalibleAmt > model.CatAmount)
                    {
                        var duplicate = contex.Categories.Where(e => e.CatName == model.CatName).FirstOrDefault();
                        if(duplicate==null)
                        { 
                        data.AvalibleAmt -= model.CatAmount;
                        Category cat = new Category()
                        {
                            Userid = 1,
                            CatAmount = model.CatAmount,
                            CatName = model.CatName,
                            CatAvalibleAmt = model.CatAmount
                        };
                        contex.Categories.Add(cat);
                        contex.SaveChanges();
                        return Ok();
                        }
                        else
                        {
                            return Ok("You Have A same Name category....");
                        }
                    }
                    else
                    {
                        return BadRequest("Your Amount Is More then Avalible Balense");
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Getdata()
        {
            using (var context = new ExpenseTrackerEntities2())
            {
                List<Category> userInfos = context.Categories.ToList();
                return Ok(userInfos);
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateCategory(Category ex)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ExpenseTrackerEntities2())
                {
                    var data = context.Categories.FirstOrDefault(x => x.Id == ex.Id);
                    if (data != null)
                    {
                        var oldavalible = data.CatAmount;
                        data.CatName= ex.CatName;
                        if(ex.CatAmount!=oldavalible)
                        {
                            var diff1 =  oldavalible- ex.CatAmount ;
                            var diff =  ex.CatAmount-  oldavalible ;
                            if ((data.CatAvalibleAmt-diff1)>=0)
                            {
                                var cat = context.Limits.Where(c => c.UserId == 1).FirstOrDefault();
                                
                                cat.AvalibleAmt += data.CatAmount;
                                cat.AvalibleAmt -= ex.CatAmount;
                                if(cat.AvalibleAmt>=0)
                                { 
                                    
                                data.CatAvalibleAmt += diff;
                                data.CatAmount = ex.CatAmount;
                                }
                                else
                                {
                                    cat.AvalibleAmt -= ex.CatAmount;
                                    cat.AvalibleAmt += data.CatAmount;
                                    return Ok("Can't Update Because You don't have Much Balance");
                                }
                            }
                            else
                            {
                                context.SaveChanges();
                                return Ok("Can't Shrink Amount Because You don't have Much Balance");
                            }
                        }
                        
                        context.SaveChanges();
                        return Ok("Update");
                    }
                    else
                    {
                        return Ok("Data Not Found");
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Deletedata(int id)
        {
            using (var context = new ExpenseTrackerEntities2())
            {
                var data = context.Categories.Where(model => model.Id == id).FirstOrDefault();
                var cat = context.Limits.Where(c => c.UserId == 1).FirstOrDefault();
                cat.AvalibleAmt += data.CatAmount;
                context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                IList<Category> expense = context.Categories.ToList();
                
                return Ok(expense);
            }
        }
    }
}
