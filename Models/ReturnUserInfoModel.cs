using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class ReturnUserInfoModel
    {
        public int UserID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Invalid Entry! Try again.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets is allowed.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Invalid Entry! Try again.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets is allowed.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Home Address")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        public string HomeAddress { get; set; }

        [Display(Name = "Age")]
        [Range(18, 99, ErrorMessage = "Invalid Age")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers is allowed.")]
        public int Age { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        public string City { get; set; }

        [Display(Name = "Region")]
        [Required(ErrorMessage = "You can't leave this empty region.")]
        public string Region { get; set; }

        [Display(Name = "ZIP Code")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers is allowed.")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        public int ZIPcode { get; set; }

        [Display(Name = "SIM Number")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        public string SimNumber { get; set; }

        public string ImgPath { get; set; }

        public HttpPostedFileBase HoldNewImg { get; set; }
    }
}