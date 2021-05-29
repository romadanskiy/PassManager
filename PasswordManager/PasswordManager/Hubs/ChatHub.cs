using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PasswordManager.Models;

namespace PasswordManager.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private UserManager<User> _userManager;
        
        public ChatHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task Send(string message, string to)
        {
            var user = Context.User;

            if (user != null)
            {
                var userName = user.Identity?.Name;
                var isTechSupport = user.IsInRole("techsupport");
                
                if (isTechSupport)
                    userName = "Служба поддержки";
                
                var users = _userManager.Users.ToList();
                var supports = new List<string>();
                foreach (var u in users)
                {
                    var roles = await _userManager.GetRolesAsync(u);
                    if (roles.Contains("techsupport"))
                        supports.Add(u.Email);
                }

                if (to == "techsupport")
                {
                    if (!supports.Contains(Context.UserIdentifier))
                        await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName, isTechSupport);
                    await Clients.Users(supports).SendAsync("Receive", message, userName, isTechSupport);
                }
                else
                {
                    if (Context.UserIdentifier != to) // если получатель и текущий пользователь не совпадают
                        await Clients.User(to).SendAsync("Receive", message, userName, isTechSupport);
                    await Clients.Users(supports).SendAsync("Receive", message, userName, isTechSupport);
                }
            }

        }
    }
}