namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataAnnotationsToCreateRaceModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreateRaces", "RaceName", c => c.String(nullable: false));
            AlterColumn("dbo.CreateRaces", "RaceLocation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreateRaces", "RaceLocation", c => c.String());
            AlterColumn("dbo.CreateRaces", "RaceName", c => c.String());
        }
    }
}
