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
        public int RRecharge { get; set; }
        public int SRecharge { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        public TransactionModelView()
        {

        }

        public TransactionModelView(int transactionId, string phoneNumber, string paymentMethod, int rRecharge, int sRecharge, DateTime dateTime, string status)
        {
            TransactionId = transactionId;
            PhoneNumber = phoneNumber;
            PaymentMethod = paymentMethod;
            RRecharge = rRecharge;
            SRecharge = sRecharge;
            DateTime = dateTime;
            Status = status;
        }
    }
}