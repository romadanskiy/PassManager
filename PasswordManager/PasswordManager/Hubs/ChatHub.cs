using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Models;

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
        
        /// <summary>
        /// Отправляет сообщение конкретному получателю
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="to">Идентификатор получателя (email). Если он равен "techsupport", то письмо направлено техподдержке</param>
        public async Task Send(string message, string to)
        {
            var user = Context.User; // отправитель (текущий пользователь)

            if (user != null)
            {
                var userIdentifier = Context.UserIdentifier ?? "Неопознанный отправитель";
                var isTechSupport = user.IsInRole("techsupport"); // является ли текущий пользователь техподдержкой
                
                /*
                if (isTechSupport)
                    userIdentifier = "Служба поддержки"; */

                var supports = await GetTechSupports(); // список всей техподдержки

                if (to == "techsupport") // если письмо направлено техподдержке
                {
                    if (!supports.Contains(userIdentifier)) // если отправитель сам не является техподдержкой
                    { 
                        // отправляем сообщение отправителю и получателю (всей техподдержке)
                        await Clients.User(userIdentifier).SendAsync("Receive", message, userIdentifier, isTechSupport);
                        await Clients.Users(supports).SendAsync("Receive", message, userIdentifier, isTechSupport);
                    }
                    else // если сообщение внутренне (между техподдержкой)
                    {
                        // отправляем письмо всей техподдержке
                        userIdentifier = "(sup) " + userIdentifier;
                        await Clients.Users(supports).SendAsync("Receive", message, userIdentifier, isTechSupport);
                    }
                }
                else // если письмо направлено конкретному юзеру (письмо от техподдержки)
                {
                    // отправляем письмо всей техподдержке
                    userIdentifier += $" TO {to}";
                    await Clients.Users(supports).SendAsync("Receive", message, userIdentifier, isTechSupport);
                    
                    // отправляем сообщение получателю
                    userIdentifier = "Служба поддержки";
                    await Clients.User(to).SendAsync("Receive", message, userIdentifier, isTechSupport);
                }
            }

        }

        /// <summary>
        /// Возвращает всех пользователей с ролью "techsupport"
        /// </summary>
        /// <returns>Список емайлов всей техподдержки</returns>
        private async Task<List<string>> GetTechSupports()
        {
            var users = _userManager.Users.ToList();
            var supports = new List<string>();
            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                if (roles.Contains("techsupport"))
                    supports.Add(u.Email);
            }

            return supports;
        }
    }
}