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
        AdminDAO adminDAO;
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
        public ActionResult CreateAdmin(AdminVM vm, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                bool checkError = false;
                adminDAO = new AdminDAO();
                if (adminDAO.CheckUsernameExist(vm.Username))
                {
                    ModelState.AddModelError("UsernameExist", "This username has already used!");
                    checkError = true;
                }
                if (adminDAO.CheckPhoneExist(vm.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneExist", "This phone number has already used!");
                    checkError = true;
                }
                if (adminDAO.CheckEmailExist(vm.Email))
                {
                    ModelState.AddModelError("MailExist", "This email has already used!");
                    checkError = true;
                }

                if (checkError)
                {
                    vm.Password = "";
                    return View(vm);
                }

                if(vm.Password != ConfirmPassword)
                {
                    ModelState.AddModelError("WrongConfirmPass", "Confirm password doesn't match!");
                    vm.Password = "";
                    return View(vm);
                }
                adminDAO.CreateAdmin(vm);
                return RedirectToAction("CreateAdmin");
            }
            return View(vm);
        }

        public ActionResult ViewAdminList()
        {
            adminDAO = new AdminDAO();
            var listResult = adminDAO.ListAdmin();
            return View(listResult);
        }

        public ActionResult AdminInformation(int id)
        {
            adminDAO = new AdminDAO();
            var result = adminDAO.GetAdminById(id);
            return View(result);
        }

        public ActionResult EditAdmin(int id)
        {
            adminDAO = new AdminDAO();
            var result = adminDAO.GetAdminEditById(id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdmin(AdminEditVM vm)
        {
            if (ModelState.IsValid)
            {
                adminDAO = new AdminDAO();
                bool checkError = false;

                if (adminDAO.CheckPhoneExist(vm.AdminId, vm.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneExist", "This phone number has already used!");
                    checkError = true;
                }
                if (adminDAO.CheckEmailExist(vm.AdminId, vm.Email))
                {
                    ModelState.AddModelError("MailExist", "This email has already used!");
                    checkError = true;
                }

                if (checkError)
                {
                    return View(vm);
                }

                adminDAO.EditAdmin(vm);
                return RedirectToAction("ViewAdminList");
            }
            return RedirectToAction("AdminInfomation");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public JsonResult CheckCurrentPassword(int id, string password)
        {
            adminDAO = new AdminDAO();
            if (adminDAO.CheckCurrentPassword(id, password))
            {
                return Json(1);
            }
            return Json(0);
        }
    }
}