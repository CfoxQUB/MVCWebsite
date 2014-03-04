using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConorFoxWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SplashScreen()
        {
            return View("SplashScreen");
        }

        public ActionResult Index()
        {
           return View("Index");
        }

        public ActionResult About()
        {
            return View("About");
        }

        public ActionResult Timetable()
        {
            return View("Timetable");
        }

        public ActionResult Summary()
        {
            return View("Summary");
        }
    }
}
