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
    public class AdminFeedbackController : Controller
    {
        AdminFeedbackDAO adminFeedbackDAO;
        // GET: AdminArea/AdminFeedback
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeedbackList()
        {
            adminFeedbackDAO = new AdminFeedbackDAO();
            var resultList = adminFeedbackDAO.GetFeedbackList();
            return View(resultList);
        }

        public ActionResult EnableFeedback(int id)
        {
            adminFeedbackDAO = new AdminFeedbackDAO();
            adminFeedbackDAO.EnableFeedback(id);
            return RedirectToAction("FeedbackList");
        }

        public ActionResult DisableFeedback(int id)
        {
            adminFeedbackDAO = new AdminFeedbackDAO();
            adminFeedbackDAO.DisableFeedback(id);
            return RedirectToAction("FeedbackList");
        }
    }
}