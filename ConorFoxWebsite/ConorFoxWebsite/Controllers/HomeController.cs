using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using ConorFoxWebsite.Models;
using ConorFoxWebsite.WebServiceReference;

namespace ConorFoxWebsite.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Timetable for student view returned
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        public ActionResult Timetable(UserModel usermodel)
        {
            return View("Timetable", usermodel);
        }

        /// <summary>
        /// Summary view for student returned
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ActionResult Summary(UserModel userModel)
        { 
            return View("Summary", userModel);
        }
        
        /// <summary>
        /// Timetable for staff member returned
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        public ActionResult StaffTimetable(UserModel usermodel)
        {
            return View("StaffTimetable", usermodel);
        }

        /// <summary>
        /// Summary for staff memebr returned
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ActionResult StaffSummary(UserModel userModel)
        { 
            return View("StaffSummary", userModel);
        }

         
    }
}
