namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedRaceSignUpModelToIncludeGenderAndDateOfBirth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaceSignUps", "Gender", c => c.String(nullable: false));
            AddColumn("dbo.RaceSignUps", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RaceSignUps", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.RaceSignUps", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.RaceSignUps", "TshirtSize", c => c.String(nullable: false));
            DropColumn("dbo.RaceSignUps", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaceSignUps", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.RaceSignUps", "TshirtSize", c => c.String());
            AlterColumn("dbo.RaceSignUps", "LastName", c => c.String());
            AlterColumn("dbo.RaceSignUps", "FirstName", c => c.String());
            DropColumn("dbo.RaceSignUps", "DateOfBirth");
            DropColumn("dbo.RaceSignUps", "Gender");
        }
    }
}
