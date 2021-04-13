using Recharge_Mobile.Areas.User.Models.DAO;
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
            var account = Session["accountInfo"] as CustomerRechargeModelView;
            UserDAO userDAO = new UserDAO();
            var accountDetail = userDAO.AccountDetail(account.PhoneNumber);
            return View(accountDetail);
        }

        // GET: User/Login/Details/5
        public ActionResult AccountSetting()
        {
            var account = Session["accountInfo"] as CustomerRechargeModelView;
            UserDAO userDAO = new UserDAO();
            var accountInfor = userDAO.AccountSetting(account.PhoneNumber);
            return View(accountInfor);
        }
        [HttpPost]
        public ActionResult AccountSetting(string firstname, string lastname, string email, string address)
        {
            var account = Session["accountInfo"] as CustomerRechargeModelView;
            AccountModelView accountModelView = new AccountModelView()
            {
                PhoneNumber = account.PhoneNumber,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Address = address
            };
            UserDAO userDAO = new UserDAO();
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
            var account = Session["accountInfo"] as CustomerRechargeModelView;
            UserDAO userDAO = new UserDAO();
            var AccountPass = userDAO.AccountPassword(account.PhoneNumber);
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
                    userDAO.PasswordChange(account.PhoneNumber, newpassword);
                    return View();
                }
            }
        }

        public ActionResult TransactionList()
        {
            var account = Session["accountInfo"] as CustomerRechargeModelView;
            UserDAO userDAO = new UserDAO();
            var TransList = userDAO.TransactionList(account.PhoneNumber);
            return View(TransList);
        }


        public ActionResult AccountDebit()
        {
            var account = Session["accountInfo"] as CustomerRechargeModelView;
            UserDAO userDAO = new UserDAO();
            var debitTransactions = userDAO.DebitTransactionList(account.PhoneNumber);
            return View(debitTransactions);
        }

    }
}
