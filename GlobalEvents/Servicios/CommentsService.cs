using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositorioClases;

namespace Servicios
{
    public static class CommentsService
    {
        public static List<Comments> ObtenerComentarios(long idEvento)
        {
            using (Modelo context = new Modelo())
            {
                return context.Comments.Where(z => z.EventId == idEvento)
                    .Where(z => z.Estado != Estado.Eliminado && z.Estado != Estado.Bloqueado)
                    .ToList();
            }
        }

        public static List<CommentsReportes> ObtenerComentariosReportados()
        {
            using (Modelo context = new Modelo())
            {
                return context.CommentsReportes.Where(z => z.Resuelto == false).ToList();
            }
        }

        public static Comments GetById(long id)
        {
            using (Modelo context = new Modelo())
            {
                return context.Comments.SingleOrDefault(c => c.CommentId == id);
            }
        }

        public static void Create(Comments comments)
        {
            using (Modelo context = new Modelo())
            {
                context.Comments.Add(new Comments()
                {
                    iDUsuario = comments.iDUsuario,
                    EventId = comments.EventId,
                    Fecha = comments.Fecha,
                    FechaUltimaActualizacion = comments.FechaUltimaActualizacion,
                    ComentarioPadre = comments.ComentarioPadre,
                    Comentario = comments.Comentario,
                    Like = 0,
                    UnLike = 0,
                    Estado = Estado.Activo
                });
                context.SaveChanges();
            }
        }

        public static void Delete(Comments Comment)
        {
            using (Modelo context = new Modelo())
            {
                Comments com = context.Comments.Where(u => u.CommentId == Comment.CommentId).FirstOrDefault();

                // FirstOrDefault va a intentar recuperar el registro que cumpla la condición
                // si no encuentra ninguno, devuelve NULL, de ahí el siguiente IF.
                if (com != null)
                    com.Estado = Estado.Eliminado;

                // el objeto en memoria persiste los cambios en la base de datos cuando hago un save sobre el contexto.
                context.SaveChanges();
            }
        }

        public static void Edit(Comments Comment)
        {
            using (Modelo context = new Modelo())
            {
                Comments com = context.Comments.Where(u => u.CommentId == Comment.CommentId).FirstOrDefault();
                if (com != null)
                {
                    com.Comentario = Comment.Comentario;
                    com.Estado = Comment.Estado;
                    com.FechaUltimaActualizacion = DateTime.Now;
                }

                context.SaveChanges();
            }
        }

        public static void Like(int CommentId)
        {
            using (Modelo context = new Modelo())
            {
                Comments com = context.Comments.Where(u => u.CommentId == CommentId).FirstOrDefault();
                if (com != null)
                    com.Like = com.Like + 1;

                context.SaveChanges();
            }
        }

        public static void UnLike(int CommentId)
        {
            using (Modelo context = new Modelo())
            {
                Comments com = context.Comments.Where(u => u.CommentId == CommentId).FirstOrDefault();
                if (com != null)
                    com.UnLike = com.UnLike + 1;

                context.SaveChanges();
            }
        }
    }
}
