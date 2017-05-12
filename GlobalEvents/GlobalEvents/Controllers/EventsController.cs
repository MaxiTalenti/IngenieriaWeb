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
using ViewModels;

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


        public ActionResult GetEventCommets(int IdEvento)
        {
            var comments = CommentsService.ObtenerComentarios(IdEvento).Select(u => new ViewModels.Comments()
            {
                iDUsuario = u.iDUsuario,
                CommentId = u.CommentId,
                EventId = u.EventId,
                Comentario = u.Comentario,
                Like = u.Like,
                UnLike = u.UnLike,
                Fecha = u.Fecha
            }).ToList();

            var viewModel = new ViewModels.CommentsModel
            {
                CommentsList = comments,
                Comment = ""
            };

            return PartialView(@"~/Views/Events/CommentsView.cshtml", viewModel);
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

        [MyAuthorize]
        public ActionResult EventosReportados()
        {
            var comments = EventsService.ObtenerEventosReportados();
            List<EventosModeracionModel> Lista = new List<EventosModeracionModel>();
            foreach (EventsReportes reporte in comments)
            {
                EventosModeracionModel comentario = new EventosModeracionModel();
                comentario.ReporteId = reporte.ReporteId;
                comentario.EventId = reporte.EventId;
                comentario.Evento = EventsService.Get(comentario.EventId).FirstOrDefault().NombreEvento;
                comentario.Fecha = reporte.Fecha;
                comentario.IdUsuario = reporte.IdUsuario;
                comentario.Observacion = reporte.Observacion;
                comentario.Usuario = UserService.Get(comentario.IdUsuario).FirstOrDefault().Usuario;
                Lista.Add(comentario);
                comentario = null;
            }
            return View(@"EventosReportados", Lista);
        }

        [MyAuthorize]
        public ActionResult ReportarEvento(int? id)
        {
            EventsReportes reporte = new EventsReportes { EventId = (int)id };
            return View("ReportarEvento", reporte);
        }

        [HttpPost]
        public ActionResult ReportarEvento(EventsReportes reporte)
        {
            reporte.IdUsuario = WebSecurity.CurrentUserId;
            EventsService.CreateReporte(reporte);
            EventsService.CambiarEstadoEvento(reporte.EventId, EventState.Reportado);
            return RedirectToAction("Details", "Events", new { id = reporte.EventId });
        }
    }
}