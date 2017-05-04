namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifTipoDatoComPadre : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "ComentarioPadre", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "ComentarioPadre", c => c.Int(nullable: false));
        }
    }
}
