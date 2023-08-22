using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class NewPasswordModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string HiddenCurrentPassword { get; set; }

        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [DataType(DataType.Password)]
        [Compare("HiddenCurrentPassword", ErrorMessage = "Incorrect Password.")]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Password is too short.")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}