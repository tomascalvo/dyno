using System;
using System.ComponentModel.DataAnnotations;

namespace DevPath.Models
{
    public class DocumentationRead
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]

        public DateTime? DateRead { get; set; }

        // Navigation Properties
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}