using Recharge_Mobile.Areas.Recharge.Models;
using Recharge_Mobile.Models.DAO;
using Recharge_Mobile.Areas.User.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recharge_Mobile.Areas.Recharge.Models.DAO;

namespace Recharge_Mobile.Areas.Recharge.Controllers
{
    public class RechargeController : Controller
    {
      
        // GET: Recharge/ReCharge
        public ActionResult RechargeDetail()
        {
            RechargeDAO rechargeDAO = new RechargeDAO();
            var RRList = rechargeDAO.RRechargeList();
            var SRList = rechargeDAO.SRechargeList();
            ViewBag.RRlist = RRList;
            ViewBag.SRList = SRList;
            return View();
        }

        public ActionResult CheckoutPayment(int id)
        {
            RechargeDAO rechargeDAO = new RechargeDAO();
            var rechargeInfo = rechargeDAO.RRechargeById(id);
            Session["rechargeInfo"] = rechargeInfo;
            return View(rechargeInfo);
        }
        [HttpPost]
        public ActionResult CheckoutPayment(string number, string name, string expire, string cvc)
        {
            var rechargeInfor = Session["rechargeInfor"] as RRechargeModelView;
            TransactionModelView transaction = new TransactionModelView()
            {
                PhoneNumber = Session["phonenumber"].ToString(),
                PaymentMethod = "Visa",
                RRechargeId = rechargeInfor.RRechargeId,
                SRechargeId = -1,
                DateTime = DateTime.Now,
                Status = "Success"
            };
            Session["transactionInfo"] = transaction;
            Session["phonenumber"] = null;
            Session["rechargeInfo"] = null;
            return RedirectToAction("CheckoutReview");
        }

        public ActionResult CheckoutReview()
        {
            var transaction = Session["transactionInfo"] as TransactionModelView;
            int id = transaction.RRechargeId;
            RechargeDAO rechargeDAO = new RechargeDAO();
            var rechargeInfo = rechargeDAO.RRechargeById(id);
            return View(rechargeInfo);
        }

        public ActionResult CheckoutComplete()
        {
            RechargeDAO rechargeDAO = new RechargeDAO();
            var transaction = Session["transactionInfo"] as TransactionModelView;
            rechargeDAO.CheckoutComplete(transaction);
            Session["transactionInfo"] = null;
            return View();
        }
    }
}