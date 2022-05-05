using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevPath.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public enum ColorPreference
    {
        light, dark, system
    }

    public class ApplicationUser : IdentityUser
    {
        // CONSTRUCTOR
        public ApplicationUser()
        {
            SignupDate = DateTime.Now;
        }
        // CUSTOM PROPERTIES
        public string Avatar { get; set; }
        [Display(Name = "GitHub Username")]
        public string GitHubUsername { get; set; }
        [Display(Name = "Color Preference")]
        public ColorPreference? ColorPreference { get; set; }
        [Display(Name = "Current Role")]
        public int? CurrentRoleId { get; set; }
        [Display(Name = "Desired Role")]
        public int? DesiredRoleId { get; set; }

        // DateTime Properties
        [Display(Name = "Signup Date")]
        [DataType(DataType.DateTime), Required]
        public DateTime SignupDate { get; set; } = DateTime.Now;

        // NAVIGATION PROPERTIES

        // Db Contributions
        [Display(Name = "Job Listings Posted")]

        public List<EmploymentListing> EmploymentListingsCreated { get; set; }
        public List<RecruiterClient> RecruiterClientsCreated { get; set; }
        [Display(Name = "Prerequisites Added")]
        public List<SkillHierarchy> SkillHierarchies { get; set; }
        [Display(Name = "Platforms Added")]
        public List<Platform> PlatformsAdded { get; set; }
        [Display(Name = "Certifications Added")]
        public List<CertificationType> CertificationTypesAdded { get; set; }
        [Display(Name = "Courses Added")]
        public List<Course> CoursesAdded { get; set; }

        // Employment
        [Display(Name = "Accessible Job Listings")]

        public List<EmploymentListingAccess> EmploymentListingAccesses { get; set; }
        public List<EmploymentApplication> EmploymentApplications { get; set; }
        [Display(Name = "Job Offers")]
        public List<EmploymentOffer> EmploymentOffers { get; set; }
        [Display(Name = "Employment History")]
        public List<Employment> Employments { get; set; }

        // Achievements
        [Display(Name = "Projects")]
        public List<ApplicationUserProject> ApplicationUserProjects { get; set; }
        [Display(Name = "Certifications Earned")]
        public List<Certification> Certifications { get; set; }
        [Display(Name = "Courses Completed")]
        public List<CourseCompletion> CourseCompletions { get; set; }
        public List<Goal> Goals { get; set; }
        [Display(Name = "Documentation Read")]
        public List<DocumentationRead> DocumentationRead { get; set; }
        //public List<UserCompany> CompaniesOwned { get; set; }


        // IDENTITY FRAMEWORK
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}