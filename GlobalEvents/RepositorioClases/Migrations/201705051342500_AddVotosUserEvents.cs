namespace RepositorioClases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVotosUserEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VotosUsersEvents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        IdEvent = c.Long(nullable: false),
                        Voto = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.IdEvent, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdEvent);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotosUsersEvents", "IdUser", "dbo.Users");
            DropForeignKey("dbo.VotosUsersEvents", "IdEvent", "dbo.Events");
            DropIndex("dbo.VotosUsersEvents", new[] { "IdEvent" });
            DropIndex("dbo.VotosUsersEvents", new[] { "IdUser" });
            DropTable("dbo.VotosUsersEvents");
        }
    }
}
