using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    [Authorize(Roles="admin")]
    public class FeaturesController : Controller
    {
        public IActionResult EditForSubscription(int subscriptionId)
        {
            var context = new ApplicationContext();
            var subscription = context.Subscriptions
                .Include(s => s.Features)
                .FirstOrDefault(s => s.Id == subscriptionId);

            if (subscription != null)
            {
                var allFeatures = context.Features.ToList();
                
                var model = new EditFeaturesForSubscriptionViewModel
                {
                    SubscriptionId = subscriptionId,
                    SubscriptionName = subscription.Name,
                    AllFeatures = allFeatures,
                    SubscriptionFeatures = subscription.Features.Select(f => f.Id).ToList()
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditForSubscription(int subscriptionId, List<int> features)
        {
            var context = new ApplicationContext();
            // получаем подписку
            var subscription = context
                .Subscriptions
                .Include(s=>s.Features)
                .FirstOrDefault(s => s.Id==subscriptionId);
            
            if (subscription != null)
            {
                // список фичей подписки
                var subscriptionFeatures = subscription.Features.Select(f => f.Id).ToList();
                // список фичей, которые были добавлены
                var addedFeatures = features.Except(subscriptionFeatures).ToList();
                // список фичей, которые были удалены
                var removedFeatures = subscriptionFeatures.Except(features).ToList();

                foreach (var addedFeatureId in addedFeatures)
                {
                    var feature = context.Features.First(f => f.Id == addedFeatureId);
                    subscription.Features.Add(feature);
                }
                
                foreach (var removedFeatureId in removedFeatures)
                {
                    var feature = context.Features.First(f => f.Id == removedFeatureId);
                    subscription.Features.Remove(feature);
                }

                context.SaveChanges();

                return RedirectToAction("Index", "Subscriptions");
            }
 
            return NotFound();
        }
    }
}