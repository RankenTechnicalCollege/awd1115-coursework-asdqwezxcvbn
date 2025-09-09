using System.ComponentModel.DataAnnotations;

namespace HOT1_AWD1115.Models
{
    public class OrderForm
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 500, ErrorMessage = "Quantity must be between 1 and 500.")]
        public int? Quantity { get; set; }

        public string? DiscountCode { get; set; }
        private const decimal ShirtPrice = 15.00m;
        private const decimal TaxRate = 0.08m;
        public decimal Subtotal => (Quantity?? 0 )* ShirtPrice;

        public decimal DiscountAmount
        {
            get
            {
                if (string.IsNullOrEmpty(DiscountCode)) return 0;

                return DiscountCode switch
                {
                    "6175" => Subtotal * 0.30m,
                    "1390" => Subtotal * 0.20m,
                    "BB88" => Subtotal * 0.10m,
                    _ => 0
                };
            }
        }

        public bool IsDiscountValid =>
            DiscountCode == "6175" || DiscountCode == "1390" || DiscountCode == "BB88";

        public decimal Tax => (Subtotal - DiscountAmount) * TaxRate;

        public decimal Total => (Subtotal - DiscountAmount) + Tax;
    }
}
