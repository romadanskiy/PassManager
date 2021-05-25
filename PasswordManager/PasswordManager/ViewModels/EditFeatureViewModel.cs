using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class EditFeatureViewModel
    {
        public int Id { get; set; }
        
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