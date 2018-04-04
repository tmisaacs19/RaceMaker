namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequirementOnRaceAdminPassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreateRaces", "AdminPassword", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreateRaces", "AdminPassword", c => c.String(nullable: false));
        }
    }
}
