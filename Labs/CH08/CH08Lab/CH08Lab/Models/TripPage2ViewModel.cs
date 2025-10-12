using System.ComponentModel.DataAnnotations;

namespace TripLog.ViewModels
{
    public class TripPage2ViewModel
    {
        public string? AccommodationPhone { get; set; }

        [EmailAddress]
        public string? AccommodationEmail { get; set; }
    }
}