using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        // Metadata
        public bool IsApproved { get; set; } = false;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation Properties
        public string AddedById { get; set; }
        public ApplicationUser AddedBy { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<EmploymentListing> EmploymentListings { get; set; }
        [Display(Name = "Certifications Offered")]
        public List<CertificationType> CertificationTypes { get; set; }
        public List<Course> Courses { get; set; }
        //public List<AwardType> Awards { get; set; }
        //public List<TutorialType> Tutorials { get; set; }
    }
}