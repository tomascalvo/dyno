namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompanyEmploymentListingEmploymentApplicationDomainModels : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentListings", "ClientCompanyId", "dbo.Companies");
            DropForeignKey("dbo.EmploymentListingSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.EmploymentListingSkills", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentApplications", "EmploymentListingId", "dbo.EmploymentListings");
            DropIndex("dbo.EmploymentListingSkills", new[] { "SkillId" });
            DropIndex("dbo.EmploymentListingSkills", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentApplications", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentListings", new[] { "ClientCompanyId" });
            DropTable("dbo.EmploymentListingSkills");
            DropTable("dbo.EmploymentApplications");
            DropTable("dbo.EmploymentListings");
            DropTable("dbo.Companies");
        }
    }
}
