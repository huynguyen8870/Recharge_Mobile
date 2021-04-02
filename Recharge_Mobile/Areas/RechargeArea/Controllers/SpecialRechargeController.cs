using Recharge_Mobile.Areas.RechargeArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.RechargeArea.Controllers
{
    public class SpecialRechargeController : Controller
    {
        // GET: RechargeArea/SpecialRecharge
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateSpecialRecharge()
        {
            SpecialRechargeVM vm = new SpecialRechargeVM();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSpecialRecharge(SpecialRechargeVM vm)
        {
            if (ModelState.IsValid)
            {
                SpecialRechargeDAO.CreateItem(vm);
                return RedirectToAction("CreateSpecialRecharge");
            }
            return View(vm);
        }

        public ActionResult ViewSRList()
        {
            var resultList = SpecialRechargeDAO.GetList();
            return View(resultList);
        }

        public ActionResult EditSR(int id)
        {
            var result = SpecialRechargeDAO.GetItemById(id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSR(SpecialRechargeVM vm)
        {
            if (ModelState.IsValid)
            {
                SpecialRechargeDAO.EditItem(vm);
                return RedirectToAction("ViewRRList");
            }
            return View(vm);
        }

        public ActionResult ActivateSR(int id)
        {
            SpecialRechargeDAO.ActivateItem(id);
            return RedirectToAction("ViewRRList");
        }

        public ActionResult DeactivateSR(int id)
        {
            SpecialRechargeDAO.DeactivateItem(id);
            return RedirectToAction("ViewRRList");
        }
    }
}