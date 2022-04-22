using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public enum Currency
    {
        USD, BTC, ETH, EUR, CAD, MXN, GBP, AUD, SGD
    };
    public enum PayFrequency
    {
        annual, hourly, lumpSum
    };
    public class EmploymentListing
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Job title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Pay Quantity")]
        public int? PayQuantity { get; set; }
        public Currency? Currency { get; set; } = Models.Currency.USD;
        [Display(Name = "Pay Frequency")]
        public PayFrequency? PayFrequency { get; set; } = Models.PayFrequency.annual;
        [Display(Name = "Remote")]
        public bool? IsRemote { get; set; }
        public int? ClientCompanyId { get; set; }
        public virtual Company ClientCompany { get; set; }
        //public int? StaffingCompanyId { get; set; }
        //public virtual Company StaffingCompany { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string FullText { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        public List<int> EmploymentApplicationIds { get; set; }
        public ICollection<EmploymentApplication> EmploymentApplications { get; set; }
        //public int? RecruiterID { get; set; }
        //public virtual Recruiter Recruiter { get; set; }
        //public int? PlatformID { get; set; }
        //public virtual Platform Platform { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Published")]
        public DateTime? DatePublished { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Added")]

        public DateTime DateAdded { get; set; } = DateTime.Now;
        //public int? UserID { get; set; }
        //[Display(Name = "Added By")]
        //public virtual User User { get; set; }
        [Display(Name = "Requirements")]
        public List<EmploymentListingSkill> EmploymentListingSkills { get; set; }
        //[Display(Name = "Likes")]
        //public virtual ICollection<User> Users { get; set; }
        //[Display(Name = "Offers")]
        //public virtual ICollection<EmploymentOffer> EmploymentOffers { get; set; }
        //[Display(Name = "Ratings")]
        //public virtual ICollection<EmploymentRating> EmploymentRatings { get; set; }
    }
}