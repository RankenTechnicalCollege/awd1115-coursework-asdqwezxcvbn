using System.ComponentModel.DataAnnotations;

namespace CH08Lab.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        [Required]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; } = null!;

        [Required]
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public string? AccommodationPhone { get; set; }
        public string? AccommodationEmail { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
