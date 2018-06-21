namespace RaceMaker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreateRaces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RaceName = c.String(nullable: false),
                        RaceLocation = c.String(nullable: false),
                        RaceDate = c.DateTime(nullable: false),
                        RaceDistance = c.Int(nullable: false),
                        RaceCost = c.Double(nullable: false),
                        RaceDescription = c.String(),
                        AdminEmail = c.String(nullable: false),
                        AdminPassword = c.String(),
                        FilePath = c.String(),
                        FileName = c.String(),
                        AdditionalInformation = c.String(),
                        ImagePath = c.String(),
                        ImageName = c.String(),
                        CenterLat = c.Double(nullable: false),
                        CenterLong = c.Double(nullable: false),
                        CourseLat = c.Double(nullable: false),
                        CourseLng = c.Double(nullable: false),
                        OriginLat = c.Double(nullable: false),
                        OriginLng = c.Double(nullable: false),
                        DestinationLat = c.Double(nullable: false),
                        DestinationLng = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RaceCoursePoints",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PointNumber = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        CreateRaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CreateRaces", t => t.CreateRaceID, cascadeDelete: true)
                .Index(t => t.CreateRaceID);
            
            CreateTable(
                "dbo.RaceSignUps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        TshirtSize = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        RaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRaces",
                c => new
                    {
                        UserRaceID = c.Int(nullable: false, identity: true),
                        RaceID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRaceID)
                .ForeignKey("dbo.CreateRaces", t => t.RaceID, cascadeDelete: true)
                .ForeignKey("dbo.RaceSignUps", t => t.UserID, cascadeDelete: true)
                .Index(t => t.RaceID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RaceSignUpCreateRaces",
                c => new
                    {
                        RaceSignUp_ID = c.Int(nullable: false),
                        CreateRace_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RaceSignUp_ID, t.CreateRace_ID })
                .ForeignKey("dbo.RaceSignUps", t => t.RaceSignUp_ID, cascadeDelete: true)
                .ForeignKey("dbo.CreateRaces", t => t.CreateRace_ID, cascadeDelete: true)
                .Index(t => t.RaceSignUp_ID)
                .Index(t => t.CreateRace_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserRaces", "UserID", "dbo.RaceSignUps");
            DropForeignKey("dbo.UserRaces", "RaceID", "dbo.CreateRaces");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RaceSignUpCreateRaces", "CreateRace_ID", "dbo.CreateRaces");
            DropForeignKey("dbo.RaceSignUpCreateRaces", "RaceSignUp_ID", "dbo.RaceSignUps");
            DropForeignKey("dbo.RaceCoursePoints", "CreateRaceID", "dbo.CreateRaces");
            DropIndex("dbo.RaceSignUpCreateRaces", new[] { "CreateRace_ID" });
            DropIndex("dbo.RaceSignUpCreateRaces", new[] { "RaceSignUp_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserRaces", new[] { "UserID" });
            DropIndex("dbo.UserRaces", new[] { "RaceID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RaceCoursePoints", new[] { "CreateRaceID" });
            DropTable("dbo.RaceSignUpCreateRaces");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserRaces");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RaceSignUps");
            DropTable("dbo.RaceCoursePoints");
            DropTable("dbo.CreateRaces");
        }
    }
}
