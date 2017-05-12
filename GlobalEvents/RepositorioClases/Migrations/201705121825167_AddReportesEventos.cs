namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReportesEventos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventsReportes",
                c => new
                    {
                        ReporteId = c.Int(nullable: false, identity: true),
                        EventId = c.Long(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        Observacion = c.String(nullable: false, maxLength: 300),
                        Fecha = c.DateTime(nullable: false),
                        Resuelto = c.Boolean(),
                    })
                .PrimaryKey(t => t.ReporteId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventsReportes", "IdUsuario", "dbo.Users");
            DropForeignKey("dbo.EventsReportes", "EventId", "dbo.Events");
            DropIndex("dbo.EventsReportes", new[] { "IdUsuario" });
            DropIndex("dbo.EventsReportes", new[] { "EventId" });
            DropTable("dbo.EventsReportes");
        }
    }
}
