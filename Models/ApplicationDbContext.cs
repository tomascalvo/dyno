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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Project>()
            //    .HasMany(p => p.Skills)
            //    .WithMany(s => s.Projects)
            //    .Map(m =>
            //    {
            //        m.ToTable("ProjectSkills");
            //        m.MapLeftKey("ProjectId");
            //        m.MapRightKey("SkillId");
            //    });

            modelBuilder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.ProjectId, ps.SkillId });
            modelBuilder.Entity<ProjectSkill>()
                .HasRequired(ps => ps.Project)
                .WithMany(p => p.ProjectSkills)
                .HasForeignKey(ps => ps.ProjectId);
            modelBuilder.Entity<ProjectSkill>()
                .HasRequired(ps => ps.Skill)
                .WithMany(s => s.ProjectSkills)
                .HasForeignKey(ps => ps.SkillId);

            base.OnModelCreating(modelBuilder);
        }
    }
}