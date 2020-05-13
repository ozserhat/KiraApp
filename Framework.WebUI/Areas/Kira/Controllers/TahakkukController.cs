using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Helpers;
using Framework.WebUI.Models;
using Framework.WebUI.Models.ViewModels;
using System.Configuration;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    [CustomAuthorize(Roles = "Kira")]
    public class TahakkukController : Controller
    {

        #region Constructor

        private ITahakkukService _tahakkukService;
        public TahakkukController(ITahakkukService tahakkukService)
        {
            _tahakkukService = tahakkukService;

        }

        #endregion
        #region Listeleme

        public ActionResult Index(int? page, int pageSize = 15)
        {
            var model = new TahakkukVM();

            var tahakkuklar = _tahakkukService.GetirListe();
            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (tahakkuklar != null)
            {
                model.Tahakkuklar = new StaticPagedList<Tahakkuk>(tahakkuklar, model.PageNumber, model.PageSize, tahakkuklar.Count());
                model.TotalRecordCount = tahakkuklar.Count();
            }

            ModelState.AddModelError("LogMessage", "Tahakkuk Bilgisi Görüntülendi.");
            return View(model);
        }

        #endregion
    }
}