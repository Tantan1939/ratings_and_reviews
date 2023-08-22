using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.ViewModel
{
    public class CreateRoomModel
    {
        public int roomId { get; set; }

        [Required]
        public HttpPostedFileBase roomImage { get; set; }

        [Required]
        public string roomType { get; set; }

        [Required]
        public decimal roomPrice { get; set; }

    }

}