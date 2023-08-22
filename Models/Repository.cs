using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerDataRatingsAndReviewsManagementSystem.DAL;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class Repository
    {
        public UserAccTBL GetUserDetails(string UserName, string Password)
        {
            UserAccTBL user = new UserAccTBL();
            using (UserDBContext db = new UserDBContext())
            {
                user = db.UserAccTBLs.FirstOrDefault(u => u.Username.ToLower() == UserName.ToLower() &&
                                      u.AccountPassword == Password);
            }
            return user;
        }
    }
}