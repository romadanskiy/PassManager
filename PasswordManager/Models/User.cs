using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models
{
    public class User : IdentityUser
    {
        public List<Credential> Credentials { get; set; }
        public bool HasSubscription { get; set; }
        public int SubscriptionId { get; set; }
    }
}
