using Recharge_Mobile.Areas.User.Models.DAO;
using Recharge_Mobile.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string phonenumber, string password)
        {
            AccountDAO accountDAO = new AccountDAO();
            var account = accountDAO.Login(phonenumber, password);
            if (account == null)
            {
                TempData["loginFail"] = "Wrong Password or Phone Number!";
                return View("Login");
            } else
            {
                UserDAO userDAO = new UserDAO();
                var accountInfo = userDAO.AccountDetail(phonenumber);
                Session["accountInfo"] = accountInfo;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string firstname, string lastname, string email, string phonenumber, string password, string repassword)
        {
            AccountDAO accountDAO = new AccountDAO();
            if (password != repassword)
            {
                TempData["invalid-password"] = "Repassword do not match!";
                return View("Register");
            } else
            {
                accountDAO.Register(firstname, lastname, email, phonenumber, password);
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Logout()
        {
            Session["accountInfo"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}