using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PasswordManager.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message, string to)
        {
            var user = Context.User;

            if (user != null)
            {
                var userName = user.Identity?.Name;
                var isTechSupport = user.IsInRole("techsupport");
                
                if (isTechSupport)
                    userName = "Служба поддержки";

                if (Context.UserIdentifier != to) // если получатель и текущий пользователь не совпадают
                    await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName, isTechSupport);
                
                await Clients.User(to).SendAsync("Receive", message, userName, isTechSupport);
            }

        }
    }
}