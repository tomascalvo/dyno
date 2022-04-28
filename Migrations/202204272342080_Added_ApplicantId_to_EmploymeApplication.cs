namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ApplicantId_to_EmploymeApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentApplications", "ApplicantId", c => c.String(maxLength: 128));
            AlterColumn("dbo.EmploymentApplications", "DateApplied", c => c.DateTime(nullable: false));
            CreateIndex("dbo.EmploymentApplications", "ApplicantId");
            AddForeignKey("dbo.EmploymentApplications", "ApplicantId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentApplications", "ApplicantId", "dbo.AspNetUsers");
            DropIndex("dbo.EmploymentApplications", new[] { "ApplicantId" });
            AlterColumn("dbo.EmploymentApplications", "DateApplied", c => c.DateTime());
            DropColumn("dbo.EmploymentApplications", "ApplicantId");
        }
    }
}
