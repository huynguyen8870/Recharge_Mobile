using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class TransactionModelView
    {
        public int TransactionId { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int? RRechargeId { get; set; }
        public int? SRechargeId { get; set; }
        public string RechargeName { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string PaypalID { get; set; }

        public TransactionModelView()
        {

        }

        public TransactionModelView(int transactionId, string phoneNumber, string paymentMethod, int? rRechargeId, int? sRechargeId, string rechargeName, decimal price, DateTime dateTime, string status, decimal amount, string paypalID)
        {
            TransactionId = transactionId;
            PhoneNumber = phoneNumber;
            PaymentMethod = paymentMethod;
            RRechargeId = rRechargeId;
            SRechargeId = sRechargeId;
            RechargeName = rechargeName;
            Price = price;
            DateTime = dateTime;
            Status = status;
            Amount = amount;
            PaypalID = paypalID;
        }
    }
}