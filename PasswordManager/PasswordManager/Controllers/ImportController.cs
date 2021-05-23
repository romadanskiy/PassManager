using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.Models;
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
        public IActionResult Import(IFormFile upload)
        {
            if (upload == null) return RedirectToAction("Index");
            
            var contentType = upload.ContentType;

            if (contentType != "application/json" && contentType != "application/xml" && contentType != "text/xml")
            {
                TempData["importMessage"] = "Неподдерживаемый формат";
                return RedirectToAction("Index");
            }

            var str = "";
            using (var reader = new StreamReader(upload.OpenReadStream()))
            {
                str = reader.ReadToEnd();
            }

            return RedirectToAction("ImportResult", new {contentString = str, contentType = contentType});

        }
        
        public IActionResult ImportResult(string contentString, string contentType)
        {
            var importer = contentType switch
            {
                "application/json" => new Importer(ImportType.Json),
                "application/xml" => new Importer(ImportType.Xml),
                "text/xml" => new Importer(ImportType.Xml),
                _ => null
            };
            
            try
            {
                var importResult = importer?.ParseContentString<List<ExportImportCredentialViewModel>>(contentString);
                return View(importResult);
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