using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.ViewModel
{
    public class ItermViewModel
    {
        public Guid ItemId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string ItemCode { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal ItemPrice { get; set; }
        [Required]
        public HttpPostedFileBase ImagePath { get; set; }
    }
}