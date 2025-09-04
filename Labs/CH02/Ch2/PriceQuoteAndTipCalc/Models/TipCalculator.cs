using System.ComponentModel.DataAnnotations;

namespace PriceQuoteAndTipCalc.Models
{
    public class TipCalculator
    {
        [Required(ErrorMessage = "Meal cost is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Meal cost must be greater than 0.")]
        public decimal? MealCost { get; set; }
        public decimal Tip15 => MealCost.GetValueOrDefault() * 0.15m;
        public decimal Tip20 => MealCost.GetValueOrDefault() * 0.20m;
        public decimal Tip25 => MealCost.GetValueOrDefault() * 0.25m;

    }
}