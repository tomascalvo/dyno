using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public enum CurrencyEnum
    {
        USD, BTC, ETH, EUR, CAD, MXN, GBP, AUD, SGD
    };
    public enum PayFrequencyEnum
    {
        [Display(Name = "Annual Salary")]
        annual,
        [Display(Name = "Hourly Wage")]
        hourly,
        [Display(Name = "Lump Sum")]
        lumpSum
    };
    public enum WorkLocationEnum
    {
        [Display(Name = "Fully Remote")]
        remote,
        [Display(Name = "In Office")]
        office,
        [Display(Name = "Hybrid")]
        hybrid
    }
    public class EmploymentListing
    {
        // PROPERTIES
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Job title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string FullText { get; set; }
        [Display(Name = "Work Arrangement")]
        public WorkLocationEnum? WorkLocation { get; set; }

        // DATETIME PROPERTIES
        [DataType(DataType.DateTime)]
        [Display(Name = "Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime? DateArchived { get; set; }

        // PAYMENT PROPERTIES
        [Display(Name = "Payment Quantity")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public int? PayQuantity { get; set; }
        public CurrencyEnum? Currency { get; set; } = Models.CurrencyEnum.USD;
        [Display(Name = "Pay Frequency")]
        public PayFrequencyEnum? PayFrequency { get; set; } = Models.PayFrequencyEnum.annual;

        // NAVIGATION PROPERTIES
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        [Display(Name = "Owner(s)")]
        public List<EmploymentListingAccess> EmploymentListingAccesses { get; set; }
        public int? ClientCompanyId { get; set; }
        [Display(Name = "Client Company")]

        public Company ClientCompany { get; set; }
        public int? StaffingCompanyId { get; set; }
        [Display(Name = "Staffing Company")]

        public Company StaffingCompany { get; set; }
        public List<int> EmploymentApplicationIds { get; set; }
        public ICollection<EmploymentApplication> EmploymentApplications { get; set; }
        [Display(Name = "Requirements")]
        public List<EmploymentListingSkill> EmploymentListingSkills { get; set; }
        public int? RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
        [Display(Name = "Offers")]
        public List<EmploymentOffer> EmploymentOffers { get; set; }
        //public int? PlatformID { get; set; }
        //public virtual Platform Platform { get; set; }
        //[Display(Name = "Ratings")]
        //public virtual ICollection<EmploymentRating> EmploymentRatings { get; set; }
    }
}