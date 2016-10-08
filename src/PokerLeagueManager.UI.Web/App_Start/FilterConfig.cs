using System.Web.Mvc;

namespace PokerLeagueManager.UI.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AIHandleErrorAttribute());
        }
    }
}