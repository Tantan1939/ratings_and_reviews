using CustomerDataRatingsAndReviewsManagementSystem.DAL;
using CustomerDataRatingsAndReviewsManagementSystem.Models;
using CustomerDataRatingsAndReviewsManagementSystem.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerDataRatingsAndReviewsManagementSystem.Controllers
{

    [AuthorizeRole(Role.USER)]
    public class AddTransactionController : Controller
    {
        private TransactionEntity addTransac = new TransactionEntity();
        private string connectionString = @"data source=DESKTOP-U7NH4L3\SQLEXPRESS;initial catalog=SIA101db;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
        private UserDBContext usrId = new UserDBContext();

        // GET: AddTransaction
        public ActionResult Index()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                CreateTransactionModel accd = new CreateTransactionModel();
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
                        accd.Username = reader.GetString(1);
                    }
                }
                reader.Close();
                connection.Close();
                return View("Index", accd);
            }
        }

        [HttpPost]
        public ActionResult InsertTransactionData(CreateTransactionModel transacInfo)
        {
            if (ModelState.IsValid)
            {
                string NewImage = Guid.NewGuid() + Path.GetExtension(transacInfo.RoomImage.FileName);
                transacInfo.RoomImage.SaveAs(Server.MapPath("/TransacImage/" + NewImage));

                TransactionDB addToDb = new TransactionDB();
                addToDb.UserID = transacInfo.UserID;
                addToDb.Username = transacInfo.Username;
                addToDb.LastUpdateTime = DateTime.Now.ToString("hh:mm tt");
                addToDb.LastUpdateDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"); //change
                addToDb.RoomImage = "/TransacImage/" + NewImage;
                addToDb.RoomType = transacInfo.RoomType;
                addToDb.ArrivalDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                addToDb.ArrivalTime = DateTime.Now.ToString("hh:mm tt");
                addToDb.DepartureDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                addToDb.DepartureTime = DateTime.Now.ToString("hh:mm tt");
                addToDb.NumberOfNight = transacInfo.NumberOfNight;
                addToDb.NumberOfOccupants = transacInfo.NumberOfOccupants;
                addToDb.RoomMasterID = transacInfo.RoomMasterID;
                addToDb.RoomPrice = transacInfo.RoomPrice;
                addToDb.Status = "Booked";
                addToDb.IsRated = "False";
                addTransac.TransactionDBs.Add(addToDb);
                addTransac.SaveChanges();

                //code to send data to notification table
                string currentusername = User.Identity.GetUserName();
                SignalRDBentities entity = new SignalRDBentities();
                notificationTable emp = new notificationTable();
                var userData = usrId.UserAccTBLs.Where(x => x.Username == currentusername).FirstOrDefault();
                emp.notificationFrom = "Booking";
                emp.Username = currentusername;
                emp.Userid = userData.UserID;
                emp.AddedOn = DateTime.Now;
                emp.Message = "Booked Successfully.";
                emp.IsRead = 0;
                emp.ImgSource = "/TransacImage/" + NewImage;
                entity.notificationTables.Add(emp);
                entity.SaveChanges();
                ModelState.Clear();

                return Json(new { Success = true, Message = "Added", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { Success = false, Message = "model state not valid", JsonRequestBehavior.AllowGet });
            }
        }
    }
}