using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class FeedbackUserVM
    {
        public FeedbackUserVM()
        {
                
        }

        public int FeedbackId { get; set; }
        public int? CustomerId { get; set; }
        public string CusLastName { get; set; }
        public string CusPhone { get; set; }
        public string CusEmail { get; set; }

        public string GuestName { get; set; }
        public string GuestPhone { get; set; }
        public string GuestEmail { get; set; }

        public string Title { get; set; }
        public string Detail { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}