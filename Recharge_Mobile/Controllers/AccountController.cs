using Recharge_Mobile.Areas.User.Models.DAO;
using Recharge_Mobile.Models.DAO;
using Recharge_Mobile.Models.Views;
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

        //[HttpPost]
        //public ActionResult Login(string phonenumber, string password)
        //{
        //    AccountDAO accountDAO = new AccountDAO();
        //    var account = accountDAO.Login(phonenumber, password);
        //    if (account == null)
        //    {
        //        TempData["loginFail"] = "Wrong Password or Phone Number!";
        //        return View("Login");
        //    }
        //    else
        //    {
        //        UserDAO userDAO = new UserDAO();
        //        var accountInfo = userDAO.AccountDetail(phonenumber);
        //        Session["accountInfo"] = accountInfo;
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
        [HttpPost]
        public ActionResult Login(string phonenumber, string password)
        {
            AccountDAO accountDAO = new AccountDAO();
            var account = accountDAO.Login(phonenumber, password);
            if (account == null)
            {
                TempData["loginFail"] = "Wrong Password or Phone Number!";
                return View();
            }
            else if (account.Status != "Active")
            {
                TempData["loginFail"] = "Your account has been deactived, please contact admin for more information!";
                return View();
            }
            else
            {
                UserDAO userDAO = new UserDAO();
                var accountInfo = userDAO.AccountDetail(phonenumber);
                Session["accountInfo"] = accountInfo;
                //new
                AccountInfoVM currentUser = new AccountInfoVM()
                {
                    id = account.CustomerId,
                    Username = "",
                    PhoneNumber = account.PhoneNumber,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Role = "Customer"
                };
                Session["currentUser"] = currentUser;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(string username, string password)
        {
            AccountDAO accountDAO = new AccountDAO();
            var account = accountDAO.LoginAdmin(username, password);
            if (account == null)
            {
                TempData["loginFail"] = "Wrong Password or Username!";
                return View();
            }
            else if (account.Status != "Active")
            {
                TempData["loginFail"] = "Your account has been deactived!";
                return View();
            }
            else
            {
                AccountInfoVM currentUser = new AccountInfoVM()
                {
                    id = account.AdminId,
                    Username = account.Username,
                    PhoneNumber = account.PhoneNumber,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Role = "Admin"
                };
                Session["currentUser"] = currentUser;
                return RedirectToAction("AdminInformation", "Admin", new { Area = "AdminArea" });
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
                accountDAO.CustomerTransactionOnRegister(phonenumber);
                accountDAO.Register(firstname, lastname, email, phonenumber, password);
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Logout()
        {
            Session["accountInfo"] = null;
            Session.Remove("currentUser");
            return RedirectToAction("Index", "Home");
        }
    }
}