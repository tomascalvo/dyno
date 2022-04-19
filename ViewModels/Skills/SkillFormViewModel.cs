using DevPath.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.ViewModels.Skills
{
    public class SkillFormViewModel
    {
        public int? Id { get; set; } // Id is nullable in the ViewModel because this form can be used to edit existing Skill records (which have an Id) as well as to create new Skill records (in which case there is no Id to send to the view).
        public string Icon { get; set; }
        [Required(ErrorMessage = "Enter skill title.")]

        public string Title { get; set; }
        [StringLength(1000, ErrorMessage = "Skill description cannot exceed 1000 characters in length.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [StringLength(250, ErrorMessage = "Developer property cannot be longer than 250 characters.")]
        public string Developer { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Added")]

        public DateTime? DateAdded { get; set; } = DateTime.Now;
        [DataType(DataType.Url)]
        public string RepositoryUrl { get; set; }
        [DataType(DataType.Url)]
        public string DocumentationUrl { get; set; }

        public string ViewTitle
        {
            get
            {
                if (Id != 0)
                {
                    return "Edit Skill";
                }
                else
                {
                    return "New Skill";
                }
            }
        }

        public SkillFormViewModel()
        {
            Id = 0;
        }

        public SkillFormViewModel(Skill skill)
        {
            Id = skill.Id;
            Icon = skill.Icon;
            Title = skill.Title;
            Description = skill.Description;
            ReleaseDate = skill.ReleaseDate;
            DateAdded = skill.DateAdded;
            RepositoryUrl = skill.RepositoryUrl;
            DocumentationUrl = skill.DocumentationUrl;
        }
    }
}