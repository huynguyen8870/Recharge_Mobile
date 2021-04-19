using Recharge_Mobile.Areas.User.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recharge_Mobile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string phonenumber)
        {
            Session["phonenumber"] = phonenumber;
            return RedirectToAction("RechargeDetail", "Recharge", new { Area = "Recharge" });
        }
        
        public ActionResult SiteMap()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Feedback(string GuestName, string GuestEmail, string GuestPhone, string Title, string Detail)
        {
            Models.Views.FeedbackModelView feedbackModelView = new Models.Views.FeedbackModelView()
            {
                GuestName = GuestName,
                GuestEmail = GuestEmail,
                GuestPhone = GuestPhone,
                Title = Title,
                Detail = Detail,
                Role = "Guest",
                Status = "Waiting"
            };
            Models.DAO.FeedbackDAO feedbackDAO = new Models.DAO.FeedbackDAO();
            feedbackDAO.Feedback(feedbackModelView);
            return View("FeedbackSuccess");
        }

        public ActionResult FeedbackSuccess()
        {
            return View();
        }
    }
}