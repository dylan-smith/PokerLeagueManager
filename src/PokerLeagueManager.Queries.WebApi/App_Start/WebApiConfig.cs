using System.Web.Http;

namespace PokerLeagueManager.Queries.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "QueryApi",
                routeTemplate: "api/{controller}/{action}");
        }
    }
}
