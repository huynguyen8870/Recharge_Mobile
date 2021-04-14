using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminFeedbackDAO
    {
        RechargeMobileEntities entities;
        public IList<FeedbackUserVM> GetFeedbackList()
        {
            entities = new RechargeMobileEntities();
            var listFeedback = entities.Feedbacks.ToList();
            var listCustomer = entities.Customers.ToList();
            var listFeebackCustomer = from Feedback in listFeedback
                                      join Customer in listCustomer
                                      on Feedback.CustomerId equals Customer.CustomerId into gj
                                      from sub in gj.DefaultIfEmpty()
                                      select new 
                                      {
                                          FeedbackId = Feedback.FeedbackId,
                                          CustomerId = Feedback.CustomerId,
                                          CusLastName = sub?.LastName,
                                          CusPhone = sub?.PhoneNumber,
                                          CusEmail = sub?.Email,
                                          GuestName = Feedback.GuestName,
                                          GuestPhone = Feedback.GuestPhone,
                                          GuestEmail = Feedback.GuestEmail,
                                          Title = Feedback.Title,
                                          Detail = Feedback.Detail,
                                          Role = Feedback.Role,
                                          Status = Feedback.Status
                                      };
            var list = listFeebackCustomer.Select(d => new FeedbackUserVM() 
            { 
                FeedbackId = d.FeedbackId,
                CustomerId = d.CustomerId,
                CusLastName = d.CusLastName,
                CusPhone = d.CusPhone,
                CusEmail = d.CusEmail,
                GuestName = d.GuestName,
                GuestPhone = d.GuestPhone,
                GuestEmail = d.GuestEmail,
                Title = d.Title,
                Detail = d.Detail,
                Role = d.Role,
                Status = d.Status
            }).ToList();
            return list;
        }

        public void EnableFeedback(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Feedbacks.Where(d => d.FeedbackId == id).FirstOrDefault();
            item.Status = "Enabled";
            entities.SaveChanges();
        }

        public void DisableFeedback(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Feedbacks.Where(d => d.FeedbackId == id).FirstOrDefault();
            item.Status = "Disabled";
            entities.SaveChanges();
        }
    }
}