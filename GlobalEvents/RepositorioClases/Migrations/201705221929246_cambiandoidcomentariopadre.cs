namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiandoidcomentariopadre : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "ComentarioPadre", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "ComentarioPadre", c => c.Int());
        }
    }
}
