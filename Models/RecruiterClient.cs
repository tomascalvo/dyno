using System;

namespace DevPath.Models
{
    public class RecruiterClient
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        // NAVIGATION PROPERTIES
        public int RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}