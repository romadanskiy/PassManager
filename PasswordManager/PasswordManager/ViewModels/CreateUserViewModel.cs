using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class  CreateUserViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}