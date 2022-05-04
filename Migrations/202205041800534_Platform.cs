namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Platform : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        AddedById = c.String(maxLength: 128),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .Index(t => t.Id)
                .Index(t => t.AddedById);
            
            AddColumn("dbo.EmploymentListings", "PlatformId", c => c.Int());
            CreateIndex("dbo.EmploymentListings", "PlatformId");
            AddForeignKey("dbo.EmploymentListings", "PlatformId", "dbo.Platforms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Platforms", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Platforms", "Id", "dbo.Companies");
            DropForeignKey("dbo.EmploymentListings", "PlatformId", "dbo.Platforms");
            DropIndex("dbo.Platforms", new[] { "AddedById" });
            DropIndex("dbo.Platforms", new[] { "Id" });
            DropIndex("dbo.EmploymentListings", new[] { "PlatformId" });
            DropColumn("dbo.EmploymentListings", "PlatformId");
            DropTable("dbo.Platforms");
        }
    }
}
