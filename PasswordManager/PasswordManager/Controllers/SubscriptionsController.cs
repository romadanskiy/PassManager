using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    [Authorize(Roles="admin")]
    public class SubscriptionsController : Controller
    {
        public IActionResult Index()
        {
            var context = new ApplicationContext();
            var subscriptions = context
                .Subscriptions
                .Include(s => s.Features)
                .ToList();
            return View(subscriptions);
        }
        
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CreateSubscriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationContext();
                var subscription = new Subscription()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price
                };
                context.Subscriptions.Add(subscription);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int subscriptionId)
        {
            var context = new ApplicationContext();
            var subscription = context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
            if (subscription == null)
            {
                return NotFound();
            }

            var model = new EditSubscriptionViewModel
            {
                Id = subscriptionId, 
                Name = subscription.Name, 
                Description = subscription.Description,
                Price = subscription.Price
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditSubscriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationContext();
                var subscription = context.Subscriptions.FirstOrDefault(s => s.Id == model.Id);
                if (subscription != null)
                {
                    subscription.Name = model.Name;
                    subscription.Description = model.Description;
                    subscription.Price = model.Price;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int subscriptionId)
        {
            var context = new ApplicationContext();
            var subscription = context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
            if (subscription != null)
            {
                context.Subscriptions.Remove(subscription);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}