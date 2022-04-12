using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBAPI_MVC_CrudOp.Models;
using System.Net.Http;

namespace WEBAPI_MVC_CrudOp.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<Person> personObj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64481/api/EmpCrud");
            var consumeApi = hc.GetAsync("EmpCrud");
            consumeApi.Wait();

            var readdata = consumeApi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayData = readdata.Content.ReadAsAsync<IList<Person>>();
                displayData.Wait();
                personObj = displayData.Result;
            }
            return View(personObj);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create( Person insertemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64481/api/EmpCrud");
            var insertrecord = hc.PostAsJsonAsync<Person>("EmpCrud", insertemp);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }

            return View("create");
        }

        public ActionResult Details(int id)
        {
            EmpClass empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64481/api/EmpCrud");
            var consumeApi = hc.GetAsync("EmpCrud?id="+id.ToString());
            consumeApi.Wait();
            var readdata = consumeApi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayData = readdata.Content.ReadAsAsync<EmpClass>();
                displayData.Wait();
                empobj = displayData.Result;
            }
            return View(empobj);
        }
        public ActionResult Edit(int id)
        {
            EmpClass empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64481/api/EmpCrud");
            var consumeApi = hc.GetAsync("EmpCrud?id=" + id.ToString());
            consumeApi.Wait();
            var readdata = consumeApi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayData = readdata.Content.ReadAsAsync<EmpClass>();
                displayData.Wait();
                empobj = displayData.Result;
            }
            return View(empobj);
        }
        [HttpPost]
        public ActionResult Edit( EmpClass ec)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64481/api/EmpCrud");
            var insertrecord = hc.PutAsJsonAsync<EmpClass>("EmpCrud", ec);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.message = "Person Recored not Updated...!!";
            }

            return View(ec);


        }
        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:64481/api/EmpCrud");

            var delrecord = hc.DeleteAsync("EmpCrud/"+id.ToString());
            delrecord.Wait();

            var displaydata = delrecord.Result;
            if (displaydata.IsSuccessStatusCode) {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}