using System;

namespace DevPath.Models
{
    public class EmploymentListingSkill
    {
        public int Id { get; set; }
        public int EmploymentListingId { get; set; }
        public EmploymentListing EmploymentListing { get; set; }
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
