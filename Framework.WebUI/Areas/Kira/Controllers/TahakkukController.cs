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
        public ActionResult Index(TahakkukRequest request, int? page, int pageSize = 15)
        {
            var model = new TahakkukVM();

            if (request.Tutar.HasValue)
            {
                var tutarval = Convert.ToInt32(request.Tutar.Value);
                var tutarlist = _tahakkukService.Getir(tutarval);

                request.Tutar = tutarlist.Tutar.Value;
            }

            var tahakkuklar = _tahakkukService.GetirSorguListe(request);

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (tahakkuklar != null)
            {
                model.OdemeDurumuSelectList = OdemeDurumuSelectList();
                model.Tahakkuklar = new StaticPagedList<Tahakkuk>(tahakkuklar, model.PageNumber, model.PageSize, tahakkuklar.Count());
                model.TotalRecordCount = tahakkuklar.Count();
            }

            return View(model);
        }

        public SelectList OdemeDurumuSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Tahsil",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Vade",
                                    Value="0"

                                    }
            };

            return new SelectList(newList, "Value", "Text");
        }
        #endregion
    }
}