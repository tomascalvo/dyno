namespace DevPath.Migrations
{
    using DevPath.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DevPath.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DevPath.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Title = "Strength",
                    Icon = "https://static.vecteezy.com/system/resources/thumbnails/003/179/657/small/dumbbell-equipment-gym-line-style-icon-free-vector.jpg",
                    Description = "A MERN stack web app for tracking fitness and training together remotely.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/otrera-server",
                    DeploymentUrl = "https://otrera.netlify.app",
                },
                new Project
                {
                    Id = 2,
                    Title = "DevPath",
                    Icon = "https://icon-library.com/images/software-icon/software-icon-6.jpg",
                    Description = "An ASP.NET web app for matching web developers with relevant jobs and learning resources.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/devpath",
                    DeploymentUrl = "https://devpath.azurewebsites.net/",
                },
                new Project
                {
                    Id = 3,
                    Title = "Consensa",
                    Icon = "https://www.seekpng.com/png/detail/28-280338_vote-icon-png-download-icono-elecciones-png.png",
                    Description = "An ASP.NET web app for ranked-choice voting that uses QR codes, websockets, encryption, and a distributed database to help users quickly reach consensus.",
                    //OwnerID = 1,
                    RepositoryUrl = "",
                    DeploymentUrl = "",
                },
                new Project
                {
                    Id = 4,
                    Title = "Indulgence",
                    Icon = "https://cdn-icons-png.flaticon.com/512/2397/2397091.png",
                    Description = "An ASP.NET web app that allows users to purchase beautifully illustrated illuminated certificates stored in a distributed database that absolves them of their microaggressions.",
                    //OwnerID = 1,
                    RepositoryUrl = "",
                    DeploymentUrl = "",
                },
            };
            projects.ForEach(project => context.Projects.AddOrUpdate(p => p.Id, project));

            var skills = new List<Skill>
            {
                new Skill
                {
                    Id = 1,
                    Icon = "https://camo.githubusercontent.com/8d56e87edf99e89bfc457cd62462e0b7aae19e6b197b1df5c542d474d8d76f81/68747470733a2f2f646576656c6f7065722e6665646f726170726f6a6563742e6f72672f7374617469632f6c6f676f2f6373686172702e706e67",
                    Title = "C#",
                    Description = "C# is a general-purpose, multi-paradigm programming language. C# encompasses static typing, strong typing, lexically scoped, imperative, declarative, functional, generic, object-oriented (class-based), and component-oriented programming disciplines",
                    Developer = "Mad Torgersen, Microsoft",
                    ReleaseDate = new DateTime(2000, 12, 1),
                    DateAdded = DateTime.Now,
                    RepositoryUrl = "",
                    DocumentationUrl = "https://docs.microsoft.com/en-us/dotnet/csharp/"
                },
                                new Skill
                {
                    Id = 2,
                    Title = "Mocha",
                    Description = "Mocha is a feature-rich asynchronous JavaScript test framework running on Node.js and in the browser. Mocha tests run serially, allowing for flexible and accurate reporting, while mapping uncaught exceptions to the correct test cases. Hosted on GitHub.",
                    Developer = "Mocha",
                    //ReleaseDate = new DateTime(2011, 11, 22),
                    ReleaseDate = DateTime.Parse("2011-11-22"),

                    RepositoryUrl = "https://github.com/mochajs/mocha",
                    DateAdded = DateTime.Parse("2022-02-08"),
                    DocumentationUrl = "https://mochajs.org",
                    Icon = "~/Content/images/mocha.svg",
                },
                new Skill
                {
                    Id = 3,
                    Title = "Docker",
                    Description = "Docker is an open source containerization platform. It enables developers to package applications into containers—standardized executable components combining application source code with the operating system (OS) libraries and dependencies required to run that code in any environment.",
                    Developer = "Solomon Hykes",
                    //ReleaseDate = new DateTime(2013, 3, 20),
                    ReleaseDate = DateTime.Parse("2013-03-20"),
                    RepositoryUrl = "github.com/moby/moby",
                    DocumentationUrl = "https://docs.docker.com/",
                    Icon = "~/Content/images/docker.png",
                },
                new Skill
                {
                    Title = "ASP.NET",
                    Description = "ASP.NET extends the .NET developer platform with tools and libraries specifically for building web apps. .NET is a developer platform made up of tools, programming languages, and libraries for building many different types of applications.",
                    Developer = "Microsoft",
                    //ReleaseDate = new DateTime(2002, 1, 5),
                    ReleaseDate = DateTime.Parse("2002-01-05"),
                    DocumentationUrl = "https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0",
                    Icon = "~/Content/images/aspdotnet.png",
                },
                new Skill
                {
                    Title = "Entity Framework",
                    Description = "Entity Framework is an open-source ORM framework for .NET applications supported by Microsoft. It enables developers to work with data using objects of domain specific classes without focusing on the underlying database tables and columns where this data is stored. With the Entity Framework, developers can work at a higher level of abstraction when they deal with data, and can create and maintain data-oriented applications with less code compared with traditional applications.",
                    Developer = "Microsoft",
                    //ReleaseDate = new DateTime(2008, 8, 11),
                    ReleaseDate = DateTime.Parse("2008-08-11"),
                    RepositoryUrl = "https://github.com/dotnet/ef6",
                    DocumentationUrl = "https://docs.microsoft.com/en-us/ef/",
                    Icon = "~/Content/images/aspdotnet.png",
                },
                new Skill
                {
                    Title = "Microsoft Visual Studio",
                    Description = "Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft. It is used to develop computer programs, as well as websites, web apps, web services and mobile apps.",
                    Developer = "Microsoft",
                    //ReleaseDate = new DateTime(1997, 3, 19),
                    ReleaseDate = DateTime.Parse("1997-03-19"),
                    DocumentationUrl = "https://docs.microsoft.com/en-us/visualstudio/windows/?view=vs-2022",
                    Icon = "~/Content/images/visual-studio.svg",
                },
                new Skill
                {
                    Title = "SQL Server",
                    Description = "Microsoft SQL Server is a relational database management system developed by Microsoft.",
                    Developer = "Microsoft",
                    //ReleaseDate = new DateTime(1989, 4, 24),
                    ReleaseDate = DateTime.Parse("1989-04-24"),
                    DocumentationUrl = "https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15",
                    Icon = "~/Content/images/sql-server.png",
                },
                new Skill
                {
                    Title = "Git",
                    Description = "Git is a free and open source distributed version control system designed to handle everything from small to very large projects with speed and efficiency.",
                    Developer = "Junio Hamano and others",
                    //ReleaseDate = new DateTime(2005, 4, 7),
                    ReleaseDate = DateTime.Parse("2005-04-07"),
                    DocumentationUrl = "https://git-scm.com/doc",
                    Icon = "~/Content/images/git.png",
                },
                new Skill
                {
                    Title = "Bootstrap",
                    Description = "Bootstrap is the most popular CSS Framework for developing responsive and mobile-first websites.",
                    Developer = "Mark Otto, Jacob Thornton, Boostrap Core Team",
                    //ReleaseDate = new DateTime(2011, 8, 19),
                    ReleaseDate = DateTime.Parse("2011-08-19"),
                    RepositoryUrl = "https://github.com/twbs/bootstrap",
                    DocumentationUrl = "https://getbootstrap.com/docs/4.1/getting-started/introduction/",
                    Icon = "~/Content/images/bootstrap.png",
                },
                new Skill
                {
                    Title = "HTML",
                    Developer = "Tim Berners-Lee, WHATWG",
                    //ReleaseDate = new DateTime(1993, 1, 1),
                    ReleaseDate = DateTime.Parse("1993-01-01"),
                    Description = "HTML is the standard markup language for documents designed to be displayed in a web browser.",
                    Icon = "~/Content/images/html.png",
                    DocumentationUrl = "https://developer.mozilla.org/en-US/docs/Web/HTML",
                },
                new Skill
                {
                    Title = "CSS",
                    Description = "CSS is a stylesheet language used for describing the presentation of a document written in a markup language such as HTML.",
                    //ReleaseDate = new DateTime(1996, 12, 17),
                    ReleaseDate = DateTime.Parse("1996-12-17"),
                    Developer = "W3C",
                    DocumentationUrl = "https://developer.mozilla.org/en-US/docs/Web/CSS",
                    Icon = "~/Content/images/html.png",

                },
                new Skill
                {
                    Title = "Wordpress",
                    Description = "WordPress is a free and open-source content management system written in PHP and paired with a MySQL or MariaDB database. Features include a plugin architecture and a template system, referred to within WordPress as Themes.",
                    Icon = "~/Content/images/wordpress.png",
                    ReleaseDate = DateTime.Parse("2003-05-27"),
                    Developer = "WordPress Foundation",
                    DocumentationUrl = "https://developer.wordpress.com/docs/",
                },
                new Skill
                {
                    Title = "Digital Marketing",
                    Description = "Digital marketing is the component of marketing that uses internet and online based digital technologies such as desktop computers, mobile phones and other digital media and platforms to promote products and services.",
                }
            };
            skills.ForEach(skill => context.Skills.AddOrUpdate(s => s.Id, skill));
            context.SaveChanges();
        }
    }
}
