using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class CertificationType
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Certification title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        public bool IsApproved { get; set; }

        // Metadata
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime DateApproved { get; set; }

        // Navigation Properties
        public string AddedById { get; set; }
        public ApplicationUser AddedBy { get; set; }
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
        public List<Skill> Skills { get; set; }
        [Display(Name = "Certifications Awarded")]
        public List<Certification> CertificationsAwarded { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }
    }
}