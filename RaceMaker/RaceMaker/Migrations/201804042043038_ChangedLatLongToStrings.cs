namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLatLongToStrings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreateRaces", "lat", c => c.String());
            AlterColumn("dbo.CreateRaces", "lng", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreateRaces", "lng", c => c.Single(nullable: false));
            AlterColumn("dbo.CreateRaces", "lat", c => c.Single(nullable: false));
        }
    }
}
