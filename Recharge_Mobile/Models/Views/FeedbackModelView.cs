using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Models.Views
{
    public class FeedbackModelView
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestPhone { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        public FeedbackModelView()
        {

        }

        public FeedbackModelView(int feedbackId, int customerId, string guestName, string guestEmail, string guestPhone, string title, string detail, string role, string status)
        {
            FeedbackId = feedbackId;
            CustomerId = customerId;
            GuestName = guestName;
            GuestEmail = guestEmail;
            GuestPhone = guestPhone;
            Title = title;
            Detail = detail;
            Role = role;
            Status = status;
        }
    }
}