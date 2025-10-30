using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CH11Lab.Models.Validation;

namespace CH11Lab.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Birth date must be in the past.")]
        [Display(Name = "Birth Date")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Hire date must be in the past.")]
        [HireDateRange(ErrorMessage = "Hire date must not be before 1/1/1995.")]
        [Display(Name = "Hire Date")]
        public DateTime DateOfHire { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee? Manager { get; set; }

        [UniqueEmployee]
        [NotSamePerson]
        public string? ValidationProxy => $"{FirstName}|{LastName}|{DOB:yyyy-MM-dd}|{ManagerId}";
    }
}