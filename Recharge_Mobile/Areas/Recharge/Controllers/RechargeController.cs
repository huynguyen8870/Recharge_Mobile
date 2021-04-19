using Recharge_Mobile.Areas.Recharge.Models;
using Recharge_Mobile.Models.DAO;
using Recharge_Mobile.Areas.User.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recharge_Mobile.Areas.Recharge.Models.DAO;
using Recharge_Mobile.Areas.User.Models.DAO;

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

        public ActionResult CheckoutPayment(int id, string status)
        {
            RechargeDAO rechargeDAO = new RechargeDAO();

            if (status == "regular")
            {
                var rechargeInfor = rechargeDAO.RRechargeById(id);
                ViewBag.PackerName = rechargeInfor.RRName;
                ViewBag.Price = rechargeInfor.Price;
                TempData["PackageName"] = rechargeInfor.RRName;
                TempData["Price"] = rechargeInfor.Price;
            } else
            {
                var rechargeInfor = rechargeDAO.SRechargeById(id);
                ViewBag.PackerName = rechargeInfor.SRName;
                ViewBag.Price = rechargeInfor.Price;
                TempData["PackageName"] = rechargeInfor.SRName;
                TempData["Price"] = rechargeInfor.Price;
            }

            TempData["rechargeId"] = id;
            TempData["rechargeType"] = status;
            
            return View();
        }
        [HttpPost]
        public ActionResult CheckoutPayment(string number, string name, string expire, string cvc)
        {
            string type = TempData["rechargeType"].ToString();
            int rechargeId = Convert.ToInt32(TempData["rechargeId"].ToString());
            decimal amount = decimal.Parse(TempData["Price"].ToString());

            if (type == "regular")
            {
                TransactionModelView transaction = new TransactionModelView()
                {
                    PhoneNumber = Session["phonenumber"].ToString(),
                    PaymentMethod = "Visa",
                    RRechargeId = rechargeId,
                    SRechargeId = -1,
                    DateTime = DateTime.Now,
                    Status = "Success",
                    Amount = amount
                };
                Session["transactionInfo"] = transaction;
            } else
            {
                TransactionModelView transaction = new TransactionModelView()
                {
                    PhoneNumber = Session["phonenumber"].ToString(),
                    PaymentMethod = "Visa",
                    RRechargeId = -1,
                    SRechargeId = rechargeId,
                    DateTime = DateTime.Now,
                    Status = "Success",
                    Amount = amount
                };
                Session["transactionInfo"] = transaction;
            }
            
            Session["phonenumber"] = null;
            Session["rechargeInfo"] = null;
            TempData["paymentMethod"] = "Credit Card";
            return RedirectToAction("CheckoutReview");
        }

        public ActionResult CheckoutDebit()
        {
            string type = TempData["rechargeType"].ToString();
            int rechargeId = Convert.ToInt32(TempData["rechargeId"].ToString());
            decimal amount = decimal.Parse(TempData["Price"].ToString());

            if (type.Equals("regular"))
            {
                TransactionModelView transaction = new TransactionModelView()
                {
                    PhoneNumber = ((CustomerRechargeModelView)Session["accountInfo"]).PhoneNumber,
                    PaymentMethod = "Debit",
                    RRechargeId = rechargeId,
                    SRechargeId = -1,
                    DateTime = DateTime.Now,
                    Status = "Unpaid",
                    Amount = amount
                };

                Session["transactionInfo"] = transaction;
            }
            else
            {
                TransactionModelView transaction = new TransactionModelView()
                {
                    PhoneNumber = ((CustomerRechargeModelView)Session["accountInfo"]).PhoneNumber,
                    PaymentMethod = "Debit",
                    RRechargeId = -1,
                    SRechargeId = rechargeId,
                    DateTime = DateTime.Now,
                    Status = "Unpaid",
                    Amount = amount
                };
                Session["transactionInfo"] = transaction;
            }

            Session["rechargeInfo"] = null;
            TempData["paymentMethod"] = "Debit";
            return RedirectToAction("CheckoutReview");
        }

        public ActionResult CheckoutPaypal()
        {
            string type = TempData["rechargeType"].ToString();
            int rechargeId = Convert.ToInt32(TempData["rechargeId"].ToString());
            decimal amount = decimal.Parse(TempData["Price"].ToString());
            var accountNumber = ((CustomerRechargeModelView)Session["accountInfo"]).PhoneNumber ?? null;
            string phonenumber;
            if(accountNumber == null)
            {
                phonenumber = Session["phonenumber"].ToString();

            } else
            {
                phonenumber = accountNumber ;
            }

            if (type.Equals("regular"))
            {
                TransactionModelView transaction = new TransactionModelView()
                {
                    PhoneNumber = phonenumber,
                    PaymentMethod = "Paypal",
                    RRechargeId = rechargeId,
                    SRechargeId = -1,
                    DateTime = DateTime.Now,
                    Status = "Success",
                    Amount = amount
                };

                Session["transactionInfo"] = transaction;
            }
            else
            {
                TransactionModelView transaction = new TransactionModelView()
                {
                    PhoneNumber = phonenumber,
                    PaymentMethod = "Paypal",
                    RRechargeId = -1,
                    SRechargeId = rechargeId,
                    DateTime = DateTime.Now,
                    Status = "Success",
                    Amount = amount
                };
                Session["transactionInfo"] = transaction;
            }

            Session["rechargeInfo"] = null;
            TempData["paymentMethod"] = "Paypal";
            return RedirectToAction("CheckoutReview");
        }


        public ActionResult CheckoutReview()
        {
            var transaction = Session["transactionInfo"] as TransactionModelView;

            RechargeDAO rechargeDAO = new RechargeDAO();
            if (transaction.RRechargeId > 0)
            {
                var rechargeInfo = rechargeDAO.RRechargeById(transaction.RRechargeId ?? 0);
                ViewBag.packageName = rechargeInfo.RRName;
                ViewBag.packageTime = rechargeInfo.BaseTime;
                ViewBag.packageDuration = rechargeInfo.Duration;
                ViewBag.packagePrice = rechargeInfo.Price;
                
            } else
            {
                var rechargeInfo = rechargeDAO.SRechargeById(transaction.SRechargeId ?? 0);
                ViewBag.packageName = rechargeInfo.SRName;
                ViewBag.packageTime = 0;
                ViewBag.packageDuration = rechargeInfo.Duration;
                ViewBag.packagePrice = rechargeInfo.Price;
            }
            return View();
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