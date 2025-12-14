using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class Mechanics
    {
        [Key]
        public int MechanicId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public string Name { get; set; }

        public int SkillLevel { get; set; }

        public float HourlyLimitPerWeek { get; set; }

        public float TotalHoursWorked { get; set; }

        public ICollection<MechanicAssignment> MechanicAssignments { get; set; }
            = new List<MechanicAssignment>();
    }
}