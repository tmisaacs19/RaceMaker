namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourseLatAndLngToCRModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "CourseLat", c => c.Double(nullable: false));
            AddColumn("dbo.CreateRaces", "CourseLng", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "CourseLng");
            DropColumn("dbo.CreateRaces", "CourseLat");
        }
    }
}
