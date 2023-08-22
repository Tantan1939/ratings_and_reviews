using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class GetTransactionDetails
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string LastUpdateTime { get; set; }
        public string LastUpdateDate { get; set; }
        public string RoomImage { get; set; }
        public string RoomType { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public int NumberOfNight { get; set; }
        public int NumberOfOccupants { get; set; }
        public string RoomMasterID { get; set; }
        public decimal RoomPrice { get; set; }
        public string Status { get; set; }
        public string IsRated { get; set; }

        public GetTransactionDetails()
        {
            TransactionID = 0;
            UserID = 0;
            Username = "";
            LastUpdateTime = "";
            LastUpdateDate = "";
            RoomImage = "";
            RoomType = "";
            ArrivalDate = "";
            ArrivalTime = "";
            DepartureDate = "";
            DepartureTime = "";
            NumberOfNight = 0;
            NumberOfOccupants = 0;
            RoomMasterID = "";
            RoomPrice = 0;
            Status = "";
            IsRated = "";

        }

        public GetTransactionDetails(int transactionID, int userID, string username, string lastUpdateTime, string lastUpdateDate, string roomImage, string roomType, string arrivalDate, string arrivalTime, string departureDate, string departureTime, int numberOfNight, int numberOfOccupants, string roomMasterID, decimal roomPrice, string status, string isRated)
        {
            TransactionID = transactionID;
            UserID = userID;
            Username = username;
            LastUpdateTime = lastUpdateTime;
            LastUpdateDate = lastUpdateDate;
            RoomImage = roomImage;
            RoomType = roomType;
            ArrivalDate = arrivalDate;
            ArrivalTime = arrivalTime;
            DepartureDate = departureDate;
            DepartureTime = departureTime;
            NumberOfNight = numberOfNight;
            NumberOfOccupants = numberOfOccupants;
            RoomMasterID = roomMasterID;
            RoomPrice = roomPrice;
            Status = status;
            IsRated = isRated;
        }
    }
}