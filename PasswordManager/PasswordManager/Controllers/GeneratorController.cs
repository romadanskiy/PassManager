using Features;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    [Subscription("Generator")]
    public class GeneratorController : Controller
    {
        public IActionResult Index(GeneratePasswordViewModel model)
        {
            if (!model.IsConfigured)
                model.SetDefault();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Generate(string action, GeneratePasswordViewModel model)
        {
            switch (action)
            {
                case "generate":
                    model = GeneratePassword(model);
                    return RedirectToAction("Index", model);
                case "save":
                    return RedirectToAction("SaveFromGenerator", "Credentials",
                        new {generatedPassword = model.GeneratedPassword});
                default:
                    return Content("Неопознанное действие");
            }
        }
        
        private static GeneratePasswordViewModel GeneratePassword(GeneratePasswordViewModel model)
        {
            if (!model.IsConfigured)
                model.SetDefault();

            var generator = new Generator();
            model.GeneratedPassword = generator.GeneratePassword(
                model.PasswordLength,
                model.HasLowercase,
                model.HasUppercase,
                model.HasDigit,
                model.HasNonAlphanumeric);
            
            return model;
        }
    }
}