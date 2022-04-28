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
        private DateTime? signupDate = null;
        [Display(Name = "Signup Date")]
        [DataType(DataType.DateTime), Required]
        public DateTime SignupDate
        {
            get
            {
                return this.signupDate ?? DateTime.Now;
            }

            set { this.signupDate = value; }
        }
        [Display(Name = "Color Preference")]
        public ColorPreference? ColorPreference { get; set; }

        // NAVIGATION PROPERTIES
        [Display(Name = "Current Role")]
        public int? CurrentRoleId { get; set; }
        [Display(Name = "Desired Role")]
        public int? DesiredRoleId { get; set; }
        [Display(Name = "Accessible Job Listings")]

        public List<EmploymentListingAccess> EmploymentListingAccesses { get; set; }
        [Display(Name = "Job Listings Posted")]

        public List<EmploymentListing> EmploymentListingsCreated { get; set; }
        public List<ApplicationUserProject> ApplicationUserProjects { get; set; }
        //public List<ApplicationUserCompany> ApplicationUserCompanies { get; set; }
        //public List<ApplicationUserSkill> ApplicationUserSkills { get; set; }
        //public List<ApplicationUserAward> ApplicationUserAwards { get; set; }
        //public List<ApplicationUserCourse> ApplicationUserCourses { get; set; }
        //public List<int> EmploymentListingsAuthoredIds { get; set; }
        //[Display(Name = "Employment Listings Submitted")]
        //public List<EmploymentListing> EmploymentListingsSubmitted { get; set }
        //public List<EmploymentListingSave> EmploymentListingsSaved { get; set; }
        //public List<int> EmploymentApplicationIds { get; set; }
        //public List<EmploymentApplication> EmploymentApplications { get; set; }
        //public List<EmploymentRating> EmploymentRatings { get; set; }


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