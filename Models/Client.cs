using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class Client
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Email address cannot be longer than 50 characters.")]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

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
        public int? CurrentCompanyId { get; set; }
        [Display(Name = "Company")]
        public Company CurrentCompany { get; set; }
        [Display(Name = "Contracts")]
        public List<Company> FormerCompanies { get; set; }
        public List<ContractListing> ContractListings { get; set; }
        public List<ContractListing> Contracts { get; set; }
        //public List<Social> Socials { get; set; }
    }
}