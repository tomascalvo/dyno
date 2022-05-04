using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class SkillHierarchyPrerequisite
    {
        [Key, Column(Order = 0)]
        public int SkillHierarchyId { get; set; }
        public SkillHierarchy SkillHierarchy { get; set; }
        [Key, Column(Order = 1)]
        public int PrerequisiteId { get; set; }
        public Skill Prerequisite { get; set; }

    }
}