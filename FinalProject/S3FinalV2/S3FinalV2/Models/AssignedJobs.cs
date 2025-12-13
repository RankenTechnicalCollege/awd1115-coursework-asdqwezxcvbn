using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class AssignedJobs
    {
        [Key]
        public int AssignedJobId { get; set; }

        public int JobsId { get; set; }
        [ForeignKey("JobsId")]
        public Jobs Jobs { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customers Customer { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public float? ActualCompTime { get; set; }
        public string Priority { get; set; }
        public bool? IsCompleted { get; set; } = false;
        public List<MechanicAssignment> AssignedMechanics { get; set; } = new();
    }
}