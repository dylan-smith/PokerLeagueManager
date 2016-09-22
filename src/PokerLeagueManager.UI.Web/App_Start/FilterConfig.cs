using System.Web;
using System.Web.Mvc;

namespace PokerLeagueManager.UI.Web
{
    public class FilterConfig
    {
        private FilterConfig()
        {
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
