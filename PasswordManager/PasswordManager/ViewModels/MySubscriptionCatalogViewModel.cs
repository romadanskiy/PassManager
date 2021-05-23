using System.Collections.Generic;
using PasswordManager.Models;

namespace PasswordManager.ViewModels
{
    public class MySubscriptionCatalogViewModel
    {
        public Subscription UsersSubscription { get; set; }
        public List<Subscription> AllSubscriptions { get; set; }
        public List<Feature> AllFeatures { get; set; }
    }
}