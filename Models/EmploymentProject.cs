using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class EmploymentProject
    {
        [Key, Column(Order = 0)]

        public int EmploymentId { get; set; }
        public Employment Employment { get; set; }
        [Key, Column(Order = 1)]

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}