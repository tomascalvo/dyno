﻿namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class calvoirvineUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Avatar], [GitHubUsername], [SignupDate], [ColorPreference], [CurrentRoleId], [DesiredRoleId]) VALUES (N'ab16137b-6bff-4d62-9c3e-acbe1a4730d8', N'calvoirvine@sbcglobal.net', 0, N'ACBpNreaPdm6IbGa4IhbvlYdFOGIHffttXiEC/ItWcerDxvzEbI2ViYtYZaYdoYm7Q==', N'3621ceb7-503c-49c0-8b1c-e71332816388', NULL, 0, 0, NULL, 1, 0, N'calvoirvine@sbcglobal.net', NULL, NULL, CAST(N'2022-04-30T15:19:23.123' AS DateTime), NULL, NULL, NULL)");

            Sql(@"
    DELETE from Skills WHERE Id >= 33;
");

            Sql(@"SET IDENTITY_INSERT [dbo].[Skills] ON 
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (33, N'https://symbols.getvecta.com/stencil_88/75_microsoft-net-icon.468c070a03.png', N'ASP.NET', N'The main difference between . NET and ASP.NET is that . NET is a software framework that allows developing, running and executing applications while ASP.NET is a web framework which is a part of . NET that allows building dynamic web applications.', NULL, NULL, CAST(N'2022-04-26T17:11:33.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (34, N'https://cdn.iconscout.com/icon/free/png-256/microsoft-forms-2081996-1756309.png', N'Windows Forms', NULL, NULL, NULL, CAST(N'2022-04-26T17:11:56.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (35, N'~/Content/Icons/skill-icons/sql-logo.png', N'SQL Server', N'SQL is a query language. It is used to write queries to retrieve or manipulate the relational database data. On the other hand, SQL Server is proprietary software or an RDBMS tool that executes the SQL statements.', NULL, NULL, CAST(N'2022-04-26T17:33:08.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (36, N'https://miro.medium.com/max/300/0*goJuBKoyL-zZX4RB.png', N'OOP (Object-Oriented Programming)', NULL, NULL, NULL, CAST(N'2022-04-26T17:33:53.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (37, N'https://icon-library.com/images/web-service-icon/web-service-icon-9.jpg', N'Web Services', NULL, NULL, NULL, CAST(N'2022-04-26T17:34:11.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (38, NULL, N'Service-Oriented Architecture', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (39, N'https://symbols.getvecta.com/stencil_88/75_microsoft-net-icon.468c070a03.png', N'WCF (Windows Communication Foundation)', N'Windows Communication Foundation (WCF) is a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.', NULL, NULL, CAST(N'2022-04-26T17:35:33.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (40, NULL, N'Concurrent Development Source Control', N'Git', NULL, NULL, CAST(N'2022-04-26T17:36:15.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (41, NULL, N'Continuous Integration', N'Jenkins or Bamboo', NULL, NULL, CAST(N'2022-04-26T17:36:36.660' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (42, N'https://www.jenkins.io/images/logos/jenkins/jenkins.png', N'Jenkins', N'Continuous Integration', NULL, NULL, CAST(N'2022-04-26T17:36:43.000' AS DateTime), NULL, N'https://www.jenkins.io/doc/book/')
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (43, N'https://wac-cdn.atlassian.com/dam/jcr:3565304e-e789-486b-a722-be19f067f7c7/Bamboo-blue.svg?cdnVersion=324', N'Bamboo', N'Continuous Integration', NULL, NULL, CAST(N'2022-04-26T17:36:53.000' AS DateTime), NULL, N'https://confluence.atlassian.com/bamboo/bamboo-documentation-289276551.html')
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (44, N'https://static.thenounproject.com/png/253212-200.png', N'Agile', N'Agile Methodologies', NULL, NULL, CAST(N'2022-04-26T18:56:54.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (45, N'https://e6.pngbyte.com/pngpicture/115994/png-cycle-icon-png-Software-Development-Life-Cycle-Icon_thumbnail.png', N'Software Development Life Cycle', N'In systems engineering, information systems and software engineering, the systems development life cycle, also referred to as the application development life-cycle, is a process for planning, creating, testing, and deploying an information system.', NULL, NULL, CAST(N'2022-04-26T18:58:14.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (46, N'https://cdn-icons-png.flaticon.com/512/2103/2103478.png', N'SaaS', N'Software as a Service', NULL, NULL, CAST(N'2022-04-26T18:59:15.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (47, N'http://emmet.io/i/logo-large.png', N'Emmet', N'Emmet is a popular plugin for code editors that vastly improves the HTML and CSS workflow involved with creating web pages. Instead of having to manually type opening and closing tags for HTML elements, format nested elements, or add in CSS classes and IDs, Emmet shortcuts will autogenerate everything for you.', N'emmetio', CAST(N'2008-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-29T19:20:58.300' AS DateTime), N'github.com/emmetio/emmet', N'https://docs.emmet.io/')
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (49, N'https://openjsf.org/wp-content/uploads/sites/84/2019/10/jquery-logo-vertical_large_square.png', N'jQuery', N'jQuery is a JavaScript library designed to simplify HTML DOM tree traversal and manipulation, as well as event handling, CSS animation, and Ajax. It is free, open-source software using the permissive MIT License. As of May 2019, jQuery is used by 73% of the 10 million most popular websites.', NULL, NULL, CAST(N'2022-04-29T21:27:05.880' AS DateTime), NULL, N'https://jquery.com/')
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (50, NULL, N'RegEx', N'Regular Expressions', NULL, NULL, CAST(N'2022-04-30T14:38:12.873' AS DateTime), NULL, NULL)
INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (51, N'https://miro.medium.com/max/800/1*GwBZgjItyjEwaaZn-lxTTA.png', N'PWA', N'A progressive web application is a type of application software delivered through the web, built using common web technologies including HTML, CSS, JavaScript, and WebAssembly.', NULL, NULL, CAST(N'2022-05-02T17:59:44.657' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps')
SET IDENTITY_INSERT [dbo].[Skills] OFF
GO
");
        }

        public override void Down()
        {
        }
    }
}