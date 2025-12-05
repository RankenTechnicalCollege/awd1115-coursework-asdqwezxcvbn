using System.ComponentModel.DataAnnotations;

namespace HOT3.ViewModels
{
    public class RegisterViewModel
    {
        [Required] public string Username { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
        [DataType(DataType.Password), Compare("Password")] public string ConfirmPassword { get; set; } = "";
        public bool RememberMe { get; set; }
        [Required] public string FirstName { get; set; } = "";
        [Required] public string LastName { get; set; } = "";
    }
}
