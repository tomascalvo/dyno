using DevPath.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.ViewModels.Skills
{
    public class SkillFormViewModel
    {
        public int? Id { get; set; } // Id is nullable in the ViewModel because this form can be used to edit existing Skill records (which have an Id) as well as to create new Skill records (in which case there is no Id to send to the view).
        [Required(ErrorMessage = "Enter skill title.")]
        public string Title { get; set; }
        [StringLength(1000, ErrorMessage = "Skill description cannot exceed 1000 characters in length.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Icon { get; set; }
        [StringLength(250, ErrorMessage = "Developer property cannot be longer than 250 characters.")]
        public string Developer { get; set; }

        // DateTime Properties
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
        [Display(Name = "Repository")]
        [DataType(DataType.Url)]

        // Url Properties
        public string RepositoryUrl { get; set; }
        [Display(Name = "Documentation")]
        [DataType(DataType.Url)]
        public string DocumentationUrl { get; set; }

        // Entity Relationships
        public string AddedById { get; set; }
        [Display(Name = "Added by")]
        public ApplicationUser AddedBy { get; set; }
        public IEnumerable<SelectListItem> ProjectOptions { get; set; }
        [Display(Name = "Projects")]
        public List<int> SelectedProjectIds { get; set; } = new List<int>(); // If this property isn't initialized, validation will throw an exception.

        // View Properties
        public string ViewTitle
        {
            get
            {
                if (Id != 0)
                {
                    return $"Edit Skill: {this.Title}";
                }
                else
                {
                    return "New Skill";
                }
            }
        }

        // Constructors
        public SkillFormViewModel()
        {
            Id = 0;
            DateAdded = DateTime.Now;
        }

        public SkillFormViewModel(Skill skill) // This constructor is for editing an existing record.
        {
            Id = skill.Id;
            Icon = skill.Icon;
            Title = skill.Title;
            Description = skill.Description;
            Developer = skill.Developer;

            // DateTime Properties
            ReleaseDate = skill.ReleaseDate;
            DateAdded = skill.DateAdded;

            // Url Properties
            RepositoryUrl = skill.RepositoryUrl;
            DocumentationUrl = skill.DocumentationUrl;

            // Entity Relationships
            AddedById = skill.AddedById;
            SelectedProjectIds = skill.ProjectSkills
                .Select(ps => ps.Project.Id)
                .ToList();
        }
    }
}