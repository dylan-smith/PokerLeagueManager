using System.Web.Mvc;
using System.Web.Routing;

namespace PokerLeagueManager.UI.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: string.Empty,
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}