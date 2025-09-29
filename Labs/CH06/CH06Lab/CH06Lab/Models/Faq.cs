using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CH06Lab.Models
{
    public class Faq
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FaqId { get; set; }

        [Required]
        public string Question { get; set; } = null!;

        [Required]
        public string Answer { get; set; } = null!;

        // FK fields that relate to Topic and Category
        [Required]
        public string TopicId { get; set; } = null!;
        public Topic? Topic { get; set; }

        [Required]
        public string CategoryId { get; set; } = null!;
        public Category? Category { get; set; }
    }
}
