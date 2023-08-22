using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class ReturnUserImg
    {
        public Guid ImageID { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string ImagePath { get; set; }
    }
}