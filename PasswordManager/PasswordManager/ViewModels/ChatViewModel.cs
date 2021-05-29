using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class ChatViewModel
    {
        [Display(Name = "От")]
        public string Email { get; set; }
        
        [Display(Name = "Кому")]
        public string Receiver { get; set; }
        
        public bool IsTechSupport { get; set; }
    }
}