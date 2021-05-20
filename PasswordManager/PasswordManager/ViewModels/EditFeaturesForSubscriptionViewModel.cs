using System.Collections.Generic;
using PasswordManager.Models;

namespace PasswordManager.ViewModels
{
    public class EditFeaturesForSubscriptionViewModel
    {
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public List<Feature> AllFeatures { get; set; }
        public IList<int> SubscriptionFeatures { get; set; }
    }
}