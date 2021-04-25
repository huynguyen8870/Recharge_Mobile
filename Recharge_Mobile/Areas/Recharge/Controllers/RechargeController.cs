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
using System.Data;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;

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
            string paypalID = TempData["paymentID"].ToString();
            string phonenumber;

            if( Session["accountInfo"] == null)
            {
                phonenumber = Session["phonenumber"].ToString();
            } else
            {
                phonenumber = ((CustomerRechargeModelView)Session["accountInfo"]).PhoneNumber;
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
                    Amount = amount,
                    PaypalID = paypalID
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
                    Amount = amount,
                    PaypalID = paypalID
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

        public void GenerateInvoicePDF()
        {
            //Dummy data for Invoice (Bill).
            string companyName = "RechargeX";
            int orderNo = 2303;
            string packagename;
            //Get Transaction Information
            TransactionModelView transaction = Session["transactionInfo"] as TransactionModelView;
            RechargeDAO rechargeDAO = new RechargeDAO();
            if (transaction.RRechargeId >0)
            {
                var rechargeInfor = rechargeDAO.RRechargeById(transaction.RRechargeId ?? 0);
                packagename = rechargeInfor.RRName;
            } else
            {
                var rechargeInfor = rechargeDAO.SRechargeById(transaction.SRechargeId ?? 0);
                packagename = rechargeInfor.SRName;

            }
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Invoice</b></td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Phone number: </b>");
                    sb.Append(transaction.PhoneNumber);
                    sb.Append("</td><td align = 'right'><b>Date: </b>");
                    sb.Append(DateTime.Now);
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td><b>Company Name: </b>");
                    sb.Append(companyName);
                    sb.Append("</td><td align = 'right'><b>Payment Method: </b>");
                    sb.Append(transaction.PaymentMethod);
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    //Generate Invoice (Bill) Items Grid.
                    sb.Append("<table border = '1'>");
                    sb.Append("<tr><td><b>Recharge Name</b></td>");
                    sb.Append("<td><b>Quantity</b></td>");
                    sb.Append("<td><b>Price</b></td></tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append(packagename);
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append(1);
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append(transaction.Amount);
                    sb.Append("</td>");
                    sb.Append("<tr><td align = 'right' colspan = '2'> Total </td> ");
                    sb.Append("<td align = 'right'>");
                    sb.Append(transaction.Amount);
                    sb.Append("</td>");
                    sb.Append("</tr></table>");

                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + orderNo + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
    }
}