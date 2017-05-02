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
            // Por el momento acá va a ser destacados, después vemos si mostramos otros.
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.Destacado == true).ToList();
            return View(Lista);
        }

        public ActionResult Mios()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.IdUser == WebSecurity.CurrentUserId).ToList();
            return View(Lista);
        }

        public ActionResult Destacados()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.Destacado == true).ToList();
            return View(Lista);
        }

        public ActionResult Musica()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.IdCategoria == Categorias.Musica).ToList();
            return View(Lista);
        }

        public ActionResult Fiestas()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.IdCategoria == Categorias.Fiestas).ToList();
            return View(Lista);
        }

        public ActionResult Artes()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.IdCategoria == Categorias.Artes).ToList();
            return View(Lista);
        }

        public ActionResult Gastronomia()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.IdCategoria == Categorias.Gastronomia).ToList();
            return View(Lista);
        }

        public ActionResult Clases()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.IdCategoria == Categorias.Clases).ToList();
            return View(Lista);
        }

        public ActionResult Deportes()
        {
            List<Events> Lista = EventsService.ObtenerEventos().Where(z => z.IdCategoria == Categorias.Deportes).ToList();
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

        [HttpGet]
        public ActionResult Listado()
        {
            List<Events> Lista = EventsService.ObtenerEventos();
            return View(Lista);
        }

        // GET: Events/Create
        [MyAuthorize]
        public ActionResult Create()
        {
            Events model = new Events { IdCategoria = Categorias.Musica, FechaInicio = DateTime.Now, FechaFin = DateTime.Now};
            return View(model);
        }

        // POST: Events/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize]
        public ActionResult Create([Bind(Include = "Id,NombreEvento,lat,lng,Descripcion,FechaInicio,FechaFin,IdUser,Estado,Destacado,Direccion,IdCategoria,RutaImagen,HoraInicio,HoraFin")]
        Events events, HttpPostedFileBase file, string HoraInicio, string HoraFin)
        {
            if (ModelState.IsValid)
            {
                TimeSpan Inicio = TimeSpan.Parse(HoraInicio);
                TimeSpan Fin = TimeSpan.Parse(HoraFin);
                events.IdUser = WebSecurity.CurrentUserId;               
                EventsService.Create(events, file, Inicio, Fin);
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
        public ActionResult Edit([Bind(Include = "Id,NombreEvento,lat,lng,Descripcion,FechaInicio,FechaFin,IdUser,Estado, Destacado, Direccion, IdCategoria, RutaImagen, HoraInicio, HoraFin")] Events events,
            HttpPostedFileBase file, string HoraInicio, string HoraFin)
        {
            if (ModelState.IsValid)
            {
                TimeSpan Inicio = TimeSpan.Parse(HoraInicio);
                TimeSpan Fin = TimeSpan.Parse(HoraFin);
                events.IdUser = WebSecurity.CurrentUserId;
                //db.Entry(events).State = EntityState.Modified;
                //db.SaveChanges();
                EventsService.Edit(events, file, Inicio, Fin);
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