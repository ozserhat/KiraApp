using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        // GET: Admin/Roles
        private IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public ActionResult Index(int? page, int pageSize = 15)
        {            
            var roller = _roleService.GetAll();

            var model = new RolPageVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (roller != null)
            {
                model.Roller = new StaticPagedList<Role>(roller, model.PageNumber, model.PageSize, roller.Count());
                model.TotalRecordCount = roller.Count();
            }

            return View(model);
        }
    }
}