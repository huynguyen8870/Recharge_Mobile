using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.Recharge.Controllers
{
    public class RechargeController : Controller
    {
      
        // GET: Recharge/ReCharge
        public ActionResult RechageDetail()
        {
            List<Models.RRechargeModelView> rRechargeModelViews = new List<Models.RRechargeModelView>();
            Models.RRechargeModelView rRechargeModelView = new Models.RRechargeModelView()
            {
                RRechargeId = 100,
                RRName = "ABC10",
                BaseTime = 3600,
                BonusTime = 0,
                TotalTime = 3600,
                Price = 14,
                Duration = 10,
                Description = "",
                Status = "Active"
            };
            Models.RRechargeModelView rRechargeModelView1 = new Models.RRechargeModelView()
            {
                RRechargeId = 101,
                RRName = "DEF10",
                BaseTime = 12800,
                BonusTime = 0,
                TotalTime = 12800,
                Price = 20,
                Duration = 30,
                Description = "",
                Status = "Active"
            };
            rRechargeModelViews.Add(rRechargeModelView1);
            rRechargeModelViews.Add(rRechargeModelView);
            return View(rRechargeModelViews);
        }

        public ActionResult CheckoutPayment(int id)
        {
            int RechargeId = id;
            Models.RRechargeModelView rRechargeModelView = new Models.RRechargeModelView()
            {
                RRechargeId = 100,
                RRName = "ABC10",
                BaseTime = 3600,
                BonusTime = 0,
                TotalTime = 3600,
                Price = 14,
                Duration = 10,
                Description = "",
                Status = "Active"
            };
            return View(rRechargeModelView);
        }

        public ActionResult CheckoutReview()
        {
            return View();
        }

        public ActionResult CheckoutComplete()
        {
            return View();
        }
    }
}