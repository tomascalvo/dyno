namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmploymentOfferEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentOffers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullText = c.String(),
                        Terms = c.String(),
                        Benefits = c.String(),
                        DateOffered = c.DateTime(nullable: false),
                        StartDate = c.DateTime(),
                        Expiration = c.DateTime(),
                        Accepted = c.DateTime(),
                        Declined = c.DateTime(),
                        PaymentQuantity = c.Decimal(storeType: "money"),
                        Currency = c.Int(),
                        PayFrequency = c.Int(),
                        EmploymentListingId = c.Int(nullable: false),
                        RecipientId = c.String(nullable: false, maxLength: 128),
                        RecruiterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recruiters", t => t.RecruiterId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId, cascadeDelete: true)
                .Index(t => t.EmploymentListingId)
                .Index(t => t.RecipientId)
                .Index(t => t.RecruiterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentOffers", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentOffers", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentOffers", "RecruiterId", "dbo.Recruiters");
            DropIndex("dbo.EmploymentOffers", new[] { "RecruiterId" });
            DropIndex("dbo.EmploymentOffers", new[] { "RecipientId" });
            DropIndex("dbo.EmploymentOffers", new[] { "EmploymentListingId" });
            DropTable("dbo.EmploymentOffers");
        }
    }
}
