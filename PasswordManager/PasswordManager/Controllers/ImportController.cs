using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using PasswordManager.ViewModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PasswordManager.Controllers
{
    [Subscription("Import")]
    public class ImportController : Controller
    {
        private UserManager<User> _userManager;
        
        public ImportController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportResult(IFormFile upload)
        {
            if (upload == null)
                return RedirectToAction("Index");
            
            var contentType = upload.ContentType;
            var importer = Importer.CreateImporter(contentType);

            if (importer == null)
            {
                TempData["importMessage"] = "Неподдерживаемый формат";
                return RedirectToAction("Index");
            }

            var contentString = "";
            using (var reader = new StreamReader(upload.OpenReadStream()))
            {
                contentString = reader.ReadToEnd();
            }

            try
            {
                var importResult = importer.ParseContentString<List<ExportImportCredentialViewModel>>(contentString);
                return View("ImportResult", importResult);
            }
            catch
            {
                TempData["importMessage"] = "Не удалось извлечь пароли из файла";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveAll(string jsonModel)
        {
            var model = JsonSerializer.Deserialize<List<ExportImportCredentialViewModel>>(jsonModel);

            if (model == null)
                return RedirectToAction("Index");
            
            var context = new ApplicationContext();
            var user = await _userManager.GetUserAsync(User);
            foreach (var credentialToSave in model)
            {
                var newCredential = new Credential
                {
                    Source = credentialToSave.Source, 
                    Login = credentialToSave.Login, 
                    Password = credentialToSave.Password, 
                    UserId = user.Id
                };
                context.Credentials.Add(newCredential);
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Credentials");
        }
    }
}