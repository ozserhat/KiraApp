using System.Web.Mvc;

namespace Framework.WebUI.Areas.Kira
{
    public class KiraAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Kira";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Kira_default",
                "Kira/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                            new[] { "Framework.WebUI.Areas.Kira.Controllers" }
            );
        }
    }
}