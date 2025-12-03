using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class AssignedJobs
    {
        [Key]
        public int AssignedJobId { get; set; }

        [ForeignKey("JobsId")]
        public Jobs JobsId { get; set; }

        [ForeignKey("CustomerId")]
        public Customers CustomerId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public float? ActualCompTime { get; set; }

        public string Priority { get; set; }

        public bool? IsCompleted { get; set; } = false;
    }
}