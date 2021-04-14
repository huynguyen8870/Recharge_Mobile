using Recharge_Mobile.Areas.RechargeArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.AdminArea.Controllers
{
    public class SpecialRechargeController : Controller
    {
        SpecialRechargeDAO specialRechargeDAO;
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
                specialRechargeDAO = new SpecialRechargeDAO();
                specialRechargeDAO.CreateItem(vm);
                return RedirectToAction("CreateSpecialRecharge");
            }
            return View(vm);
        }

        public ActionResult ViewSRList()
        {
            specialRechargeDAO = new SpecialRechargeDAO();
            var resultList = specialRechargeDAO.GetList();
            return View(resultList);
        }

        public ActionResult EditSR(int id)
        {
            specialRechargeDAO = new SpecialRechargeDAO();
            var result = specialRechargeDAO.GetItemById(id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSR(SpecialRechargeVM vm)
        {
            specialRechargeDAO = new SpecialRechargeDAO();
            if (ModelState.IsValid)
            {
                specialRechargeDAO.EditItem(vm);
                return RedirectToAction("ViewRRList");
            }
            return View(vm);
        }

        public ActionResult ActivateSR(int id)
        {
            specialRechargeDAO = new SpecialRechargeDAO();
            specialRechargeDAO.ActivateItem(id);
            return RedirectToAction("ViewRRList");
        }

        public ActionResult DeactivateSR(int id)
        {
            specialRechargeDAO = new SpecialRechargeDAO();
            specialRechargeDAO.DeactivateItem(id);
            return RedirectToAction("ViewRRList");
        }
    }
}