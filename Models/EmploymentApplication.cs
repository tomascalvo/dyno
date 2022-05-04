using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class EmploymentApplication
    {
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Cover Letter")]
        public string CoverLetter { get; set; }
        [Display(Name = "Date Applied")]
        public DateTime DateApplied { get; set; } = DateTime.Now;
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [Range(0, 5, ErrorMessage = "Rating value must be between 0 and 5.")]
        public int? Rating { get; set; }

        // Navigation Properties
        public string ApplicantId { get; set; }
        [Display(Name = "Applicant")]
        public ApplicationUser Applicant { get; set; }
        public int EmploymentListingId { get; set; }
        [Display(Name = "Employment Ad")]
        public EmploymentListing EmploymentListing { get; set; }
    }
}