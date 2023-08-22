using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerDataRatingsAndReviewsManagementSystem.Models
{
    public class PathAndRoom
    {
        public int Tid { get; set; }
        public string imgPth { get; set; }
        public string rmTyp { get; set; }

        public PathAndRoom()
        {
            Tid = 0;
            imgPth = "";
            rmTyp = "";
        }

        public PathAndRoom(int tid, string imgPth, string rmTyp)
        {
            Tid = tid;
            this.imgPth = imgPth;
            this.rmTyp = rmTyp;
        }
    }
}