using System.Web.Mvc;

namespace Framework.WebUI.Areas.SosyalYardim
{
    public class SosyalYardimAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SosyalYardim";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SosyalYardim_default",
                "SosyalYardim/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}