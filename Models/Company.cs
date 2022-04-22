using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class Company
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Logo { get; set; }
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }
        //public virtual ICollection<Social> Socials { get; set; }
        public string Country { get; set; }
        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }
        public string City { get; set; }
        //public string CEOId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime? DateFounded { get; set; }
        public string OrganizationLookupId { get; set; }
        [Display(Name = "Employment Listings")]
        public virtual ICollection<EmploymentListing> EmploymentListings { get; set; }
        //[Display(Name = "Employment Offers")]
        //public virtual ICollection<EmploymentOffer> EmploymentOffers { get; set; }
        //public virtual ICollection<Recruiter> Recruiters { get; set; }
        //[Display(Name = "Certifications Offered")]

        //public virtual ICollection<Certification> Certifications { get; set; }
        //[Display(Name = "Awards Offered")]
        //public virtual ICollection<Award> Awards { get; set; }
    }
}