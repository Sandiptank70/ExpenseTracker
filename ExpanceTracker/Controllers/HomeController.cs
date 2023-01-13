using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExpenseTracker.Controllers
{
    [System.Web.Mvc.RoutePrefix("Home")]
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        
        public ActionResult Index()
        {
            HttpClient client1 = new HttpClient();
            ViewBag.Title = "Home Page";
            ViewBag.categoryselect = new SelectList(drop(), "Id", "CatName");


            IEnumerable<ExpanceModel> expans = new List<ExpanceModel>();
            client1.BaseAddress = new Uri("https://localhost:44342/api/WebApi");
            var respose = client1.GetAsync("WebApi");
            respose.Wait();
            var test = respose.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<ExpanceModel>>();
                display.Wait();
                expans = display.Result;
            }
            ViewBag.cate = catelist();
            ViewBag.limit = expenseLimit(null);

            
            return View(expans);
            
        }
        [System.Web.Mvc.Route("TotalExpenseLimit")]
        public ActionResult AddLimit()
        {
            ViewBag.total = expenseLimit(null);
            return View();
        }
        [System.Web.Mvc.Route("TotalExpenseLimit")]
        [System.Web.Mvc.HttpPost]
        public ActionResult AddLimit(Limit li)
        {
            client.BaseAddress = new Uri("https://localhost:44342/api/WebApi");
            var response = client.PostAsJsonAsync<Limit>("WebApi", li);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                
                return RedirectToAction("addCategory");
            }
            
            return View("AddLimit");
        }
        [System.Web.Mvc.Route("CategoryAdd")]
        public ActionResult addCategory()
        {
            
            return View();
        }
        [System.Web.Mvc.Route("CategoryAdd")]
        [System.Web.Mvc.HttpPost]
        public ActionResult addCategory(CategoryModel model)
        {
            client.BaseAddress = new Uri("https://localhost:44342/api/Category");
            var response = client.PostAsJsonAsync<CategoryModel>("Category", model);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {

                return RedirectToAction("Category");
            }

            return RedirectToAction("Index");
        }
        [System.Web.Mvc.Route("Category")]
        public ActionResult displayCategory()
        {
            List<Category> categoryModel = new List<Category>();
            client.BaseAddress = new Uri("https://localhost:44342/api/Category");
            var respose = client.GetAsync("Category");
            respose.Wait();
            var test = respose.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Category>>();
                display.Wait();
                categoryModel = display.Result;
            }
            ViewBag.limit = expenseLimit(null);
            return View(categoryModel);

        }
        [System.Web.Mvc.Route("ExpenseAdd")]
        public ActionResult Expance()
        {
            //ViewBag.categoryselect = new SelectList(drop(), "Id", "CatName");
            ViewBag.categoryselect = new SelectList(drop(), "Id", "CatName");
            
            return View();
        }
        [System.Web.Mvc.Route("ExpenseAdd")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Expance(ExpanceModel expmodel)
        {
            client.BaseAddress = new Uri("https://localhost:44342/api/Expance");
            var response = client.PostAsJsonAsync<ExpanceModel>("Expance", expmodel);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Expance");
        }
        private List<Category> drop()
        {
            HttpClient dropclient = new HttpClient();
            List<Category> categoryModel = new List<Category>();
            dropclient.BaseAddress = new Uri("https://localhost:44342/api/Category");
            var respose = dropclient.GetAsync("Category");
            respose.Wait();
            var test = respose.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Category>>();
                display.Wait();
                categoryModel = display.Result;
            }

            return categoryModel;
        }

        public ActionResult updateExpense(Expance id)
        {
            return View("Index");
        }


        [System.Web.Mvc.HttpGet, System.Web.Http.ActionName("Delete")]
        public ActionResult Deleteexpance(int id)
        {
            IEnumerable<ExpanceModel> expans = new List<ExpanceModel>();
            client.BaseAddress = new Uri("https://localhost:44342/api/WebApi");
            var response = client.DeleteAsync("WebApi/" + id.ToString());

            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<ExpanceModel>>();
                display.Wait();
                expans = display.Result;
            }
            ViewBag.cate = catelist();
            ViewBag.limit = expenseLimit(null);
            return View("Index", expans);
        }

        [System.Web.Mvc.HttpGet, System.Web.Http.ActionName("Delete")]
        public ActionResult DeleteCategory(int id)
        {
            IEnumerable<Category> expans = new List<Category>();
            client.BaseAddress = new Uri("https://localhost:44342/api/Category");
            var response = client.DeleteAsync("Category/" + id.ToString());

            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Category>>();
                display.Wait();
                expans = display.Result;
            }
            return RedirectToAction("displayCategory", expans);
            
        }

        public List<Category> catelist()
        {

            HttpClient catlistclient = new HttpClient();
                List<Category> categoryModel = new List<Category>();
            catlistclient.BaseAddress = new Uri("https://localhost:44342/api/Category");
                var respose = catlistclient.GetAsync("Category");
                respose.Wait();
                var test = respose.Result;
                if (test.IsSuccessStatusCode)
                {
                    var display = test.Content.ReadAsAsync<List<Category>>();
                    display.Wait();
                    categoryModel = display.Result;
                }

                return categoryModel;

            
        }
        public int expenseLimit(int? id)
        {
            HttpClient limitclient = new HttpClient();
            int limit=0 ;
            limitclient.BaseAddress = new Uri("https://localhost:44342/api/Limit");
            if(id!=null)
            {
                var response = limitclient.GetAsync("Limit/" + id.ToString());
                response.Wait();
                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    var display = test.Content.ReadAsAsync<int>();
                    display.Wait();
                    limit = display.Result;
                }

                return limit;
            }
            else
            {
                var respose = limitclient.GetAsync("Limit");
                respose.Wait();
                var test = respose.Result;
                if (test.IsSuccessStatusCode)
                {
                    var display = test.Content.ReadAsAsync<int>();
                    display.Wait();
                    limit = display.Result;
                }

                return limit;
            }
            

        }
        [System.Web.Mvc.HttpGet]
        public ActionResult Filter(int id) {
            IEnumerable<ExpanceModel> expans = new List<ExpanceModel>();
            client.BaseAddress = new Uri("https://localhost:44342/api/WebApi");
            var response = client.GetAsync("WebApi/" + id.ToString());

            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<ExpanceModel>>();
                display.Wait();
                expans = display.Result;
            }
            ViewBag.cate = catelist();
            ViewBag.limit = expenseLimit(id);
            ViewBag.categoryselect = new SelectList(drop(), "Id", "CatName");
            return View("Index", expans);
        }

    }
}
