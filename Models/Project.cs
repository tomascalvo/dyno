﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Project title must be 1 to 50 characters in length.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
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

        //public int? OwnerID { get; set; }
        //public virtual ApplicationUser Owner { get; set; }
        //public virtual ICollection<ApplicationUser> Developers { get; set; }
        //public virtual ICollection<Skill> Skills { get; set; }
        //public virtual ICollection<Project> Projects { get; set; }

        //public virtual ICollection<Tutorial> Tutorials { get; set; }
        //public virtual ICollection<Workflow> Workflows { get; set; }
        //public int? CompanyID { get; set; }
        //public virtual Company Company { get; set; }
        //public virtual ICollection<Quote> Quotes { get; set; }

    }
}