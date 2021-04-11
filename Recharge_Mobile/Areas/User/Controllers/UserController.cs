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
        public ActionResult AccountDetail()
        {
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            var accountDetail = userDAO.AccountDetail("0123456789");
            return View(accountDetail);
        }

        // GET: User/Login/Details/5
        public ActionResult AccountSetting()
        {
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            var accountInfor = userDAO.AccountSetting(1);
            return View(accountInfor);
        }
        [HttpPost]
        public ActionResult AccountSetting(string firstname, string lastname, string email, string address)
        {
            AccountModelView accountModelView = new AccountModelView()
            {
                CustomerId = 1,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Address = address
            };
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            userDAO.AccountChange(accountModelView);
            return View();
        }


        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldpassword, string newpassword, string repassword)
        {
            //Check password
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            var AccountPass = userDAO.AccountPassword("0123456789");
            if(oldpassword != AccountPass)
            {
                TempData["oldpassword"] = "Wrong Password!";
                return View();
            } else
            {
                //Check confirm password
                if (newpassword != repassword)
                {
                    TempData["repassword"] = "Confirm password have to same as above!";
                    return View();
                } else
                {
                    TempData["success"] = "Your password had been change!";
                    userDAO.PasswordChange("0123456789", newpassword);
                    return View();
                }
            }
        }

        public ActionResult TransactionList()
        {
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            var TransList = userDAO.TransactionList("0123456789");
            return View(TransList);
        }


        public ActionResult AccountDebit()
        {
            Models.DAO.UserDAO userDAO = new Models.DAO.UserDAO();
            var debitTransactions = userDAO.DebitTransactionList("0123456789");
            return View(debitTransactions);
        }

    }
}
