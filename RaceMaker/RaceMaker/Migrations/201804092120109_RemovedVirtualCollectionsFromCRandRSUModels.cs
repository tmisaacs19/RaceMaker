namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedVirtualCollectionsFromCRandRSUModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RaceSignUpCreateRaces", "RaceSignUp_ID", "dbo.RaceSignUps");
            DropForeignKey("dbo.RaceSignUpCreateRaces", "CreateRace_ID", "dbo.CreateRaces");
            DropIndex("dbo.RaceSignUpCreateRaces", new[] { "RaceSignUp_ID" });
            DropIndex("dbo.RaceSignUpCreateRaces", new[] { "CreateRace_ID" });
            DropTable("dbo.RaceSignUpCreateRaces");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RaceSignUpCreateRaces",
                c => new
                    {
                        RaceSignUp_ID = c.Int(nullable: false),
                        CreateRace_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RaceSignUp_ID, t.CreateRace_ID });
            
            CreateIndex("dbo.RaceSignUpCreateRaces", "CreateRace_ID");
            CreateIndex("dbo.RaceSignUpCreateRaces", "RaceSignUp_ID");
            AddForeignKey("dbo.RaceSignUpCreateRaces", "CreateRace_ID", "dbo.CreateRaces", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RaceSignUpCreateRaces", "RaceSignUp_ID", "dbo.RaceSignUps", "ID", cascadeDelete: true);
        }
    }
}
