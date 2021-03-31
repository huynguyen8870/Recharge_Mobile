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

        // POST: User/Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
