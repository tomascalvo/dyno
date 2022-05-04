namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Url = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        AddedById = c.String(maxLength: 128),
                        PlatformId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platforms", t => t.PlatformId)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .Index(t => t.AddedById)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.CourseCompletions",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        DateAdded = c.DateTime(nullable: false),
                        DateCompleted = c.DateTime(nullable: false),
                        Rating = c.Int(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => new { t.CourseId, t.UserId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CourseSkills",
                c => new
                    {
                        CourseRefId = c.Int(nullable: false),
                        SkillRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseRefId, t.SkillRefId })
                .ForeignKey("dbo.Courses", t => t.CourseRefId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillRefId, cascadeDelete: true)
                .Index(t => t.CourseRefId)
                .Index(t => t.SkillRefId);
            
            AddColumn("dbo.CertificationTypes", "CourseId", c => c.Int());
            AddColumn("dbo.EmploymentApplications", "Rating", c => c.Int());
            CreateIndex("dbo.CertificationTypes", "CourseId");
            AddForeignKey("dbo.CertificationTypes", "CourseId", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseCompletions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CertificationTypes", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseSkills", "SkillRefId", "dbo.Skills");
            DropForeignKey("dbo.CourseSkills", "CourseRefId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.CourseCompletions", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseSkills", new[] { "SkillRefId" });
            DropIndex("dbo.CourseSkills", new[] { "CourseRefId" });
            DropIndex("dbo.CourseCompletions", new[] { "UserId" });
            DropIndex("dbo.CourseCompletions", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "PlatformId" });
            DropIndex("dbo.Courses", new[] { "AddedById" });
            DropIndex("dbo.CertificationTypes", new[] { "CourseId" });
            DropColumn("dbo.EmploymentApplications", "Rating");
            DropColumn("dbo.CertificationTypes", "CourseId");
            DropTable("dbo.CourseSkills");
            DropTable("dbo.CourseCompletions");
            DropTable("dbo.Courses");
        }
    }
}
