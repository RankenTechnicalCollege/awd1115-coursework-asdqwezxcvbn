using System.ComponentModel.DataAnnotations;

namespace S3FinalV2.Models
{
    public class WorkWeekAssignment
    {
        [Key]
        public int Id { get; set; }

        public int WorkWeekId { get; set; }
        public WorkWeek WorkWeek { get; set; }

        public int AssignedJobId { get; set; }
        public AssignedJobs AssignedJob { get; set; }
    }
}
