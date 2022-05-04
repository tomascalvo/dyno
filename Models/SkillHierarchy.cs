using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages;

namespace DevPath.Models
{
    public class SkillHierarchy
    {
        // Properties
        [Key]
        public int Id { get; set; }
        private string title = null;
        public string Title
        {
            get
            {
                return !this.title.IsEmpty() ? this.title : this.Principal.Title;
            }
            set { title = value; }
        }
        private string description = null;
        public string Description
        {
            get
            {
                string descriptionSingle = $"{this.Prerequisites[0].Prerequisite.Title} is a prerequisite of {this.Principal.Title}.";
                string descriptionMultiple = $"Proficiency in {this.Principal.Title} requires proficiency in at least one of the following skills: ";
                for (int i = 0; i < this.Prerequisites.Count; i++)
                {
                    descriptionMultiple += this.Prerequisites[i].Prerequisite.Title;
                    if (i < this.Prerequisites.Count - 1)
                    {
                        descriptionMultiple += ", ";
                    }
                    else
                    {
                        descriptionMultiple += ".";
                    }
                }
                string defaultDescription = this.Prerequisites.Count > 1 ? descriptionMultiple : descriptionSingle;
                return !this.Description.IsEmpty() ? this.description : defaultDescription;
            }
            set { description = value; }
        }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; } = false;

        // Navigation Properties
        public int PrincipalId { get; set; }
        public Skill Principal { get; set; }
        public List<SkillHierarchyPrerequisite> Prerequisites { get; set; }
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}