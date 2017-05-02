using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RepositorioClases
{
    public partial class Comments
    {
        [Required]
        [Display(Name = "Usuario creador")]
        public int iDUsuario { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Número evento")]
        public int iDEvento { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public DateTime FechaUltimaActualizacion { get; set; }

        public int ComentarioPAdre { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "No se permite que el comentario sea mayor a los 5000 carácteres")]
        public int Comentario { get; set; }

    }
}
