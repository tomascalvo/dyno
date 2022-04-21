using System;

namespace DevPath.Models
{
    public class ProjectSkill
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
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