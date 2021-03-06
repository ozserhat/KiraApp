﻿
using Framework.DataAccess.Concrete.EntityFramework;
using Framework.Entities.Concrete;
using Framework.WebUI.Controllers;
using Framework.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Framework.WebUI.App_Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = this.OnCacheAuthorization(new HttpContextWrapper(context));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            int userId = 0;

            string controllerName, actionName;

            controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller";

            actionName = filterContext.ActionDescriptor.ActionName;
            ClaimsIdentity claimsIdentity;
            var httpContext = HttpContext.Current;
            claimsIdentity = httpContext.User.Identity as ClaimsIdentity;

            if (claimsIdentity.FindFirst("UserId") != null)
                userId = Int32.Parse(claimsIdentity.FindFirst("UserId").Value);

            if (this.AuthorizeCore(filterContext.HttpContext) && GetPermissions(controllerName, actionName, userId))
            {
                filterContext.Controller.TempData["OpenAuthorizationPopup"] = true;
                filterContext.Controller.TempData["ActionVisibility"] = true;

                base.OnAuthorization(filterContext);
            }
            else if (this.AuthorizeCore(filterContext.HttpContext) && GetPermissions(controllerName, actionName, userId))
            {
                filterContext.Controller.TempData["OpenAuthorizationPopup"] = false;
                filterContext.Controller.TempData["ActionVisibility"] = false;

                filterContext.Controller.TempData["returnUrl"] = filterContext.HttpContext.Request.UrlReferrer;
                filterContext.Controller.ViewData.ModelState.AddModelError("LogMessage", "Yetkiniz Bulunmamaktadır!!!");
                //var viewResult = new PartialViewResult();
                //viewResult.ViewName = "_Unauthorized";
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {

                filterContext.Controller.TempData["OpenAuthorizationPopup"] = false;
                filterContext.Controller.TempData["ActionVisibility"] = false;

                filterContext.Controller.TempData["returnUrl"] = filterContext.HttpContext.Request.UrlReferrer;
                filterContext.Controller.ViewData.ModelState.AddModelError("LogMessage", "Yetkiniz Bulunmamaktadır!!!");
                //var viewResult = new PartialViewResult();
                //viewResult.ViewName = "_Unauthorized";
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnVisibility(AuthorizationContext filterContext)
        {
            int userId = 0;

            string controllerName, actionName;

            controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller";

            actionName = filterContext.ActionDescriptor.ActionName;
            ClaimsIdentity claimsIdentity;
            var httpContext = HttpContext.Current;
            claimsIdentity = httpContext.User.Identity as ClaimsIdentity;

            if (claimsIdentity.FindFirst("UserId") != null)
                userId = Int32.Parse(claimsIdentity.FindFirst("UserId").Value);

            if (this.AuthorizeCore(filterContext.HttpContext) && GetPermissions(controllerName, actionName, userId))
            {
                filterContext.Controller.TempData["OpenVisibilityPopup"] = true;
                base.OnAuthorization(filterContext);
            }
            else
            {

                filterContext.Controller.TempData["OpenVisibilityPopup"] = false;
                filterContext.Controller.TempData["returnUrl"] = filterContext.HttpContext.Request.UrlReferrer;
                filterContext.Controller.ViewData.ModelState.AddModelError("LogMessage", "Yetkiniz Bulunmamaktadır!!!");
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public bool GetPermissions(string controllerName, string actionName, int userId)
        {
            ControllerAction controllerAction = new ControllerAction();

            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
                controllerAction = ControllerGetByName(controllerName, actionName);

            var user_Permission = (controllerAction != null ? PermissionsGetById(controllerAction.Id, userId) : null);
            var user_visibility = (controllerAction != null ? UserVisibilityGetById(controllerAction.Id, userId) : null);

            if (user_Permission != null)
                return true;

            if (user_visibility != null)
                return true;

            return false;
        }
        public ControllerAction ControllerGetByName(string ControllerName, string ActionName)
        {
            ControllerAction result = null;

            EfControllerActionsDal _controllerService = new EfControllerActionsDal();

            result = _controllerService.GetByName(ControllerName, ActionName);

            return result;
        }

        public User_Permission PermissionsGetById(int id, int userid)
        {
            User_Permission result = null;

            EfUserPermissionsDal _permissionService = new EfUserPermissionsDal();
            result = _permissionService.GetByPermissionControllerId(id, userid);

            return result;
        }

        public User_Permission UserVisibilityGetById(int id, int userid)
        {
            User_Permission result = null;

            EfUserPermissionsDal _permissionService = new EfUserPermissionsDal();
            result = _permissionService.GetByVisibilityControllerId(id, userid);

            return result;
        }
    }
}