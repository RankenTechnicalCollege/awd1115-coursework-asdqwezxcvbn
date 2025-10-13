using System.ComponentModel.DataAnnotations;

namespace HOT3.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = "";

        [Required, StringLength(120)]
        public string Slug { get; set; } = "";

        [Required, StringLength(150)]
        public string ImageFileName { get; set; } = "placeholder.png";

        [Required, StringLength(60)]
        public string Category { get; set; } = "";

        [Required, StringLength(80)]
        public string Manufacturer { get; set; } = "";

        [Range(0, 10000)]
        public int Stock { get; set; }

        [Required, Range(0.01, 100000)]
        public decimal Price { get; set; }

        [StringLength(1200)]
        public string Description { get; set; } = "";
    }
}