namespace DevPath.Models
{
    public class Workflow
    {
        // Properties
        public int Id { get; set; }

        // Navigation Properties
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TopStepId { get; set; }
        public WorkflowStep TopStep { get; set; }
    }
}