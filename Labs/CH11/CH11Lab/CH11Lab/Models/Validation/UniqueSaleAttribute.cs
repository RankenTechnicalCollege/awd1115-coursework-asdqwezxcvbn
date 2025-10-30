using System.ComponentModel.DataAnnotations;
using System.Linq;
using CH11Lab.Data;
using CH11Lab.Models;

namespace CH11Lab.Models.Validation
{
    public class UniqueSaleAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var db = (ApplicationDbContext?)context.GetService(typeof(ApplicationDbContext));
            if (db == null) return new ValidationResult("Database context not available.");

            var sale = context.ObjectInstance as Sale;
            if (sale == null) return ValidationResult.Success;

            bool exists = db.Sales.Any(s =>
                s.EmployeeId == sale.EmployeeId &&
                s.Year == sale.Year &&
                s.Quarter == sale.Quarter &&
                s.SaleId != sale.SaleId);

            if (exists)
            {
                var employee = db.Employees.Find(sale.EmployeeId);
                string name = employee != null ? $"{employee.FirstName} {employee.LastName}" : "that employee";
                return new ValidationResult($"Sales for {name} for {sale.Year} Q{sale.Quarter} are already in the database.");
            }

            return ValidationResult.Success;
        }
    }
}