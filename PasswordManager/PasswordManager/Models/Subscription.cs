using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Column(TypeName="money")]
        public decimal Price { get; set; }
        
        public List<Feature> Features { get; set; }
    }
}