namespace RepositorioClases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Events
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="Evento")]
        public string NombreEvento { get; set; }

        [Required]
        [StringLength(50)]
        public string lat { get; set; }

        [Required]
        [StringLength(50)]
        public string lng { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Finalización")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }
        
        [Required]
        [Display(Name = "Usuario")]
        public int IdUser { get; set; }

        [Required]
        [Range(0, 6, ErrorMessage = "Seleccione una categoría correcta")]
        public Categorias IdCategoria { get; set; }

        [Required]
        [Display(Name = "Evento Destacado")]
        public bool Destacado { get; set; }

        [Required]
        [StringLength(200)]
        public string Direccion { get; set; }

        //[Required]
        //[StringLength(200)]
        public String RutaImagen { get; set; }

        [ForeignKey("EventId")]
        public virtual List<Comments> Comments { get; set; }

        [Required]
        public EventState Estado { get; set; }

        public Nullable<TimeSpan> HoraInicio { get; set; }

        public Nullable<TimeSpan> HoraFin { get; set; }
    }

    public enum EventState
    {
        Habilitado = 1,
        Bloqueado = 2,
        Reportado = 3,
        Eliminado = 4,
        Inhabilitado = 5
    }

    public enum Categorias
    {
        Musica = 1,
        Fiestas = 2,
        Artes = 3,
        Gastronomia = 4,
        Clases = 5,
        Deportes = 6
    }
}
