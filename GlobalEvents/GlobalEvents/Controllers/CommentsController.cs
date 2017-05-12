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
    public class CommentsController : Controller
    {
        public ActionResult ComentariosReportados()
        {
            //var comments = CommentsService.ObtenerComentariosReportados().Select(u => new ViewModels.ComentariosEventosModeracionModel()
            //{
            //    IdUsuario = u.IdUsuario,
            //    CommentId = u.CommentId,
            //    //EventId = u.,
            //    Observacion = u.Observacion,
            //    //Usuario = u.IdUsuario;
            //    //Evento = u.Evento
            //    Fecha = u.Fecha
            //}).ToList();
            var comments = CommentsService.ObtenerComentariosReportados();
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

        public ActionResult ReportarComentario(int? id) 
        {
            CommentsReportes reporte = new CommentsReportes{ CommentId = (int)id };
            return View("ReportarComentario", reporte);
        }

        [HttpPost]
        public ActionResult ReportarComentario(CommentsReportes reporte)
        {
            reporte.IdUsuario = WebSecurity.CurrentUserId;
            CommentsService.CreateReporte(reporte);
            RepositorioClases.Comments comentario = CommentsService.GetById(reporte.CommentId);
            CommentsService.CambiarEstadoComentario(reporte.CommentId, Estado.Reportado);
            return RedirectToAction("Details", "Events", new { id = comentario.EventId });
        }



        public ViewResult Details(int id)
        {
            RepositorioClases.Comments comment = CommentsService.GetById(id);
            ViewModels.Comments comentario = new ViewModels.Comments();
            comentario.Comentario = comment.Comentario;
            comentario.CommentId = comment.CommentId;
            comentario.Estado = Estado.Reportado;
            comentario.EventId = comment.EventId;
            comentario.Fecha = comment.Fecha;
            comentario.FechaUltimaActualizacion = comment.FechaUltimaActualizacion;
            comentario.iDUsuario = comment.iDUsuario;
            Users user = UserService.Get(comment.iDUsuario).FirstOrDefault();
            comentario.Usuario = user.Nombre;
            return View(comentario);
        }
    }
}
