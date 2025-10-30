using System;
using System.ComponentModel.DataAnnotations;

namespace CH11Lab.Models.Validation
{
    public class HireDateRangeAttribute : ValidationAttribute
    {
        private static readonly DateTime MinHireDate = new DateTime(1995, 1, 1);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is DateTime dt)
            {
                if (dt >= MinHireDate) return ValidationResult.Success;
                return new ValidationResult(ErrorMessage ?? $"Hire date must be on or after {MinHireDate:d}.");
            }

            return new ValidationResult("Invalid date value.");
        }
    }
}