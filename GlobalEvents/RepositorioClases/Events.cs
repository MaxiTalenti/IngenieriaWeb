namespace RepositorioClases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Events
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string EventName { get; set; }

        [StringLength(50)]
        public string lat { get; set; }

        [StringLength(50)]
        public string lng { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
