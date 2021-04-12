using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.Recharge.Models
{
    public class SRechargeModelView
    {
        public int SRechargeId { get; set; }
        public string SRName { get; set; }
        public long Duration { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public SRechargeModelView()
        {

        }

        public SRechargeModelView(int sRechargeId, string sRName, long duration, decimal price, string description, string status)
        {
            SRechargeId = sRechargeId;
            SRName = sRName;
            Duration = duration;
            Price = price;
            Description = description;
            Status = status;
        }
    }
}