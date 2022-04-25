using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class ProjectSkill
    {
        // DEFAULT CONSTRUCTOR
        public ProjectSkill()
        {

        }

        // EASY CONSTRUCTOR FOR SEED METHOD
        public ProjectSkill(int projectId, int skillId)
        {
            ProjectId = projectId;
            SkillId = skillId;
        }
        [Key, Column(Order = 0)]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
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