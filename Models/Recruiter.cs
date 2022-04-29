using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DevPath.Models
{
    public class Recruiter
    {
        // PROPERTIES
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Email address cannot be longer than 50 characters.")]
        public string Email { get; set; }

        // NAME PROPERTIES
        [Required]
        [Display(Name = "First Name")]
        [Column("FirstName")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name and middle name combined cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÜÑ]+[a-záéíóúüñA-Z""'\s-]*$")]
        public string FirstMidName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÜÑ]+[a-záéíóúüñA-Z""'\s-]*$")]
        public string LastName { get; set; }
        [Display(Name = "Name")]
        public string FullName { get { return FirstMidName + " " + LastName; } }


        // NAVIGATION PROPERTIES
        public int? StaffingCompanyId { get; set; }
        [Display(Name = "Company")]
        public Company StaffingCompany { get; set; }
        [Display(Name = "Listings")]
        public List<EmploymentListing> EmploymentListings { get; set; }
        [Display(Name = "Offers")]
        public List<RecruiterClient> RecruiterClients { get; set; }
        public List<EmploymentOffer> EmploymentOffers { get; set; }
    }
}