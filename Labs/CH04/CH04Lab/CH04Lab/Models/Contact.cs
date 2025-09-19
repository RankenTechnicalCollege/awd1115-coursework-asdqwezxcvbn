using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CH04Lab.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category? Category { get; set; }
        public DateTime DateAdded { get; set; }
        [NotMapped]
        public string Slug => GenerateSlug($"{FirstName} {LastName}");

        private static string GenerateSlug(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            var normalized = value.ToLowerInvariant().Trim();
            var sb = new System.Text.StringBuilder();
            foreach (var ch in normalized)
            {
                if (char.IsLetterOrDigit(ch) || ch == '-')
                {
                    sb.Append(ch);
                }
                else if (char.IsWhiteSpace(ch))
                {
                    if (sb.Length > 0 && sb[^1] != '-') sb.Append('-');
                }
            }
            var slug = sb.ToString().Trim('-');
            return slug;
        }
    }
}