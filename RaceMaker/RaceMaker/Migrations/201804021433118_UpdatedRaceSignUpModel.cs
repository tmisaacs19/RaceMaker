namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedRaceSignUpModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaceSignUps", "FirstName", c => c.String());
            AddColumn("dbo.RaceSignUps", "LastName", c => c.String());
            AddColumn("dbo.RaceSignUps", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.RaceSignUps", "TshirtSize", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaceSignUps", "TshirtSize");
            DropColumn("dbo.RaceSignUps", "Age");
            DropColumn("dbo.RaceSignUps", "LastName");
            DropColumn("dbo.RaceSignUps", "FirstName");
        }
    }
}
