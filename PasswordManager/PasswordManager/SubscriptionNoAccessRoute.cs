using Microsoft.AspNetCore.Routing;

namespace PasswordManager
{
    public static class SubscriptionNoAccessRoute
    {
        public static readonly RouteValueDictionary NoAccessRoute = new(new
        {
            Controller = "Home",
            Action = "NoAccess"
        });

        public static readonly RouteValueDictionary SigninRoute = new(new
        {
            Controller = "Account",
            Action = "Login"
        });
    }
}