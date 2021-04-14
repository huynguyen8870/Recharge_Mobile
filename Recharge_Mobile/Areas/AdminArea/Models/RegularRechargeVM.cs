using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.RechargeArea.Models
{
    public class RegularRechargeVM
    {
        public RegularRechargeVM()
        {

        }

        public RegularRechargeVM(int rRechargeId, string rRName, int baseTimeMinute, int bonusTimeMinute, int totalTimeMinute,
            decimal price, int durationDay, string description, string status)
        {
            RRechargeId = rRechargeId;
            RRName = rRName;
            BasteTimeMinute = baseTimeMinute;
            BonusTimeMinute = bonusTimeMinute;
            TotalTimeMinute = totalTimeMinute;
            Price = price;
            DurationDay = durationDay;
            Description = description;
            Status = status;
        }

        public int RRechargeId { get; set; }
        [StringLength(100, ErrorMessage = "max length = 100 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter Recharge's name!")]
        public string RRName { get; set; }
        [Range(1, 60000, ErrorMessage = "Base time must be between 1 and 60,000 minutes!")]
        public int BasteTimeMinute { get; set; }
        [Range(0, 60000, ErrorMessage = "Bonus time must be between 0 and 60,000 minutes!")]
        public int BonusTimeMinute { get; set; }
        public int TotalTimeMinute { get; set; }
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