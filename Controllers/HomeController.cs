using CustomerDataRatingsAndReviewsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CustomerDataRatingsAndReviewsManagementSystem.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.USER)]
    public class HomeController : Controller
    {
        [Authorize]
        [AuthorizeRole(Role.USER)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [AuthorizeRole(Role.USER)]
        public ActionResult About()
        {
            string userd = User.Identity.GetUserName();
            ViewBag.ha = userd;
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpPost]
        public JsonResult AjaxMethod(PersonModel person)
        {
            int personId = person.PersonId;
            string name = person.Name;
            string gender = person.Gender;
            string city = person.City;
            ModelState.Clear();
            return Json(person);
        }

        [Authorize]
        [AuthorizeRole(Role.USER)]
        public ActionResult Contact()
        {
            //page without refresh
            return View();
        }

        [Authorize]
        [HttpPost]
        [AuthorizeRole(Role.USER)]
        public ActionResult Contact(EmpModel obj)
        {
            //page without refresh
            ModelState.Clear();
            return PartialView("Contact", new EmpModel());
        }

        [Authorize]
        [AuthorizeRole(Role.ADMIN)]
        public ActionResult UserPage()
        {
            ViewBag.Message = "User Page";
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}