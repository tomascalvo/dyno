namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecruiterEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecruiterClients",
                c => new
                    {
                        RecruiterId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RecruiterId, t.CompanyId })
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Recruiters", t => t.RecruiterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .Index(t => t.RecruiterId)
                .Index(t => t.CompanyId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Recruiters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 50),
                        StaffingCompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.StaffingCompanyId)
                .Index(t => t.StaffingCompanyId);
            
            AddColumn("dbo.EmploymentListings", "RecruiterId", c => c.Int());
            CreateIndex("dbo.EmploymentListings", "RecruiterId");
            AddForeignKey("dbo.EmploymentListings", "RecruiterId", "dbo.Recruiters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecruiterClients", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Recruiters", "StaffingCompanyId", "dbo.Companies");
            DropForeignKey("dbo.RecruiterClients", "RecruiterId", "dbo.Recruiters");
            DropForeignKey("dbo.EmploymentListings", "RecruiterId", "dbo.Recruiters");
            DropForeignKey("dbo.RecruiterClients", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Recruiters", new[] { "StaffingCompanyId" });
            DropIndex("dbo.RecruiterClients", new[] { "CreatorId" });
            DropIndex("dbo.RecruiterClients", new[] { "CompanyId" });
            DropIndex("dbo.RecruiterClients", new[] { "RecruiterId" });
            DropIndex("dbo.EmploymentListings", new[] { "RecruiterId" });
            DropColumn("dbo.EmploymentListings", "RecruiterId");
            DropTable("dbo.Recruiters");
            DropTable("dbo.RecruiterClients");
        }
    }
}
