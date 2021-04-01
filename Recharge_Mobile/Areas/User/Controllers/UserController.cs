using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.User.Controllers
{
    public class UserController : Controller
    {
        // GET: User/Login
        public ActionResult TransactionList()
        {
            List<Models.Views.TransactionModelView> transactionModelView = new List<Models.Views.TransactionModelView>();
            Models.Views.TransactionModelView transactionModelView1 = new Models.Views.TransactionModelView()
            {
                TransactionId = 1,
                PhoneNumber = 0123456789,
                TypeRecharge = "RRecharge",
                RRecharge = 10,
                SRecharge = 0,
                DateTime = DateTime.Now,
                Status = "Success"
            };
            Models.Views.TransactionModelView transactionModelView2 = new Models.Views.TransactionModelView()
            {
                TransactionId =2,
                PhoneNumber = 0123456789,
                TypeRecharge = "RRecharge",
                RRecharge = 10,
                SRecharge = 0,
                DateTime = DateTime.Now,
                Status = "Cancle"
            };
            transactionModelView.Add(transactionModelView1);
            transactionModelView.Add(transactionModelView2);
            return View(transactionModelView);
        }

        // GET: User/Login/Details/5
        public ActionResult AccountSetting()
        {
            Models.Views.AccountModelView accountModelView = new Models.Views.AccountModelView()
            {
                CustomerId = 1,
                PhoneNumber = 0123456789,
                Password = "abcxyz123",
                FirstName = "Huy",
                LastName = "Nguyen",
                Email = "abc@gmail.com",
                Address = "abc abc xyz",
                Status = "Active"
            };
            return View(accountModelView);
        }

        // GET: User/Login/Create
        public ActionResult AccountDetail()
        {
            Models.Views.CustomerRechargeModelView customerRechargeModelView = new Models.Views.CustomerRechargeModelView()
            {
                Id = 100,
                PhoneNumber = 0123456789,
                TimeRemain = 12800,
                EndTime = DateTime.Now,
                SPTimeRemain = DateTime.Now,
                DebitAmount = 1000,
                TimeToPay = DateTime.Now
            };
            return View(customerRechargeModelView);
        }

        public ActionResult AccountDebit()
        {
            List<Models.Views.DebitTransaction> debitTransactions = new List<Models.Views.DebitTransaction>();
            Models.Views.DebitTransaction debitTransaction = new Models.Views.DebitTransaction()
            {
                Id = 12334447,
                TransactionId = 102,
                DateTime = DateTime.UtcNow,
                Status = "Waiting"
            };
            Models.Views.DebitTransaction debitTransaction1 = new Models.Views.DebitTransaction()
            {
                Id = 12334447,
                TransactionId = 103,
                DateTime = DateTime.UtcNow,
                Status = "Waiting"
            };
            debitTransactions.Add(debitTransaction);
            debitTransactions.Add(debitTransaction1);
            return View(debitTransactions);
        }
    }
}
