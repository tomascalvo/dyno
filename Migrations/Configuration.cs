namespace DevPath.Migrations
{
    using DevPath.Models;
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
            context.SaveChanges();
        }
    }
}
