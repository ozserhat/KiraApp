using Framework.WebUI.App_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Framework.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          
            #region smart config

            routes.LowercaseUrls = true;

            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Account",
                action = "Login",
                id = UrlParameter.Optional,
            },
            namespaces: new string[] { "Framework.WebUI.Controllers" }
            ).RouteHandler = new DashRouteHandler();

          //  routes.MapRoute(
          //    name: "Admin",
          //    url: "{controller}/{action}/{id}",
          //    defaults: new { controller = "Anasayfa", action = "Index", id = UrlParameter.Optional },
          //    namespaces: new[] { "Framework.WebUI.Areas.Controllers" }
          //);

          //  routes.MapRoute(
          //    name: "Emlak",
          //    url: "{controller}/{action}/{id}",
          //    defaults: new { controller = "Anasayfa", action = "Index", id = UrlParameter.Optional },
          //    namespaces: new[] { "Framework.WebUI.Areas.Controllers" }
          //);

            //route.DataTokens["UseNamespaceFallback"] = false;

            #endregion
        }
    }
}
