using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class CreateFeatureViewModel
    {
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Название контроллера")]
        public string ControllerName { get; set; }
        
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}