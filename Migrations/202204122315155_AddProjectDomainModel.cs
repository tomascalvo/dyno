namespace DevPath.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectDomainModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Icon = c.String(),
                        RepositoryUrl = c.String(),
                        DeploymentUrl = c.String(),
                        Added = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Projects");
        }
    }
}
