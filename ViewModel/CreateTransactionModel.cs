using CustomerDataRatingsAndReviewsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerDataRatingsAndReviewsManagementSystem.ViewModel
{
    public class CreateTransactionModel
    {
        public int TransactionID { get; set; }

        [Required]
        public int UserID { get; set; }

        public string Username { get; set; }

        [Required]
        public HttpPostedFileBase RoomImage { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public int NumberOfNight { get; set; }

        [Required]
        public int NumberOfOccupants { get; set; }

        [Required]
        public string RoomMasterID { get; set; }

        [Required]
        public decimal RoomPrice { get; set; }

        [Required]
        public string Status { get; set; }

        public string IsRated { get; set; }
    }
}