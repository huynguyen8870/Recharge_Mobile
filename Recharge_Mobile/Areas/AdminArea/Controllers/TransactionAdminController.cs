using Recharge_Mobile.Areas.AdminArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.AdminArea.Controllers
{
    public class TransactionAdminController : Controller
    {
        TransactionAdminDAO transactionAdminDAO;
        // GET: AdminArea/TransactionAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransactionList()
        {
            transactionAdminDAO = new TransactionAdminDAO();
            var resultList = transactionAdminDAO.TransactionList();
            return View(resultList);
        }

        public ActionResult TransactionListByDate()
        {
            transactionAdminDAO = new TransactionAdminDAO();
            var result = transactionAdminDAO.TransactionListByDate(DateTime.Now);
            var resultList = result.Item1;
            ViewBag.totalAmount = result.Item2;
            return View(resultList);
        }
        [HttpPost]
        public ActionResult TransactionListByDate(DateTime checkdate)
        {
            transactionAdminDAO = new TransactionAdminDAO();
            var result = transactionAdminDAO.TransactionListByDate(checkdate);
            var resultList = result.Item1;
            ViewBag.totalAmount = result.Item2;
            ViewBag.daycheck = checkdate;
            return View(resultList);
        }

        public ActionResult AdvanceSearch()
        {
            AdvanceSearchVM vm = new AdvanceSearchVM();
            vm.DateFrom = DateTime.Now.Date;
            vm.DateTo = DateTime.Now.Date;
            vm.RechargeType = "None";
            vm.PaymentMethod = "None";
            vm.Status = "None";
            return View(vm);
        }
        [HttpPost]
        public ActionResult AdvanceSearch(AdvanceSearchVM vm)
        {
            if (ModelState.IsValid)
            {
                if(vm.DateFrom > vm.DateTo)
                {
                    ModelState.AddModelError("InvalidDay", "Date-to must greater or equal Date-from!");
                    return View(vm);
                }
                transactionAdminDAO = new TransactionAdminDAO();
                ViewBag.SearchResultList = transactionAdminDAO.AdvanceSearch(vm).Item1;
                ViewBag.TotalAmount = transactionAdminDAO.AdvanceSearch(vm).Item2;
                return View(vm);
            }
            return View(vm);
        }
    }
}