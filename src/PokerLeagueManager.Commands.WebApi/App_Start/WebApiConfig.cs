using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace PokerLeagueManager.Commands.WebApi
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
                name: "CommandApi",
                routeTemplate: "{commandName}",
                defaults: new { controller = "Command" });

            config.Services.Add(typeof(IExceptionLogger), new AIExceptionLogger());
        }
    }
}
