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
        public string Image { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsTutorial { get; set; } = false;

        // DateTime Properties
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation Properties
        public string AddedById { get; set; }
        public ApplicationUser AddedBy { get; set; }
        public int? PlatformId { get; set; }
        public Platform Platform { get; set; }
        public List<CourseCompletion> Completions { get; set; }
        public List<Skill> Skills { get; set; }

    }
}