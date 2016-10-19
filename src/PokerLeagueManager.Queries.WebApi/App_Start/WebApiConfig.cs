using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace PokerLeagueManager.Queries.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "QueryApi",
                routeTemplate: "{queryName}",
                defaults: new { controller = "Query" });

            config.Services.Add(typeof(IExceptionLogger), new AIExceptionLogger());
        }
    }
}
