namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreateRacesController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreateRaces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RaceName = c.String(),
                        RaceLocation = c.String(),
                        RaceDate = c.DateTime(nullable: false),
                        RaceDistance = c.Int(nullable: false),
                        RaceCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CreateRaces");
        }
    }
}
