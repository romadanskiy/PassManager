using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Controllers
{
    public class ExportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}