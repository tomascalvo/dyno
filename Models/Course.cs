using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation Properties
        public List<CourseCompletion> Completions { get; set; }
        public string AddedById { get; set; }
        public ApplicationUser AddedBy { get; set; }
        public int? PlatformId { get; set; }
        public Platform Platform { get; set; }
        public List<Skill> Skills { get; set; }

    }
}