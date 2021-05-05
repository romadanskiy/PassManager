using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}