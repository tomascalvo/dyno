using DevPath.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.ViewModels.Projects
{
    public class ProjectFormViewModel
    {

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

        public DateTime? Added { get; set; }

        public string PageTitle
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

        public ProjectFormViewModel()
        {
            Id = 0;
            Added = DateTime.Now;
        }

        public ProjectFormViewModel(Project project)
        {
            Id = project.Id;
            Title = project.Title;
            Description = project.Description;
            Icon = project.Icon;
            RepositoryUrl = project.RepositoryUrl;
            DeploymentUrl = project.DeploymentUrl;
            Added = project.DateAdded;
        }
    }
}