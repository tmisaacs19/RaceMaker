namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAdditionalInformationtoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "AdditionalInformation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "AdditionalInformation");
        }
    }
}
