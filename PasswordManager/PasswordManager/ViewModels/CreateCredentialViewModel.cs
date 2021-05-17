using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class CreateCredentialViewModel
    {
        [Required]
        [Display(Name = "URL, название сайта или любая удобная пометка")]
        public string Source { get; set; }
        
        [Required]
        [Display(Name = "Почта или логин")]
        public string Login { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}