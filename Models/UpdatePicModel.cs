using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class UpdatePicModel
    {
        public int UserID { get; set; }

        public HttpPostedFileBase ImgPath { get; set; }
    }
}