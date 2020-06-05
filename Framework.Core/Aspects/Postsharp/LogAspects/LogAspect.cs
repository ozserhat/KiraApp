using Framework.Core.CrossCuttingConcerns.Logging;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net;
using Framework.Core.CrossCuttingConcerns.Security.Web;
using log4net;
//using PostSharp.Aspects;
//using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Framework.Core.Aspects.Postsharp.LogAspects
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class LogAspect : Attribute, IActionFilter, IExceptionFilter
    {
        private readonly Type _loggerType;
        private LoggerService _loggerService;
        private static readonly ILog _log = LogManager.GetLogger("DatabaseLogger");
        public string LogMessage { get; set; }

        public Type LoggerType => _loggerType;

        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            int userId = 0;
            string userName = "";
            string logMessage = "";
            string logType = "";

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            ClaimsIdentity claimsIdentity;
            var httpContext = HttpContext.Current;
            claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            string actionType = "";
            ActionDescriptor controllerActionDescriptor = filterContext.ActionDescriptor;

            var request = filterContext.HttpContext.Request;


            if (controllerActionDescriptor != null)
            {
                actionType = controllerActionDescriptor.GetCustomAttributes(inherit: true).Select(a => a.GetType().Name.Replace("Attribute", "")).FirstOrDefault();
            }

            var form = filterContext.HttpContext.Request.Form;
            var dictionary = form.AllKeys.ToDictionary(k => k, k => form[k]);


            logMessage = (filterContext.Controller.ViewData["logMessage"] != null ? filterContext.Controller.ViewData["logMessage"].ToString() : "");

            if (claimsIdentity.FindFirst("UserId") != null)
            {
                userName = claimsIdentity.FindFirst("UserName").Value;
                userId = Int32.Parse(claimsIdentity.FindFirst("UserId").Value);
            }

            if (filterContext.Controller.ViewData.ModelState["LogMessage"] != null)
                logMessage = filterContext.Controller.ViewData.ModelState["LogMessage"].Errors.First().ErrorMessage;

            if (controllerName == "Unauthorized")
            {
                logType = "Error";
                if (string.IsNullOrEmpty(logMessage))
                    logMessage = "Yetkisiz İşlem!!";
            }
            else
                logType = "Info";

            var logParameters = new List<LogParameter>()
            {
               new LogParameter
               {
                ActionName = actionName,
                ControllerName = controllerName,
                Url=HttpContext.Current.Request.Url.AbsoluteUri,
                Date = DateTime.Now,
                LogType=logType,
                HttpType = (!string.IsNullOrEmpty(actionType)?actionType:"HttpGet"),
                UserId = userId,
                UserName = userName,
                Message = logMessage,
                ExceptionMessage=" ",
                StackTrace = " "
               }
             };

            _loggerService = new LoggerService(_log);
            _loggerService.Info(logParameters);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "*");
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
        }

        public void OnException(ExceptionContext filterContext) 
        {
            int userId = 0;
            string userName = "";
            string logMessage = "";
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            ClaimsIdentity claimsIdentity;
            var httpContext = HttpContext.Current;
            claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            string actionType = "";


            var request = filterContext.HttpContext.Request;

            Type typeOfRequest = filterContext.HttpContext.Request.RequestType.ToLower() == "Get" ? typeof(HttpGetAttribute) : typeof(HttpPostAttribute);

            MethodInfo method = filterContext.Controller.GetType().GetMethods()
                  .Where(x => x.ReflectedType == typeOfRequest
                                  && x.Name == actionName).FirstOrDefault();

            if (method != null)
            {
                IEnumerable<Attribute> attributes = method.GetCustomAttributes();

                actionType = attributes.Select(a => a.GetType().Name.Replace("Attribute", "")).FirstOrDefault();
            }

            var form = filterContext.HttpContext.Request.Form;
            var dictionary = form.AllKeys.ToDictionary(k => k, k => form[k]);


            logMessage = (filterContext.Controller.ViewData["logMessage"] != null ? filterContext.Controller.ViewData["logMessage"].ToString() : "");

            if (claimsIdentity.FindFirst("UserId") != null)
            {
                userName = claimsIdentity.FindFirst("UserName").Value;
                userId = Int32.Parse(claimsIdentity.FindFirst("UserId").Value);
            }

            if (filterContext.Controller.ViewData.ModelState["LogMessage"] != null)
                logMessage = filterContext.Controller.ViewData.ModelState["LogMessage"].Errors.First().ErrorMessage;


            var logParameters = new List<LogParameter>()
            {
               new LogParameter
               {
                ActionName = actionName,
                ControllerName = controllerName,
                Url=HttpContext.Current.Request.Url.AbsoluteUri,
                Date = DateTime.Now,
                LogType="Error",
                HttpType = (!string.IsNullOrEmpty(actionType)?actionType:"HttpGet"),
                UserId = userId,
                UserName = userName,
                Message = (!string.IsNullOrEmpty(logMessage)?logMessage:"-"),
                ExceptionMessage=filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace
               }
             };

            _loggerService = new LoggerService(_log);
            _loggerService.Info(logParameters);

            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Session.Clear();
            filterContext.HttpContext.Session.Abandon();
            filterContext.HttpContext.ClearError();

            //Redirect user
            filterContext.HttpContext.Server.TransferRequest("~/Error/");
        }
    }
}
