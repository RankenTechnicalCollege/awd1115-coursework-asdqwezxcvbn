using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CH06Lab.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public ICollection<Faq> Faqs { get; set; } = new List<Faq>();
    }
}
