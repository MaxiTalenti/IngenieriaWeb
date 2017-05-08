namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsReportes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentsReportes",
                c => new
                    {
                        ReporteId = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReporteId)
                .ForeignKey("dbo.Comments", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentsReportes", "IdUsuario", "dbo.Users");
            DropForeignKey("dbo.CommentsReportes", "IdUsuario", "dbo.Comments");
            DropIndex("dbo.CommentsReportes", new[] { "IdUsuario" });
            DropTable("dbo.CommentsReportes");
        }
    }
}
