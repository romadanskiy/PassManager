using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}