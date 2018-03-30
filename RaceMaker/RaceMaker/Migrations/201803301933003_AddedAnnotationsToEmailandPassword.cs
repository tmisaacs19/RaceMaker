namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnnotationsToEmailandPassword : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreateRaces", "AdminEmail", c => c.String(nullable: false));
            AlterColumn("dbo.CreateRaces", "AdminPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreateRaces", "AdminPassword", c => c.String());
            AlterColumn("dbo.CreateRaces", "AdminEmail", c => c.String());
        }
    }
}
