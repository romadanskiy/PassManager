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
        
        public IActionResult Subscriptions()
        {
            var context = new ApplicationContext();
            
            var subscriptions = context.Subscriptions
                .Include(s => s.Features)
                .OrderBy(s => s.Price)
                .ToList();

            var features = context.Features.ToList();

            var model = new SubscriptionsCatalogViewModel {AllSubscriptions = subscriptions, AllFeatures = features};
            
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
                    return RedirectToAction("Index", "Credentials");
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
    }
}