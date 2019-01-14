using System.Web.Mvc;

namespace PokerLeagueManager.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/angular/dist/index.cshtml");
        }
    }
}