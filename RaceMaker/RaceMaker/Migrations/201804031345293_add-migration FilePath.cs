namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationFilePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "FilePath");
        }
    }
}
