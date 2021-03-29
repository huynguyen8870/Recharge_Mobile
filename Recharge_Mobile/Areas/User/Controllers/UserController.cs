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
            return View();
        }

        // GET: User/Login/Details/5
        public ActionResult AccountProfile()
        {
            return View();
        }

        // GET: User/Login/Create
        public ActionResult Create()
        {
            return View();
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
