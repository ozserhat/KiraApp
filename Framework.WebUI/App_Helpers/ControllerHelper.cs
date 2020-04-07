using Framework.Business.Abstract;
using Framework.Entities.ComplexTypes;
using Framework.WebUI.Areas.Admin.Controllers;
using Framework.WebUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.App_Helpers
{
    public class ControllerHelper
    {
        private IUserPermissionsService _userPermissionService;

        public ControllerHelper(IUserPermissionsService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        public IEnumerable<ControllerActionList> GetAll()
        {
            Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new
                    {
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        ReturnType = x.ReturnType.Name,
                        Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
                    })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            var list = new List<ControllerActionList>();

            foreach (var item in controlleractionlist)
            {
                list.Add(new ControllerActionList()
                {
                    Controller = item.Controller,
                    Action = item.Action,
                    Attributes = item.Attributes,
                    ReturnType = item.ReturnType
                });
            }

            return list;
        }
    }
}