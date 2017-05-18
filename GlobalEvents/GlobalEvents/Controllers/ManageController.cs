using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Servicios;
using GlobalEvents.Filters;
using RepositorioClases;

namespace GlobalEvents.Controllers
{
    [InitializeSimpleMembership]
    [MyAuthorize(Roles = "Admin")]
    public class ManageController : Controller
    {   
        public ActionResult Reportes()
        {
            List<CommentsReportes> Comentarios = ReportServices.ObtenerComentariosReportados();
            List<EventsReportes> Eventos = ReportServices.ObtenerEventosReportados();
            var a = new Reportes();
            a.Comentarios = Comentarios;
            a.Eventos = Eventos;
            return View(a);
        }

        [HttpGet]
        /// <summary>
        /// el id es el del comentario
        /// </summary>
        public ActionResult CommentReported(int? id)
        {
            if (id == null)
            {
                return Errores.MostrarError(DatosErrores.ErrorParametros);
            }
            // Obtiene el comentario.
            Comments comment = CommentsService.GetById((int)id);
            // Si no hay comentario con ese id.
            if (comment == null)
            {
                return Errores.MostrarError(DatosErrores.ErrorParametros);
            }

            // Genera objeto con comentario y todos los reportes.
            CommentsDetailsReport reporte = new CommentsDetailsReport();
            Comments comentario = new Comments {
                CommentId = comment.CommentId,
                Comentario = comment.Comentario,
                Estado = comment.Estado,
                EventId = comment.EventId,
                Fecha = comment.Fecha,
                ComentarioPadre = comment.ComentarioPadre,
                FechaUltimaActualizacion = comment.FechaUltimaActualizacion,
                iDUsuario = comment.iDUsuario,
                Like = comment.Like,
                UnLike = comment.UnLike
            };
            
            reporte.Comentario = comentario;
            List<CommentsReportes> Reportes = ReportServices.ObtenerComentariosReportados().Where(z => z.CommentId == id).Where(z => z.Resuelto == false).ToList();
            reporte.Reportes = Reportes;
            return View(reporte);
        }

        [HttpPost]
        public ActionResult CommentReported([Bind(Prefix = "Comentario", Include = "CommentId,Estado")] Comments comm)
        {
            if (CommentsService.GetById(comm.CommentId) == null)
            {
                return Errores.MostrarError(DatosErrores.ErrorParametros);
            }
            CommentsService.CambiarEstadoComentario(comm.CommentId, comm.Estado);
            return RedirectToAction("Details", "Comments", new { id = comm.CommentId });
        }

        [HttpGet]
        public ActionResult CerrarReporteComentario(int ComentarioId)
        {
            if (CommentsService.GetById(ComentarioId) == null)
            {
                return Errores.MostrarError(DatosErrores.ErrorParametros);
            }
            ReportServices.ResolverReportesComentarios(ComentarioId);
            return RedirectToAction("Details", "Comments", new { id = ComentarioId });
        }

        ///  Parte de eventos

        [HttpGet]
        /// <summary>
        /// el id es el del comentario
        /// </summary>
        public ActionResult EventReported(int id)
        {
            // Obtiene el evento.
            Events comment = EventsService.Get(id).SingleOrDefault();
            if (comment == null)
            {
                return Errores.MostrarError(DatosErrores.ErrorParametros);
            }
            // Genera objeto con evento y todos los reportes.
            EventsDetailsReport reporte = new EventsDetailsReport();
            Events evento = new Events
            {
                Id = comment.Id,
                NombreEvento = comment.NombreEvento,
                lng = comment.lng,
                Descripcion = comment.Descripcion,
                FechaInicio = comment.FechaInicio,
                FechaFin = comment.FechaFin,
                IdUser = comment.IdUser,
                IdCategoria = comment.IdCategoria,
                Destacado = comment.Destacado,
                Direccion = comment.Direccion,
                RutaImagen = comment.RutaImagen,
                Estado = comment.Estado,
                HoraInicio = comment.HoraInicio,
                HoraFin = comment.HoraFin
            };

            reporte.Eventos = evento;
            List<EventsReportes> Reportes = ReportServices.ObtenerEventosReportados().Where(z => z.EventId == id).Where(z => z.Resuelto == false).ToList();
            reporte.Reportes = Reportes;
            return View(reporte);
        }

        [HttpPost]
        public ActionResult EventReported([Bind(Prefix = "Eventos", Include = "Id,Estado")] Events even)
        {
            EventsService.CambiarEstadoEvento(even.Id, even.Estado);
            return RedirectToAction("Details", "Events", new { id = even.Id });
        }

        [HttpGet]
        public ActionResult CerrarReporteEventos(int? EventoId)
        {
            if (EventoId == null)
            {
                return Errores.MostrarError(DatosErrores.ErrorParametros);
            }
            ReportServices.ResolverReportesEventos((int)EventoId);
            return RedirectToAction("Details", "Events", new { id = EventoId });
        }

    }
}
