using System.Collections.Generic;

namespace PasswordManager.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string Description { get; set; }
        
        public List<Subscription> Subscriptions { get; set; }
    }
}