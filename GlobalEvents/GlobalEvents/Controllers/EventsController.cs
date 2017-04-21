using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepositorioClases;
using Servicios;
using GlobalEvents.Filters;
using WebMatrix.WebData;

namespace GlobalEvents.Controllers
{
    public class EventsController : Controller
    {
        private Modelo db = new Modelo();

        // GET: Events
        public ActionResult Index()
        {
            List<Events> Lista = EventsService.ObtenerEventos();
            return View(Lista);
        }

        // GET: Events/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = EventsService.Get(id).FirstOrDefault();
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create
        [MyAuthorize]
        public ActionResult Create()
        {
            Events model = new Events { IdCategoria = Categorias.Musica, FechaInicio = DateTime.Now, FechaFin = DateTime.Now, Destacado = true };
            return View(model);
        }

        // POST: Events/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize]
        public ActionResult Create([Bind(Include = "Id,NombreEvento,lat,lng,Descripcion,FechaInicio,FechaFin,IdUser,Estado, Destacado, Direccion, IdCategoria")] Events events)
        {
            if (ModelState.IsValid)
            {
                events.IdUser = WebSecurity.CurrentUserId;               
                EventsService.Create(events);
                return RedirectToAction("Index");
            }

            return View(events);
        }

        // GET: Events/Edit/5
        [MyAuthorize]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = EventsService.Get(id).FirstOrDefault();
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize]
        public ActionResult Edit([Bind(Include = "Id,NombreEvento,lat,lng,Descripcion,FechaInicio,FechaFin,IdUser,Estado, Destacado, Direccion, IdCategoria")] Events events)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(events).State = EntityState.Modified;
                //db.SaveChanges();
                EventsService.Edit(events);
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = EventsService.Get(id).FirstOrDefault();
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Events events = EventsService.Get(id).FirstOrDefault();
            EventsService.Delete(events);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [MyAuthorize]
        public ActionResult MapView()
        {
            return View();
        }

        public JsonResult GetEvents(int? id)
        {
            return Json(
                Servicios.EventsService.GetForMap(id),
                JsonRequestBehavior.AllowGet);
        }
    }
}