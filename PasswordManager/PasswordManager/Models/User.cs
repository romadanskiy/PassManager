using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class User : IdentityUser
    {
        public List<Credential> Credentials { get; set; }
        public bool HasSubscription { get; set; }
        public int SubscriptionId { get; set; }
    }
}
