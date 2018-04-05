namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatLongAsStrings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "lat", c => c.String());
            AddColumn("dbo.CreateRaces", "lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "lng");
            DropColumn("dbo.CreateRaces", "lat");
        }
    }
}
