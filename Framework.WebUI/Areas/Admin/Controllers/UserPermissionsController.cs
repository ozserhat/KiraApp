using Framework.Business.Abstract;
using Framework.Entities.ComplexTypes;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class UserPermissionsController : Controller
    {
        ControllerHelper _helper;
        private IUserPermissionsService _userPermissionService;

        public UserPermissionsController(IUserPermissionsService userPermissionService, ControllerHelper helper)
        {
            _helper = helper;
            _userPermissionService = userPermissionService;
        }

        public ActionResult Index(int? page, int pageSize = 15)
        {
            var list = _helper.GetAll();

            var model = new ControllerActionsVm();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            //if (list != null)
            //{
            //    model.ControllerActionList = new StaticPagedList<ControllerActionList>(list, model.PageNumber, model.PageSize, list.Count());
            //    model.TotalRecordCount = list.Count();
            //}
            return View(model);
        }
    }
}