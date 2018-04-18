namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageNameFieldToCreateRaceModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "ImageName");
        }
    }
}
