using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorioClases
{
    public partial class Comments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public virtual Events Event { get; set; }

    }

    public enum Estado
    {
        Activo = 1,
        Reportado = 2,
        Bloqueado = 3,
        Eliminado = 4
    }
}
