using Microsoft.AspNetCore.Mvc;

namespace CH04Lab.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
