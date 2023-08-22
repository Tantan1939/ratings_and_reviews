using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.ViewModel
{
    public class ReturnRooms
    {
        public int RoomID { get; set; }
        public string RoomImage { get; set; }
        public string RoomType { get; set; }
        public decimal RoomPrice { get; set; }

        public ReturnRooms()
        {
            RoomID = 0;
            RoomImage = "wala";
            RoomType = "wala din";
            RoomPrice = 150;
        }

        public ReturnRooms(int roomID, string roomImage, string roomType, decimal roomPrice)
        {
            RoomID = roomID;
            RoomImage = roomImage;
            RoomType = roomType;
            RoomPrice = roomPrice;
        }
    }
}