using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.Recharge.Models
{
    public class RRechargeModelView
    {
        public int RRechargeId { get; set; }
        public string RRName { get; set; }
        public long BaseTime { get; set; }
        public long BonusTime { get; set; }
        public long TotalTime { get; set; }
        public decimal Price { get; set; }
        public long Duration { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public RRechargeModelView()
        {

        }

        public RRechargeModelView(int rRechargeId, string rRName, long baseTime, long bonusTime, long totalTime, decimal price, long duration, string description, string status)
        {
            RRechargeId = rRechargeId;
            RRName = rRName;
            BaseTime = baseTime;
            BonusTime = bonusTime;
            TotalTime = totalTime;
            Price = price;
            Duration = duration;
            Description = description;
            Status = status;
        }
    }
}