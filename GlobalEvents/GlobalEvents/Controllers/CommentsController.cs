using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Servicios;
using RepositorioClases;

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
    }
}
