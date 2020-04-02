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

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
            //    namespaces: new[] { "CsbUygTemplate.Web.Controllers" }
            //);

            #region smart config
            routes.LowercaseUrls = true;
            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Account",
                action = "Login",
                id = UrlParameter.Optional
            }).RouteHandler = new DashRouteHandler();
            #endregion
        }
    }
}
