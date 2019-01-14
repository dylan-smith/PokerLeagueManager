using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace PokerLeagueManager.Events.WebApi
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
                name: "EventApi",
                routeTemplate: "{eventName}",
                defaults: new { controller = "Event" });

            config.Services.Add(typeof(IExceptionLogger), new AIExceptionLogger());
        }
    }
}
