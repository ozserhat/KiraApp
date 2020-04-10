﻿#region Using

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Framework.WebUI.App_Helpers;
using Microsoft.Ajax.Utilities;

#endregion

namespace Framework.WebUI
{
    public static class HtmlHelperExtensions
    {
        private static string _displayVersion;

        /// <summary>
        ///     Retrieves a non-HTML encoded string containing the assembly version as a formatted string.
        ///     <para>If a project name is specified in the application configuration settings it will be prefixed to this value.</para>
        ///     <para>
        ///         e.g.
        ///         <code>1.0 (build 100)</code>
        ///     </para>
        ///     <para>
        ///         e.g.
        ///         <code>ProjectName 1.0 (build 100)</code>
        ///     </para>
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString AssemblyVersion(this HtmlHelper helper)
        {
            if (_displayVersion.IsNullOrWhiteSpace())
                SetDisplayVersion();

            return helper.Raw(_displayVersion);
        }

        /// <summary>
        ///     Compares the requested route with the given <paramref name="value" /> value, if a match is found the
        ///     <paramref name="attribute" /> value is returned.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">The action value to compare to the requested route action.</param>
        /// <param name="attribute">The attribute value to return in the current action matches the given action value.</param>
        /// <returns>A HtmlString containing the given attribute value; otherwise an empty string.</returns>
        public static IHtmlString RouteIf(this HtmlHelper helper, string value, string attribute)
        {
            var currentController =
                (helper.ViewContext.RequestContext.RouteData.Values["controller"] ?? string.Empty).ToString().UnDash();
            var currentAction =
                (helper.ViewContext.RequestContext.RouteData.Values["action"] ?? string.Empty).ToString().UnDash();

            var hasController = value.Equals(currentController, StringComparison.InvariantCultureIgnoreCase);
            var hasAction = value.Equals(currentAction, StringComparison.InvariantCultureIgnoreCase);

            return hasAction || hasController ? new HtmlString(attribute) : new HtmlString(string.Empty);
        }

        /// <summary>
        ///     Compares the requested route with the given <paramref name="value" /> value, if a match is found the
        ///     <paramref name="attribute" /> value is returned.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">The action value to compare to the requested route action.</param>
        /// <param name="attribute">The attribute value to return in the current action matches the given action value.</param>
        /// <returns>A HtmlString containing the given attribute value; otherwise an empty string.</returns>
        public static IHtmlString RouteIf(this HtmlHelper helper, string actionValue, string controllerValue, string attribute)
        {
            var currentController =
                (helper.ViewContext.RequestContext.RouteData.Values["controller"] ?? string.Empty).ToString().UnDash();
            var currentAction =
                (helper.ViewContext.RequestContext.RouteData.Values["action"] ?? string.Empty).ToString().UnDash();

            string value = controllerValue + "/" + actionValue;
            string current = currentController + "/" + currentAction;

            var has = value.Equals(current, StringComparison.InvariantCultureIgnoreCase);

            return has ? new HtmlString(attribute) : new HtmlString(string.Empty);
        }

