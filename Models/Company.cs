using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class Company
    {
        // PROPERTIES
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Logo { get; set; }
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }
        public bool IsStaffingCompany { get; set; } = false;
        public string OrganizationLookupId { get; set; }
        // LOCATION PROPERTIES
        public string Country { get; set; }
        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string Location
        {
            get
            {
                var locationString =
                    $"{this.City}" +
                    $"{((this.City != null && this.Country != null) ? ", " : "")}" +
                    $"{this.StateProvince}" +
                    $"{((this.StateProvince != null && this.Country != null) ? ", " : "")}" +
                    $"{this.Country}";
                return locationString;
            }
        }
        //DATETIME PROPERTIES
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime? DateFounded { get; set; }
        private DateTime? dateAdded = null;
        [Display(Name = "Date Added")]

        public DateTime DateAdded
        {
            get
            {
                return this.dateAdded ?? DateTime.Now;
            }

            set { this.dateAdded = value; }
        }


        // NAVIGATION PROPERTIES
        [Display(Name = "Employment Listings")]
        public List<EmploymentListing> EmploymentListings { get; set; }

        public List<Recruiter> RecruitersEmployed { get; set; }
        public List<RecruiterClient> RecruitersConsulting { get; set; }
        public Platform Platform { get; set; }

        //public string CEOId { get; set; }
        //public virtual ICollection<Social> Socials { get; set; }
    }
}