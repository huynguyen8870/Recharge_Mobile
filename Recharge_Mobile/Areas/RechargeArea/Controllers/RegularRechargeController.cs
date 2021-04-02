using Recharge_Mobile.Areas.RechargeArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.RechargeArea.Controllers
{
    public class RegularRechargeController : Controller
    {
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
                RegularRechargeDAO.CreateItem(vm);
                return RedirectToAction("CreateRegularRecharge");
            }
            return View(vm);
        }

        public ActionResult ViewRRList()
        {
            var resultList = RegularRechargeDAO.GetList();
            return View(resultList);
        }

        public ActionResult EditRR(int id)
        {
            var result = RegularRechargeDAO.GetItemById(id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRR(RegularRechargeVM vm)
        {
            if (ModelState.IsValid)
            {
                RegularRechargeDAO.EditItem(vm);
                return RedirectToAction("ViewRRList");
            }
            return View(vm);
        }

        public ActionResult ActivateRR(int id)
        {
            RegularRechargeDAO.ActivateItem(id);
            return RedirectToAction("ViewRRList");
        }

        public ActionResult DeactivateRR(int id)
        {
            RegularRechargeDAO.DeactivateItem(id);
            return RedirectToAction("ViewRRList");
        }
    }
}