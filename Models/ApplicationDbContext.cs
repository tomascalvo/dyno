using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DevPath.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EmploymentListing> EmploymentListings { get; set; }
        public DbSet<EmploymentListingSkill> EmploymentListingSkills { get; set; }
        public DbSet<EmploymentApplication> EmploymentApplications { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Using fluent API to customize M2M relationship between Project and Skill.

            modelBuilder.Entity<ProjectSkill>()
                //.HasKey(ps => new { ps.ProjectId, ps.SkillId }); /*THIS LINE CONFIGURES A COMPOSITE PRIMARY KEY */
                .HasKey(ps => ps.Id);
            modelBuilder.Entity<ProjectSkill>()
                .HasRequired(ps => ps.Project)
                .WithMany(p => p.ProjectSkills)
                .HasForeignKey(ps => ps.ProjectId);
            modelBuilder.Entity<ProjectSkill>()
                .HasRequired(ps => ps.Skill)
                .WithMany(s => s.ProjectSkills)
                .HasForeignKey(ps => ps.SkillId);

            // Customize 1 TO MANY Relationship between Company and EmploymentListing

            modelBuilder.Entity<Company>()
                .HasMany(c => c.EmploymentListings)
                .WithRequired(el => el.ClientCompany)
                .HasForeignKey(el => el.ClientCompanyId);

            // Customize Many to Many relationship between EmploymentListing and Skill

            modelBuilder.Entity<EmploymentListingSkill>()
                .HasKey(els => new { els.EmploymentListingId, els.SkillId });
            modelBuilder.Entity<EmploymentListingSkill>()
                .HasRequired(els => els.EmploymentListing)
                .WithMany(el => el.EmploymentListingSkills)
                .HasForeignKey(els => els.EmploymentListingId);
            modelBuilder.Entity<EmploymentListingSkill>()
                .HasRequired(els => els.Skill)
                .WithMany(s => s.EmploymentListingSkills)
                .HasForeignKey(els => els.SkillId);


            // Customize 1 TO MANY Relationship between EmploymentListing and EmploymentApplication

            modelBuilder.Entity<EmploymentListing>()
                .HasMany(el => el.EmploymentApplications)
                .WithRequired(ea => ea.EmploymentListing)
                .HasForeignKey(ea => ea.EmploymentListingId);

            // This method is necessary because this MVC app is using Identity Framework and the DbContext needs to include the built-in identity models as datasets.

            base.OnModelCreating(modelBuilder);
        }
    }
}