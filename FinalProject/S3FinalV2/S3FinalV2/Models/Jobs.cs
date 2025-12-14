using System.ComponentModel.DataAnnotations;

namespace S3FinalV2.Models
{
    public class Jobs
    {
        [Key]
        public int JobId { get; set; }

        public string Name { get; set; }

        public int SkillLevel { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public string EstCompTime { get; set; } 

        public float AvgCompletionTime { get; set; }

        public ICollection<AssignedJobs> AssignedJobs { get; set; }
            = new List<AssignedJobs>();
    }
}