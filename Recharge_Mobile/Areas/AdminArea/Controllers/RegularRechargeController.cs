using Recharge_Mobile.Areas.RechargeArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.AdminArea.Controllers
{
    public class RegularRechargeController : Controller
    {
        RegularRechargeDAO regularRechargeDAO;
        // GET: RechargeArea/RegularRecharge
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRegularRecharge()
        {
            RegularRechargeVM vm = new RegularRechargeVM();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRegularRecharge(RegularRechargeVM vm)
        {
            if (ModelState.IsValid)
            {
                regularRechargeDAO = new RegularRechargeDAO();
                regularRechargeDAO.CreateItem(vm);
                return RedirectToAction("CreateRegularRecharge");
            }
            return View(vm);
        }

        public ActionResult ViewRRList()
        {
            regularRechargeDAO = new RegularRechargeDAO();
            var resultList = regularRechargeDAO.GetList();
            return View(resultList);
        }

        public ActionResult EditRR(int id)
        {
            regularRechargeDAO = new RegularRechargeDAO();
            var result = regularRechargeDAO.GetItemById(id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRR(RegularRechargeVM vm)
        {
            regularRechargeDAO = new RegularRechargeDAO();
            if (ModelState.IsValid)
            {
                regularRechargeDAO.EditItem(vm);
                return RedirectToAction("ViewRRList");
            }
            return View(vm);
        }

        public ActionResult ActivateRR(int id)
        {
            regularRechargeDAO = new RegularRechargeDAO();
            regularRechargeDAO.ActivateItem(id);
            return RedirectToAction("ViewRRList");
        }

        public ActionResult DeactivateRR(int id)
        {
            regularRechargeDAO = new RegularRechargeDAO();
            regularRechargeDAO.DeactivateItem(id);
            return RedirectToAction("ViewRRList");
        }
    }
}