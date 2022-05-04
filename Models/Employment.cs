using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class Employment
    {
        // Constructors

        // Properties
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        // Payment Properties
        [Display(Name = "Starting Payment Quantity")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public int? InitialPay { get; set; }
        [Display(Name = "Final Payment Quantity")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public int? FinalPay { get; set; }
        public CurrencyEnum? Currency { get; set; } = Models.CurrencyEnum.USD;
        [Display(Name = "Pay Frequency")]
        public PayFrequencyEnum? PayFrequency { get; set; } = PayFrequencyEnum.annual;

        // DateTime Properties
        [Display(Name = "Date Added")]
        [DataType(DataType.DateTime), Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Quit Date")]
        [DataType(DataType.DateTime)]
        public DateTime? QuitDate { get; set; }

        // Location Properties
        public string Country { get; set; }
        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string Location
        {
            get
            {
                if (this.Country == null || this.City == null)
                {
                    return this.Employer.Location;
                }
                var locationString =
                    $"{this.City}" +
                    $"{((this.City != null && this.Country != null) ? ", " : "")}" +
                    $"{this.StateProvince}" +
                    $"{((this.StateProvince != null && this.Country != null) ? ", " : "")}" +
                    $"{this.Country}";
                return locationString;
            }
        }

        // Navigation Properties
        [Required]
        public string EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }
        [Required]
        public int EmployerId { get; set; }
        public Company Employer { get; set; }
        public int? OfferId { get; set; }
        public EmploymentOffer Offer { get; set; }
        [Display(Name = "Skills")]
        public List<EmploymentSkill> EmploymentSkills { get; set; }
        public List<EmploymentProject> EmploymentProjects { get; set; }
    }
}