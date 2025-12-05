using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class WorkWeekAssignment
    {
        [Key]
        public int WorkWeekAssignmentId { get; set; }

        public int WorkWeekId { get; set; }
        public WorkWeek? WorkWeek { get; set; }

        public int AssignedJobsId { get; set; }
        public AssignedJobs? AssignedJobs { get; set; }
    }
}
