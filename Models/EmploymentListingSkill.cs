using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class EmploymentListingSkill
    {
        [Key, Column(Order = 0)]
        public int EmploymentListingId { get; set; }
        public EmploymentListing EmploymentListing { get; set; }
        [Key, Column(Order = 1)]

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
        private DateTime? dateAdded = null;
        public DateTime DateAdded
        {
            get
            {
                return this.dateAdded.HasValue ? this.dateAdded.Value : DateTime.Now;
            }
            set { this.dateAdded = value; }
        }
    }
}
