namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileNameandPathModelFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreateRaces", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreateRaces", "FileName");
        }
    }
}
