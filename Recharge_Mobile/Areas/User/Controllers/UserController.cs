using Recharge_Mobile.Areas.User.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.User.Controllers
{
    public class UserController : Controller
    {
        // GET: User/Login/Details/5
        public ActionResult AccountSetting()
        {
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            var accountInfor = userDAO.AccountSetting(1);
            return View(accountInfor);
        }
        [HttpPost]
        //public ActionResult AccountSetting()
        //{

        //}

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult TransactionList()
        {
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            var TransList = userDAO.TransactionList("0123456789");
            return View(TransList);
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
            List<Models.Views.DebitTransactionModelView> debitTransactions = new List<Models.Views.DebitTransactionModelView>();
            Models.Views.DebitTransactionModelView debitTransaction = new Models.Views.DebitTransactionModelView()
            {
                Id = 12334447,
                TransactionId = 102,
                DateTime = DateTime.UtcNow,
                Status = "Waiting"
            };
            Models.Views.DebitTransactionModelView debitTransaction1 = new Models.Views.DebitTransactionModelView()
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
