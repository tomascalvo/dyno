namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        Details = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CurrentCompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CurrentCompanyId)
                .Index(t => t.CurrentCompanyId);
            
            AddColumn("dbo.Companies", "Client_Id", c => c.Int());
            CreateIndex("dbo.Companies", "Client_Id");
            AddForeignKey("dbo.Companies", "Client_Id", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "CurrentCompanyId", "dbo.Companies");
            DropIndex("dbo.Clients", new[] { "CurrentCompanyId" });
            DropIndex("dbo.Companies", new[] { "Client_Id" });
            DropColumn("dbo.Companies", "Client_Id");
            DropTable("dbo.Clients");
        }
    }
}
