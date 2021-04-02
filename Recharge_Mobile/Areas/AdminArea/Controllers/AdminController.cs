using Recharge_Mobile.Areas.AdminArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.AdminArea.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminArea/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateAdmin()
        {
            AdminVM vm = new AdminVM();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(AdminVM vm)
        {
            if (ModelState.IsValid)
            {
                AdminDAO.CreateAdmin(vm);
                return RedirectToAction("CreateAdmin");
            }
            return View(vm);
        }

        public ActionResult ViewAdminList()
        {
            var listResult = AdminDAO.ListAdmin();
            return View(listResult);
        }

        public ActionResult AdminInfomation(int id)
        {
            var result = AdminDAO.GetAdminById(id);
            return View(result);
        }

        public ActionResult EditAdmin(int id)
        {
            var result = AdminDAO.GetAdminById(id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdmin(AdminVM vm)
        {
            if (ModelState.IsValid)
            {
                AdminDAO.EditAdmin(vm);
                return RedirectToAction("AdminInfomation");
            }
            return View(vm);
        }
    }
}