using Microsoft.AspNetCore.Identity;

namespace S3FinalV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserId { get; set; } = string.Empty;  

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required string Role { get; set; }
    }
}
