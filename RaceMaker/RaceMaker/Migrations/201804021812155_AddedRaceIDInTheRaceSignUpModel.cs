namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRaceIDInTheRaceSignUpModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaceSignUps", "RaceID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaceSignUps", "RaceID");
        }
    }
}
