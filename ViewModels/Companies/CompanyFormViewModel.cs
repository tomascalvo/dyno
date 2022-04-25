using DevPath.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.ViewModels.Companies
{
    public class CompanyFormViewModel
    {
        // CONSTRUCTORS

        public CompanyFormViewModel()
        {
            Id = 0;
            DateAdded = DateTime.Now;
        }

        public CompanyFormViewModel(Company company)
        {
            Id = company.Id;
            Title = company.Title;
            Description = company.Description;
            Logo = company.Logo;
            WebsiteUrl = company.WebsiteUrl;
            Country = company.Country;
            StateProvince = company.StateProvince;
            City = company.City;
            DateFounded = company.DateFounded;
            DateAdded = company.DateAdded;
        }

        // MODEL PROPERTIES


        // the Id property is nullable in this ViewModel because the ViewModel can be used both to edit an existing Company record (in which case Id is not null) or a new Company record( in which case the Id is null)
        public int? Id { get; set; }
        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Logo { get; set; }
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }
        //public virtual ICollection<Social> Socials { get; set; }
        public string Country { get; set; }
        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }
        public string City { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime? DateFounded { get; set; }
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        //public string OrganizationLookupId { get; set; }


        // Page Title

        public string PageTitle
        {
            get
            {
                if (Id != 0)
                {
                    return "Edit Company";
                }
                else
                {
                    return "New Company";
                }
            }
        }

    }
}