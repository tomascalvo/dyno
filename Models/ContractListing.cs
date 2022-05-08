using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class ContractListing
    {
        // Properties
        public int Id { get; set; }
        public int ContractId { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string Specifications { get; set; }

        // Navigation Properties
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ContractBid> Bids { get; set; }
        public string AddedById { get; set; }
        public ApplicationUser AddedBy { get; set; }

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
        [DataType(DataType.Date)]
        public DateTime? DateArchived { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateDue { get; set; }
    }
}