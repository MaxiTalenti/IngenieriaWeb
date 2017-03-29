using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlobalEvents.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetEvents()
        {
            return Json(
                new List<Events>()
                {
                    new Events { id = 1, Event = "Prueba", lat= "-30.945932", lng = "-61.562566" },
                    new Events { id = 2, Event = "Prueba Dos", lat = "-30.954932", lng = "-61.562400" }
                } ,
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult MapView()
        {
            return View();
        }

        public class Events
        {
            public int id { get; set; }
            public string Event { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
        }
    }
}