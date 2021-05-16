using Microsoft.AspNetCore.Mvc;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    public class GeneratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Generate(string action, GeneratePasswordViewModel model)
        {
            switch (action)
            {
                case "generate":
                    ViewBag.GeneratedPassword = "PaRol456!23";
                    return View("Index");
                case "save":
                    return Content("Сохранить пароль");
                default:
                    return Content("Неопознанное действие");
            }
        }
    }
}