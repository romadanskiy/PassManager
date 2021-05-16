using Microsoft.AspNetCore.Mvc;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    public class GeneratorController : Controller
    {
        public IActionResult Index()
        {
            var model = new GeneratePasswordViewModel()
            {
                IsConfigured = false,
                PasswordLength = 12,
                HasLowercase = true,
                HasUppercase = true,
                HasDigit = true,
                HasNonAlphanumeric = true
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Generate(string action, GeneratePasswordViewModel model)
        {
            switch (action)
            {
                case "generate":
                    model.GeneratedPassword = "PaRol456!23";
                    return View("Index", model);
                case "save":
                    return Content($"Сохранить пароль: {model.GeneratedPassword}");
                default:
                    return Content("Неопознанное действие");
            }
        }
    }
}