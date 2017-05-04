namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Like", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UnLike", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "Estado", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Comentario", c => c.String(nullable: false, maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Comentario", c => c.String(nullable: false));
            DropColumn("dbo.Comments", "Estado");
            DropColumn("dbo.Comments", "UnLike");
            DropColumn("dbo.Comments", "Like");
        }
    }
}
