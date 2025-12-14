using System.ComponentModel.DataAnnotations;

namespace S3FinalV2.Models
{
    public class WorkWeek
    {
        [Key]
        public int Id { get; set; }

        public DateTime WeekStart { get; set; }

        public DateTime WeekEnd { get; set; }

        public ICollection<WorkWeekAssignment> WorkWeekAssignments { get; set; }
            = new List<WorkWeekAssignment>();
    }
}