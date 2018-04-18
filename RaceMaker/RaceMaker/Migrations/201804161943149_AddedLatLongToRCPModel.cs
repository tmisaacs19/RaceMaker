namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatLongToRCPModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaceCoursePoints", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.RaceCoursePoints", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaceCoursePoints", "Longitude");
            DropColumn("dbo.RaceCoursePoints", "Latitude");
        }
    }
}
