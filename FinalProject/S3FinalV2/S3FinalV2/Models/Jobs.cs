using System.ComponentModel.DataAnnotations;

namespace S3FinalV2.Models
{
    public class Jobs
    {
        [Key]
        public int JobId { get; set; }

        public required int SkillLevel { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public string? Priority { get; set; }

        public float EstCompTime { get; set; }

        public float AvgCompletionTime { get; set; }
    }
}