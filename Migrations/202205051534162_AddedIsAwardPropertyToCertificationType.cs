namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsAwardPropertyToCertificationType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Certifications", "ProjectId", c => c.Int());
            AddColumn("dbo.CertificationTypes", "IsAward", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Certifications", "ProjectId");
            AddForeignKey("dbo.Certifications", "ProjectId", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Certifications", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Certifications", new[] { "ProjectId" });
            DropColumn("dbo.CertificationTypes", "IsAward");
            DropColumn("dbo.Certifications", "ProjectId");
        }
    }
}
