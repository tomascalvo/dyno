using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DevPath.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            // THIS DATABASE INITIALIZATION STRATEGY DROPS AND CREATES A DB ONLY WHEN MODEL CLASSES (ENTITY CLASSES) HAVE BEEN CHANGED
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

            // THIS DATABASE INITIALIZATION STRATEGY DROPS AND CREATES A DB EVERY TIME THE APPLICATION RUNS IRRESPECTIVE OF MODEL CHANGES OR LACK THEREOF.
            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());

            //DATABASE INITIALIZATION STRATEGIES EXPLAINED: https://www.entityframeworktutorial.net/code-first/database-initialization-strategy-in-code-first.aspx

            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EmploymentListing> EmploymentListings { get; set; }
        public DbSet<EmploymentListingSkill> EmploymentListingSkills { get; set; }
        public DbSet<EmploymentApplication> EmploymentApplications { get; set; }
        public DbSet<ApplicationUserProject> ApplicationUserProjects { get; set; }
        public DbSet<EmploymentListingAccess> EmploymentListingAccesses { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<EmploymentOffer> EmploymentOffers { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<EmploymentSkill> EmploymentSkills { get; set; }
        public DbSet<SkillHierarchy> Prerequisites { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<CertificationType> CertificationTypes { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCompletion> CourseCompletions { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<DocumentationRead> DocumentationRead { get; set; }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Using fluent API to customize M2M relationship between Project and Skill.

            modelBuilder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.ProjectId, ps.SkillId }); /*THIS LINE CONFIGURES A COMPOSITE PRIMARY KEY */
            //.HasKey(ps => ps.Id);
            modelBuilder.Entity<ProjectSkill>()
                .HasRequired(ps => ps.Project)
                .WithMany(p => p.ProjectSkills)
                .HasForeignKey(ps => ps.ProjectId);
            modelBuilder.Entity<ProjectSkill>()
                .HasRequired(ps => ps.Skill)
                .WithMany(s => s.ProjectSkills)
                .HasForeignKey(ps => ps.SkillId);

            // Customize 1 (or 0) to many Relationship between ClientCompany and EmploymentListing

            modelBuilder.Entity<Company>()
                .HasMany(c => c.EmploymentListings)
                .WithOptional(el => el.ClientCompany)
                .HasForeignKey(el => el.ClientCompanyId);

            // Customize 1 (or 0) to many Relationship between StaffingCompany and EmploymentListing

            modelBuilder.Entity<Company>()
                .HasMany(c => c.EmploymentListings)
                .WithOptional(el => el.StaffingCompany)
                .HasForeignKey(el => el.StaffingCompanyId);

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

            // CUSTOMIZE MANY TO MANY Relationship (EmploymentListingAccess) between ApplicationUser and EmploymentListing (FOR CONTROLLING JOB LISTING EDIT/DELETE PRIVILEGES)

            modelBuilder.Entity<EmploymentListingAccess>()
                .HasKey(ela => new { ela.ApplicationUserId, ela.EmploymentListingId });
            modelBuilder.Entity<EmploymentListingAccess>()
                .HasRequired(ela => ela.ApplicationUser)
                .WithMany(au => au.EmploymentListingAccesses)
                .HasForeignKey(ela => ela.ApplicationUserId);
            modelBuilder.Entity<EmploymentListingAccess>()
                .HasRequired(ela => ela.EmploymentListing)
                .WithMany(el => el.EmploymentListingAccesses)
                .HasForeignKey(ela => ela.EmploymentListingId);

            // 0 OR 1 TO MANY // EmploymentListing & ApplicationUser / EmploymentListingsCreated & Creator 

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.EmploymentListingsCreated)
                .WithOptional(el => el.Creator)
                .HasForeignKey(el => el.CreatorId);

            // User and EmploymentApplication: One to Many
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.EmploymentApplications)
                .WithRequired(application => application.Applicant)
                .HasForeignKey(application => application.ApplicantId);

            // Customize M2M relationship between ApplicationUser and Project

            modelBuilder.Entity<ApplicationUserProject>()
                .HasKey(aup => new { aup.ApplicationUserId, aup.ProjectId });
            modelBuilder.Entity<ApplicationUserProject>()
                .HasRequired(aup => aup.ApplicationUser)
                .WithMany(au => au.ApplicationUserProjects)
                .HasForeignKey(aup => aup.ApplicationUserId);
            modelBuilder.Entity<ApplicationUserProject>()
                .HasRequired(aup => aup.Project)
                .WithMany(p => p.ApplicationUserProjects)
                .HasForeignKey(aup => aup.ProjectId);

            // MANY TO 1 OR 0 RELATIONSHIP: (Staffing) Company to Recruiters
            modelBuilder.Entity<Company>()
                .HasMany(c => c.RecruitersEmployed)
                .WithOptional(r => r.StaffingCompany)
                .HasForeignKey(r => r.StaffingCompanyId);

            // MANY TO MANY RELATIONSHIP: Recruiters to Client Companies
            modelBuilder.Entity<RecruiterClient>()
                .HasKey(rc => new { rc.RecruiterId, rc.CompanyId });
            modelBuilder.Entity<RecruiterClient>()
                .HasRequired(rc => rc.Recruiter)
                .WithMany(r => r.RecruiterClients)
                .HasForeignKey(rc => rc.RecruiterId);
            modelBuilder.Entity<RecruiterClient>()
                .HasRequired(rc => rc.Company)
                .WithMany(c => c.RecruitersConsulting)
                .HasForeignKey(rc => rc.CompanyId);

            // MANY TO ONE OR ZERO RELATIONSHIP: RecruiterClients to ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.RecruiterClientsCreated)
                .WithOptional(el => el.Creator)
                .HasForeignKey(el => el.CreatorId);

            // EmploymentListing to EmploymentOffer: 1 TO MANY
            modelBuilder.Entity<EmploymentListing>()
                .HasMany(el => el.EmploymentOffers)
                .WithRequired(eo => eo.EmploymentListing)
                .HasForeignKey(eo => eo.EmploymentListingId);

            // Recruiter to EmploymentOffer: 1 TO MANY
            modelBuilder.Entity<Recruiter>()
                .HasMany(r => r.EmploymentOffers)
                .WithOptional(eo => eo.Recruiter)
                .HasForeignKey(eo => eo.RecruiterId);

            // ApplicationUser to EmploymentOffer: 1 TO MANY
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.EmploymentOffers)
                .WithRequired(eo => eo.Recipient)
                .HasForeignKey(eo => eo.RecipientId);

            // ApplicationUser to Employment: 1 TO MANY
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.Employments)
                .WithRequired(employment => employment.Employee)
                .HasForeignKey(employment => employment.EmployeeId);

            // Employment to Skill: Many to Many
            modelBuilder.Entity<EmploymentSkill>()
                .HasKey(es => new { es.EmploymentId, es.SkillId });
            modelBuilder.Entity<EmploymentSkill>()
                .HasRequired(es => es.Employment)
                .WithMany(e => e.EmploymentSkills)
                .HasForeignKey(es => es.EmploymentId);
            modelBuilder.Entity<EmploymentSkill>()
                .HasRequired(es => es.Skill)
                .WithMany(s => s.EmploymentSkills)
                .HasForeignKey(es => es.SkillId);

            // Employment to Project: Many to Many
            modelBuilder.Entity<EmploymentProject>()
                .HasKey(ep => new { ep.EmploymentId, ep.ProjectId });
            modelBuilder.Entity<EmploymentProject>()
                .HasRequired(ep => ep.Employment)
                .WithMany(e => e.EmploymentProjects)
                .HasForeignKey(ep => ep.EmploymentId);
            modelBuilder.Entity<EmploymentProject>()
                .HasRequired(ep => ep.Project)
                .WithMany(p => p.EmploymentProjects)
                .HasForeignKey(ep => ep.ProjectId);

            // SkillHierarchy to Principal: Many to One Relationship
            modelBuilder.Entity<SkillHierarchy>()
                .HasKey(sh => sh.Id);
            modelBuilder.Entity<SkillHierarchy>()
                .HasRequired(sh => sh.Principal)
                .WithMany(principal => principal.Prerequisites)
                .HasForeignKey(sh => sh.PrincipalId)
                .WillCascadeOnDelete(false);

            // SkillHierarchy to Prerequisite: Many to Many Relationship

            modelBuilder.Entity<SkillHierarchyPrerequisite>()
                .HasKey(shp => new { shp.SkillHierarchyId, shp.PrerequisiteId });
            modelBuilder.Entity<SkillHierarchyPrerequisite>()
                .HasRequired(shp => shp.SkillHierarchy)
                .WithMany(sh => sh.Prerequisites)
                .HasForeignKey(shp => shp.SkillHierarchyId);
            modelBuilder.Entity<SkillHierarchyPrerequisite>()
                .HasRequired(shp => shp.Prerequisite)
                .WithMany(p => p.Principals)
                .HasForeignKey(shp => shp.PrerequisiteId);

            // SkillHierarchy to Creator: Many to One or Zero Relationship
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.SkillHierarchies)
                .WithOptional(el => el.Creator)
                .HasForeignKey(el => el.CreatorId);

            // Company to Platform: One to One or Zero
            modelBuilder.Entity<Company>()
                .HasOptional(c => c.Platform)
                .WithRequired(p => p.Company);

            // Platform to EmploymentListings: One to Many
            modelBuilder.Entity<Platform>()
                .HasMany(p => p.EmploymentListings)
                .WithOptional(el => el.Platform)
                .HasForeignKey(el => el.PlatformId);

            // Platform to ApplicationUser: Many to One or Zero
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.PlatformsAdded)
                .WithOptional(p => p.AddedBy)
                .HasForeignKey(p => p.AddedById);

            // CertificationType to Platform: Many to One
            modelBuilder.Entity<Platform>()
                .HasMany(p => p.CertificationTypes)
                .WithRequired(ct => ct.Platform)
                .HasForeignKey(ct => ct.PlatformId);

            // Certification to CertificationType: Many to One
            modelBuilder.Entity<CertificationType>()
                .HasMany(type => type.CertificationsAwarded)
                .WithRequired(award => award.CertificationType)
                .HasForeignKey(award => award.CertificationTypeId);

            // CertificationType to AddedBy: Many to One or Zero
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.CertificationTypesAdded)
                .WithOptional(type => type.AddedBy)
                .HasForeignKey(type => type.AddedById);

            // Certification to Recipient: Many to One
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.Certifications)
                .WithRequired(award => award.Recipient)
                .HasForeignKey(award => award.RecipientId);

            //!!!!!!!!!!!!!!!!!!!!!!!!
            // CertificationType to Course: One or Zero to One or Zero
            modelBuilder.Entity<CertificationType>()
                .HasOptional(ct => ct.Course);

            // Course to CourseCompletions: One to Many
            modelBuilder.Entity<Course>()
                .HasMany(course => course.Completions)
                .WithRequired(completion => completion.Course)
                .HasForeignKey(completion => completion.CourseId);

            // Course to User: One or Zero to Many
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(user => user.CoursesAdded)
                .WithOptional(course => course.AddedBy)
                .HasForeignKey(course => course.AddedById);

            // Platform to Courses: One or Zero to Many
            modelBuilder.Entity<Platform>()
                .HasMany(p => p.Courses)
                .WithOptional(c => c.Platform)
                .HasForeignKey(c => c.PlatformId);

            // Course to Skills: Many to Many
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Skills)
                .WithMany(s => s.Courses)
                .Map(cs =>
                {
                    cs.MapLeftKey("CourseRefId");
                    cs.MapRightKey("SkillRefId");
                    cs.ToTable("CourseSkills");
                });

            // CourseCompletion to User: Many to One
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.CourseCompletions)
                .WithRequired(cc => cc.User)
                .HasForeignKey(cc => cc.UserId);

            // Users to Goals: One to Many
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Goals)
                .WithRequired(g => g.User)
                .HasForeignKey(g => g.UserId);

            // Goals to Skills: Many to Many
            modelBuilder.Entity<Goal>()
                .HasMany(g => g.Skills)
                .WithMany(s => s.Goals)
                .Map(sg =>
                {
                    sg.MapLeftKey("GoalRefId");
                    sg.MapRightKey("SkillRefId");
                    sg.ToTable("GoalSkills");
                });

            // DocumentationRead to User : One or zero to One
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.DocumentationRead)
                .WithRequired(d => d.User)
                .HasForeignKey(d => d.UserId);


            // DocumentationRead to Skill: One or zero to One
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.DocumentationRead)
                .WithRequired(d => d.Skill)
                .HasForeignKey(d => d.SkillId);


            // This method is necessary because this MVC app is using Identity Framework and the DbContext needs to include the built-in identity models as datasets.

            base.OnModelCreating(modelBuilder);
        }
    }
}