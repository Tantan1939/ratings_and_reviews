using CustomerDataRatingsAndReviewsManagementSystem.ViewModel;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CustomerDataRatingsAndReviewsManagementSystem.Data
{
    internal class RoomDAO
    {
        private string connectionString = @"data source=DESKTOP-U7NH4L3\SQLEXPRESS;initial catalog=SIA101db;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";

        public List<ReturnRooms> FetchAll()
        {
            List<ReturnRooms> returnList = new List<ReturnRooms>();

            //access the database
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
                        // create new room object. add it to list to return
                        ReturnRooms rmss = new ReturnRooms();
                        rmss.RoomID = reader.GetInt32(0);
                        rmss.RoomImage = reader.GetString(1);
                        rmss.RoomType = reader.GetString(2);
                        rmss.RoomPrice = reader.GetDecimal(3);
                        returnList.Add(rmss);
                    }
                }
            }
            return returnList;
        }

    }
}