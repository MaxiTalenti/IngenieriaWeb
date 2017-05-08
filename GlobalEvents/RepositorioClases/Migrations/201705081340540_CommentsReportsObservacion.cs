namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsReportsObservacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentsReportes", "Observacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentsReportes", "Observacion");
        }
    }
}
