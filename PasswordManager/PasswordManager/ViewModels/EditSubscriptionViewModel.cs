using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.ViewModels
{
    public class EditSubscriptionViewModel
    {
        public int Id { get; set; }
        
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