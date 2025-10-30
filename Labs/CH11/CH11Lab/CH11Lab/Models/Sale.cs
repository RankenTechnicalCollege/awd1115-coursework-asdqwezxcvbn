using System.ComponentModel.DataAnnotations;
using CH11Lab.Models.Validation;

namespace CH11Lab.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Quarter must be between 1 and 4.")]
        public int Quarter { get; set; }

        [Required]
        [Range(2001, int.MaxValue, ErrorMessage = "Year must be after 2000.")]
        public int Year { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        [UniqueSale]
        public string? ValidationProxy => $"{EmployeeId}|{Year}|{Quarter}";
    }
}