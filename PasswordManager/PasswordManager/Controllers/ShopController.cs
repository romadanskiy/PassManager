using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    public class ShopController : Controller
    {
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
    }
}