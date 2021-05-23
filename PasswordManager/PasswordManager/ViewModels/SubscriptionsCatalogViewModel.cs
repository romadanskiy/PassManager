using System.Collections.Generic;
using PasswordManager.Models;

namespace PasswordManager.ViewModels
{
    public class SubscriptionsCatalogViewModel
    {
        public List<Subscription> AllSubscriptions { get; set; }
        public List<Feature> AllFeatures { get; set; }
    }
}