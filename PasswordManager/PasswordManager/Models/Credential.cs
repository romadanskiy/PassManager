using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class Credential
    {
        public int Id { get; set; }
        
        [Required]
        public string Source { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
    }
}