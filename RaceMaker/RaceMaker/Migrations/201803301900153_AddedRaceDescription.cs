namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRaceDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "RaceDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "RaceDescription");
        }
    }
}
