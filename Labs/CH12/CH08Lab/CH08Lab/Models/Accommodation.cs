using System.ComponentModel.DataAnnotations;

namespace CH08Lab.Models
{
    public class Accommodation
    {
        [Key]
        public int AccommodationId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}