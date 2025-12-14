using Microsoft.AspNetCore.Identity;

namespace S3FinalV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Role { get; set; }

        public Customers? Customer { get; set; }
        public Mechanics? Mechanic { get; set; }
    }
}