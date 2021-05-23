using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class CreateSubscriptionViewModel
    {
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}