namespace RepositorioClases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Users
    {
        public Int32 Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }

        public string Usuario { get; set; }

        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        //public virtual Roles Roles { get; set; }
    }
}
