namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJoinTableEmploymentListingAccess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentListingAccesses",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        EmploymentListingId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CanEdit = c.Boolean(nullable: false),
                        CanArchive = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.EmploymentListingId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.EmploymentListingId);
            
            AddColumn("dbo.EmploymentListings", "CreatorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.EmploymentListings", "CreatorId");
            AddForeignKey("dbo.EmploymentListings", "CreatorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentListings", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentListingAccesses", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentListingAccesses", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.EmploymentListings", new[] { "CreatorId" });
            DropIndex("dbo.EmploymentListingAccesses", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentListingAccesses", new[] { "ApplicationUserId" });
            DropColumn("dbo.EmploymentListings", "CreatorId");
            DropTable("dbo.EmploymentListingAccesses");
        }
    }
}
