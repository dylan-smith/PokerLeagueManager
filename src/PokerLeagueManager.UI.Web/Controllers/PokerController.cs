using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerLeagueManager.UI.Web.Controllers
{
    // [Authorize]
    public class PokerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}