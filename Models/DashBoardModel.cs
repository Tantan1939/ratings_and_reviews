using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class DashBoardModel
    {
        public string clientName { get; set; }

        public string clientAge { get; set; }
    }

    public class UserList
    {
        public List<DashBoardModel> clients { get; set; }
    }
}