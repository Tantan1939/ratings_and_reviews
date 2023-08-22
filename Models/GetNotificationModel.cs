using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class GetNotificationModel
    {
        public int notificationID { get; set; }
        public string notificationFrom { get; set; }
        public string Username { get; set; }
        public int Userid { get; set; }
        public string AddedOn { get; set; }
        public string Message { get; set; }
        public int Isread { get; set; }
        public string ImgSource { get; set; }

        public GetNotificationModel()
        {
            notificationID = 0;
            notificationFrom = "";
            Username = "";
            Userid = 0;
            AddedOn = "";
            Message = "";
            Isread = 0;
            ImgSource = "";
        }

        public GetNotificationModel(int notificationID, string notificationFrom, string username, int userid, string addedOn, string message, int isread, string imgSource)
        {
            this.notificationID = notificationID;
            this.notificationFrom = notificationFrom;
            Username = username;
            Userid = userid;
            AddedOn = addedOn;
            Message = message;
            Isread = isread;
            ImgSource = imgSource;
        }
    }
}