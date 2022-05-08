namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ContractEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Certifications", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectCourseCompletions", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectSkills", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmploymentProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ApplicationUserProjects", "ProjectId", "dbo.Projects");
            DropPrimaryKey("dbo.Projects");
            CreateTable(
                "dbo.ContractBids",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                    ListingId = c.Int(nullable: false),
                    ContractorId = c.Int(nullable: false),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateDue = c.DateTime(),
                    DateDeclined = c.DateTime(),
                    Contractor_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Contractor_Id)
                .ForeignKey("dbo.ContractListings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId)
                .Index(t => t.Contractor_Id);

            CreateTable(
                "dbo.Contracts",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Description = c.String(),
                    ContractBidId = c.Int(nullable: false),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateArchived = c.DateTime(),
                    DateDue = c.DateTime(),
                    DateDelivered = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractBids", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.ContractListings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ContractId = c.Int(nullable: false),
                    Url = c.String(),
                    Description = c.String(),
                    Specifications = c.String(),
                    ClientId = c.Int(nullable: false),
                    PlatformId = c.Int(nullable: false),
                    AddedById = c.String(maxLength: 128),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    DateArchived = c.DateTime(),
                    DateDue = c.DateTime(),
                    Client_Id = c.Int(),
                    Client_Id1 = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id1)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.PlatformId)
                .Index(t => t.AddedById)
                .Index(t => t.Client_Id)
                .Index(t => t.Client_Id1);

            AddColumn("dbo.Projects", "ContractBidId", c => c.Int());
            AddColumn("dbo.Projects", "EmploymentId", c => c.Int());
            AddColumn("dbo.Skills", "ContractListing_Id", c => c.Int());
            AlterColumn("dbo.Projects", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Projects", "Id");
            CreateIndex("dbo.Projects", "Id");
            CreateIndex("dbo.Projects", "EmploymentId");
            CreateIndex("dbo.Skills", "ContractListing_Id");
            AddForeignKey("dbo.Skills", "ContractListing_Id", "dbo.ContractListings", "Id");
            AddForeignKey("dbo.Projects", "Id", "dbo.ContractBids", "Id");
            AddForeignKey("dbo.Projects", "EmploymentId", "dbo.Employments", "Id");
            AddForeignKey("dbo.Certifications", "ProjectId", "dbo.Projects", "Id");
            AddForeignKey("dbo.EmploymentProjects", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectSkills", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectCourseCompletions", "Project_Id", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserProjects", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectCourseCompletions", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectSkills", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmploymentProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Certifications", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "EmploymentId", "dbo.Employments");
            DropForeignKey("dbo.Projects", "Id", "dbo.ContractBids");
            DropForeignKey("dbo.Skills", "ContractListing_Id", "dbo.ContractListings");
            DropForeignKey("dbo.ContractListings", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.ContractListings", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ContractListings", "Client_Id1", "dbo.Clients");
            DropForeignKey("dbo.ContractListings", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ContractBids", "ListingId", "dbo.ContractListings");
            DropForeignKey("dbo.ContractListings", "AddedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContractBids", "Contractor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "Id", "dbo.ContractBids");
            DropIndex("dbo.Skills", new[] { "ContractListing_Id" });
            DropIndex("dbo.ContractListings", new[] { "Client_Id1" });
            DropIndex("dbo.ContractListings", new[] { "Client_Id" });
            DropIndex("dbo.ContractListings", new[] { "AddedById" });
            DropIndex("dbo.ContractListings", new[] { "PlatformId" });
            DropIndex("dbo.ContractListings", new[] { "ClientId" });
            DropIndex("dbo.Contracts", new[] { "Id" });
            DropIndex("dbo.ContractBids", new[] { "Contractor_Id" });
            DropIndex("dbo.ContractBids", new[] { "ListingId" });
            DropIndex("dbo.Projects", new[] { "EmploymentId" });
            DropIndex("dbo.Projects", new[] { "Id" });
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Projects", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Skills", "ContractListing_Id");
            DropColumn("dbo.Projects", "EmploymentId");
            DropColumn("dbo.Projects", "ContractBidId");
            DropTable("dbo.ContractListings");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractBids");
            AddPrimaryKey("dbo.Projects", "Id");
            AddForeignKey("dbo.ApplicationUserProjects", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmploymentProjects", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectSkills", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectCourseCompletions", "Project_Id", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Certifications", "ProjectId", "dbo.Projects", "Id");
        }
    }
}
