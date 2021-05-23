using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    public class ShopController : Controller
    {
        private UserManager<User> _userManager;
        
        public ShopController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Subscriptions()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.HasSubscription)
                return RedirectToAction("MySubscription");
            
            var context = new ApplicationContext();
            
            var subscriptions = context.Subscriptions
                .Include(s => s.Features)
                .OrderBy(s => s.Price)
                .ToList();

            var features = context.Features.ToList();

            var model = new SubscriptionsCatalogViewModel
            {
                AllSubscriptions = subscriptions, 
                AllFeatures = features,
            };
            
            return View(model);
        }

        public async Task<IActionResult> MySubscription()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!user.HasSubscription)
                return RedirectToAction("Subscriptions");
            
            var context = new ApplicationContext();

            var usersSubscription = context.Subscriptions.FirstOrDefault(s => s.Id == user.SubscriptionId);
            if (usersSubscription == null)
                return RedirectToAction("Subscriptions");
            
            var subscriptions = context.Subscriptions
                .Include(s => s.Features)
                .OrderBy(s => s.Price)
                .ToList();

            var features = context.Features.ToList();

            var model = new MySubscriptionCatalogViewModel
            {
                UsersSubscription = usersSubscription,
                AllSubscriptions = subscriptions, 
                AllFeatures = features
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BuySubscription(int subscriptionId)
        {
            var user = await _userManager.GetUserAsync(User);
            
            var context = new ApplicationContext();
            var subscription = context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);

            if (subscription != null)
            {
                user.HasSubscription = true;
                user.SubscriptionId = subscription.Id;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("MySubscription");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return RedirectToAction("Subscriptions");
        }

        [HttpPost]
        public async Task<IActionResult> CancelSubscription(int subscriptionId)
        {
            var user = await _userManager.GetUserAsync(User);
            user.HasSubscription = false;
            user.SubscriptionId = default;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Subscriptions");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("MySubscription");
        }
    }
}