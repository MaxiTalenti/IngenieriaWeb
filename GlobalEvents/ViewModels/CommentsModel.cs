using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RepositorioClases;

namespace ViewModels
{
    public class CommentsModel
    {
        public class Comments
        {
            public int CommentId { get; set; }

            [Required]
            [Display(Name = "Usuario creador")]
            public int iDUsuario { get; set; }

            [Required]
            [Display(Name = "Número evento")]
            public long EventId { get; set; }

            [Required]
            public DateTime Fecha { get; set; }

            [Required]
            public DateTime FechaUltimaActualizacion { get; set; }

            public int ComentarioPadre { get; set; }

            [Required]
            [StringLength(3000, ErrorMessage = "No se permite que el comentario sea mayor a los 5000 carácteres")]
            public string Comentario { get; set; }

            public int Like { get; set; }

            public int UnLike { get; set; }

            public Estado Estado { get; set; }
        }
    }
}
