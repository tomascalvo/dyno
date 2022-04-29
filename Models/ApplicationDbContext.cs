﻿using Microsoft.AspNet.Identity.EntityFramework;
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

            // This method is necessary because this MVC app is using Identity Framework and the DbContext needs to include the built-in identity models as datasets.

            base.OnModelCreating(modelBuilder);
        }
    }
}