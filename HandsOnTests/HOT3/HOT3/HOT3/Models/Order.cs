using System.ComponentModel.DataAnnotations;

namespace HOT3.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string ProductSlug { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}