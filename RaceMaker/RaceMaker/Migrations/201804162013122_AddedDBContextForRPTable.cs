namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDBContextForRPTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaceCoursePoints",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PointNumber = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        CreateRaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CreateRaces", t => t.CreateRaceID, cascadeDelete: true)
                .Index(t => t.CreateRaceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaceCoursePoints", "CreateRaceID", "dbo.CreateRaces");
            DropIndex("dbo.RaceCoursePoints", new[] { "CreateRaceID" });
            DropTable("dbo.RaceCoursePoints");
        }
    }
}
