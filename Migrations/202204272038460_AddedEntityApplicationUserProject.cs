namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedEntityApplicationUserProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserProjects",
                c => new
                {
                    ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    ProjectId = c.Int(nullable: false),
                    CanEdit = c.Boolean(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.ApplicationUserId, t.ProjectId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ProjectId);

            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String());
            AddColumn("dbo.AspNetUsers", "GitHubUsername", c => c.String());
            AddColumn("dbo.AspNetUsers", "SignupDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ColorPreference", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CurrentRoleId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "DesiredRoleId", c => c.Int());

            Sql(@"
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 1, 1, '2022-04-27 16:37:00');
            ");
        }

        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ApplicationUserProjects", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserProjects", new[] { "ProjectId" });
            DropIndex("dbo.ApplicationUserProjects", new[] { "ApplicationUserId" });
            DropColumn("dbo.AspNetUsers", "DesiredRoleId");
            DropColumn("dbo.AspNetUsers", "CurrentRoleId");
            DropColumn("dbo.AspNetUsers", "ColorPreference");
            DropColumn("dbo.AspNetUsers", "SignupDate");
            DropColumn("dbo.AspNetUsers", "GitHubUsername");
            DropColumn("dbo.AspNetUsers", "Avatar");
            DropTable("dbo.ApplicationUserProjects");
        }
    }
}
