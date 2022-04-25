namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(),
                        Logo = c.String(),
                        WebsiteUrl = c.String(),
                        Country = c.String(),
                        StateProvince = c.String(),
                        City = c.String(),
                        DateFounded = c.DateTime(),
                        DateAdded = c.DateTime(nullable: false),
                        OrganizationLookupId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmploymentListings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        PayQuantity = c.Decimal(storeType: "money"),
                        Currency = c.Int(),
                        PayFrequency = c.Int(),
                        IsRemote = c.Boolean(),
                        ClientCompanyId = c.Int(nullable: false),
                        FullText = c.String(),
                        Url = c.String(),
                        DatePublished = c.DateTime(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.ClientCompanyId, cascadeDelete: true)
                .Index(t => t.ClientCompanyId);
            
            CreateTable(
                "dbo.EmploymentApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmploymentListingId = c.Int(nullable: false),
                        Comment = c.String(),
                        CoverLetter = c.String(),
                        DateApplied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .Index(t => t.EmploymentListingId);
            
            CreateTable(
                "dbo.EmploymentListingSkills",
                c => new
                    {
                        EmploymentListingId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmploymentListingId, t.SkillId })
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.EmploymentListingId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icon = c.String(),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Developer = c.String(maxLength: 250),
                        ReleaseDate = c.DateTime(),
                        DateAdded = c.DateTime(),
                        RepositoryUrl = c.String(),
                        DocumentationUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectSkills",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.SkillId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Icon = c.String(),
                        RepositoryUrl = c.String(),
                        DeploymentUrl = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EmploymentListings", "ClientCompanyId", "dbo.Companies");
            DropForeignKey("dbo.EmploymentListingSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ProjectSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ProjectSkills", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmploymentListingSkills", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentApplications", "EmploymentListingId", "dbo.EmploymentListings");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProjectSkills", new[] { "SkillId" });
            DropIndex("dbo.ProjectSkills", new[] { "ProjectId" });
            DropIndex("dbo.EmploymentListingSkills", new[] { "SkillId" });
            DropIndex("dbo.EmploymentListingSkills", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentApplications", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentListings", new[] { "ClientCompanyId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.EmploymentListingSkills");
            DropTable("dbo.EmploymentApplications");
            DropTable("dbo.EmploymentListings");
            DropTable("dbo.Companies");
        }
    }
}
