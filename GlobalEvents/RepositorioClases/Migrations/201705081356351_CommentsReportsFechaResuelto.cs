namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsReportsFechaResuelto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentsReportes", "Fecha", c => c.DateTime(nullable: false));
            AddColumn("dbo.CommentsReportes", "Resuelto", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentsReportes", "Resuelto");
            DropColumn("dbo.CommentsReportes", "Fecha");
        }
    }
}
