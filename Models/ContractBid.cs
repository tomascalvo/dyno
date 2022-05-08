using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class ContractBid
    {
        // Properties
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Navigation Properties
        public int ListingId { get; set; }
        public ContractListing Listing { get; set; }
        public int ContractorId { get; set; }
        public ApplicationUser Contractor { get; set; }
        public Contract Contract { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        // PAYMENT PROPERTIES
        [Display(Name = "Payment Quantity")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public int? PayQuantity { get; set; }
        public CurrencyEnum? Currency { get; set; } = Models.CurrencyEnum.USD;
        [Display(Name = "Pay Frequency")]
        public PayFrequencyEnum? PayFrequency { get; set; } = Models.PayFrequencyEnum.annual;

        // DATETIME PROPERTIES
        [DataType(DataType.DateTime)]
        [Display(Name = "Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        [Display(Name = "Due")]
        public DateTime? DateDue { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Declined")]
        public DateTime? DateDeclined { get; set; }

    }
}