using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class Contract
    {
        // Properties
        [ForeignKey("ContractBid")]
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Navigation Properties
        public int ContractBidId { get; set; }
        public ContractBid ContractBid { get; set; }

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
        [DataType(DataType.DateTime)]
        public DateTime? DateDelivered { get; set; }
    }
}