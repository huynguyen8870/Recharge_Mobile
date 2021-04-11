using Recharge_Mobile.Models.Entities;
using Recharge_Mobile.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Models.DAO
{
    public class FeedbackDAO
    {
        static RechargeMobileEntities entities;

        public void Feedback(FeedbackModelView feedbackModelView)
        {
            entities = new RechargeMobileEntities();
            Feedback feedback = new Feedback()
            {
                CustomerId = feedbackModelView.CustomerId,
                GuestName = feedbackModelView.GuestName,
                GuestEmail = feedbackModelView.GuestEmail,
                GuestPhone = feedbackModelView.GuestPhone,
                Title = feedbackModelView.Title,
                Detail = feedbackModelView.Detail,
                Role = feedbackModelView.Role,
                Status = feedbackModelView.Status
            };
            entities.Feedbacks.Add(feedback);
            entities.SaveChanges();
        }
    }
}