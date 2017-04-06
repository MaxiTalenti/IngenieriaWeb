namespace RepositorioClases
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Modelo : DbContext
    {
        public Modelo()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
