namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRCPCollectionFromCreateRaceModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreateRaces", "RaceCoursePoint_ID", "dbo.RaceCoursePoints");
            DropIndex("dbo.CreateRaces", new[] { "RaceCoursePoint_ID" });
            DropColumn("dbo.CreateRaces", "RaceCoursePoint_ID");
            DropTable("dbo.RaceCoursePoints");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RaceCoursePoints",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PointID = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.CreateRaces", "RaceCoursePoint_ID", c => c.Int());
            CreateIndex("dbo.CreateRaces", "RaceCoursePoint_ID");
            AddForeignKey("dbo.CreateRaces", "RaceCoursePoint_ID", "dbo.RaceCoursePoints", "ID");
        }
    }
}
