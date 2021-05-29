using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models;
using PasswordManager.ViewModels;

namespace PasswordManager.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private UserManager<User> _userManager;
        
        public ChatController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ChatViewModel();
            
            if (user != null)
            {
                if (User.IsInRole("techsupport"))
                {
                    model.IsTechSupport = true;
                    model.Email = "Служба поддержки";
                }
                else
                {
                    model.Email = user.Email;
                    model.Receiver = "techsupport";
                }
            }
            
            return View(model);
        }
    }
}