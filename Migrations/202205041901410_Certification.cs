namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Certification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certifications",
                c => new
                    {
                        RecipientId = c.String(nullable: false, maxLength: 128),
                        CertificationTypeId = c.Int(nullable: false),
                        Url = c.String(),
                        Image = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        DateAwarded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipientId, t.CertificationTypeId })
                .ForeignKey("dbo.CertificationTypes", t => t.CertificationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .Index(t => t.RecipientId)
                .Index(t => t.CertificationTypeId);
            
            CreateTable(
                "dbo.CertificationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(),
                        Url = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateApproved = c.DateTime(nullable: false),
                        AddedById = c.String(maxLength: 128),
                        PlatformId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .Index(t => t.AddedById)
                .Index(t => t.PlatformId);
            
            AddColumn("dbo.Skills", "CertificationType_Id", c => c.Int());
            CreateIndex("dbo.Skills", "CertificationType_Id");
            AddForeignKey("dbo.Skills", "CertificationType_Id", "dbo.CertificationTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CertificationTypes", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certifications", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Skills", "CertificationType_Id", "dbo.CertificationTypes");
            DropForeignKey("dbo.CertificationTypes", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.Certifications", "CertificationTypeId", "dbo.CertificationTypes");
            DropIndex("dbo.Skills", new[] { "CertificationType_Id" });
            DropIndex("dbo.CertificationTypes", new[] { "PlatformId" });
            DropIndex("dbo.CertificationTypes", new[] { "AddedById" });
            DropIndex("dbo.Certifications", new[] { "CertificationTypeId" });
            DropIndex("dbo.Certifications", new[] { "RecipientId" });
            DropColumn("dbo.Skills", "CertificationType_Id");
            DropTable("dbo.CertificationTypes");
            DropTable("dbo.Certifications");
        }
    }
}
