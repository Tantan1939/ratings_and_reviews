using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class RateAndReviewModel
    {
        public int RatingID { get; set; }
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public int RateNumber { get; set; }
        public string Feedbacks { get; set; }
        public string DateInserted { get; set; }

        public RateAndReviewModel()
        {
            RatingID = 0;
            TransactionID = 0;
            UserID = 0;
            Username = "";
            RateNumber = 0;
            Feedbacks = "";
            DateInserted = "";
        }

        public RateAndReviewModel(int ratingID, int transactionID, int userID, string username, int rateNumber, string feedbacks, string dateInserted)
        {
            RatingID = ratingID;
            TransactionID = transactionID;
            UserID = userID;
            Username = username;
            RateNumber = rateNumber;
            Feedbacks = feedbacks;
            DateInserted = dateInserted;
        }
    }
}