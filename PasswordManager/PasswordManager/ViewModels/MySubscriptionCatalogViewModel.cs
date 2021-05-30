using System.Collections.Generic;
using Models;

namespace PasswordManager.ViewModels
{
    public class MySubscriptionCatalogViewModel
    {
        public Subscription UsersSubscription { get; set; }
        public List<Subscription> AllSubscriptions { get; set; }
        public List<Feature> AllFeatures { get; set; }
    }
}