using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class CustomerRechargeModelView
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public long TimeRemain { get; set; }
        public DateTime RegularTime { get; set; }
        public DateTime SpecialTime { get; set; }
        public decimal DebitAmount { get; set; }
        public DateTime TimeToPay { get; set; }

        public CustomerRechargeModelView()
        {

        }

        public CustomerRechargeModelView(int id, string phoneNumber, long timeRemain, DateTime regularTime, DateTime specialTime, decimal debitAmount, DateTime timeToPay)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            TimeRemain = timeRemain;
            RegularTime = regularTime;
            SpecialTime = specialTime;
            DebitAmount = debitAmount;
            TimeToPay = timeToPay;
        }
    }
}