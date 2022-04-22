using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class EmploymentApplication
    {
        public int Id { get; set; }
        //public int UserID { get; set; }
        //[Display(Name = "Applicant")]
        //public virtual User User { get; set; }
        public int EmploymentListingId { get; set; }
        [Display(Name = "Employment Ad")]
        public virtual EmploymentListing EmploymentListing { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Cover Letter")]
        public string CoverLetter { get; set; }
        [Display(Name = "Date Applied")]
        public DateTime? DateApplied { get; set; }
    }
}