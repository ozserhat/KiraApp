using System.Web.Mvc;

namespace Framework.WebUI.Areas.Emlak
{
    public class EmlakAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Emlak";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Emlak_default",
                "Emlak/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                            new[] { "Framework.WebUI.Areas.Emlak.Controllers" }
            );
        }
    }
}