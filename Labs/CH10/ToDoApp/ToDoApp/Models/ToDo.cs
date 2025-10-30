using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter A Due Date")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please Select A Category")]
        public string CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Please Select A Status")]
        public string StatusId { get; set; } = string.Empty;

        [ValidateNever]

        public Status Status { get; set; } = null;

        public bool Overdue => StatusId == "open" && DueDate < DateTime.Today;

    }
}
