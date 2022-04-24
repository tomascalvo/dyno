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

        public List<ProjectSkill> AddProjectSkillsToList(int projectId, int[] skillIds)
        {
            var projectSkillsList = new List<ProjectSkill>();

            foreach (int skillId in skillIds)
            {
                projectSkillsList.Add(new ProjectSkill(projectId, skillId));
            }
            return projectSkillsList;
        }

        public List<ProjectSkill> AddProjectSkillsToList(int projectId, int[] skillIds, List<ProjectSkill> projectSkillsList)
        {
            foreach (int skillId in skillIds)
            {
                projectSkillsList.Add(new ProjectSkill(projectId, skillId));
            }
            return projectSkillsList;
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
                    Icon = "~/Content/Icons/project-icons/strength-logo-3-lightmode.svg",
                    Description = "A MERN stack web app for tracking fitness and training together remotely.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/otrera-server",
                    DeploymentUrl = "https://otrera.netlify.app",
                },
                new Project
                {
                    Id = 2,
                    Title = "Dyno",
                    Icon = "~/Content/Icons/project-icons/dyno-logo-lightmode.png",
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
                new Project
                {
                    Id = 5,
                    Title = "Bamberga Minerals",
                    Icon = "~/Content/Icons/project-icons/bamberga-logo-darkmode.svg",
                    Description = "A website that displays interactive 3D graphics using the Three.js library.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/threejs-site",
                    DeploymentUrl = "https://bamberga.netlify.app/",
                },
                new Project
                {
                    Id = 6,
                    Title = "Weather Beach",
                    Icon = "~/Content/Icons/project-icons/island.svg",
                    Description = "A PWA that uses React and OpenWeather API to display weather information by city.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/weather-pwa",
                    DeploymentUrl = "https://weatherbeach.netlify.app/",
                },
                new Project
                {
                    Id = 7,
                    Title = "Pandemic Tracker",
                    Icon = "~/Content/Icons/project-icons/graph.png",
                    Description = "A React COVID-19 tracker that consumes data from a Johns Hopkins University API.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/covid-tracker",
                    DeploymentUrl = "https://v-tracker.netlify.app/",
                },
                new Project
                {
                    Id = 8,
                    Title = "IO Chat",
                    Icon = "~/Content/Icons/project-icons/chat.png",
                    Description = "A real-time chat app using React and Socket.IO",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/react-chat-app",
                    DeploymentUrl = "https://io-chat.netlify.app/",
                },
                new Project
                {
                    Id = 9,
                    Title = "Cumberland Honey Shop",
                    Icon = "~/Content/Icons/project-icons/honey.svg",
                    Description = "A React e-commerce app using Commerce.JS.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/E-Commerce_WebApp",
                    DeploymentUrl = "https://cumberlandhoney.netlify.app/",
                },
            };
            projects.ForEach(project => context.Projects.AddOrUpdate(p => p.Title, project));

            var skills = new List<Skill>
            {
                new Skill
                {
                    Id = 1,
                    Icon = "https://camo.githubusercontent.com/8d56e87edf99e89bfc457cd62462e0b7aae19e6b197b1df5c542d474d8d76f81/68747470733a2f2f646576656c6f7065722e6665646f726170726f6a6563742e6f72672f7374617469632f6c6f676f2f6373686172702e706e67",
                    Title = "C#",
                    Description = "C# is a general-purpose, multi-paradigm programming language. C# encompasses static typing, strong typing, lexically scoped, imperative, declarative, functional, generic, object-oriented (class-based), and component-oriented programming disciplines",
                    Developer = "Mads Torgersen, Microsoft",
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
                    Icon = "https://camo.githubusercontent.com/58045a79a69afea4cab1cea6def6d911fba3956cf5fd683addf41c032aa64088/68747470733a2f2f636c6475702e636f6d2f78465646784f696f41552e737667",
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
                    Icon = "https://www.pngfind.com/pngs/m/255-2553250_icon-docker-notext-color-docker-icon-png-transparent.png",
                },
                new Skill
                {
                    Id = 4,
                    Title = ".NET",
                    Description = "The .NET Framework is a software framework developed by Microsoft that runs primarily on Microsoft Windows. It includes a large class library called Framework Class Library and provides language interoperability across several programming languages.",
                    //ReleaseDate = new DateTime(2002, 1, 5),
                    ReleaseDate = DateTime.Parse("2002-01-05"),
                    DocumentationUrl = "https://docs.microsoft.com/en-us/dotnet/",
                    Icon = "https://e7.pngegg.com/pngimages/534/663/png-clipart-net-framework-software-framework-c-microsoft-asp-net-microsoft-blue-angle.png",
                },
                new Skill
                {
                    Id = 5,
                    Title = "Entity Framework",
                    Description = "Entity Framework is an open-source ORM framework for .NET applications supported by Microsoft. It enables developers to work with data using objects of domain specific classes without focusing on the underlying database tables and columns where this data is stored. With the Entity Framework, developers can work at a higher level of abstraction when they deal with data, and can create and maintain data-oriented applications with less code compared with traditional applications.",
                    Developer = "Microsoft",
                    //ReleaseDate = new DateTime(2008, 8, 11),
                    ReleaseDate = DateTime.Parse("2008-08-11"),
                    RepositoryUrl = "https://github.com/dotnet/ef6",
                    DocumentationUrl = "https://docs.microsoft.com/en-us/ef/",
                    Icon = "https://static.javatpoint.com/tutorial/entity-framework/images/entity-framework-tutorial.png",
                },
                new Skill
                {
                    Id = 6,
                    Title = "Microsoft Visual Studio",
                    Description = "Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft. It is used to develop computer programs, as well as websites, web apps, web services and mobile apps.",
                    Developer = "Microsoft",
                    //ReleaseDate = new DateTime(1997, 3, 19),
                    ReleaseDate = DateTime.Parse("1997-03-19"),
                    DocumentationUrl = "https://docs.microsoft.com/en-us/visualstudio/windows/?view=vs-2022",
                    Icon = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/Visual_Studio_Icon_2019.svg/512px-Visual_Studio_Icon_2019.svg.png?20210214224138",
                },
                new Skill
                {
                    Id = 7,
                    Title = "SQL",
                    Description = "Microsoft SQL Server is a relational database management system developed by Microsoft. As a database server, it is a software product with the primary function of storing and retrieving data as requested by other software applications—which may run either on the same computer or on another computer across a network.",
                    Developer = "Microsoft",
                    //ReleaseDate = new DateTime(1989, 4, 24),
                    ReleaseDate = DateTime.Parse("1989-04-24"),
                    DocumentationUrl = "https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15",
                    Icon = "~/Content/Icons/skill-icons/sql-logo.png",
                },
                new Skill
                {
                    Id = 8,
                    Title = "Git",
                    Description = "Git is a free and open source distributed version control system designed to handle everything from small to very large projects with speed and efficiency.",
                    Developer = "Junio Hamano and others",
                    //ReleaseDate = new DateTime(2005, 4, 7),
                    ReleaseDate = DateTime.Parse("2005-04-07"),
                    DocumentationUrl = "https://git-scm.com/doc",
                    Icon = "https://git-scm.com/images/logos/downloads/Git-Icon-1788C.png",
                },
                new Skill
                {
                    Id = 9,
                    Title = "Bootstrap",
                    Description = "Bootstrap is a free and open-source CSS framework directed at responsive, mobile-first front-end web development. It contains CSS- and JavaScript-based design templates for typography, forms, buttons, navigation, and other interface components.",
                    Developer = "Mark Otto, Jacob Thornton, Boostrap Core Team",
                    //ReleaseDate = new DateTime(2011, 8, 19),
                    ReleaseDate = DateTime.Parse("2011-08-19"),
                    RepositoryUrl = "https://github.com/twbs/bootstrap",
                    DocumentationUrl = "https://getbootstrap.com/docs/4.1/getting-started/introduction/",
                    Icon = "~/Content/Icons/skill-icons/bootstrap-logo.png",
                },
                new Skill
                {
                    Id = 10,
                    Title = "HTML5",
                    Developer = "Tim Berners-Lee, WHATWG",
                    //ReleaseDate = new DateTime(1993, 1, 1),
                    ReleaseDate = DateTime.Parse("1993-01-01"),
                    Description = "HTML is the standard markup language for documents designed to be displayed in a web browser.",
                    Icon = "https://www.wired.com/images_blogs/business/2011/08/HTML5_Logo_512.png",
                    DocumentationUrl = "https://developer.mozilla.org/en-US/docs/Web/HTML",
                },
                new Skill
                {
                    Id = 11,
                    Title = "CSS3",
                    Description = "CSS3 is the latest version of the CSS specification. CSS is a stylesheet language used for describing the presentation of a document written in a markup language such as HTML.",
                    //ReleaseDate = new DateTime(1996, 12, 17),
                    ReleaseDate = DateTime.Parse("1996-12-17"),
                    Developer = "W3C",
                    DocumentationUrl = "https://developer.mozilla.org/en-US/docs/Web/CSS",
                    Icon = "https://cdn.iconscout.com/icon/free/png-256/css-37-226088.png",

                },
                new Skill
                {
                    Id = 12,
                    Title = "Wordpress",
                    Description = "WordPress is a free and open-source content management system written in PHP and paired with a MySQL or MariaDB database. Features include a plugin architecture and a template system, referred to within WordPress as Themes.",
                    Icon = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/WordPress_blue_logo.svg/1024px-WordPress_blue_logo.svg.png",
                    ReleaseDate = DateTime.Parse("2003-05-27"),
                    Developer = "WordPress Foundation",
                    DocumentationUrl = "https://developer.wordpress.com/docs/",
                },
                new Skill
                {
                    Id = 13,
                    Icon = "https://images-platform.99static.com//y50FdI4Or7filffZ5qSXAn5YMTI=/0x0:2000x2000/fit-in/500x500/projects-files/71/7131/713106/ec5bd3a0-f210-4729-ae13-51241c5657eb.jpg",
                    Title = "Digital Marketing",
                    Description = "Digital marketing is the component of marketing that uses internet and online based digital technologies such as desktop computers, mobile phones and other digital media and platforms to promote products and services.",
                },
                new Skill
                {
                    Id = 14,
                    Icon = "~/Content/Icons/skill-icons/react-icon.png",
                    Title = "React",
                    Description = "React.js is an open-source JavaScript library that is used for building user interfaces specifically for single-page applications.",
                    Developer = "Jordan Walke at Facebook",
                    ReleaseDate = new DateTime(2013, 5, 29),
                    DateAdded = DateTime.Now,
                    DocumentationUrl = "https://reactjs.org/docs/getting-started.html",
                    RepositoryUrl = "https://github.com/facebook/react",
                },
                new Skill
                {
                    Id = 15,
                    Icon = "~/Content/Icons/skill-icons/axios-logo.svg",
                    Title = "Axios",
                    Description = "Axios.js is a promise-based HTTP Client for node.js and the browser. On the server-side it uses the native node.js http module, while on the client (browser) it uses XMLHttpRequests.",
                    Developer = "Matt Zabriskie",
                    ReleaseDate = new DateTime(2014, 9, 18),
                    DateAdded = DateTime.Now,
                    DocumentationUrl = "https://axios-http.com/docs/intro",
                    RepositoryUrl = "https://github.com/axios/axios",
                },
                new Skill
                {
                    Id = 16,
                    Title = "Commerce.js",
                    Icon = "~/Content/Icons/skill-icons/commercejs-logo.png",
                    Description = "Commerce.js is a headless eCommerce platform built specifically for develoeprs and designers to build custom eCommerce solutions.",
                    DocumentationUrl = "https://commercejs.com/docs/",
                    ReleaseDate = new DateTime(2016, 1, 1),
                    DateAdded = DateTime.Now,
                },
                new Skill
                {
                    Id = 17,
                    DateAdded = DateTime.Now,
                    Title = "Google OAuth",
                    Icon = "~/Content/Icons/skill-icons/google-logo.png",
                    Description = "OAuth is an open standard for access delegation, commonly used as a way for Internet users to grant websites or applications access to their information on other websites without giving them the passwords.",
                    DocumentationUrl = "https://developers.google.com/identity/protocols/oauth2",
                    Developer = "Google",
                },
                new Skill
                {
                    Id = 18,
                    DateAdded = DateTime.Now,
                    Title = "PWAs (Progressive Web Apps)",
                    Icon = "~/Content/Icons/skill-icons/pwa-logo.png",
                    Description = "Progressive Web Apps use services workers and manifests to give users an experience on par with native apps.",
                    DocumentationUrl = "https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps",
                    Developer = "Google",
                    ReleaseDate = new DateTime(2015, 1, 1),
                },
                new Skill
                {
                    Id = 19,
                    DateAdded = DateTime.Now,
                    Title = "Google Lighthouse",
                    Icon = "~/Content/Icons/skill-icons/google-lighthouse-logo.png",
                    Description = "Google Lighthouse audits progressive web applications for performance, accessibility, and search engine optimization.",
                    DocumentationUrl = "https://developers.google.com/web/tools/lighthouse",
                    Developer = "Google",
                    ReleaseDate = new DateTime(2018, 5, 8),
                },
                new Skill
                {
                    Id = 20,
                    DateAdded = DateTime.Now,
                    Title = "SocketIO",
                    Icon = "~/Content/Icons/skill-icons/socketio-logo.png",
                    Description = "Socket.IO is a JavaScript library that enables realtime, bi-directional communication between web clients and servers.",
                    DocumentationUrl = "https://socket.io/docs/v4/",
                    Developer = "Guillermo Rauch",
                    ReleaseDate = new DateTime(2010, 1, 1)
                },
                new Skill {
                    Id = 21,
                    DateAdded = DateTime.Now,
                    Icon = "~/Content/Icons/skill-icons/themeui-logo.png",
                    Title = "Theme UI",
                    Description = "Theme UI is a library for creating themeable user interfaces based on constraint-based design principles. Build custom component libraries, design systems, web applications, Gatsby themes, and more with a flexible API for best-in-class developer ergonomics.",
                },
                new Skill {
                    Id = 22,
                    DateAdded = DateTime.Now,
                    Icon = "~/Content/Icons/skill-icons/materialui-logo.png",
                    Title = "Material-UI",
                    Description = "Material UI is an open-source, front-end framework for React components.",
                },
                new Skill {
                    Id = 23,
                    DateAdded = DateTime.Now,
                    Title = "Node.js",
                    Icon = "~/Content/Icons/skill-icons/node-logo.png",
                    Description = "Node.js is a JavaScript runtime built on Chrome\'s V8 JavaScript engine. Node.js uses an event-driven, non-blocking I/O model that makes it lightweight and efficient.",
                },
                new Skill {
                    Id = 24,
                    DateAdded = DateTime.Now,
                    Title = "MongoDB",
                    Icon = "~/Content/Icons/skill-icons/mongodb-logo.png",
                    Description = "MongoDB is a document-oriented database which stores data in JSON-like documents with dynamic schema.",
                },
                new Skill {
                    Id = 25,
                    DateAdded = DateTime.Now,
                    Title = "Vite",
                    Icon = "~/Content/Icons/skill-icons/vite-logo.svg",
                    Description = "Vite is a front end build tool and dev server.",
                },
                new Skill {
                    Id = 26,
                    DateAdded = DateTime.Now,
                    Title = "Three.js",
                    Icon = "~/Content/Icons/skill-icons/threejs-logo.png",
                    Description = "Three.js is a cross-browser JavaScript library and application programming interface (API) used to create and display animated 3D computer graphics in a web browser using WebGL.",
                },
                new Skill
                {
                    Id = 27,
                    DateAdded = DateTime.Now,
                    Title = "REST API",
                    Icon = "~/Content/Icons/skill-icons/rest_api.png",
                    Description = "A REST API (also known as RESTful API) is an application programming interface (API or web API) that conforms to the constraints of REST architectural style and allows for interaction with RESTful web services. REST stands for representational state transfer.",
                    Developer = "Roy Fielding"
                },
                new Skill
                {
                    Id = 28,
                    DateAdded = DateTime.Now,
                    Title = "Chart.js",
                    Icon = "~/Content/Icons/skill-icons/chartjs-logo.svg",
                    Description = "Chart.js is an open-source JavaScript library for data visualization.",
                },
                new Skill
                {
                    Id = 29,
                    DateAdded = DateTime.Now,
                    Title = "Heroku",
                    Icon = "~/Content/Icons/skill-icons/heroku.png",
                    Description = "Heroku is a platform as a service (PaaS) that enables developers to build, run, and operate applications entirely in the cloud.",
                },
                new Skill
                {
                    Id = 30,
                    DateAdded = DateTime.Now,
                    Title = "Netlify",
                    Icon = "~/Content/Icons/skill-icons/netlify.svg",
                    Description = "Netlify is a frontend web hosting platform.",
                },
                new Skill
                {
                    Id = 31,
                    DateAdded = DateTime.Now,
                    Title = "Hostinger",
                    Icon = "~/Content/Icons/skill-icons/hostinger.svg",
                    Description = "Hostinger is shared web hosting.",
                },
                new Skill
                {
                    Id = 32,
                    DateAdded = DateTime.Now,
                    Title = "Azure",
                    Icon = "~/Content/Icons/skill-icons/azure.png",
                    Description = "Azure is a public cloud computing platform—with solutions including Infrastructure as a Service (IaaS), Platform as a Service (PaaS), and Software as a Service (SaaS) that can be used for services such as analytics, virtual computing, storage, and networking.",
                }


            };
            skills.ForEach(skill => context.Skills.AddOrUpdate(s => s.Title, skill));


            // ADD PROJECT SKILLS FOR STRENGTH
            var projectSkills = AddProjectSkillsToList(1, new int[] { 8, 10, 11, 14, 15, 17, 20, 22, 23, 24, 27, 28, 29, 30 });

            // ADD PROJECTSKILLS FOR DYNO
            projectSkills = AddProjectSkillsToList(2, new int[] { 1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 15, 17, 27, 32 }, projectSkills);

            // ADD PROJECTSKILLS FOR BAMBERGA MINERALS
            projectSkills = AddProjectSkillsToList(5, new int[] { 8, 10, 11, 25, 26, 30 }, projectSkills);

            // ADD PROJECTSKILLS FOR WEATHER BEACH
            projectSkills = AddProjectSkillsToList(6, new int[] { 8, 10, 11, 14, 15, 18, 27, 30 }, projectSkills);

            // ADD PROJECTSKILLS FOR PANDEMIC TRACKER
            projectSkills = AddProjectSkillsToList(7, new int[] { 8, 10, 11, 27, 28, 30 }, projectSkills);

            // ADD PROJECTSKILLS FOR IO CHAT
            projectSkills = AddProjectSkillsToList(8, new int[] { 8, 10, 11, 20, 29, 30 }, projectSkills);

            // ADD PROJECTSKILLS FOR CUMBERLAND HONEY
            projectSkills = AddProjectSkillsToList(9, new int[] { 8, 10, 11, 16, 22, 27, 30 }, projectSkills);

            projectSkills.ForEach(projectSkill => context.ProjectSkills.AddOrUpdate(ps => ps.Id, projectSkill));



            context.SaveChanges();
        }
    }
}
