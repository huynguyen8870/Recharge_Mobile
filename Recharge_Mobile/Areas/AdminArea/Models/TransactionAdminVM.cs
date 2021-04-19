using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class TransactionAdminVM
    {
        public TransactionAdminVM()
        {

        }

        public TransactionAdminVM(int transactionId, string phoneNumber, string paymentMethod, int rRechargeId,
            int sRechargeId, string rechargeType, string rechargeName, decimal amount, DateTime dateTime, string postPayment, string status)
        {
            TransactionId = transactionId;
            PhoneNumber = phoneNumber;
            PaymentMethod = paymentMethod;
            RRechargeId = rRechargeId;
            SRechargeId = sRechargeId;
            RechargeType = rechargeType;
            RechargeName = rechargeName;
            Amount = amount;
            DateTime = dateTime;
            PostPayment = postPayment;
            Status = status;
        }

        public int TransactionId { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int? RRechargeId { get; set; }
        public int? SRechargeId { get; set; }
        public string RechargeType { get; set; }
        public string RechargeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string PostPayment { get; set; }
        public string Status { get; set; }
    }
}