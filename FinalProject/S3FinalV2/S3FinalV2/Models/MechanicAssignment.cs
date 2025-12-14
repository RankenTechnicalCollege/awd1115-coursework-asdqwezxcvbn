using System.ComponentModel.DataAnnotations;

namespace S3FinalV2.Models
{
    public class MechanicAssignment
    {
        [Key]
        public int Id { get; set; }

        public int MechanicId { get; set; }
        public Mechanics Mechanic { get; set; }

        public int AssignedJobId { get; set; }
        public AssignedJobs AssignedJob { get; set; }

        public float AssignedHours { get; set; }

        public float ActualHours { get; set; }
    }
}