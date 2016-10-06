using System.Configuration;
using System.Web.Http;
using Microsoft.ApplicationInsights.Extensibility;

namespace PokerLeagueManager.Events.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["AppInsightsKey"];
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
