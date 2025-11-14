using System.ComponentModel.DataAnnotations;

namespace CH08Lab.Models
{
    public class TripPage2ViewModel
    {
        public string? AccommodationPhone { get; set; }

        [EmailAddress]
        public string? AccommodationEmail { get; set; }
    }
}