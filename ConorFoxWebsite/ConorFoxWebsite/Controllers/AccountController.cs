using System.Web.Mvc;
using System.Web.Security;
using ConorFoxWebsite.Models;
using ConorFoxWebsite.WebServiceReference;

namespace ConorFoxWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebsiteServiceClient _wsClient = new WebsiteServiceClient();

        /// <summary>
        /// Logon view on startup returned
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentlogOn()
        {
            return View();
        }
        
        /// <summary>
        /// staff Logon page returned on maviagtion to view
        /// </summary>
        /// <returns></returns>
        public ActionResult StafflogOn()
        {
            return View();
        }

        /// <summary>
        /// Submission of Logon details for student
        /// redirect to summary view on success or
        /// validation returned on failed
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StudentLogOn(LogOnModel model, string returnUrl)
        {
            var user = _wsClient.StudentLogin(model.UserName, model.Password);
            if (user != null)
            {
                //usermodel generated from returendinformation on student who has logged in to be passed
                //between views 
                var userModel = new UserModel
                
                {   Id= user.StudentId,
                    Forename = user.StudentForename,
                    Surname = user.StudentSurname,
                    Role = "Student",
                    UserName = user.StudentEmail
                };
                //cookie setup
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    //Redirect for successful login
                        return RedirectToAction("Summary", "Home", userModel);
          }
            
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            //current view returned with validation due to login fail
            return View(model);
        }
        
        /// <summary>
        /// Submission of Logon details for stafff
        /// redirect to summary view on success or
        /// validation returned on failed
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StaffLogOn(LogOnModel model, string returnUrl)
        {
            var user = _wsClient.StaffLogin(model.UserName, model.Password);
            if (user != null)
            {
                //usermodel generated from returendinformation on staff member who has logged in to be passed
                //between views 
                var userModel = new UserModel
                {   Id = user.StaffId,
                    Forename = user.StaffForename,
                    Surname = user.StaffSurname,
                    Role = "Staff",
                    UserName = user.StaffEmail
                };
                //cookie setup
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    //current view returned with validation due to login fail
                        return RedirectToAction("StaffSummary", "Home", userModel);
          }
            
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                //current view returned with validation due to login fail
            return View();
        }

        /// <summary>
        /// returns logoff view
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            return View();
        } 
    }
}
