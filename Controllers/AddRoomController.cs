using CustomerDataRatingsAndReviewsManagementSystem.DAL;
using CustomerDataRatingsAndReviewsManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;
using CustomerDataRatingsAndReviewsManagementSystem.Data;
using System.Web.Script.Serialization;

namespace CustomerDataRatingsAndReviewsManagementSystem.Controllers
{
    public class AddRoomController : Controller
    {
        private RoomModelEntities objEntities = new RoomModelEntities();
        // GET: AddRoom
        RoomModelEntities context = new RoomModelEntities();
        private string connectionString = @"data source=DESKTOP-U7NH4L3\SQLEXPRESS;initial catalog=SIA101db;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";

        public ActionResult NewRoom()
        {
            return View();
        }

        [HttpPost]
        public JsonResult newRoom(CreateRoomModel rm)
        {
            if (ModelState.IsValid)
            {
                string NewImage = Guid.NewGuid() + Path.GetExtension(rm.roomImage.FileName);
                rm.roomImage.SaveAs(Server.MapPath("/TransacImage/" + NewImage));

                roomDB rmdb = new roomDB();
                rmdb.RoomImage = "/TransacImage/" + NewImage;
                rmdb.RoomPrice = rm.roomPrice;
                rmdb.RoomType = rm.roomType;

                objEntities.roomDBs.Add(rmdb);
                objEntities.SaveChanges();
                return Json(new { Success = true, Message = "Added", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { Success = false, Message = "model state not valid", JsonRequestBehavior.AllowGet });
            }
            
        }

        //get all list and send back in json
        public JsonResult NewList()
        {
            List<ReturnRooms> rmmodel = new List<ReturnRooms>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.roomDB";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReturnRooms rm = new ReturnRooms();
                        rm.RoomID = reader.GetInt32(0);
                        rm.RoomImage = reader.GetString(1);
                        rm.RoomType = reader.GetString(2);
                        rm.RoomPrice = reader.GetDecimal(3);
                        rmmodel.Add(rm);
                    }
                }
            }

            //codes to display only one set of list
            //var newList = objEntities.roomDBs.ToList();
            var json = JsonConvert.SerializeObject(rmmodel);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

    }
}