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
    public class TransactionController : Controller
    {
        private string connectionString = @"data source=DESKTOP-U7NH4L3\SQLEXPRESS;initial catalog=SIA101db;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
        private UserDBContext usrId = new UserDBContext();
        private RateEntities re = new RateEntities();
        private TransactionEntity Te = new TransactionEntity();

        // GET: Transaction
        public ActionResult History()
        {
            return View();
        }

        //get all list and send back in json
        public JsonResult GetList()
        {
            List<UserTransactionModel> rmmodel = new List<UserTransactionModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.TransactionDB WHERE Username = '" + User.Identity.GetUserName() + "' ORDER BY LastUpdateDate DESC ";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserTransactionModel rm = new UserTransactionModel();
                        //rm.RoomID = reader.GetInt32(0);
                        rm.TransactionID = reader.GetInt32(0);
                        rm.UserID = reader.GetInt32(1);
                        rm.Username = reader.GetString(2);
                        rm.LastUpdateTime = reader.GetString(3);
                        rm.LastUpdateDate = reader.GetString(4);
                        rm.RoomImage = reader.GetString(5);
                        rm.RoomType = reader.GetString(6);
                        rm.ArrivalDate = reader.GetString(7);
                        rm.ArrivalTime = reader.GetString(8);
                        rm.DepartureDate = reader.GetString(9);
                        rm.DepartureTime = reader.GetString(10);
                        rm.NumberOfNight = reader.GetInt32(11);
                        rm.NumberOfOccupants = reader.GetInt32(12);
                        rm.RoomMasterID = reader.GetString(13);
                        rm.RoomPrice = reader.GetDecimal(14);
                        rm.Status = reader.GetString(15);
                        rm.IsRated = reader.GetString(16);
                        rmmodel.Add(rm);
                    }
                }
            }
            var json = JsonConvert.SerializeObject(rmmodel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //get the transaction details
        [HttpPost]
        public JsonResult GetDetails(int transacIDd)
        {
            List<GetTransactionDetails> rmmodel = new List<GetTransactionDetails>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.TransactionDB WHERE TransactionID = '" + transacIDd + "' ";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GetTransactionDetails rm = new GetTransactionDetails();
                        //rm.RoomID = reader.GetInt32(0);
                        rm.TransactionID = reader.GetInt32(0);
                        rm.UserID = reader.GetInt32(1);
                        rm.Username = reader.GetString(2);
                        rm.LastUpdateTime = reader.GetString(3);
                        rm.LastUpdateDate = reader.GetString(4);
                        rm.RoomImage = reader.GetString(5);
                        rm.RoomType = reader.GetString(6);
                        rm.ArrivalDate = reader.GetString(7);
                        rm.ArrivalTime = reader.GetString(8);
                        rm.DepartureDate = reader.GetString(9);
                        rm.DepartureTime = reader.GetString(10);
                        rm.NumberOfNight = reader.GetInt32(11);
                        rm.NumberOfOccupants = reader.GetInt32(12);
                        rm.RoomMasterID = reader.GetString(13);
                        rm.RoomPrice = reader.GetDecimal(14);
                        rm.Status = reader.GetString(15);
                        rm.IsRated = reader.GetString(16);
                        rmmodel.Add(rm);
                    }
                }
            }
            var json = JsonConvert.SerializeObject(rmmodel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //rate and review save to database
        [HttpPost]
        public JsonResult SaveFeedbacks(int transacsID, string article, int numberOfStars)
        {
            FeedbackDB rm = new FeedbackDB();
            string cname = User.Identity.GetUserName();
            var userData = usrId.UserAccTBLs.Where(x => x.Username == cname).FirstOrDefault();

            rm.TransactionID = transacsID;
            rm.UserID = userData.UserID;
            rm.Username = userData.Username;
            rm.RateNumber = numberOfStars;

            if(article == "")
            {
                rm.Feedbacks = "NoComment";
            }
            else
            {
                rm.Feedbacks = article;
            }
            rm.DateInserted = DateTime.Now.ToString();
            re.FeedbackDBs.Add(rm);
            re.SaveChanges();
            ModelState.Clear();

            var updateTransacISrated = Te.TransactionDBs.Where(x => x.TransactionID == transacsID).FirstOrDefault();
            updateTransacISrated.IsRated = "True";
            Te.Entry(updateTransacISrated).State = System.Data.Entity.EntityState.Modified;
            Te.SaveChanges();

            //code to send data to notification table
            string currentusername = User.Identity.GetUserName();
            SignalRDBentities entity = new SignalRDBentities();
            notificationTable emp = new notificationTable();
            //get image path of a transaction with transaction id
            var getimgpath = Te.TransactionDBs.Where(x => x.TransactionID == transacsID).FirstOrDefault();
            //get userid with current username
            var getuser = usrId.UserAccTBLs.Where(x => x.Username == currentusername).FirstOrDefault();
            emp.notificationFrom = "Feedback";
            emp.Username = currentusername;
            emp.Userid = getuser.UserID;
            emp.AddedOn = DateTime.Now;
            emp.Message = "We received your feedback.";
            emp.IsRead = 0;
            emp.ImgSource = getimgpath.RoomImage;
            entity.notificationTables.Add(emp);
            entity.SaveChanges();

            var json = JsonConvert.SerializeObject(rm);
            return Json(json, JsonRequestBehavior.AllowGet);
            //return Json(new { Success = false, Message = "Success" }, JsonRequestBehavior.AllowGet);
        }

        // search for existing feedback with transaction id
        [HttpPost]
        public JsonResult SearchForFeedbacks(int transacIDd)
        {
            List<RateAndReviewModel> rateModel = new List<RateAndReviewModel>();
            var getData = re.FeedbackDBs.Where(x => x.TransactionID == transacIDd).FirstOrDefault();
            RateAndReviewModel rm = new RateAndReviewModel();

            if (getData != null)
            {
                rm.RatingID = getData.RatingID;
                rm.TransactionID = getData.TransactionID;
                rm.UserID = getData.UserID;
                rm.Username = getData.Username;
                rm.RateNumber = getData.RateNumber;
                rm.Feedbacks = getData.Feedbacks;
                rm.DateInserted = getData.DateInserted;
                rateModel.Add(rm);

                var json = JsonConvert.SerializeObject(rateModel);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                rm.RatingID = 0;
                rm.TransactionID = 0;
                rm.UserID = 0;
                rateModel.Add(rm);

                var json = JsonConvert.SerializeObject(rateModel);
                return Json(json, JsonRequestBehavior.AllowGet);
            }

        }

        //get image path and roomtype
        [HttpPost]
        public JsonResult ImgPathAndRoomtype(int id)
        {
            List<PathAndRoom> par = new List<PathAndRoom>();
            var getData = Te.TransactionDBs.Where(x => x.TransactionID == id).FirstOrDefault();
            PathAndRoom pr = new PathAndRoom();

            pr.Tid = getData.TransactionID;
            pr.imgPth = getData.RoomImage;
            pr.rmTyp = getData.RoomType;

            par.Add(pr);

            var json = JsonConvert.SerializeObject(par);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //Check for existing transaction with UserID
        public JsonResult FindTransaction()
        {
            object o;
            string currentusername = User.Identity.GetUserName();

            //to get userid
            var userData = usrId.UserAccTBLs.Where(x => x.Username == currentusername).FirstOrDefault();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT COUNT(UserID) FROM dbo.TransactionDB WHERE UserID = '" + userData.UserID + "' ";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                o = command.ExecuteScalar();

                var json = JsonConvert.SerializeObject(o);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

    }
}