using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class AccountRegistrationModel
    {
        [Display(Name = "Email or Phone Number")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Username should minimum 8 characters and a maximum of 20 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers are allowed.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Password is too short.")]
        public string AccountPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [DataType(DataType.Password)]
        [Compare("AccountPassword", ErrorMessage = "Incorrect Password.")]
        public string ConfirmPassword { get; set; }

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
        [Required(ErrorMessage = "You can't leave this empty.")]
        public string Region { get; set; }

        [Display(Name = "ZIP Code")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers is allowed.")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        public int ZIPcode { get; set; }

        [Display(Name = "SIM Number")]
        [Required(ErrorMessage = "You can't leave this empty.")]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number.")]
        public string SimNumber { get; set; }

        public HttpPostedFileBase ImagePath { get; set; }
    }
}