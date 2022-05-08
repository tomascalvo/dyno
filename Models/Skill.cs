using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DevPath.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        [Required]

        [StringLength(50, ErrorMessage = "Skill title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Developer(s)")]
        [StringLength(250, ErrorMessage = "Developer property cannot be longer than 250 characters.")]
        public string Developer { get; set; }

        // URL PROPERTIES
        [DataType(DataType.Url)]
        [Display(Name = "Repository")]

        public string RepositoryUrl { get; set; }
        [DataType(DataType.Url)]
        [Display(Name = "Documentation")]
        public string DocumentationUrl { get; set; }

        // DATETIME PROPERTIES
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; } = DateTime.Now;

        // NAVIGATION PROPERTIES
        public List<ProjectSkill> ProjectSkills { get; set; }
        public List<EmploymentListingSkill> EmploymentListingSkills { get; set; }
        public List<EmploymentSkill> EmploymentSkills { get; set; }
        public List<SkillHierarchy> Prerequisites { get; set; }
        public List<SkillHierarchyPrerequisite> Principals { get; set; }
        public List<Course> Courses { get; set; }
        public List<Goal> Goals { get; set; }
        [Display(Name = "Documentation Read")]

        public List<DocumentationRead> DocumentationRead { get; set; }
        public List<WorkflowStep> Workflows { get; set; }

        //public virtual ICollection<Flashcard> Flashcards { get; set; }
    }
}