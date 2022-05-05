using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class CourseCompletion
    {
        // Compound Key
        [Key, Column(Order = 0)]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Properties
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [Display(Name = "Date Completed")]

        public DateTime DateCompleted { get; set; }
        [Range(0, 5, ErrorMessage = "Rating value must be between 0 and 5.")]
        public int? Rating { get; set; }
        public string Comment { get; set; }

        // Navigation Properties
        public List<Project> Projects { get; set; }
    }
}