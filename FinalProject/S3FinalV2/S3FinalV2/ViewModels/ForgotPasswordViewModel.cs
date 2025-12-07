using System.ComponentModel.DataAnnotations;
namespace S3FinalV2.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress] public string Email { get; set; } = "";
    }
}
