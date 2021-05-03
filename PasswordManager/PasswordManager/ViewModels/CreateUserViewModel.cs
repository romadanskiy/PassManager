using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class CreateUserViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}