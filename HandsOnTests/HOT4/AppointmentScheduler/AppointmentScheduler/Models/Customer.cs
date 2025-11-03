using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Phone number format is invalid.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Phone must be 10-15 digits, optional leading +.")]
        public string Phone { get; set; } = string.Empty;
    }
}