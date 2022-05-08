using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class WorkflowStep
    {
        // Properties
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Workflow title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime DateApproved { get; set; } = DateTime.Now;


        // Navigation Properties
        public string AddedById { get; set; }
        public ApplicationUser AddedBy { get; set; }
        public List<WorkflowStep> SubSteps { get; set; }
        public List<WorkflowStep> SuperSteps { get; set; }
        public List<Skill> Skills { get; set; }
    }
}