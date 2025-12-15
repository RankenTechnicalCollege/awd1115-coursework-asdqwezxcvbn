using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Phone { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string VehicleInfo { get; set; }

        public ICollection<AssignedJobs> AssignedJobs { get; set; } = new List<AssignedJobs>();
    }
}