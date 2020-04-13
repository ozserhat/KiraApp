using Framework.Core.CrossCuttingConcerns.Logging;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net;
//using PostSharp.Aspects;
//using PostSharp.Extensibility;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Framework.Core.Aspects.Postsharp.LogAspects
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class LogAspect : Attribute, IActionFilter
    {
        private Type _loggerType;
        private LoggerService _loggerService;

        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            var actionName = filterContext.ActionDescriptor.GetType().Name;
            var controllerName = filterContext.Controller.GetType().Name;
            var message = string.Format("Controller {0} generated an error.", controllerName);
            var logParameters = new LogParameter()
            {
                ActionName = actionName,
                ControllerName = controllerName,
                Date = DateTime.Now,
                Type = filterContext.Result.ToString(),
                Detail = "",
                Value = ""
            };

            _loggerService.Error(logParameters);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.Controller.GetType().Name;
            var message = string.Format("Controller {0} generated an error.", controllerName);
            var logParameters = new LogParameter()
            {
                ActionName = actionName,
                ControllerName = controllerName,
                Date = DateTime.Now,
                Type = "",
                Detail = "",
                Value = ""
            };

            _loggerService.Error(logParameters);
        }

        public void OnException(ExceptionContext filterContext)
        {
            var actionName = "";
            var controllerName = filterContext.Controller.GetType().Name;
            var message = string.Format("Controller {0} generated an error.", controllerName);
            var logParameters = new LogParameter()
            {
                ActionName = actionName,
                ControllerName = controllerName,
                Date = DateTime.Now,
                Type = filterContext.Result.ToString(),
                Detail = "",
                Value = ""
            };

            _loggerService.Error(logParameters);
        }
    }
}
