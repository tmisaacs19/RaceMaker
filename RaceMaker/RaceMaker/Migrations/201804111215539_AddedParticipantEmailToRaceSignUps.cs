namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParticipantEmailToRaceSignUps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaceSignUps", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaceSignUps", "Email");
        }
    }
}
