namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedEmploymentEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentSkills",
                c => new
                {
                    EmploymentId = c.Int(nullable: false),
                    SkillId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.EmploymentId, t.SkillId })
                .ForeignKey("dbo.Employments", t => t.EmploymentId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.EmploymentId)
                .Index(t => t.SkillId);

            CreateTable(
                "dbo.Employments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    InitialPay = c.Decimal(storeType: "money"),
                    FinalPay = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    DateAdded = c.DateTime(nullable: false),
                    StartDate = c.DateTime(),
                    QuitDate = c.DateTime(),
                    Country = c.String(),
                    StateProvince = c.String(),
                    City = c.String(),
                    EmployeeId = c.String(nullable: false, maxLength: 128),
                    EmployerId = c.Int(nullable: false),
                    OfferId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.EmployerId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentOffers", t => t.OfferId)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.EmployerId)
                .Index(t => t.OfferId);

            CreateTable(
                "dbo.EmploymentProjects",
                c => new
                {
                    EmploymentId = c.Int(nullable: false),
                    ProjectId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.EmploymentId, t.ProjectId })
                .ForeignKey("dbo.Employments", t => t.EmploymentId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmploymentId)
                .Index(t => t.ProjectId);

            // Add more Skill records

            Sql(@"
SET IDENTITY_INSERT [dbo].[Skills] ON 
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (52, N'https://www.wired.com/images_blogs/business/2011/08/HTML5_Logo_512.png', N'Semantic HTML', NULL, NULL, NULL, CAST(N'2022-05-04T11:29:28.063' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Glossary/Semantics')
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (53, NULL, N'MERN Stack', NULL, NULL, NULL, CAST(N'2022-05-04T11:43:13.857' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (54, NULL, N'Mongoose', NULL, NULL, NULL, CAST(N'2022-05-04T11:43:53.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (55, NULL, N'JSON', N'JavaScript Object Notation', NULL, NULL, CAST(N'2022-05-04T11:44:16.247' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (56, NULL, N'IDE', N'Integrated Development Environment', NULL, NULL, CAST(N'2022-05-04T11:44:35.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (57, NULL, N'VS Code', NULL, NULL, NULL, CAST(N'2022-05-04T11:45:01.510' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (58, NULL, N'Design Patterns', NULL, NULL, NULL, CAST(N'2022-05-04T11:45:17.527' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (59, NULL, N'MVC', NULL, NULL, NULL, CAST(N'2022-05-04T11:45:23.110' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (60, NULL, N'Express.js ', NULL, NULL, NULL, CAST(N'2022-05-04T11:55:37.380' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (61, NULL, N'Data Structures', N'Array, Graph, Binary Tree, Dictionary, Stack', NULL, NULL, CAST(N'2022-05-04T11:56:33.637' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (62, NULL, N'OAuth', NULL, NULL, NULL, CAST(N'2022-05-04T11:58:36.820' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (63, NULL, N'Facebook OAuth', NULL, NULL, NULL, CAST(N'2022-05-04T11:58:55.697' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (64, NULL, N'Authentication', NULL, NULL, NULL, CAST(N'2022-05-04T11:59:04.147' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (65, NULL, N'Session Authentication ', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:17.700' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (66, NULL, N'Token Authentication', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:31.300' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (67, NULL, N'Debugging', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:45.647' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (68, NULL, N'Glimpse', NULL, NULL, NULL, CAST(N'2022-05-04T12:08:51.380' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (69, NULL, N'Performance Optimization', NULL, NULL, NULL, CAST(N'2022-05-04T12:18:02.307' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (70, NULL, N'Caching', NULL, NULL, NULL, CAST(N'2022-05-04T12:18:16.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (71, NULL, N'Asynchronous Programming', NULL, NULL, NULL, CAST(N'2022-05-04T12:18:25.920' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (72, NULL, N'Identity Framework', NULL, NULL, NULL, CAST(N'2022-05-04T12:19:22.687' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (73, NULL, N'Postman', NULL, NULL, NULL, CAST(N'2022-05-04T12:19:36.353' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (74, NULL, N'Stripe', NULL, NULL, NULL, CAST(N'2022-05-04T12:20:19.760' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (75, NULL, N'Redux', N'State Management', NULL, NULL, CAST(N'2022-05-04T12:21:43.447' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (76, NULL, N'React Router', N'Library', NULL, NULL, CAST(N'2022-05-04T12:22:08.740' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (77, NULL, N'React Hook Form', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:15.010' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (78, NULL, N'Mobile-First Design', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:33.270' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (79, NULL, N'Responsive Design', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:39.950' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (80, NULL, N'Adobe Photoshop', NULL, NULL, NULL, CAST(N'2022-05-04T12:22:56.353' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (81, NULL, N'GIMP', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:00.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (82, NULL, N'Inkscape', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:09.437' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (83, NULL, N'GoDaddy', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:23.883' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (84, NULL, N'SPA', N'Single-Page Application', NULL, NULL, CAST(N'2022-05-04T12:23:42.873' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (85, NULL, N'Figma', NULL, NULL, NULL, CAST(N'2022-05-04T12:23:50.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (86, NULL, N'Web Standards', NULL, NULL, NULL, CAST(N'2022-05-04T12:24:40.867' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Skills] OFF
GO
");

        }

        public override void Down()
        {
            DropForeignKey("dbo.Employments", "EmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmploymentSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.EmploymentSkills", "EmploymentId", "dbo.Employments");
            DropForeignKey("dbo.Employments", "OfferId", "dbo.EmploymentOffers");
            DropForeignKey("dbo.EmploymentProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmploymentProjects", "EmploymentId", "dbo.Employments");
            DropForeignKey("dbo.Employments", "EmployerId", "dbo.Companies");
            DropIndex("dbo.EmploymentProjects", new[] { "ProjectId" });
            DropIndex("dbo.EmploymentProjects", new[] { "EmploymentId" });
            DropIndex("dbo.Employments", new[] { "OfferId" });
            DropIndex("dbo.Employments", new[] { "EmployerId" });
            DropIndex("dbo.Employments", new[] { "EmployeeId" });
            DropIndex("dbo.EmploymentSkills", new[] { "SkillId" });
            DropIndex("dbo.EmploymentSkills", new[] { "EmploymentId" });
            DropTable("dbo.EmploymentProjects");
            DropTable("dbo.Employments");
            DropTable("dbo.EmploymentSkills");
        }
    }
}
