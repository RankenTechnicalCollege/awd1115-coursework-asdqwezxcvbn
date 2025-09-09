using System.ComponentModel.DataAnnotations;

namespace HOT1_AWD1115.Models
{
    public class DistanceConverter
    {
        [Required(ErrorMessage = "Length In Inches Is Required")]
        [Range(1, 500, ErrorMessage = "Length Must Be Between 1 to 500 Inches")]
        public double Inches { get; set; }

        public string Centimeters
        {
            get { return $"{Inches} Inches Is {Inches * 2.54} Centimeters"; }
        }
    }
}
