using System.ComponentModel.DataAnnotations;

namespace TripLog.ViewModels
{
    public class TripPage1ViewModel
    {
        [Required]
        public string Destination { get; set; } = null!;

        [Required]
        public string Accommodation { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
