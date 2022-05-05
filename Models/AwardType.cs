using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class AwardType
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }


        // DateTime Properties
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime DateApproved { get; set; }

        [Display(Name = "Awarded")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime? DateFounded { get; set; }

        // Navigation Properties
        public string AddedById { get; set; }
        public ApplicationUser AddedBy { get; set; }
        public int? SponsorId { get; set; }
        public Platform Sponsor { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }
        public List<Award> Awards { get; set; }
        public List<Skill> Skills { get; set; }
    }
}