using System.ComponentModel.DataAnnotations;

namespace CH08Lab.Models
{
    public class TripPage1ViewModel
    {
        [Required(ErrorMessage = "Please select a destination")]
        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Please select an accommodation")]
        public int AccommodationId { get; set; }

        public IEnumerable<Destination> Destinations { get; set; } = new List<Destination>();
        public IEnumerable<Accommodation> Accommodations { get; set; } = new List<Accommodation>();

        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
