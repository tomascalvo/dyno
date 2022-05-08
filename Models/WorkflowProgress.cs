namespace DevPath.Models
{
    public class WorkflowProgress
    {
        // Properties
        public int Id { get; set; }
        public bool IsComplete { get; set; }

        // Navigation Properties
        public int WorkflowStepId { get; set; }
        public WorkflowStep WorkflowStep { get; set; }
    }
}