namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFKFeaturesToCreateRaceandRaceSignUpClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaceSignUpCreateRaces",
                c => new
                    {
                        RaceSignUp_ID = c.Int(nullable: false),
                        CreateRace_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RaceSignUp_ID, t.CreateRace_ID })
                .ForeignKey("dbo.RaceSignUps", t => t.RaceSignUp_ID, cascadeDelete: true)
                .ForeignKey("dbo.CreateRaces", t => t.CreateRace_ID, cascadeDelete: true)
                .Index(t => t.RaceSignUp_ID)
                .Index(t => t.CreateRace_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaceSignUpCreateRaces", "CreateRace_ID", "dbo.CreateRaces");
            DropForeignKey("dbo.RaceSignUpCreateRaces", "RaceSignUp_ID", "dbo.RaceSignUps");
            DropIndex("dbo.RaceSignUpCreateRaces", new[] { "CreateRace_ID" });
            DropIndex("dbo.RaceSignUpCreateRaces", new[] { "RaceSignUp_ID" });
            DropTable("dbo.RaceSignUpCreateRaces");
        }
    }
}
