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

        public ActionResult RRreview(int id)
        {
            RechargeDAO rechargeDAO = new RechargeDAO();
            var rechargeInfo = rechargeDAO.RRechargeById(id);
            Session["RRInfo"] = rechargeInfo;
            return RedirectToAction("CheckoutPayment");
        }

        public ActionResult SRreview(int id)
        {
            RechargeDAO rechargeDAO = new RechargeDAO();
            var rechargeInfo = rechargeDAO.SRechargeById(id);
            Session["SRInfo"] = rechargeInfo;
            return RedirectToAction("CheckoutPayment");
        }

        public ActionResult CheckoutPayment()
        {
            if(Session["RRInfo"] != null)
            {
                var rechargeInfor = Session["RRInfo"] as RRechargeModelView;
                ViewBag.PackerName = rechargeInfor.RRName;
                ViewBag.Price = rechargeInfor.Price;
                Session["RRInfo"] = null;
            } else
            {
                var rechargeInfor = Session["SRInfo"] as SRechargeModelView;
                ViewBag.PackerName = rechargeInfor.SRName;
                ViewBag.Price = rechargeInfor.Price;
                Session["SRInfo"] = null;
            }
            return View();
        }
        [HttpPost]
        public ActionResult CheckoutPayment(string number, string name, string expire, string cvc)
        {

            TransactionModelView transaction = new TransactionModelView()
            {
                PhoneNumber = Session["phonenumber"].ToString(),
                PaymentMethod = "Visa",
                RRechargeId = 3,
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
            int id = transaction.RRechargeId ?? 0;
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

        public ActionResult CheckoutTransaction(int id)
        {
            RechargeDAO rechargeDAO = new RechargeDAO();
            TransactionModelView transaction = rechargeDAO.TransactionById(id);
            Session["transactionInfo"] = transaction;
            return View(transaction);
        }
        [HttpPost]
        public ActionResult CheckoutTransaction(string number, string name, string expire, string cvc)
        {
            return RedirectToAction("TransactionReview");
        }

        public ActionResult TransactionReview()
        {
            TransactionModelView transaction = Session["transactionInfo"] as TransactionModelView;
            return View(transaction);
        }

        public ActionResult TransactionComplete()
        {
            TransactionModelView transaction = Session["transactionInfo"] as TransactionModelView;
            RechargeDAO rechargeDAO = new RechargeDAO();
            rechargeDAO.PaidTransaction(transaction.TransactionId);
            return RedirectToAction("AccountDebit", "User", new { Area = "User" });
        }

    }
}