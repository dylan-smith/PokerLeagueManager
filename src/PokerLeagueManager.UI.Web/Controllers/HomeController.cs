using System.Web.Mvc;

namespace PokerLeagueManager.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/angular/dist/index.cshtml");
        }
    }
}