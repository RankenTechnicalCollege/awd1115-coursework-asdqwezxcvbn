using System.ComponentModel.DataAnnotations;

namespace ADW1115HOT2.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; } = string.Empty;

        public Product? Product { get; set; }
    }
}
