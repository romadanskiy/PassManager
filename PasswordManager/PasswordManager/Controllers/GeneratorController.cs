using Microsoft.AspNetCore.Mvc;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
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
                    model = Generate(model);
                    return RedirectToAction("Index", model);
                case "save":
                    return Content($"Сохранить пароль: {model.GeneratedPassword}");
                default:
                    return Content("Неопознанное действие");
            }
        }

        private static GeneratePasswordViewModel Generate(GeneratePasswordViewModel model)
        {
            return model.IsConfigured ? GeneratePassword(model) : GenerateRandomPassword(model);
        }

        private static GeneratePasswordViewModel GenerateRandomPassword(GeneratePasswordViewModel model)
        {
            var randomPassword = "RaNdOm4568!5";
            model.SetDefault();
            model.GeneratedPassword = randomPassword;
            return model;
        }
        
        private static GeneratePasswordViewModel GeneratePassword(GeneratePasswordViewModel model)
        {
            var configuredPassword = "ConFIguReD46@)4";
            model.GeneratedPassword = configuredPassword;
            return model;
        }
    }
}