using System;

namespace DevPath.Models
{
    public class EmploymentListingAccess
    {
        // CONSTRUCTORS

        // PROPERTIES
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool CanEdit { get; set; } = true;
        public bool CanArchive { get; set; } = true;
        public bool CanDelete { get; set; } = true;

        // NAVIGATION PROPERTIES
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int EmploymentListingId { get; set; }
        public EmploymentListing EmploymentListing { get; set; }
    }
}