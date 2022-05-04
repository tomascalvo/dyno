namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorUserEmploymentApplications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmploymentApplications", "ApplicantId", "dbo.AspNetUsers");
            DropIndex("dbo.EmploymentApplications", new[] { "ApplicantId" });
            AlterColumn("dbo.EmploymentApplications", "ApplicantId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.EmploymentApplications", "ApplicantId");
            AddForeignKey("dbo.EmploymentApplications", "ApplicantId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentApplications", "ApplicantId", "dbo.AspNetUsers");
            DropIndex("dbo.EmploymentApplications", new[] { "ApplicantId" });
            AlterColumn("dbo.EmploymentApplications", "ApplicantId", c => c.String(maxLength: 128));
            CreateIndex("dbo.EmploymentApplications", "ApplicantId");
            AddForeignKey("dbo.EmploymentApplications", "ApplicantId", "dbo.AspNetUsers", "Id");
        }
    }
}
