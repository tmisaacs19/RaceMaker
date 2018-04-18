namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImagePathFieldToCRModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "ImagePath");
        }
    }
}
