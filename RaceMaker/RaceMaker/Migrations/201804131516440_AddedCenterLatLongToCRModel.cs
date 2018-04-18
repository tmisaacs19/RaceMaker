namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCenterLatLongToCRModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "CenterLat", c => c.Double(nullable: false));
            AddColumn("dbo.CreateRaces", "CenterLong", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "CenterLong");
            DropColumn("dbo.CreateRaces", "CenterLat");
        }
    }
}
