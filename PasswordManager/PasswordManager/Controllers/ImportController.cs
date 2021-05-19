using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.ViewModels;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PasswordManager.Controllers
{
    [Authorize]
    public class ImportController : Controller
    {
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
            catch (Exception e)
            {
                TempData["importMessage"] = "Не удалось извлечь пароли из файла";
                return RedirectToAction("Index");
            }
        }
    }
}