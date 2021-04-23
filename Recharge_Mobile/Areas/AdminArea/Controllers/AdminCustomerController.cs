using CDStore.Models.Filters;
using Recharge_Mobile.Areas.AdminArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Areas.AdminArea.Controllers
{
    [CAuthFilter(RoleName = "Admin")]
    public class AdminCustomerController : Controller
    {
        AdminCustomerDAO adminCustomerDAO;
        // GET: AdminArea/AdminCustomer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerList()
        {
            adminCustomerDAO = new AdminCustomerDAO();
            var resultList = adminCustomerDAO.GetCustomerList();
            return View(resultList);
        }
        public ActionResult CustomerListByPhone(string searchNumber)
        {
            adminCustomerDAO = new AdminCustomerDAO();
            var resultList = adminCustomerDAO.GetCustomerListByPhone(searchNumber);
            return View("CustomerList", resultList);
        }

        public ActionResult CustomerDetails(int id)
        {
            adminCustomerDAO = new AdminCustomerDAO();
            var results = adminCustomerDAO.GetCustomerDetails(id);
            ViewBag.CustomerTransactions = results.Item2;
            return View(results.Item1);
        }

        public ActionResult ActivateCustomer(int id)
        {
            adminCustomerDAO = new AdminCustomerDAO();
            adminCustomerDAO.ActivateCustomer(id);
            return RedirectToAction("CustomerList");
        }

        public ActionResult DeactivateCustomer(int id)
        {
            adminCustomerDAO = new AdminCustomerDAO();
            adminCustomerDAO.DeactivateCustomer(id);
            return RedirectToAction("CustomerList");
        }
    }
}