using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class CustomerRechargeModelView
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public long TimeRemain { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime SPTimeRemain { get; set; }
        public decimal DebitAmount { get; set; }
        public DateTime TimeToPay { get; set; }

        public CustomerRechargeModelView()
        {

        }

        public CustomerRechargeModelView(int id, int phoneNumber, long timeRemain, DateTime endTime, DateTime sPTimeRemain, decimal debitAmount, DateTime timeToPay)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            TimeRemain = timeRemain;
            EndTime = endTime;
            SPTimeRemain = sPTimeRemain;
            DebitAmount = debitAmount;
            TimeToPay = timeToPay;
        }
    }
}