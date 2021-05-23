using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager
{
    public class SubscriptionAttribute : ActionFilterAttribute
    {
        private string ControllerName { get; set; }

        public SubscriptionAttribute(string controllerName)
        {
            ControllerName = controllerName;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var claimsPrincipalUser = actionContext.HttpContext.User;
            
            // Если пользователь не аутентифицирован, ...
            if (claimsPrincipalUser.Identity is {IsAuthenticated: false})
            {
                // отправляем на страинцу "Вход"
                var signinRoute = SubscriptionNoAccessRoute.SigninRoute;
                actionContext.Result = new RedirectToRouteResult(signinRoute);
                return;
            }
            
            // Если пользователь админ, разрешаем доступ без проверки подписки.
            if (claimsPrincipalUser.IsInRole("admin")) 
                return;
            
            var userId = claimsPrincipalUser.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var context = new ApplicationContext();
            var user = context.Users.FirstOrDefault(u => u.Id == userId); // Получаем текущего пользователя.
            if (user is {HasSubscription: true}) //  Если у него есть подписка, ...
            {
                var subscription = context
                    .Subscriptions
                    .Include(s=>s.Features)
                    .FirstOrDefault(s => s.Id == user.SubscriptionId); // достаем её,
                // проверяем, что она существует и имеет доступ к этой фиче.
                if (subscription != null && subscription.Features.Any(f => f.ControllerName == ControllerName)) 
                {
                    return; // Если всё ок, разрешаем доступ к контроллеру.
                }
            }
            
            // Иначе отправляем на страинцу "Нет доступа".
            var noAccessRoute = SubscriptionNoAccessRoute.NoAccessRoute;
            actionContext.Result = new RedirectToRouteResult(noAccessRoute);
        }
    }
}