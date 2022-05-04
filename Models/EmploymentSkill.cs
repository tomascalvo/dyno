using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class EmploymentSkill
    {
        [Key, Column(Order = 0)]

        public int EmploymentId { get; set; }
        public Employment Employment { get; set; }
        [Key, Column(Order = 1)]

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}