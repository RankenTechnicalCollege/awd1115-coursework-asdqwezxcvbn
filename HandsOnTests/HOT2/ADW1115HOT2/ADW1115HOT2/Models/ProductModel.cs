using System.ComponentModel.DataAnnotations;

namespace ADW1115HOT2.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; } = string.Empty;

        public string ProductDescShort { get; set; } = string.Empty;
        public string ProductDescLong { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product Image is required.")]
        public string ProductImage { get; set; } = string.Empty;

        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000.")]
        public decimal ProductPrice { get; set; } = 0.00m;

        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
        public int ProductQty { get; set; } = 0;

        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public string Slug => ProductName?.Replace(" ", "-").ToLower() ?? string.Empty;
    }
}
