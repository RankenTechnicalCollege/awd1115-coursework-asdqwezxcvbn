using System;
using System.ComponentModel.DataAnnotations;

namespace CH11Lab.Models.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is DateTime dt)
            {
                if (dt < DateTime.Today) return ValidationResult.Success;
                return new ValidationResult(ErrorMessage ?? "Date must be in the past.");
            }

            return new ValidationResult("Invalid date value.");
        }
    }
}