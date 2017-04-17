using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;
using GlobalEvents.Filters;

namespace GlobalEvents.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        [MyAuthorize]
        public ActionResult MapView()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            return Json(
                Servicios.EventsService.ObtenerEventos(),
                JsonRequestBehavior.AllowGet);
        }


    }
}