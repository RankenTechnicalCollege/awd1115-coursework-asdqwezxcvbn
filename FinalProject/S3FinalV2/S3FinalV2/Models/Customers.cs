using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser UserId { get; set; }  

        public required string Name { get; set; }

        public required string CustomerEmail { get; set; }

        public required string CustomerPhone { get; set; }

        public required string CustomerPassword { get; set; }

        public required string VehicleInfo { get; set; }
    }
}
