using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.RechargeArea.Models
{
    public class SpecialRechargeVM
    {
        public SpecialRechargeVM()
        {

        }

        public SpecialRechargeVM(int sRechargeId, string sRName,
            decimal price, int durationDay, string description, string status)
        {
            SRechargeId = sRechargeId;
            SRName = sRName;
            Price = price;
            DurationDay = durationDay;
            Description = description;
            Status = status;
        }

        public int SRechargeId { get; set; }
        [StringLength(100, ErrorMessage = "max length = 100 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter Recharge's name!")]
        public string SRName { get; set; }
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between 0.01 and 1000.00!")]
        public decimal Price { get; set; }
        [Range(1, 365, ErrorMessage = "Using time must be between 1 and 365 days!")]
        public int DurationDay { get; set; }
        [StringLength(2000, ErrorMessage = "max length = 2000 characters!")]
        public string Description { get; set; }
        [StringLength(50, ErrorMessage = "max length = 50 characters!")]
        public string Status { get; set; }
    }
}