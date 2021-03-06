using DevPath.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.ViewModels.Projects
{
    public class ProjectFormViewModel
    {
        // CONSTRUCTORS
        public ProjectFormViewModel()
        {
            Id = 0; // This constructor initializes Id to 0 so that when ProjectForm is used to create a new Project record the ProjectController recognizes it as new and not an existing Project record.
            DateAdded = DateTime.Now;
        }

        public ProjectFormViewModel(Project project) // This constructor is for editing an existing record.
        {
            Id = project.Id;
            Title = project.Title;
            Description = project.Description;
            Icon = project.Icon;

            // Url Properties
            RepositoryUrl = project.RepositoryUrl;
            DeploymentUrl = project.DeploymentUrl;

            // DateTime Properties
            DateAdded = project.DateAdded;

            // Entity Relationships
            SelectedSkillIds = project.ProjectSkills
                .Select(ps => ps.Skill.Id)
                .ToList();
        }

        // DOMAIN MODEL PROPERTIES
        public int? Id { get; set; }
        [StringLength(50, ErrorMessage = "Project title must be 1 to 50 characters in length.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        [Display(Name = "Repo Url")]
        [DataType(DataType.Url)]
        public string RepositoryUrl { get; set; }
        [Display(Name = "Deployment Url")]
        [DataType(DataType.Url)]
        public string DeploymentUrl { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Skills")]

        // SKILL OPTIONS
        public IEnumerable<SelectListItem> SkillOptions { get; set; }
        public List<int> SelectedSkillIds { get; set; } = new List<int>();  // If this property isn't initialized, validation will throw an exception.

        // VIEW TITLE

        public string ViewTitle
        {
            get
            {
                if (Id != 0)
                {
                    return "Edit Project";
                }
                else
                {
                    return "New Project";
                }
            }
        }
    }
}