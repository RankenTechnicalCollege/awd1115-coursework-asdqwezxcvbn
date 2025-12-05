namespace HOT3.Models
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new();
        public int TotalQuantity => Items.Sum(i => i.Quantity);
        public decimal TotalPrice => Items.Sum(i => i.LineTotal);
    }
}