using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerDataRatingsAndReviewsManagementSystem.Models;
using CustomerDataRatingsAndReviewsManagementSystem.DAL;
using CustomerDataRatingsAndReviewsManagementSystem.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
using CustomerDataRatingsAndReviewsManagementSystem.TestingModels;

namespace CustomerDataRatingsAndReviewsManagementSystem.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.USER)]
    public class DashBoardController : Controller
    {
        private SampleListConn objECartDbEntities = new SampleListConn();
        private AccImgEntities imgitem = new AccImgEntities();
        private UserDBContext usrId = new UserDBContext();
        private string connectionString = @"data source=DESKTOP-U7NH4L3\SQLEXPRESS;initial catalog=SIA101db;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";

        [HttpPost]
        public ActionResult UploadImg(ReturnUserInfoModel userim)
        {
            try
            {
                if (userim.HoldNewImg != null && userim.HoldNewImg.ContentLength > 0)
                {
                    UpdatePicModel newpic = new UpdatePicModel();
                    newpic.UserID = userim.UserID;
                    newpic.ImgPath = userim.HoldNewImg;

                    string NewImage = Guid.NewGuid() + Path.GetExtension(newpic.ImgPath.FileName);
                    newpic.ImgPath.SaveAs(Server.MapPath("/Images/" + NewImage));
                    UserAccTBL obimg = new UserAccTBL();
                    string uname = User.Identity.GetUserName();
                    obimg.ImagePath = "/Images/" + NewImage;
                    var userData = usrId.UserAccTBLs.Where(x => x.Username == uname).FirstOrDefault();

                    if (userData != null)
                    {
                        userData.ImagePath = obimg.ImagePath;
                        usrId.Entry(userData).State = System.Data.Entity.EntityState.Modified;
                        usrId.SaveChanges();

                        return Json(new { Success = true, Message = "Image Saved" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "An error occurred. Please try again." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Success = false, Message = "No image selected." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        //get - display account details
        public ActionResult ManageAccount()
        {
            //connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ReturnUserInfoModel accd = new ReturnUserInfoModel();
                string names = User.Identity.GetUserName();
                string sqlQuery = "SELECT * FROM dbo.UserAccTBL WHERE Username = @names";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@names", System.Data.SqlDbType.NVarChar).Value = names;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        accd.UserID = reader.GetInt32(0);
                        accd.FirstName = reader.GetString(3);
                        accd.LastName = reader.GetString(4);
                        accd.EmailAddress = reader.GetString(5);
                        accd.HomeAddress = reader.GetString(6);
                        accd.Age = reader.GetInt32(7);
                        accd.City = reader.GetString(8);
                        accd.Region = reader.GetString(9);
                        accd.ZIPcode = reader.GetInt32(10);
                        accd.SimNumber = reader.GetString(11);
                        accd.ImgPath = reader.GetString(13);
                    }
                }
                reader.Close();
                connection.Close();
                return View("ManageAccount", accd);
            }
        }

        //get new image path
        public JsonResult newPathDisplay(string ImagePath)
        {
            string imgPathString = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string names = User.Identity.GetUserName();
                string sqlQuery = "SELECT * FROM dbo.UserAccTBL WHERE Username = @names";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@names", System.Data.SqlDbType.NVarChar).Value = names;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        imgPathString = reader.GetString(13);
                    }
                }
                reader.Close();
                connection.Close();
            }
            var json = JsonConvert.SerializeObject(imgPathString);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //post updated account details
        [HttpPost]
        public JsonResult ManageAccount(ReturnUserInfoModel userModel)
        {
            if (ModelState.IsValid)
            {
                var userData = usrId.UserAccTBLs.Where(x => x.UserID == userModel.UserID).FirstOrDefault();
                if (userData != null)
                {
                    userData.FirstName = userModel.FirstName;
                    userData.LastName = userModel.LastName;
                    userData.EmailAddress = userModel.EmailAddress;
                    userData.HomeAddress = userModel.HomeAddress;
                    userData.Age = userModel.Age;
                    userData.City = userModel.City;
                    userData.Region = userModel.Region;
                    userData.ZIPcode = userModel.ZIPcode;
                    userData.SimNumber = userModel.SimNumber;

                    usrId.Entry(userData).State = System.Data.Entity.EntityState.Modified;
                    usrId.SaveChanges();
                }
                return Json(new { Success = true, Message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "Fill all fields" }, JsonRequestBehavior.AllowGet);
            }
        }

        //get - change password view
        public ActionResult UpdatePassword()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string names = User.Identity.GetUserName();
                string sqlQuery = "SELECT UserID, Username, AccountPassword FROM dbo.UserAccTBL WHERE Username = @names";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@names", System.Data.SqlDbType.NVarChar).Value = names;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                NewPasswordModel accd = new NewPasswordModel();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        accd.UserID = reader.GetInt32(0);
                        accd.Username = reader.GetString(1);
                        accd.HiddenCurrentPassword = reader.GetString(2);
                    }
                }
                return View("UpdatePassword", accd);
            }
        }

        //post - new password
        [HttpPost]
        public JsonResult UpdatePassword(NewPasswordModel userModel)
        {
            if (ModelState.IsValid)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE dbo.UserAccTBL SET AccountPassword=@AccountPassword WHERE UserID ='" + userModel.UserID + "' AND Username = '"+ userModel.Username +"' ", connection);
                command.Parameters.AddWithValue("@AccountPassword", userModel.NewPassword);
                command.ExecuteNonQuery();
                connection.Close();
                ModelState.Clear();
                return Json(new { Success = true, Message = "Update Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "Fill all fields." }, JsonRequestBehavior.AllowGet);
            }
        }

        //get
        public ActionResult DisplaySample()
        {
            return View();
        }

        [HttpPost]
        public JsonResult disp(ItermViewModel objItemViewModel)
        {
            if (ModelState.IsValid)
            {
                if(objItemViewModel.ImagePath != null && objItemViewModel.ImagePath.ContentLength > 0)
                {
                    return Json(new { Success = false, Message = "image path not null" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName);
                    objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

                    Item objItem = new Item();
                    objItem.ImagePath = "~/Images/" + NewImage;
                    objItem.CategoryId = objItemViewModel.CategoryId;
                    objItem.Description = objItemViewModel.Description;
                    objItem.ItemCode = objItemViewModel.ItemCode;
                    objItem.ItemId = Guid.NewGuid();
                    objItem.ItemName = objItemViewModel.ItemName;
                    objItem.ItemPrice = objItemViewModel.ItemPrice;
                    objECartDbEntities.Items.Add(objItem);
                    objECartDbEntities.SaveChanges();

                    return Json(new { Success = true, Message = "Item is added Successfully." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, Message = "Fill all fields." }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ShoppingView()
        {
            IEnumerable<ShoppingViewModel> listOfShoppingViewModels = (from objItem in objECartDbEntities.Items
                                                                       join
                                                                           objCate in objECartDbEntities.Items
                                                                           on objItem.CategoryId equals objCate.CategoryId
                                                                       select new ShoppingViewModel()
                                                                       {
                                                                           ImagePath = objItem.ImagePath,
                                                                           ItemName = objItem.ItemName,
                                                                           Description = objItem.Description,
                                                                           ItemPrice = objItem.ItemPrice,
                                                                           ItemId = objItem.ItemId,
                                                                           CategoryId = objCate.CategoryId,
                                                                           ItemCode = objItem.ItemCode
                                                                       }

                ).ToList();
            return View(listOfShoppingViewModels);
        }

    }
}