        public static ControllerBase GetControllerByName(this HtmlHelper htmlHelper, string controllerName)
        {
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = factory.CreateController(htmlHelper.ViewContext.RequestContext, controllerName);
            if (controller == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "The IControllerFactory '{0}' did not return a controller for the name '{1}'.", factory.GetType(), controllerName));
            }
            return (ControllerBase)controller;
        }

        public static bool ActionAuthorized(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            ControllerBase controllerBase = string.IsNullOrEmpty(controllerName) ? htmlHelper.ViewContext.Controller : htmlHelper.GetControllerByName(controllerName);
            ControllerContext controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerContext.Controller.GetType());
            //ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);
            ActionDescriptor actionDescriptor = (ReflectedActionDescriptor)controllerDescriptor.FindAction(controllerContext, actionName);
            if (actionDescriptor == null)
            {
                actionDescriptor = controllerDescriptor.GetCanonicalActions().FirstOrDefault(a => a.ActionName == actionName);

                if (actionDescriptor == null)
                    return false;
            }
            FilterInfo filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));

            AuthorizationContext authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);
            foreach (IAuthorizationFilter authorizationFilter in filters.AuthorizationFilters)
            {
                authorizationFilter.OnAuthorization(authorizationContext); //This call
                if (authorizationContext.Result != null)
                    return false;
            }
            return true;
        }
        /// <summary>
        ///     Renders the specified partial view with the parent's view data and model if the given setting entry is found and
        ///     represents the equivalent of true.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="partialViewName">The name of the partial view.</param>
        /// <param name="appSetting">The key value of the entry point to look for.</param>
        public static void RenderPartialIf(this HtmlHelper htmlHelper, string partialViewName, string appSetting)
        {
            var setting = Settings.GetValue<bool>(appSetting);

            htmlHelper.RenderPartialIf(partialViewName, setting);
        }

        /// <summary>
        ///     Renders the specified partial view with the parent's view data and model if the given setting entry is found and
        ///     represents the equivalent of true.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="partialViewName">The name of the partial view.</param>
        /// <param name="condition">The boolean value that determines if the partial view should be rendered.</param>
        public static void RenderPartialIf(this HtmlHelper htmlHelper, string partialViewName, bool condition)
        {
            if (!condition)
                return;

            htmlHelper.RenderPartial(partialViewName);
        }

        /// <summary>
        ///     Retrieves a non-HTML encoded string containing the assembly version and the application copyright as a formatted
        ///     string.
        ///     <para>If a company name is specified in the application configuration settings it will be suffixed to this value.</para>
        ///     <para>
        ///         e.g.
        ///         <code>1.0 (build 100) © 2015</code>
        ///     </para>
        ///     <para>
        ///         e.g.
        ///         <code>1.0 (build 100) © 2015 CompanyName</code>
        ///     </para>
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString Copyright(this HtmlHelper helper)
        {
            var copyright =
                string.Format("{0} &copy; {1} {2}", helper.AssemblyVersion(), DateTime.Now.Year, Settings.Company)
                    .Trim();

            return helper.Raw(copyright);
        }

        private static void SetDisplayVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            _displayVersion =
                string.Format("{4} {0}.{1}.{2} (build {3})", version.Major, version.Minor, version.Build,
                    version.Revision, Settings.Project).Trim();
        }

        /// <summary>
        ///     Returns an unordered list (ul element) of validation messages that utilizes bootstrap markup and styling.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="alertType">The alert type styling rule to apply to the summary element.</param>
        /// <param name="heading">The optional value for the heading of the summary element.</param>
        /// <returns></returns>
        public static HtmlString ValidationBootstrap(this HtmlHelper htmlHelper, string alertType = "danger", string heading = "")
        {
            if (htmlHelper.ViewData.ModelState.IsValid)
                return new HtmlString(string.Empty);

            var sb = new StringBuilder();

            sb.AppendFormat("<div class=\"alert alert-{0} alert-block\">", alertType);
            sb.Append("<button class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>");

            if (!heading.IsNullOrWhiteSpace())
            {
                sb.AppendFormat("<h4 class=\"alert-heading\">{0}</h4>", heading);
            }

            sb.Append(htmlHelper.ValidationSummary());
            sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }

        /// <summary>
        ///  Returns a __RequestVerificationToken when you ajax post.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();
            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");

            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");

            return new MvcHtmlString(string.Format(@"{0}:""{1}""", "__RequestVerificationToken", tokenValue));
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.AuthAction(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.AuthAction(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.AuthAction(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.AuthAction(linkText, actionName, null, routeValues, new RouteValueDictionary(), showActionLinkAsDisabled);
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.AuthAction(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), showActionLinkAsDisabled);
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.AuthAction(linkText, actionName, null, routeValues, htmlAttributes, showActionLinkAsDisabled);
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool showActionLinkAsDisabled = false)
        {
            return htmlHelper.AuthAction(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes), showActionLinkAsDisabled);
        }

        public static MvcHtmlString AuthAction(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName,
            RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool showActionLinkAsDisabled)
        {
            controllerName = routeValues["controllerName"].ToString();

            if (htmlHelper.ActionAuthorized(actionName, controllerName))
            {
                StringBuilder innerHtml = new StringBuilder();

                TagBuilder tagi = new TagBuilder("i");

                TagBuilder tagBuilder = new TagBuilder("a");
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                urlHelper.Action(actionName, controllerName);
                tagBuilder.MergeAttribute("href", htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsoluteUri);

                tagBuilder.MergeAttribute("class", htmlAttributes["class"].ToString());
                tagi.AddCssClass("glyphicon glyphicon-plus linkStyle");
                tagi.InnerHtml = linkText;
                innerHtml.Append(tagi.ToString());
                tagBuilder.InnerHtml = innerHtml.ToString();
                MvcHtmlString.Create(tagBuilder.ToString());
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            else
            {
                if (showActionLinkAsDisabled)
                {
                    StringBuilder innerHtml = new StringBuilder();

                    TagBuilder tagi = new TagBuilder("i");

                    TagBuilder tagBuilder = new TagBuilder("a");
                    tagBuilder.MergeAttribute("href", "#");

                    tagBuilder.MergeAttribute("class", htmlAttributes["class"].ToString());
                    tagBuilder.MergeAttribute("onclick", "GetirUyari()");
                    //tagi.AddCssClass("glyphicon glyphicon-plus linkStyle");
                    tagi.InnerHtml = linkText;
                    innerHtml.Append(tagi.ToString());
                    tagBuilder.InnerHtml = innerHtml.ToString();
                    return MvcHtmlString.Create(tagBuilder.ToString());
                }
                else
                {
                    StringBuilder innerHtml = new StringBuilder();

                    TagBuilder tagi = new TagBuilder("i");

                    TagBuilder tagBuilder = new TagBuilder("a");
                    UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                    urlHelper.Action(actionName, controllerName);
                    tagBuilder.MergeAttribute("href", routeValues["controllerName"] + "/" + actionName);
                    tagBuilder.MergeAttribute("class", htmlAttributes["class"].ToString());
                    //tagi.AddCssClass("glyphicon glyphicon-plus linkStyle");
                    tagi.InnerHtml = linkText;
                    innerHtml.Append(tagi.ToString());
                    tagBuilder.InnerHtml = innerHtml.ToString();
                    return MvcHtmlString.Create(tagBuilder.ToString());
                }
            }
        }

        public static MvcHtmlString AuthMainAction(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName,
            string htmlAttributes, bool showActionLinkAsDisabled)
        {

            if (htmlHelper.ActionAuthorized(actionName, controllerName))
            {
                StringBuilder innerHtml = new StringBuilder();

                TagBuilder tagi = new TagBuilder("i");

                TagBuilder tagBuilder = new TagBuilder("a");
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                urlHelper.Action(actionName, controllerName);
                tagBuilder.MergeAttribute("href", htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsoluteUri);
                tagBuilder.AddCssClass("linkStyle");
                tagi.AddCssClass(htmlAttributes);
                tagi.InnerHtml = linkText;
                innerHtml.Append(tagi.ToString());
                tagBuilder.InnerHtml = innerHtml.ToString();
                return MvcHtmlString.Create(tagBuilder.ToString());
            }
            else
            {
                if (showActionLinkAsDisabled)
                {
                    StringBuilder innerHtml = new StringBuilder();

                    TagBuilder tagi = new TagBuilder("i");

                    TagBuilder tagBuilder = new TagBuilder("a");
                    tagBuilder.MergeAttribute("href", "#");

                    tagBuilder.MergeAttribute("onclick", "GetirUyari()");
                    tagBuilder.AddCssClass("linkStyle");
                    tagi.AddCssClass(htmlAttributes);
                    tagi.InnerHtml = linkText;
                    innerHtml.Append(tagi.ToString());
                    tagBuilder.InnerHtml = innerHtml.ToString();
                    return MvcHtmlString.Create(tagBuilder.ToString());
                }
                else
                {
                    StringBuilder innerHtml = new StringBuilder();

                    TagBuilder tagi = new TagBuilder("i");

                    TagBuilder tagBuilder = new TagBuilder("a");
                    UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                    urlHelper.Action(actionName, controllerName);
                    tagBuilder.MergeAttribute("href", controllerName + "/" + actionName);
                    tagBuilder.AddCssClass("linkStyle");
                    tagi.AddCssClass(htmlAttributes);
                    tagi.InnerHtml = linkText;
                    innerHtml.Append(tagi.ToString());
                    tagBuilder.InnerHtml = innerHtml.ToString();
                    return MvcHtmlString.Create(tagBuilder.ToString());
                }
            }
        }
    }
}