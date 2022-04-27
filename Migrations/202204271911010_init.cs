namespace DevPath.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 50),
                    Description = c.String(),
                    Logo = c.String(),
                    WebsiteUrl = c.String(),
                    Country = c.String(),
                    StateProvince = c.String(),
                    City = c.String(),
                    DateFounded = c.DateTime(),
                    DateAdded = c.DateTime(nullable: false),
                    OrganizationLookupId = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EmploymentListings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 50),
                    PayQuantity = c.Decimal(storeType: "money"),
                    Currency = c.Int(),
                    PayFrequency = c.Int(),
                    WorkLocation = c.Int(),
                    ClientCompanyId = c.Int(),
                    FullText = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.ClientCompanyId)
                .Index(t => t.ClientCompanyId);

            CreateTable(
                "dbo.EmploymentApplications",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EmploymentListingId = c.Int(nullable: false),
                    Comment = c.String(),
                    CoverLetter = c.String(),
                    DateApplied = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .Index(t => t.EmploymentListingId);

            CreateTable(
                "dbo.EmploymentListingSkills",
                c => new
                {
                    EmploymentListingId = c.Int(nullable: false),
                    SkillId = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.EmploymentListingId, t.SkillId })
                .ForeignKey("dbo.EmploymentListings", t => t.EmploymentListingId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.EmploymentListingId)
                .Index(t => t.SkillId);

            CreateTable(
                "dbo.Skills",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Icon = c.String(),
                    Title = c.String(nullable: false, maxLength: 50),
                    Description = c.String(),
                    Developer = c.String(maxLength: 250),
                    ReleaseDate = c.DateTime(),
                    DateAdded = c.DateTime(),
                    RepositoryUrl = c.String(),
                    DocumentationUrl = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ProjectSkills",
                c => new
                {
                    ProjectId = c.Int(nullable: false),
                    SkillId = c.Int(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.ProjectId, t.SkillId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.SkillId);

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
                    DateAdded = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            // SEED DATA
            Sql(@"
                SET IDENTITY_INSERT [dbo].[Companies] ON 
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId]) VALUES (3, N'Dice', N'Tech Recruiter', NULL, N'https://www.dice.com/hiring/post-jobs?utm_source=google&utm_medium=cpc&utm_campaign=Search%20-%20-DSA%20Campaign&gclid=CjwKCAjwjZmTBhB4EiwAynRmDzv7wPgIoAyfleS3khLrWNkuu--sGo9h1Xezrq4jevkmanJYAf6gJxoCRlYQAvD_BwE', NULL, NULL, NULL, NULL, CAST(N'2022-04-25T07:39:00.000' AS DateTime), NULL)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId]) VALUES (5, N'Skuid', N'Software Company', NULL, N'https://www.skuid.com/', NULL, NULL, N'Chattanooga', NULL, CAST(N'2022-04-25T07:40:00.000' AS DateTime), NULL)
                GO
                INSERT [dbo].[Companies] ([Id], [Title], [Description], [Logo], [WebsiteUrl], [Country], [StateProvince], [City], [DateFounded], [DateAdded], [OrganizationLookupId]) VALUES (7, N'CodeScience', N'Software Company', NULL, N'https://www.codescience.com/?utm_source=google&utm_medium=gmb&utm_campaign=traffic&utm_content=website&utm_term=link', NULL, NULL, N'Chattanooga', NULL, CAST(N'2022-04-25T07:41:00.000' AS DateTime), NULL)
                GO
                SET IDENTITY_INSERT [dbo].[Companies] OFF
                GO
                SET IDENTITY_INSERT [dbo].[Skills] ON 
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (1, N'https://camo.githubusercontent.com/8d56e87edf99e89bfc457cd62462e0b7aae19e6b197b1df5c542d474d8d76f81/68747470733a2f2f646576656c6f7065722e6665646f726170726f6a6563742e6f72672f7374617469632f6c6f676f2f6373686172702e706e67', N'C#', N'C# is a general-purpose, multi-paradigm programming language. C# encompasses static typing, strong typing, lexically scoped, imperative, declarative, functional, generic, object-oriented (class-based), and component-oriented programming disciplines', N'Mads Torgersen, Microsoft', CAST(N'2000-12-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.177' AS DateTime), N'', N'https://docs.microsoft.com/en-us/dotnet/csharp/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (2, N'https://camo.githubusercontent.com/58045a79a69afea4cab1cea6def6d911fba3956cf5fd683addf41c032aa64088/68747470733a2f2f636c6475702e636f6d2f78465646784f696f41552e737667', N'Mocha', N'Mocha is a feature-rich asynchronous JavaScript test framework running on Node.js and in the browser. Mocha tests run serially, allowing for flexible and accurate reporting, while mapping uncaught exceptions to the correct test cases. Hosted on GitHub.', N'Mocha', CAST(N'2011-11-22T00:00:00.000' AS DateTime), CAST(N'2022-02-08T00:00:00.000' AS DateTime), N'https://github.com/mochajs/mocha', N'https://mochajs.org')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (3, N'https://www.pngfind.com/pngs/m/255-2553250_icon-docker-notext-color-docker-icon-png-transparent.png', N'Docker', N'Docker is an open source containerization platform. It enables developers to package applications into containers—standardized executable components combining application source code with the operating system (OS) libraries and dependencies required to run that code in any environment.', N'Solomon Hykes', CAST(N'2013-03-20T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'github.com/moby/moby', N'https://docs.docker.com/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (4, N'https://e7.pngegg.com/pngimages/534/663/png-clipart-net-framework-software-framework-c-microsoft-asp-net-microsoft-blue-angle.png', N'.NET', N'The .NET Framework is a software framework developed by Microsoft that runs primarily on Microsoft Windows. It includes a large class library called Framework Class Library and provides language interoperability across several programming languages.', NULL, CAST(N'2002-01-05T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://docs.microsoft.com/en-us/dotnet/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (5, N'https://static.javatpoint.com/tutorial/entity-framework/images/entity-framework-tutorial.png', N'Entity Framework', N'Entity Framework is an open-source ORM framework for .NET applications supported by Microsoft. It enables developers to work with data using objects of domain specific classes without focusing on the underlying database tables and columns where this data is stored. With the Entity Framework, developers can work at a higher level of abstraction when they deal with data, and can create and maintain data-oriented applications with less code compared with traditional applications.', N'Microsoft', CAST(N'2008-08-11T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'https://github.com/dotnet/ef6', N'https://docs.microsoft.com/en-us/ef/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (6, N'https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/Visual_Studio_Icon_2019.svg/512px-Visual_Studio_Icon_2019.svg.png?20210214224138', N'Microsoft Visual Studio', N'Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft. It is used to develop computer programs, as well as websites, web apps, web services and mobile apps.', N'Microsoft', CAST(N'1997-03-19T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://docs.microsoft.com/en-us/visualstudio/windows/?view=vs-2022')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (7, N'~/Content/Icons/skill-icons/sql-logo.png', N'SQL', N'Microsoft SQL Server is a relational database management system developed by Microsoft. As a database server, it is a software product with the primary function of storing and retrieving data as requested by other software applications—which may run either on the same computer or on another computer across a network.', N'Microsoft', CAST(N'1989-04-24T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (8, N'https://git-scm.com/images/logos/downloads/Git-Icon-1788C.png', N'Git', N'Git is a free and open source distributed version control system designed to handle everything from small to very large projects with speed and efficiency.', N'Junio Hamano and others', CAST(N'2005-04-07T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://git-scm.com/doc')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (9, N'~/Content/Icons/skill-icons/bootstrap-logo.png', N'Bootstrap', N'Bootstrap is a free and open-source CSS framework directed at responsive, mobile-first front-end web development. It contains CSS- and JavaScript-based design templates for typography, forms, buttons, navigation, and other interface components.', N'Mark Otto, Jacob Thornton, Boostrap Core Team', CAST(N'2011-08-19T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'https://github.com/twbs/bootstrap', N'https://getbootstrap.com/docs/4.1/getting-started/introduction/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (10, N'https://www.wired.com/images_blogs/business/2011/08/HTML5_Logo_512.png', N'HTML5', N'HTML is the standard markup language for documents designed to be displayed in a web browser.', N'Tim Berners-Lee, WHATWG', CAST(N'1993-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/HTML')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (11, N'https://cdn.iconscout.com/icon/free/png-256/css-37-226088.png', N'CSS3', N'CSS3 is the latest version of the CSS specification. CSS is a stylesheet language used for describing the presentation of a document written in a markup language such as HTML.', N'W3C', CAST(N'1996-12-17T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/CSS')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (12, N'https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/WordPress_blue_logo.svg/1024px-WordPress_blue_logo.svg.png', N'Wordpress', N'WordPress is a free and open-source content management system written in PHP and paired with a MySQL or MariaDB database. Features include a plugin architecture and a template system, referred to within WordPress as Themes.', N'WordPress Foundation', CAST(N'2003-05-27T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.wordpress.com/docs/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (13, N'https://images-platform.99static.com//y50FdI4Or7filffZ5qSXAn5YMTI=/0x0:2000x2000/fit-in/500x500/projects-files/71/7131/713106/ec5bd3a0-f210-4729-ae13-51241c5657eb.jpg', N'Digital Marketing', N'Digital marketing is the component of marketing that uses internet and online based digital technologies such as desktop computers, mobile phones and other digital media and platforms to promote products and services.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (14, N'~/Content/Icons/skill-icons/react-icon.png', N'React', N'React.js is an open-source JavaScript library that is used for building user interfaces specifically for single-page applications.', N'Jordan Walke at Facebook', CAST(N'2013-05-29T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'https://github.com/facebook/react', N'https://reactjs.org/docs/getting-started.html')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (15, N'~/Content/Icons/skill-icons/axios-logo.svg', N'Axios', N'Axios.js is a promise-based HTTP Client for node.js and the browser. On the server-side it uses the native node.js http module, while on the client (browser) it uses XMLHttpRequests.', N'Matt Zabriskie', CAST(N'2014-09-18T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), N'https://github.com/axios/axios', N'https://axios-http.com/docs/intro')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (16, N'~/Content/Icons/skill-icons/commercejs-logo.png', N'Commerce.js', N'Commerce.js is a headless eCommerce platform built specifically for develoeprs and designers to build custom eCommerce solutions.', NULL, CAST(N'2016-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://commercejs.com/docs/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (17, N'~/Content/Icons/skill-icons/google-logo.png', N'Google OAuth', N'OAuth is an open standard for access delegation, commonly used as a way for Internet users to grant websites or applications access to their information on other websites without giving them the passwords.', N'Google', NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developers.google.com/identity/protocols/oauth2')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (18, N'~/Content/Icons/skill-icons/pwa-logo.png', N'PWAs (Progressive Web Apps)', N'Progressive Web Apps use services workers and manifests to give users an experience on par with native apps.', N'Google', CAST(N'2015-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (19, N'~/Content/Icons/skill-icons/google-lighthouse-logo.png', N'Google Lighthouse', N'Google Lighthouse audits progressive web applications for performance, accessibility, and search engine optimization.', N'Google', CAST(N'2018-05-08T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://developers.google.com/web/tools/lighthouse')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (20, N'~/Content/Icons/skill-icons/socketio-logo.png', N'SocketIO', N'Socket.IO is a JavaScript library that enables realtime, bi-directional communication between web clients and servers.', N'Guillermo Rauch', CAST(N'2010-01-01T00:00:00.000' AS DateTime), CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, N'https://socket.io/docs/v4/')
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (21, N'~/Content/Icons/skill-icons/themeui-logo.png', N'Theme UI', N'Theme UI is a library for creating themeable user interfaces based on constraint-based design principles. Build custom component libraries, design systems, web applications, Gatsby themes, and more with a flexible API for best-in-class developer ergonomics.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (22, N'~/Content/Icons/skill-icons/materialui-logo.png', N'Material-UI', N'Material UI is an open-source, front-end framework for React components.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (23, N'~/Content/Icons/skill-icons/node-logo.png', N'Node.js', N'Node.js is a JavaScript runtime built on Chrome''s V8 JavaScript engine. Node.js uses an event-driven, non-blocking I/O model that makes it lightweight and efficient.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (24, N'~/Content/Icons/skill-icons/mongodb-logo.png', N'MongoDB', N'MongoDB is a document-oriented database which stores data in JSON-like documents with dynamic schema.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (25, N'~/Content/Icons/skill-icons/vite-logo.svg', N'Vite', N'Vite is a front end build tool and dev server.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (26, N'~/Content/Icons/skill-icons/threejs-logo.png', N'Three.js', N'Three.js is a cross-browser JavaScript library and application programming interface (API) used to create and display animated 3D computer graphics in a web browser using WebGL.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (27, N'~/Content/Icons/skill-icons/rest_api.png', N'REST API', N'A REST API (also known as RESTful API) is an application programming interface (API or web API) that conforms to the constraints of REST architectural style and allows for interaction with RESTful web services. REST stands for representational state transfer.', N'Roy Fielding', NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (28, N'~/Content/Icons/skill-icons/chartjs-logo.svg', N'Chart.js', N'Chart.js is an open-source JavaScript library for data visualization.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (29, N'~/Content/Icons/skill-icons/heroku.png', N'Heroku', N'Heroku is a platform as a service (PaaS) that enables developers to build, run, and operate applications entirely in the cloud.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (30, N'~/Content/Icons/skill-icons/netlify.svg', N'Netlify', N'Netlify is a frontend web hosting platform.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (31, N'~/Content/Icons/skill-icons/hostinger.svg', N'Hostinger', N'Hostinger is shared web hosting.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (32, N'~/Content/Icons/skill-icons/azure.png', N'Azure', N'Azure is a public cloud computing platform—with solutions including Infrastructure as a Service (IaaS), Platform as a Service (PaaS), and Software as a Service (SaaS) that can be used for services such as analytics, virtual computing, storage, and networking.', NULL, NULL, CAST(N'2022-04-27T14:56:43.180' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (33, NULL, N'ASP.NET', N'The main difference between . NET and ASP.NET is that . NET is a software framework that allows developing, running and executing applications while ASP.NET is a web framework which is a part of . NET that allows building dynamic web applications.', NULL, NULL, CAST(N'2022-04-26T17:11:33.557' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (34, NULL, N'Windows Forms', NULL, NULL, NULL, CAST(N'2022-04-26T17:11:56.627' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (35, NULL, N'SQL Server', N'SQL is a query language. It is used to write queries to retrieve or manipulate the relational database data. On the other hand, SQL Server is proprietary software or an RDBMS tool that executes the SQL statements.', NULL, NULL, CAST(N'2022-04-26T17:33:08.550' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (36, NULL, N'OOP (Object-Oriented Programming)', NULL, NULL, NULL, CAST(N'2022-04-26T17:33:53.033' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (37, NULL, N'Web Services', NULL, NULL, NULL, CAST(N'2022-04-26T17:34:11.423' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (38, NULL, N'Service-Oriented Architecture', NULL, NULL, NULL, CAST(N'2022-04-26T17:34:27.367' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (39, NULL, N'WCF (Windows Communication Foundation)', N'Windows Communication Foundation (WCF) is a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.', NULL, NULL, CAST(N'2022-04-26T17:35:33.680' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (40, NULL, N'Concurrent Development Source Control', N'Git', NULL, NULL, CAST(N'2022-04-26T17:36:15.687' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (41, NULL, N'Continuous Integration', N'Jenkins or Bamboo', NULL, NULL, CAST(N'2022-04-26T17:36:36.660' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (42, NULL, N'Jenkins', NULL, NULL, NULL, CAST(N'2022-04-26T17:36:43.117' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (43, NULL, N'Bamboo', NULL, NULL, NULL, CAST(N'2022-04-26T17:36:53.570' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (44, NULL, N'Agile', N'Agile Methodologies', NULL, NULL, CAST(N'2022-04-26T18:56:54.470' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (45, NULL, N'Software Development Life Cycle', N'In systems engineering, information systems and software engineering, the systems development life cycle, also referred to as the application development life-cycle, is a process for planning, creating, testing, and deploying an information system.', NULL, NULL, CAST(N'2022-04-26T18:58:14.403' AS DateTime), NULL, NULL)
                GO
                INSERT [dbo].[Skills] ([Id], [Icon], [Title], [Description], [Developer], [ReleaseDate], [DateAdded], [RepositoryUrl], [DocumentationUrl]) VALUES (46, NULL, N'SaaS', N'Software as a Service', NULL, NULL, CAST(N'2022-04-26T18:59:15.750' AS DateTime), NULL, NULL)
                GO
                SET IDENTITY_INSERT [dbo].[Skills] OFF
                GO
                SET IDENTITY_INSERT [dbo].[Projects] ON 
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (1, N'Strength', N'A MERN stack web app for tracking fitness and training together remotely.', N'~/Content/Icons/project-icons/strength-logo-3-lightmode.svg', N'https://github.com/tomascalvo/otrera-server', N'https://otrera.netlify.app', CAST(N'2022-04-27T14:56:42.947' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (2, N'Dyno', N'An ASP.NET web app for matching web developers with relevant jobs and learning resources.', N'~/Content/Icons/project-icons/dyno-logo-lightmode.png', N'https://github.com/tomascalvo/devpath', N'https://dyno.azurewebsites.net/', CAST(N'2022-04-27T14:56:42.967' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (3, N'Consensa', N'An ASP.NET web app for ranked-choice voting that uses QR codes, websockets, encryption, and a distributed database to help users quickly reach consensus.', N'https://www.seekpng.com/png/detail/28-280338_vote-icon-png-download-icono-elecciones-png.png', N'', N'', CAST(N'2022-04-27T14:56:42.970' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (4, N'Indulgence', N'An ASP.NET web app that allows users to purchase beautifully illustrated illuminated certificates stored in a distributed database that absolves them of their microaggressions.', N'https://cdn-icons-png.flaticon.com/512/2397/2397091.png', N'', N'', CAST(N'2022-04-27T14:56:42.973' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (5, N'Bamberga Minerals', N'A website that displays interactive 3D graphics using the Three.js library.', N'~/Content/Icons/project-icons/bamberga-logo-darkmode.svg', N'https://github.com/tomascalvo/threejs-site', N'https://bamberga.netlify.app/', CAST(N'2022-04-27T14:56:42.977' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (6, N'Weather Beach', N'A PWA that uses React and OpenWeather API to display weather information by city.', N'~/Content/Icons/project-icons/island.svg', N'https://github.com/tomascalvo/weather-pwa', N'https://weatherbeach.netlify.app/', CAST(N'2022-04-27T14:56:42.977' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (7, N'Pandemic Tracker', N'A React COVID-19 tracker that consumes data from a Johns Hopkins University API.', N'~/Content/Icons/project-icons/graph.png', N'https://github.com/tomascalvo/covid-tracker', N'https://v-tracker.netlify.app/', CAST(N'2022-04-27T14:56:42.980' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (8, N'IO Chat', N'A real-time chat app using React and Socket.IO', N'~/Content/Icons/project-icons/chat.png', N'https://github.com/tomascalvo/react-chat-app', N'https://io-chat.netlify.app/', CAST(N'2022-04-27T14:56:42.980' AS DateTime))
                GO
                INSERT [dbo].[Projects] ([Id], [Title], [Description], [Icon], [RepositoryUrl], [DeploymentUrl], [DateAdded]) VALUES (9, N'Cumberland Honey Shop', N'A React e-commerce app using Commerce.JS.', N'~/Content/Icons/project-icons/honey.svg', N'https://github.com/tomascalvo/E-Commerce_WebApp', N'https://cumberlandhoney.netlify.app/', CAST(N'2022-04-27T14:56:42.983' AS DateTime))
                GO
                SET IDENTITY_INSERT [dbo].[Projects] OFF
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 8, CAST(N'2022-04-27T14:56:43.373' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 10, CAST(N'2022-04-27T14:56:43.377' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 11, CAST(N'2022-04-27T14:56:43.380' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 14, CAST(N'2022-04-27T14:56:43.383' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 15, CAST(N'2022-04-27T14:56:43.387' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 17, CAST(N'2022-04-27T14:56:43.390' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 20, CAST(N'2022-04-27T14:56:43.393' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 22, CAST(N'2022-04-27T14:56:43.397' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 23, CAST(N'2022-04-27T14:56:43.397' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 24, CAST(N'2022-04-27T14:56:43.400' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 27, CAST(N'2022-04-27T14:56:43.403' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 28, CAST(N'2022-04-27T14:56:43.407' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 29, CAST(N'2022-04-27T14:56:43.410' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (1, 30, CAST(N'2022-04-27T14:56:43.413' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 1, CAST(N'2022-04-27T14:56:43.413' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 2, CAST(N'2022-04-27T14:56:43.417' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 4, CAST(N'2022-04-27T14:56:43.420' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 5, CAST(N'2022-04-27T14:56:43.423' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 6, CAST(N'2022-04-27T14:56:43.427' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 7, CAST(N'2022-04-27T14:56:43.427' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 8, CAST(N'2022-04-27T14:56:43.430' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 9, CAST(N'2022-04-27T14:56:43.433' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 10, CAST(N'2022-04-27T14:56:43.437' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 11, CAST(N'2022-04-27T14:56:43.440' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 15, CAST(N'2022-04-27T14:56:43.443' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 17, CAST(N'2022-04-27T14:56:43.447' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 27, CAST(N'2022-04-27T14:56:43.450' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (2, 32, CAST(N'2022-04-27T14:56:43.453' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 8, CAST(N'2022-04-27T14:56:43.457' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 10, CAST(N'2022-04-27T14:56:43.460' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 11, CAST(N'2022-04-27T14:56:43.460' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 25, CAST(N'2022-04-27T14:56:43.463' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 26, CAST(N'2022-04-27T14:56:43.467' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (5, 30, CAST(N'2022-04-27T14:56:43.470' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 8, CAST(N'2022-04-27T14:56:43.473' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 10, CAST(N'2022-04-27T14:56:43.477' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 11, CAST(N'2022-04-27T14:56:43.480' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 14, CAST(N'2022-04-27T14:56:43.480' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 15, CAST(N'2022-04-27T14:56:43.483' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 18, CAST(N'2022-04-27T14:56:43.487' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 27, CAST(N'2022-04-27T14:56:43.490' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (6, 30, CAST(N'2022-04-27T14:56:43.493' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 8, CAST(N'2022-04-27T14:56:43.497' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 10, CAST(N'2022-04-27T14:56:43.500' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 11, CAST(N'2022-04-27T14:56:43.503' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 27, CAST(N'2022-04-27T14:56:43.507' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 28, CAST(N'2022-04-27T14:56:43.510' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (7, 30, CAST(N'2022-04-27T14:56:43.513' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 8, CAST(N'2022-04-27T14:56:43.517' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 10, CAST(N'2022-04-27T14:56:43.517' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 11, CAST(N'2022-04-27T14:56:43.523' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 20, CAST(N'2022-04-27T14:56:43.527' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 29, CAST(N'2022-04-27T14:56:43.527' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (8, 30, CAST(N'2022-04-27T14:56:43.533' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 8, CAST(N'2022-04-27T14:56:43.537' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 10, CAST(N'2022-04-27T14:56:43.537' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 11, CAST(N'2022-04-27T14:56:43.543' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 16, CAST(N'2022-04-27T14:56:43.547' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 22, CAST(N'2022-04-27T14:56:43.547' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 27, CAST(N'2022-04-27T14:56:43.550' AS DateTime))
                GO
                INSERT [dbo].[ProjectSkills] ([ProjectId], [SkillId], [DateAdded]) VALUES (9, 30, CAST(N'2022-04-27T14:56:43.557' AS DateTime))
                GO
                INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b7d929a7-6d73-4323-b36a-89860290eb62', N'CanManageAll')
                GO
                INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a16f8309-c8ec-4c5a-979d-fcf922d9f534', N'CanManageSkills')
                GO
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', N'tomasrcalvo@gmail.com', 0, N'ADS8V61ZGUpdjfEUFR5kkclc02pqN6amIriak/5axo3nzvM7iX+hLGly9+LjLyxM7Q==', N'2fe4ee84-37ac-41dc-b06d-154b8b64ed93', NULL, 0, 0, NULL, 1, 0, N'tomasrcalvo@gmail.com')
                GO
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b18365bd-d9f9-43f3-a0fb-3095031f6dfc', N'dominic@dyno.com', 0, N'ADOQblsg84otW9DQlHPDRKl2poswgulgcf5LodZGlpIfs/XIC/BSi6wPUqC/r7x6QQ==', N'ec286855-ea84-4549-9f66-faf56582a6ff', NULL, 0, 0, NULL, 1, 0, N'dominic@dyno.com')
                GO
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ceac373c-3600-4c8b-9184-64081e6fcbad', N'guest@vidly.com', 0, N'ADCeCs5RMNTBWmIzW4S3X/4SBSufIAThRRHHZmMoCc1Jk7/sStailSPTGcdxzABIKA==', N'7f82477e-6329-45f1-b8a6-d173c0a448a6', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                GO
                INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b18365bd-d9f9-43f3-a0fb-3095031f6dfc', N'a16f8309-c8ec-4c5a-979d-fcf922d9f534')
                GO
                INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'526b3cfb-9dba-43ba-a23e-2bc9830a2e56', N'b7d929a7-6d73-4323-b36a-89860290eb62')
                GO
            ");
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EmploymentListings", "ClientCompanyId", "dbo.Companies");
            DropForeignKey("dbo.EmploymentListingSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ProjectSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.ProjectSkills", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmploymentListingSkills", "EmploymentListingId", "dbo.EmploymentListings");
            DropForeignKey("dbo.EmploymentApplications", "EmploymentListingId", "dbo.EmploymentListings");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProjectSkills", new[] { "SkillId" });
            DropIndex("dbo.ProjectSkills", new[] { "ProjectId" });
            DropIndex("dbo.EmploymentListingSkills", new[] { "SkillId" });
            DropIndex("dbo.EmploymentListingSkills", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentApplications", new[] { "EmploymentListingId" });
            DropIndex("dbo.EmploymentListings", new[] { "ClientCompanyId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.EmploymentListingSkills");
            DropTable("dbo.EmploymentApplications");
            DropTable("dbo.EmploymentListings");
            DropTable("dbo.Companies");
        }
    }
}
