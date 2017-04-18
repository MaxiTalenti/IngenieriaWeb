namespace RepositorioClases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Events
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreEvento { get; set; }

        [StringLength(50)]
        public string lat { get; set; }

        [StringLength(50)]
        public string lng { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
        
        [Required]
        public int IdUser { get; set; }

        [Required]
        public EventState Estado { get; set; }
    }

    public enum EventState
    {
        Habilitado = 1,
        Bloqueado = 2,
        Reportado = 3,
        Eliminado = 4,
        Inhabilitado = 5
    }
}
