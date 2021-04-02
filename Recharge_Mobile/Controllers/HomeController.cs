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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
            try
            {
                if (ModelState.IsValid)
                {
                    Models.Views.FeedbackModelView feedbackModelView = new Models.Views.FeedbackModelView();
                    feedbackModelView.GuestName = GuestName;
                    feedbackModelView.GuestEmail = GuestEmail;
                    feedbackModelView.GuestPhone = GuestPhone;
                    feedbackModelView.Title = Title;
                    feedbackModelView.Detail = Detail;

                    return View("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult FeedbackSuccess()
        {
            return View();
        }
    }
}