namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        iDUsuario = c.Int(nullable: false),
                        EventId = c.Long(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        FechaUltimaActualizacion = c.DateTime(nullable: false),
                        ComentarioPAdre = c.Int(nullable: false),
                        Comentario = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "EventId", "dbo.Events");
            DropTable("dbo.Comments");
        }
    }
}
