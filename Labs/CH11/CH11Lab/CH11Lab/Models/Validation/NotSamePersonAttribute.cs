using System.ComponentModel.DataAnnotations;
using System.Linq;
using CH11Lab.Data;
using CH11Lab.Models;

namespace CH11Lab.Models.Validation
{
    public class NotSamePersonAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var db = (ApplicationDbContext?)context.GetService(typeof(ApplicationDbContext));
            if (db == null) return new ValidationResult("Database context not available.");

            var employee = context.ObjectInstance as Employee;
            if (employee == null) return ValidationResult.Success;

            if (employee.ManagerId == 0) return ValidationResult.Success;

            var manager = db.Employees.FirstOrDefault(m => m.EmployeeId == employee.ManagerId);
            if (manager == null) return ValidationResult.Success;

            if (manager.FirstName == employee.FirstName &&
                manager.LastName == employee.LastName &&
                manager.DOB == employee.DOB)
            {
                return new ValidationResult("Manager and employee can't be the same person.");
            }

            return ValidationResult.Success;
        }
    }
}