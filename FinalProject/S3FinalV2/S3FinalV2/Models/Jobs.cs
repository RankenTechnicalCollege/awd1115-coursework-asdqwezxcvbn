using System.ComponentModel.DataAnnotations;

namespace S3FinalV2.Models
{
    public class Jobs
    {
        [Key]
        public int JobId { get; set; }

        public string Name { get; set; } = string.Empty;
        public int SkillLevel { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string EstCompTime { get; set; } = string.Empty;
        public float AvgCompletionTime { get; set; }

        public ICollection<AssignedJobs> AssignedJobs { get; set; } = new List<AssignedJobs>();
    }
}