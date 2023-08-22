using CustomerDataRatingsAndReviewsManagementSystem.DAL;
using CustomerDataRatingsAndReviewsManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerDataRatingsAndReviewsManagementSystem.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.USER)]
    public class NotificationController : Controller
    {
        private SignalRDBentities sre = new SignalRDBentities();
        private string connectionString = @"data source=DESKTOP-U7NH4L3\SQLEXPRESS;initial catalog=SIA101db;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
        private UserDBContext usrId = new UserDBContext();

        //the old one - useless - for reference only
        public JsonResult GettNotifications()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();
            var list = NC.GetData(notificationRegisterTime);

            //update session here for get only new added contacts (notification)  
            Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //get list of notifications - both read and unread
        public JsonResult GetNotifications()
        {
            string currentusername = User.Identity.GetUserName();
            List<GetNotificationModel> gnm = new List<GetNotificationModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var userData = usrId.UserAccTBLs.Where(x => x.Username == currentusername).FirstOrDefault();
                string sqlQuery = "SELECT * FROM dbo.notificationTable WHERE UserID = '" + userData.UserID + "' ORDER BY AddedOn DESC";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GetNotificationModel gm = new GetNotificationModel();
                        gm.notificationID = reader.GetInt32(0);
                        gm.notificationFrom = reader.GetString(1);
                        gm.Username = reader.GetString(2);
                        gm.Userid = reader.GetInt32(3);
                        DateTime addedOn =  reader.GetDateTime(4);
                        var timeSpan = DateTime.Now.Subtract(addedOn);

                        if (timeSpan <= TimeSpan.FromSeconds(60))
                        {
                            gm.AddedOn = string.Format("{0} seconds ago", timeSpan.Seconds);
                        }
                        else if (timeSpan <= TimeSpan.FromMinutes(60))
                        {
                            gm.AddedOn = timeSpan.Minutes > 1 ?
                                String.Format("{0} minutes ago", timeSpan.Minutes) :
                                "about a minute ago";
                        }
                        else if (timeSpan <= TimeSpan.FromHours(24))
                        {
                            gm.AddedOn = timeSpan.Hours > 1 ?
                                String.Format("{0} hours ago", timeSpan.Hours) :
                                "about an hour ago";
                        }
                        else if (timeSpan <= TimeSpan.FromDays(30))
                        {
                            gm.AddedOn = timeSpan.Days > 1 ?
                                String.Format("{0} days ago", timeSpan.Days) :
                                "yesterday";
                        }
                        else if (timeSpan <= TimeSpan.FromDays(365))
                        {
                            gm.AddedOn = timeSpan.Days > 30 ?
                                String.Format("{0} months ago", timeSpan.Days / 30) :
                                "about a month ago";
                        }
                        else
                        {
                            gm.AddedOn = timeSpan.Days > 365 ?
                                String.Format("{0} years ago", timeSpan.Days / 365) :
                                "about a year ago";
                        }

                        //gm.AddedOn = addedOn.ToString("hh:mm tt");
                        gm.Message = reader.GetString(5);
                        gm.Isread = reader.GetInt32(6);
                        gm.ImgSource = reader.GetString(7);
                        gnm.Add(gm);
                    }
                }
            }
            var json = JsonConvert.SerializeObject(gnm);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        //count unread notification
        public JsonResult countunread()
        {
            object o;
            string currentusername = User.Identity.GetUserName();

            //to get userid
            var userData = usrId.UserAccTBLs.Where(x => x.Username == currentusername).FirstOrDefault();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT COUNT(isRead) FROM dbo.notificationTable WHERE IsRead = '" + 0 + "' AND Userid = '"+ userData.UserID +"' ";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                o = command.ExecuteScalar();

                var json = JsonConvert.SerializeObject(o);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        //mark all as read
        [HttpPost]
        public JsonResult ReadAll(int cnt)
        {
            string currentusername = User.Identity.GetUserName();
            var userData = usrId.UserAccTBLs.Where(x => x.Username == currentusername).FirstOrDefault();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE dbo.notificationTable SET IsRead=@IsRead WHERE Userid ='" + userData.UserID + "' AND IsRead = '" + 0 + "' ", connection);
            command.Parameters.AddWithValue("@IsRead", 1);
            command.ExecuteNonQuery();
            connection.Close();
            string succ = "Success";
            var json = JsonConvert.SerializeObject(succ);
            return Json(json, JsonRequestBehavior.AllowGet);

        }



    }
}