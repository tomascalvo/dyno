namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedApplicationUserProjects : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 2, 1, '2022-04-27 16:37:00');
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 3, 1, '2022-04-27 16:37:00');
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 4, 1, '2022-04-27 16:37:00');
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 5, 1, '2022-04-27 16:37:00');
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 6, 1, '2022-04-27 16:37:00');
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 7, 1, '2022-04-27 16:37:00');
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 8, 1, '2022-04-27 16:37:00');
                INSERT INTO ApplicationUserProjects(ApplicationUserId, ProjectId, CanEdit, DateAdded) VALUES ('526b3cfb-9dba-43ba-a23e-2bc9830a2e56', 9, 1, '2022-04-27 16:37:00');
            ");
        }

        public override void Down()
        {
        }
    }
}
