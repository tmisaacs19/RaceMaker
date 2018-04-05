namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedGoogleMapsCreateModelInfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CreateRaces", "Address");
            DropColumn("dbo.CreateRaces", "City");
            DropColumn("dbo.CreateRaces", "State");
            DropColumn("dbo.CreateRaces", "ZipCode");
            DropColumn("dbo.CreateRaces", "lat");
            DropColumn("dbo.CreateRaces", "lng");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreateRaces", "lng", c => c.String());
            AddColumn("dbo.CreateRaces", "lat", c => c.String());
            AddColumn("dbo.CreateRaces", "ZipCode", c => c.Int(nullable: false));
            AddColumn("dbo.CreateRaces", "State", c => c.String());
            AddColumn("dbo.CreateRaces", "City", c => c.String());
            AddColumn("dbo.CreateRaces", "Address", c => c.String());
        }
    }
}
