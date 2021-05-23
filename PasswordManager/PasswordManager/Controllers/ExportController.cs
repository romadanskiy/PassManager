using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    [Subscription("Export")]
    public class ExportController : Controller
    {
        private UserManager<User> _userManager;
        
        public ExportController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var context = new ApplicationContext();
            var credentials = context.Credentials.Where(c => c.UserId == userId).ToList();
            return View(credentials);
        }

        public async Task<IActionResult> GetFile(string type)
        {
            var user = await _userManager.GetUserAsync(User);
            var context = new ApplicationContext();
            var credentials = context.Credentials.Where(c => c.UserId == user.Id).ToList();
            var listToExport = credentials
                .Select(credential => 
                    new ExportImportCredentialViewModel(credential.Source, credential.Login, credential.Password))
                .ToList();

            switch (type)
            {
                case "json":
                {
                    var exporter = new Exporter(ExportType.Json);
                    return GetFileContentResult(exporter, listToExport, "application/json", "myCredentials.json");
                }
                case "xml":
                {
                    var exporter = new Exporter(ExportType.Xml);
                    return GetFileContentResult(exporter, listToExport, "application/xml", "myCredentials.xml");
                }
                default:
                    return RedirectToAction("Index");
            }
        }

        private IActionResult GetFileContentResult<T>(Exporter exporter, T objectToExport, string contentType, string fileDownloadName)
        {
            var result = exporter.GenerateString(objectToExport);
            return File(System.Text.Encoding.UTF8.GetBytes(result), contentType, fileDownloadName);
        }
    }
}