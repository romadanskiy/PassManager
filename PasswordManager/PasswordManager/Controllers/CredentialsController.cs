using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    [Authorize]
    public class CredentialsController : Controller
    {
        private UserManager<User> _userManager;
        
        public CredentialsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var context = new ApplicationContext();
            var credentials = context.Credentials.Where(c => c.UserId == user.Id).ToList();
            return View(credentials);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateCredentialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationContext();
                
                var user = await _userManager.GetUserAsync(User);
                var credential = new Credential
                {
                    Source = model.Source, 
                    Login = model.Login, 
                    Password = model.Password, 
                    UserId = user.Id
                };
                context.Credentials.Add(credential);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var context = new ApplicationContext();
            
            var credential = context.Credentials
                .FirstOrDefault(c => c.Id == id);

            if (credential != null)
            {
                context.Credentials.Remove(credential);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(int id)
        {
            var context = new ApplicationContext();
            
            var credential = context.Credentials
                .FirstOrDefault(c => c.Id == id);

            if (credential == null)
            {
                return NotFound();
            }

            var model = new EditCredentialViewModel
            {
                Id = id,
                Source = credential.Source,
                Login = credential.Login,
                Password = credential.Password
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditCredentialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationContext();
                var credential = context.Credentials
                    .FirstOrDefault(c => c.Id == model.Id);

                if (credential != null)
                {
                    credential.Source = model.Source;
                    credential.Login = model.Login;
                    credential.Password = model.Password;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
    }
}