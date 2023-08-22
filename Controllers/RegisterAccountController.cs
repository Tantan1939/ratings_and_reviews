using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerDataRatingsAndReviewsManagementSystem.DAL;
using CustomerDataRatingsAndReviewsManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace CustomerDataRatingsAndReviewsManagementSystem.Controllers
{
    public class RegisterAccountController : Controller
    {
        private AccImgEntities objEntities = new AccImgEntities();
        private UserDBContext userdatamodel = new UserDBContext();
        private string connectionString = @"data source=DESKTOP-U7NH4L3\SQLEXPRESS;initial catalog=SIA101db;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";

        // GET: RegisterAccount
        [AllowAnonymous, HttpGet]
        public ActionResult RegisterAccount(int id = 0)
        {
            AccountRegistrationModel accmodel = new AccountRegistrationModel();
            return View(accmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAccount(AccountRegistrationModel accountmodel)
        {
            if (ModelState.IsValid)
            {
                UserDBContext dbmodel = new UserDBContext();
                if (dbmodel.UserAccTBLs.Any(x => x.Username == accountmodel.Username))
                {
                    //if may existing na username
                    //OnFailure
                    ModelState.AddModelError("","Username already exist");
                    return View(accountmodel);
                }
                else
                {
                    //SqlConnection con = new SqlConnection(connectionString);
                    //con.Open();
                    //SqlCommand cmd = new SqlCommand("INSERT INTO UserAccTBL (Username,AccountPassword,FirstName,LastName,EmailAddress,HomeAddress,Age,City,Region,ZIPcode,SimNumber,Roles,ImagePath) " +
                    //    "values(@Username,@AccountPassword,@FirstName,@LastName,@EmailAddress,@HomeAddress,@Age,@City,@Region,@ZIPcode,@SimNumber,@Roles,@ImagePath)", con);
                    //cmd.Parameters.AddWithValue("@Username", accountmodel.Username);
                    //cmd.Parameters.AddWithValue("@AccountPassword", accountmodel.AccountPassword);
                    //cmd.Parameters.AddWithValue("@FirstName", accountmodel.FirstName);
                    //cmd.Parameters.AddWithValue("@LastName", accountmodel.LastName);
                    //cmd.Parameters.AddWithValue("@EmailAddress", accountmodel.EmailAddress);
                    //cmd.Parameters.AddWithValue("@HomeAddress", accountmodel.HomeAddress);
                    //cmd.Parameters.AddWithValue("@Age", accountmodel.Age);
                    //cmd.Parameters.AddWithValue("@City", accountmodel.City);
                    //cmd.Parameters.AddWithValue("@Region", accountmodel.Region);
                    //cmd.Parameters.AddWithValue("@ZIPcode", accountmodel.ZIPcode);
                    //cmd.Parameters.AddWithValue("@SimNumber", accountmodel.SimNumber);
                    //cmd.Parameters.AddWithValue("@Roles", "USER");
                    //cmd.Parameters.AddWithValue("@ImagePath", "empty");
                    //cmd.ExecuteNonQuery();
                    //con.Close();

                    UserAccTBL addUser = new UserAccTBL();
                    addUser.Username = accountmodel.Username;
                    addUser.AccountPassword = accountmodel.AccountPassword;
                    addUser.FirstName = accountmodel.FirstName;
                    addUser.LastName = accountmodel.LastName;
                    addUser.EmailAddress = accountmodel.EmailAddress;
                    addUser.HomeAddress = accountmodel.HomeAddress;
                    addUser.Age = accountmodel.Age;
                    addUser.City = accountmodel.City;
                    addUser.Region = accountmodel.Region;
                    addUser.ZIPcode = accountmodel.ZIPcode;
                    addUser.SimNumber = accountmodel.SimNumber;
                    addUser.Roles = "USER";
                    addUser.ImagePath = "empty";

                    userdatamodel.UserAccTBLs.Add(addUser);
                    userdatamodel.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Login","MainLogin", new AccountRegistrationModel());
                }
            }
            else
            {
                ModelState.AddModelError("", "Fill all fields.");
                return View(accountmodel);
            }
        }

    }
}