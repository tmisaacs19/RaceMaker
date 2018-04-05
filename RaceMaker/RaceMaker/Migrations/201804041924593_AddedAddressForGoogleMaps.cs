namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddressForGoogleMaps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "Address", c => c.String());
            AddColumn("dbo.CreateRaces", "City", c => c.String());
            AddColumn("dbo.CreateRaces", "State", c => c.String());
            AddColumn("dbo.CreateRaces", "ZipCode", c => c.Int(nullable: false));
            AddColumn("dbo.CreateRaces", "lat", c => c.Single(nullable: false));
            AddColumn("dbo.CreateRaces", "lng", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "lng");
            DropColumn("dbo.CreateRaces", "lat");
            DropColumn("dbo.CreateRaces", "ZipCode");
            DropColumn("dbo.CreateRaces", "State");
            DropColumn("dbo.CreateRaces", "City");
            DropColumn("dbo.CreateRaces", "Address");
        }
    }
}
