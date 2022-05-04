using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class Goal
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        [DataType(DataType.DateTime), Required]

        public DateTime DateAdded { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime), Required]

        public DateTime Deadline { get; set; }
        [DataType(DataType.DateTime)]

        public DateTime? DateAchieved { get; set; }

        // Navigation Properties
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public List<Skill> Skills { get; set; }
    }
}