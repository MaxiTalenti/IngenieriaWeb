using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Servicios;
using RepositorioClases;
using GlobalEvents.Filters;
using WebMatrix.WebData;

namespace GlobalEvents.Controllers
{
    [MyAuthorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        
        public ActionResult ComentariosReportados()
        {
            var comments = ReportServices.ObtenerComentariosReportados();
            List<ComentariosEventosModeracionModel> Lista = new List<ComentariosEventosModeracionModel>();
            foreach(CommentsReportes reporte in comments)
            {
                ComentariosEventosModeracionModel comentario = new ComentariosEventosModeracionModel();
                comentario.CommentId = reporte.CommentId;
                comentario.EventId = CommentsService.GetById(comentario.CommentId).EventId;
                comentario.Evento = EventsService.Get(comentario.EventId).FirstOrDefault().NombreEvento;
                comentario.Fecha = reporte.Fecha;
                comentario.IdUsuario = reporte.IdUsuario;
                comentario.Observacion = reporte.Observacion;
                comentario.Usuario = UserService.Get(comentario.IdUsuario).FirstOrDefault().Usuario;
                Lista.Add(comentario);
                comentario = null;
            }
            return View(@"ComentariosReportados", Lista);
        }

        [MyAuthorize]
        public ActionResult Listado()
        {
            List<RepositorioClases.Comments> Lista = CommentsService.ObtenerComentarios();
            return View(Lista);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [MyAuthorize]
        public PartialViewResult Create(CommentsModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CommentsService.Create(viewModel.Comment, viewModel.IdEvento, WebSecurity.CurrentUserId);
                //return RedirectToAction("Index");
            }

            var comments = CommentsService.ObtenerComentarios(viewModel.IdEvento).Select(u => new ViewModels.Comments()
            {
                iDUsuario = u.iDUsuario,
                CommentId = u.CommentId,
                EventId = u.EventId,
                Comentario = u.Comentario,
                ComentarioPadre = u.ComentarioPadre,
                Like = u.Like,
                UnLike = u.UnLike,
                Fecha = u.Fecha
            }).ToList();

            viewModel = new ViewModels.CommentsModel
            {
                CommentsList = comments,
                Comment = string.Empty
            };

            return PartialView(@"~/Views/Events/CommentsView.cshtml", viewModel);
        }

        [MyAuthorize]
        public ActionResult ReportarComentario(int? id) 
        {
            CommentsReportes reporte = new CommentsReportes{ CommentId = (int)id };
            return View("ReportarComentario", reporte);
        }

        [HttpPost]
        [MyAuthorize]
        public ActionResult ReportarComentario(CommentsReportes reporte)
        {
            reporte.IdUsuario = WebSecurity.CurrentUserId;
            ReportServices.CreateReporte(reporte);
            RepositorioClases.Comments comentario = CommentsService.GetById(reporte.CommentId);
            CommentsService.CambiarEstadoComentario(reporte.CommentId, Estado.Reportado);
            return RedirectToAction("Details", "Events", new { id = comentario.EventId });
        }



        [MyAuthorize]
        [HttpGet]
        public ViewResult Details(int id)
        {
            RepositorioClases.Comments comment = CommentsService.GetById(id);
            ViewModels.Comments comentario = new ViewModels.Comments();
            comentario.Comentario = comment.Comentario;
            comentario.CommentId = comment.CommentId;
            comentario.Estado = comment.Estado;
            comentario.EventId = comment.EventId;
            comentario.Fecha = comment.Fecha;
            comentario.ComentarioPadre = comment.ComentarioPadre;
            comentario.FechaUltimaActualizacion = comment.FechaUltimaActualizacion;
            comentario.iDUsuario = comment.iDUsuario;
            Users user = UserService.Get(comment.iDUsuario).FirstOrDefault();
            comentario.Usuario = user.Nombre;
            return View(comentario);
        }
    }
}
