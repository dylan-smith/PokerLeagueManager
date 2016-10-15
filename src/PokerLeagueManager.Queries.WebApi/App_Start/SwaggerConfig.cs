using System.Web.Http;
using PokerLeagueManager.Queries.WebApi;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PokerLeagueManager.Queries.WebApi
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "PokerLeagueManager.Queries.WebApi");
                        c.DocumentFilter<QuerySwaggerFilter>();
                    })
                .EnableSwaggerUi();
        }
    }
}
