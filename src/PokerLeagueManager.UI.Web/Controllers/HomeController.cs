using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerLeagueManager.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (ConfigurationManager.AppSettings["Environment"] != "DEV")
            {
                return View("~/build/Index.cshtml");
            }
            else
            {
                return View("~/Index.cshtml");
            }
        }
    }
}