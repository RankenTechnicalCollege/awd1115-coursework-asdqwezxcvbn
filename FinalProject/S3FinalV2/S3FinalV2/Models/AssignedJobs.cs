using System.ComponentModel.DataAnnotations;

namespace S3FinalV2.Models
{
    public class AssignedJobs
    {
        [Key]
        public int AssignedJobId { get; set; }

        public int JobsId { get; set; }
        public Jobs? Jobs { get; set; }

        public string Priority { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public Customers? Customer { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public float EstimatedHours { get; set; }
        public float? ActualHours { get; set; }
        public bool IsCompleted { get; set; }

        public ICollection<MechanicAssignment> MechanicAssignments { get; set; } = new List<MechanicAssignment>();
        public ICollection<WorkWeekAssignment> WorkWeekAssignments { get; set; } = new List<WorkWeekAssignment>();
    }
}