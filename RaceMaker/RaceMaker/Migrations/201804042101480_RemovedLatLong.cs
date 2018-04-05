namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLatLong : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CreateRaces", "lat");
            DropColumn("dbo.CreateRaces", "lng");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreateRaces", "lng", c => c.String());
            AddColumn("dbo.CreateRaces", "lat", c => c.String());
        }
    }
}
