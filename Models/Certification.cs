using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class Certification
    {
        // Properties
        [DataType(DataType.Url)]
        public string Url { get; set; }
        public string Image { get; set; }

        // DateTime Properties
        [DataType(DataType.Date)]

        public DateTime DateAdded { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]

        [Display(Name = "Awarded")]

        public DateTime DateAwarded { get; set; }

        // Navigation Properties
        [Key, Column(Order = 0)]
        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }
        [Key, Column(Order = 1)]
        public int CertificationTypeId { get; set; }
        public CertificationType CertificationType { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}