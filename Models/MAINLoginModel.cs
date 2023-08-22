using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class MAINLoginModel
    {
        [Display(Name = "Email or Phone Number")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}