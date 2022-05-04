using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class EmploymentOffer
    {
        // PROPERTIES
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string FullText { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [DataType(DataType.MultilineText)]
        public string Terms { get; set; }
        [DataType(DataType.MultilineText)]
        public string Benefits { get; set; }

        [Range(1, 5, ErrorMessage = "Rating value must be between 1 and 5.")]
        public int? Rating { get; set; }
        // DATETIME PROPERTIES
        [Display(Name = "Offered")]
        [DataType(DataType.Date)]
        public DateTime? DateOffered { get; set; } = DateTime.Now;
        [Display(Name = "Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Expiration { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Accepted { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Declined { get; set; }

        // PAYMENT PROPERTIES
        [Display(Name = "Salary")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public int? PayQuantity { get; set; }
        public CurrencyEnum? Currency { get; set; } = Models.CurrencyEnum.USD;
        public PayFrequencyEnum? PayFrequency { get; set; } = Models.PayFrequencyEnum.annual;

        // NAVIGATION PROPERTIES
        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }
        public int EmploymentListingId { get; set; }
        [Display(Name = "Employment Listing")]
        public EmploymentListing EmploymentListing { get; set; }
        public int? RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
    }
}