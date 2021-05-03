using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}