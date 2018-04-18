namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRCPPropertyToCreateRaceModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "RaceCoursePoint_PointID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "RaceCoursePoint_PointID");
        }
    }
}
