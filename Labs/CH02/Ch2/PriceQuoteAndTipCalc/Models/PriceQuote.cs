using System.ComponentModel.DataAnnotations;

namespace PriceQuoteAndTipCalc.Models
{
    public class PriceQuote
    {
        [Required(ErrorMessage = "Subtotal Is Required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Subtotal Must Be Greater Than 0.")]
        public decimal? Subtotal { get; set; }

        [Required(ErrorMessage = "Discount Percent Is Required.")]
        [Range(0, 100, ErrorMessage = "Discount Must Be Between 0 And 100.")]
        public decimal? DiscountPercent { get; set; }

        public decimal DiscountAmount => Subtotal.GetValueOrDefault() * DiscountPercent.GetValueOrDefault() / 100;
        public decimal Total => Subtotal.GetValueOrDefault() - DiscountAmount;
    }
}