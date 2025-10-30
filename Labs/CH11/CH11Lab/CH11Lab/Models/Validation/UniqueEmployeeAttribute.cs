using System.ComponentModel.DataAnnotations;
using System.Linq;
using CH11Lab.Data;
using CH11Lab.Models;

namespace CH11Lab.Models.Validation
{
    public class UniqueEmployeeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var db = (ApplicationDbContext?)context.GetService(typeof(ApplicationDbContext));
            if (db == null) return new ValidationResult("Database context not available.");

            var employee = context.ObjectInstance as Employee;
            if (employee == null) return ValidationResult.Success;

            bool exists = db.Employees.Any(e =>
                e.FirstName == employee.FirstName &&
                e.LastName == employee.LastName &&
                e.DOB == employee.DOB &&
                e.EmployeeId != employee.EmployeeId);

            if (exists)
            {
                return new ValidationResult($"{employee.FirstName} {employee.LastName} (DOB: {employee.DOB:d}) is already in the database.");
            }

            return ValidationResult.Success;
        }
    }
}