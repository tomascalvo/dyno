namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStaffingCompanyToEmploymentListing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmploymentListings", "ClientCompanyId", "dbo.Companies");
            AddColumn("dbo.EmploymentListings", "StaffingCompanyId", c => c.Int());
            CreateIndex("dbo.EmploymentListings", "StaffingCompanyId");
            AddForeignKey("dbo.EmploymentListings", "StaffingCompanyId", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentListings", "StaffingCompanyId", "dbo.Companies");
            DropIndex("dbo.EmploymentListings", new[] { "StaffingCompanyId" });
            DropColumn("dbo.EmploymentListings", "StaffingCompanyId");
            AddForeignKey("dbo.EmploymentListings", "ClientCompanyId", "dbo.Companies", "Id");
        }
    }
}
