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
            ContextKey = "DevPath.Models.ApplicationDbContext";
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
                    Description = "A MERN stack web app for tracking fitness and training together remotely.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/otrera-server",
                    DeploymentUrl = "https://otrera.netlify.app",
                    Added = DateTime.Now,
                },
                new Project
                {
                    Id = 2,
                    Title = "DevPath",
                    Description = "An ASP.NET web app for matching web developers with relevant jobs and learning resources.",
                    //OwnerID = 1,
                    RepositoryUrl = "https://github.com/tomascalvo/devpath",
                    DeploymentUrl = "https://devpath.azurewebsites.net/",
                },
                new Project
                {
                    Id = 3,
                    Title = "Consensa",
                    Description = "An ASP.NET web app for ranked-choice voting that uses QR codes, websockets, encryption, and a distributed database to help users quickly reach consensus.",
                    //OwnerID = 1,
                    RepositoryUrl = "",
                    DeploymentUrl = "",
                },
                new Project
                {
                    Id = 4,
                    Title = "Indulgence",
                    Description = "An ASP.NET web app that allows users to purchase beautifully illustrated illuminated certificates stored in a distributed database that absolves them of their microaggressions.",
                    //OwnerID = 1,
                    RepositoryUrl = "",
                    DeploymentUrl = "",
                },
            };
            projects.ForEach(project => context.Projects.AddOrUpdate(p => p.Id, project));
            context.SaveChanges();
        }
    }
}
