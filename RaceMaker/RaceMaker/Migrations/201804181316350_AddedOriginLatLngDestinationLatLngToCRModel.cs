namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOriginLatLngDestinationLatLngToCRModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "OriginLat", c => c.Double(nullable: false));
            AddColumn("dbo.CreateRaces", "OriginLng", c => c.Double(nullable: false));
            AddColumn("dbo.CreateRaces", "DestinationLat", c => c.Double(nullable: false));
            AddColumn("dbo.CreateRaces", "DestinationLng", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "DestinationLng");
            DropColumn("dbo.CreateRaces", "DestinationLat");
            DropColumn("dbo.CreateRaces", "OriginLng");
            DropColumn("dbo.CreateRaces", "OriginLat");
        }
    }
}
