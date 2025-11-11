using System.ComponentModel.DataAnnotations;

namespace RankenClassSchedule.Models.DomainModels
{
    public class Teacher
    {
        public Teacher() => Classes = new HashSet<Class>();

        public int TeacherId { get; set; }

        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "Fist Name May Not Exceed 100 Characters")]
        [Required(ErrorMessage = "First Name Is Required")]

        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Last Name May Not Exceed 100 Characters")]
        [Required(ErrorMessage = "Last Name Is Required")]

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";    

        public ICollection<Class> Classes { get; set; }
    }
}
