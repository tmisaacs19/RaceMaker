namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPrimaryKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRaces",
                c => new
                    {
                        UserRaceID = c.Int(nullable: false, identity: true),
                        RaceID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRaceID)
                .ForeignKey("dbo.CreateRaces", t => t.RaceID, cascadeDelete: true)
                .ForeignKey("dbo.RaceSignUps", t => t.UserID, cascadeDelete: true)
                .Index(t => t.RaceID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRaces", "UserID", "dbo.RaceSignUps");
            DropForeignKey("dbo.UserRaces", "RaceID", "dbo.CreateRaces");
            DropIndex("dbo.UserRaces", new[] { "UserID" });
            DropIndex("dbo.UserRaces", new[] { "RaceID" });
            DropTable("dbo.UserRaces");
        }
    }
}
