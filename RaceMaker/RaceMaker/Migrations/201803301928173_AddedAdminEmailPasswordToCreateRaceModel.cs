namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAdminEmailPasswordToCreateRaceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaceSignUps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.CreateRaces", "AdminEmail", c => c.String());
            AddColumn("dbo.CreateRaces", "AdminPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "AdminPassword");
            DropColumn("dbo.CreateRaces", "AdminEmail");
            DropTable("dbo.RaceSignUps");
        }
    }
}
