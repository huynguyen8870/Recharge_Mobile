using CDStore.Models.Filters;
using Recharge_Mobile.Areas.AdminArea.Models;
using Recharge_Mobile.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.AdminArea.Controllers
{
    [CAuthFilter(RoleName = "Admin")]
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
                return RedirectToAction("AdminInformation");
            }
            return View(vm);
        }

        public ActionResult ViewAdminList()
        {
            adminDAO = new AdminDAO();
            var listResult = adminDAO.ListAdmin();
            return View(listResult);
        }

        public ActionResult AdminInformation()
        {
            adminDAO = new AdminDAO();
            var user = Session["currentUser"] as AccountInfoVM;
            int id = user.id;
            var result = adminDAO.GetAdminById(id);
            return View(result);
        }

        public ActionResult EditAdmin()
        {
            adminDAO = new AdminDAO();
            var user = Session["currentUser"] as AccountInfoVM;
            int id = user.id;
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
                var user = Session["currentUser"] as AccountInfoVM;
                int id = user.id;
                bool checkError = false;

                if (adminDAO.CheckPhoneExist(id, vm.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneExist", "This phone number has already used!");
                    checkError = true;
                }
                if (adminDAO.CheckEmailExist(id, vm.Email))
                {
                    ModelState.AddModelError("MailExist", "This email has already used!");
                    checkError = true;
                }

                if (checkError)
                {
                    return View(vm);
                }

                adminDAO.EditAdmin(vm, id);
                return RedirectToAction("AdminInformation");
            }
            return View(vm);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string NewPassword)
        {
            adminDAO = new AdminDAO();
            var user = Session["currentUser"] as AccountInfoVM;
            int id = user.id;
            adminDAO.ChangePassword(id, NewPassword);
            return RedirectToAction("AdminInformation");
        }

        public JsonResult CheckCurrentPassword(string password)
        {
            adminDAO = new AdminDAO();
            var user = Session["currentUser"] as AccountInfoVM;
            int id = 1;
            if (adminDAO.CheckCurrentPassword(id, password))
            {
                return Json(1);
            }
            return Json(0);
        }
    }
}