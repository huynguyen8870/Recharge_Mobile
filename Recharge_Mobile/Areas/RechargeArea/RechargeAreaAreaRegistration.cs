using System.Web.Mvc;

namespace Recharge_Mobile.Areas.RechargeArea
{
    public class RechargeAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RechargeArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RechargeArea_default",
                "RechargeArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}