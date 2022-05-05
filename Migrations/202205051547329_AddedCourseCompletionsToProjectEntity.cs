namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourseCompletionsToProjectEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectCourseCompletions",
                c => new
                    {
                        Project_Id = c.Int(nullable: false),
                        CourseCompletion_CourseId = c.Int(nullable: false),
                        CourseCompletion_UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.CourseCompletion_CourseId, t.CourseCompletion_UserId })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.CourseCompletions", t => new { t.CourseCompletion_CourseId, t.CourseCompletion_UserId }, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => new { t.CourseCompletion_CourseId, t.CourseCompletion_UserId });
            
            AddColumn("dbo.Courses", "Image", c => c.String());
            AddColumn("dbo.Courses", "IsTutorial", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "AddedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.Projects", "AddedById");
            AddForeignKey("dbo.Projects", "AddedById", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectCourseCompletions", new[] { "CourseCompletion_CourseId", "CourseCompletion_UserId" }, "dbo.CourseCompletions");
            DropForeignKey("dbo.ProjectCourseCompletions", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Projects", "AddedById", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectCourseCompletions", new[] { "CourseCompletion_CourseId", "CourseCompletion_UserId" });
            DropIndex("dbo.ProjectCourseCompletions", new[] { "Project_Id" });
            DropIndex("dbo.Projects", new[] { "AddedById" });
            DropColumn("dbo.Projects", "AddedById");
            DropColumn("dbo.Courses", "IsTutorial");
            DropColumn("dbo.Courses", "Image");
            DropTable("dbo.ProjectCourseCompletions");
        }
    }
}
