//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomerDataRatingsAndReviewsManagementSystem.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class notificationTable
    {
        public int notificationID { get; set; }
        public string notificationFrom { get; set; }
        public string Username { get; set; }
        public int Userid { get; set; }
        public System.DateTime AddedOn { get; set; }
        public string Message { get; set; }
        public int IsRead { get; set; }
        public string ImgSource { get; set; }
    }
}