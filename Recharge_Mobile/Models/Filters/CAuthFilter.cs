using Recharge_Mobile.Models.Entities;
using Recharge_Mobile.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace CDStore.Models.Filters
{
    public class CAuthFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            AccountInfoVM user = filterContext.HttpContext.Session["currentUser"] as AccountInfoVM;
            if (user == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //Redirecting the user to the Login View of Account Controller
                filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Home" },
                    { "action", "Login" },
                    { "area", ""}
               });
                //If you want to redirect to some error view, use below code
                //filterContext.Result = new ViewResult()
                //{
                //    ViewName = "Login"
                //};
                
            }
        }

        public string RoleName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AccountInfoVM user = filterContext.HttpContext.Session["currentUser"] as AccountInfoVM;
            string userRole = user.Role;

            if (userRole == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "Logout" },
                        { "area", ""}
                    });
            }

            if (userRole == RoleName)
            {
                return;
            }
            else
            {
                if (userRole == "Admin")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            { "controller", "Admin" },
                            { "action", "Index" },
                            { "area", ""}
                        });
                    return;
                }
                if (userRole == "Customer")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            { "controller", "Home" },
                            { "action", "Index" },
                            { "area", ""}
                        });
                    return;
                }
            }

            //filterContext.Result = new HttpNotFoundResult();

            //filterContext.Result = new RedirectToRouteResult(
            //   new RouteValueDictionary
            //   {
            //        { "controller", filterContext.RouteData.Values["controller"] },
            //        { "action", filterContext.RouteData.Values["action"] },
            //        { "area", filterContext.RouteData.Values["area"]}
            //   }); ;
        }
    }
}