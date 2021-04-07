using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdvanceSearchVM
    {
        public AdvanceSearchVM()
        {

        }

        public AdvanceSearchVM(DateTime dateFrom, DateTime dateTo, string phoneNumber,
            string rechargeType, string paymentMethod, string status)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            PhoneNumber = phoneNumber;
            RechargeType = rechargeType;
            PaymentMethod = paymentMethod;
            Status = status;
        }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        [RegularExpression("([0-9]{3,10})", ErrorMessage = "Must have 3-10 numbers for searching!")]
        public string PhoneNumber { get; set; }
        public string RechargeType { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}