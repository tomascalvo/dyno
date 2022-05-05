using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class Award
    {
        // Properties
        public int Id { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }

        // DateTime Properties
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [Display(Name = "Awarded")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime? DateAwarded { get; set; }

        // Navigation Properties
        [Required]
        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }
        public int AwardTypeId { get; set; }
        public AwardType AwardType { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}