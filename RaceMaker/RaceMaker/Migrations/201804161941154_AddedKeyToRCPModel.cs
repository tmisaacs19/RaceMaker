namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedKeyToRCPModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaceCoursePoints",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PointID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.CreateRaces", "RaceCoursePoint_ID", c => c.Int());
            CreateIndex("dbo.CreateRaces", "RaceCoursePoint_ID");
            AddForeignKey("dbo.CreateRaces", "RaceCoursePoint_ID", "dbo.RaceCoursePoints", "ID");
            DropColumn("dbo.CreateRaces", "RaceCoursePoint_PointID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreateRaces", "RaceCoursePoint_PointID", c => c.Int(nullable: false));
            DropForeignKey("dbo.CreateRaces", "RaceCoursePoint_ID", "dbo.RaceCoursePoints");
            DropIndex("dbo.CreateRaces", new[] { "RaceCoursePoint_ID" });
            DropColumn("dbo.CreateRaces", "RaceCoursePoint_ID");
            DropTable("dbo.RaceCoursePoints");
        }
    }
}
