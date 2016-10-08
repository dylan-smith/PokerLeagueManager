using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.ApplicationInsights.Extensibility;

namespace PokerLeagueManager.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["AppInsightsKey"];
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
