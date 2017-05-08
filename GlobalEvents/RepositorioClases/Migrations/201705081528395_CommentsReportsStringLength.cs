namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsReportsStringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommentsReportes", "Observacion", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommentsReportes", "Observacion", c => c.String());
        }
    }
}
