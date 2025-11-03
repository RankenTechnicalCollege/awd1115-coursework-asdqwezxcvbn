using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppointmentScheduler.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Models
{
    public class Appointment : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Start date and time is required.")]
        [Display(Name = "Start (Date & Time)")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "Please select a customer.")]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [NotMapped]
        public DateTime EndDateTime => StartDateTime.AddHours(1);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (StartDateTime.Minute != 0 || StartDateTime.Second != 0)
            {
                results.Add(new ValidationResult(
                    "Appointments must start on the exact hour (minutes and seconds must be 00). Example: 2025-03-15 08:00.",
                    new[] { nameof(StartDateTime) }));
            }

            if (StartDateTime <= DateTime.Now)
            {
                results.Add(new ValidationResult(
                    "Appointment must be scheduled for a future date and time. Please choose a future hour.",
                    new[] { nameof(StartDateTime) }));
            }

            var db = (AppDbContext?)validationContext.GetService(typeof(AppDbContext));
            if (db != null)
            {
                bool exists = db.Appointments.AsNoTracking()
                    .Any(a => a.StartDateTime == StartDateTime && a.Id != Id);

                if (exists)
                {
                    results.Add(new ValidationResult(
                        "Selected date/time is already booked. Please select a different date or hour.",
                        new[] { nameof(StartDateTime) }));
                }
            }
            else
            {
                
            }

            return results;
        }
    }
}