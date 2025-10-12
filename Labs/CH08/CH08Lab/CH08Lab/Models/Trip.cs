using System.ComponentModel.DataAnnotations;

namespace TripLog.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        [Required]
        public string Destination { get; set; } = null!;

        [Required]
        public string Accommodation { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? AccommodationPhone { get; set; }

        public string? AccommodationEmail { get; set; }

        public string? Activity1 { get; set; }
        public string? Activity2 { get; set; }
        public string? Activity3 { get; set; }
    }
}
