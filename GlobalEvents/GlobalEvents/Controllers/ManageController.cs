using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios;
using GlobalEvents.Filters;
using RepositorioClases;

namespace GlobalEvents.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {   
        public ActionResult Reportes()
        {
            List<RepositorioClases.CommentsReportes> Comentarios = CommentsService.ObtenerComentariosReportados();
            List<RepositorioClases.EventsReportes> Eventos = EventsService.ObtenerEventosReportados();
            var a = new RepositorioClases.Reportes();
            a.Comentarios = Comentarios;
            a.Eventos = Eventos;
            return View(a);
        }

        [HttpGet]
        [MyAuthorize]
        /// <summary>
        /// el id es el del comentario
        /// </summary>
        public ActionResult CommentReported(int id)
        {
            // Obtiene el comentario.
            Comments comment = CommentsService.GetById(id);
            // Genera objeto con comentario y todos los reportes.
            CommentsDetailsReport reporte = new CommentsDetailsReport();
            Comments comentario = new Comments {
                CommentId = comment.CommentId,
                Estado = comment.Estado,
                EventId = comment.EventId,
                Fecha = comment.Fecha,
                ComentarioPadre = comment.ComentarioPadre,
                FechaUltimaActualizacion = comment.FechaUltimaActualizacion,
                iDUsuario = comment.iDUsuario,
                Like = comment.Like,
                UnLike = comment.UnLike
            };
            
            // Busca nombre de usuario
            Users user = UserService.Get(comment.iDUsuario).FirstOrDefault();
            comentario.Comentario = user.Nombre;
            reporte.Comentario = comentario;
            List<CommentsReportes> Reportes = CommentsService.ObtenerComentariosReportados().Where(z => z.CommentId == id).ToList();
            reporte.Reportes = Reportes;
            return View(reporte);
        }

    }
}
