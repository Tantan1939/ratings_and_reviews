using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CustomerDataRatingsAndReviewsManagementSystem.Models;
using CustomerDataRatingsAndReviewsManagementSystem.DAL;
using System.Web.Security;
using System.Data.SqlClient;

namespace CustomerDataRatingsAndReviewsManagementSystem.Controllers
{
    [Authorize]
    public class MainLoginController : Controller
    {
        // GET: MainLogin
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private AccImgEntities objEntities = new AccImgEntities();

        public MainLoginController()
        {
        }

        public MainLoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //account -start
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(MAINLoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Repository repo = new Repository();
            UserAccTBL user = repo.GetUserDetails(model.Username, model.AccountPassword);
            if (user != null)
            {
                if ((model.Username == user.Username) && (model.AccountPassword == user.AccountPassword))
                {
                    //create cookie
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    FormsAuthentication.SetAuthCookie(Convert.ToString(user.UserID), model.RememberMe);
                    var authTicket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(60), false, user.Roles);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    //Based on the Role we can transfer the user to different page
                    return RedirectToAction("History", "Transaction");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "MainLogin");
        }

    }
}