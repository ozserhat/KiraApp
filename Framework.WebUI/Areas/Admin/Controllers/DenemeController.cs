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
    public class DenemeController : Controller
    {
        private IDenemeService _denemeService;

        public DenemeController(IDenemeService denemeService)
        {
            _denemeService = denemeService;
        }


        // GET: Admin/Deneme
        public ActionResult Index(int? page, int pageSize = 1)
        {
            var roller = _denemeService.GetirDenemeList();

            var model = new DenemeVm();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (roller != null)
            {
                model.Denemeler = new StaticPagedList<Deneme>(roller, model.PageNumber, model.PageSize, roller.Count());
                model.TotalRecordCount = roller.Count();
            }

            return View(model);
        }
    }
}