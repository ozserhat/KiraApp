using Framework.Business.Abstract;
using Framework.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Framework.Business.Concrete.Managers
{
    public class UserPermissionManager : IUserPermissions
    {
        //private IUserPermissions _userPermissionDal;

        //public UserPermissionManager(IUserPermissions userPermissionDal)
        //{
        //    //_userPermissionDal = userPermissionDal;
        //}
        public IEnumerable<ControllerActionList> GetAllControllerActions()
        {
            Assembly asm = Assembly.GetAssembly(typeof(HttpApplication));

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
