using Framework.Business.Concrete.Managers;
using Framework.Core.Aspects.Postsharp.LogAspects;
using Framework.Core.Aspects.Postsharp.TransactionAspects;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net;
using Framework.Core.DataAccess;
using Framework.Core.DataAccess.EntityFramework;
using Framework.WebUI.App_Helpers;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());       
            filters.Add(new LogAspect(typeof(DatabaseLogger)));

            //filters.Add(new UnitOfWorkActionFilter());
        }
    }
}
