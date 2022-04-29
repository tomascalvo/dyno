namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeEmploymentOfferRecruiterIdNullableAndOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmploymentOffers", "RecruiterId", "dbo.Recruiters");
            DropIndex("dbo.EmploymentOffers", new[] { "RecruiterId" });
            AlterColumn("dbo.EmploymentOffers", "RecruiterId", c => c.Int());
            CreateIndex("dbo.EmploymentOffers", "RecruiterId");
            AddForeignKey("dbo.EmploymentOffers", "RecruiterId", "dbo.Recruiters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentOffers", "RecruiterId", "dbo.Recruiters");
            DropIndex("dbo.EmploymentOffers", new[] { "RecruiterId" });
            AlterColumn("dbo.EmploymentOffers", "RecruiterId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmploymentOffers", "RecruiterId");
            AddForeignKey("dbo.EmploymentOffers", "RecruiterId", "dbo.Recruiters", "Id", cascadeDelete: true);
        }
    }
}
