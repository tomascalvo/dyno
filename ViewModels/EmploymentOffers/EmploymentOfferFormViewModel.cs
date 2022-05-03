using DevPath.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.ViewModels.EmploymentOffers
{
    public class EmploymentOfferFormViewModel
    {
        // Constructors

        public EmploymentOfferFormViewModel()
        {
            Id = 0;
            DateOffered = DateTime.Now;
        }

        public EmploymentOfferFormViewModel(EmploymentOfferFormViewModel offer)
        {

        }

        // Domain Model Properties

        public int? Id { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string FullText { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [DataType(DataType.MultilineText)]
        public string Terms { get; set; }
        [DataType(DataType.MultilineText)]
        public string Benefits { get; set; }
        [Range(0, 5, ErrorMessage = "Rating value must be between 0 and 5.")]
        public int? Rating { get; set; }

        // DateTime Properties

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

        // Payment Properties

        [Display(Name = "Payment Quantity")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public int? PayQuantity { get; set; } = 50000;
        public CurrencyEnum? Currency { get; set; } = Models.CurrencyEnum.USD;
        public PayFrequencyEnum? PayFrequency { get; set; } = Models.PayFrequencyEnum.annual;

        // Navigation Properties

        [Display(Name = "Recipient")]
        public string SelectedRecipientId { get; set; }
        //public ApplicationUser Recipient { get; set; }
        [Display(Name = "Employment Listing")]
        public int SelectedEmploymentListingId { get; set; }
        //public EmploymentListing EmploymentListing { get; set; }
        [Display(Name = "Recruiter")]
        public int? SelectedRecruiterId { get; set; }
        //public Recruiter Recruiter { get; set; }

        // Select Options

        public IEnumerable<EmploymentListing> EmploymentListingOptions { get; set; }
        public IEnumerable<Recruiter> RecruiterOptions { get; set; }

        // View Data

        public string ViewTitle
        {
            get
            {
                if (Id != 0)
                {
                    return "Edit Job Offer";
                }
                else
                {
                    return "New Job Offer";
                }
            }
        }

    }
}