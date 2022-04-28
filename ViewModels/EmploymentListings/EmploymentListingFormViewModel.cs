using DevPath.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.ViewModels.EmploymentListings
{
    public class EmploymentListingFormViewModel
    {
        // CONSTRUCTORS
        public EmploymentListingFormViewModel()
        {
            Id = 0; // This constructor initializes Id to 0 so that when the form is used to create a new EL record the controller recognizes it as new and not an existing EL record.
            DateAdded = DateTime.Now;
        }

        public EmploymentListingFormViewModel(EmploymentListing employmentListing) // This constructor is for editing an existing record.
        {
            Id = employmentListing.Id;
            Title = employmentListing.Title;
            PayQuantity = employmentListing.PayQuantity;
            WorkLocation = employmentListing.WorkLocation;
            FullText = employmentListing.FullText;
            Url = employmentListing.Url;
            DateAdded = employmentListing.DateAdded;
            IsArchived = employmentListing.DateArchived != null ? true : false;
            EmploymentApplicationIds = employmentListing.EmploymentApplicationIds;
            SelectedClientCompanyId = employmentListing.ClientCompanyId;
            SelectedSkillIds = employmentListing.EmploymentListingSkills.Select(els => els.Skill.Id).ToList();
        }

        // DOMAIN MODEL PROPERTIES
        public int? Id { get; set; }
        [StringLength(50, ErrorMessage = "Job title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Pay Quantity")]
        public int? PayQuantity { get; set; } = 50000;
        [Display(Name = "Work Arrangement")]
        public WorkLocationEnum? WorkLocation { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string FullText { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Published")]
        public DateTime? DatePublished { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Added")]
        public DateTime DateAdded { get; set; }
        public List<int> EmploymentApplicationIds { get; set; }
        public bool ApplicationSubmitted { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public ICollection<EmploymentApplication> EmploymentApplications { get; set; }


        // COMPANY OPTIONS
        public IEnumerable<Company> ClientCompanyOptions
        { get; set; }
        [Display(Name = "Client Company")]
        public int? SelectedClientCompanyId { get; set; }
        public IEnumerable<Company> StaffingCompanyOptions
        { get; set; }
        [Display(Name = "Staffing Company")]
        public int? SelectedStaffingCompanyId { get; set; }

        // SKILL OPTIONS
        public IEnumerable<SelectListItem> SkillOptions { get; set; }
        [Display(Name = "Requirements")]
        public List<int> SelectedSkillIds { get; set; } = new List<int>();

        // PAGE TITLE
        public string PageTitle
        {
            get
            {
                if (Id != 0)
                {
                    return "Edit Job Listing";
                }
                else
                {
                    return "New Job Listing";
                }
            }
        }
    }
}