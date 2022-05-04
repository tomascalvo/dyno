namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SkillHierarchy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillHierarchies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Description = c.String(),
                    DateAdded = c.DateTime(nullable: false),
                    IsApproved = c.Boolean(nullable: false),
                    PrincipalId = c.Int(nullable: false),
                    CreatorId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.PrincipalId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .Index(t => t.PrincipalId)
                .Index(t => t.CreatorId);

            CreateTable(
                "dbo.SkillHierarchyPrerequisites",
                c => new
                {
                    SkillHierarchyId = c.Int(nullable: false),
                    PrerequisiteId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.SkillHierarchyId, t.PrerequisiteId })
                .ForeignKey("dbo.Skills", t => t.PrerequisiteId, cascadeDelete: true)
                .ForeignKey("dbo.SkillHierarchies", t => t.SkillHierarchyId, cascadeDelete: true)
                .Index(t => t.SkillHierarchyId)
                .Index(t => t.PrerequisiteId);

            Sql(@"
                SET IDENTITY_INSERT [dbo].[Companies] ON 
                GO

                INSERT INTO Companies (Id, Description, WebsiteUrl, City, DateAdded, IsStaffingCompany) VALUES (9, 'Chemical, Sanitation, and Engineering Services.', 'http://www.vincitgroup.com/', 'Chattanooga', '2022-05-04 12:04:00.000', 0);

                SET IDENTITY_INSERT [dbo].[Companies] OFF
                GO

                INSERT INTO Recruiters (FirstName, LastName, StaffingCompanyId) VALUES ('Jane', 'Wilson', 9); 
            ");

        }

        public override void Down()
        {
            DropForeignKey("dbo.SkillHierarchies", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SkillHierarchies", "PrincipalId", "dbo.Skills");
            DropForeignKey("dbo.SkillHierarchyPrerequisites", "SkillHierarchyId", "dbo.SkillHierarchies");
            DropForeignKey("dbo.SkillHierarchyPrerequisites", "PrerequisiteId", "dbo.Skills");
            DropIndex("dbo.SkillHierarchyPrerequisites", new[] { "PrerequisiteId" });
            DropIndex("dbo.SkillHierarchyPrerequisites", new[] { "SkillHierarchyId" });
            DropIndex("dbo.SkillHierarchies", new[] { "CreatorId" });
            DropIndex("dbo.SkillHierarchies", new[] { "PrincipalId" });
            DropTable("dbo.SkillHierarchyPrerequisites");
            DropTable("dbo.SkillHierarchies");
        }
    }
}
