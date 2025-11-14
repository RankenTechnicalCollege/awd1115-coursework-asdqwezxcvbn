using System.ComponentModel.DataAnnotations;

namespace CH08Lab.Models
{
    public class Destination
    {
        [Key]
        public int DestinationId { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}