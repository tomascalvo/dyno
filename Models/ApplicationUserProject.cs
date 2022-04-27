using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPath.Models
{
    public class ApplicationUserProject
    {
        // CONSTRUCTORS
        public ApplicationUserProject()
        {
            DateAdded = DateTime.Now;
        }

        public ApplicationUserProject(string applicationUserId, int projectId)
        {
            ApplicationUserId = applicationUserId;
            ProjectId = projectId;
            DateAdded = DateTime.Now;
        }

        // PROPERTIES
        [Key, Column(Order = 0)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Key, Column(Order = 1)]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public bool CanEdit { get; set; } = true;
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