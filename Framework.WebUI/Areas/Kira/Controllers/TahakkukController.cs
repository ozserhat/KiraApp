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
using WebGrease.Css.Extensions;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    [CustomAuthorize(Roles = "Kira")]
    public class TahakkukController : Controller
    {

        #region Constructor
        private IBeyanService _beyanService;

        private IGl_BorcService _tahakkukService;
        public TahakkukController(IGl_BorcService tahakkukService, IBeyanService beyanService)
        {
            _beyanService = beyanService;
            _tahakkukService = tahakkukService;
        }

        #endregion
        #region Listeleme
        public ActionResult Index(TahakkukRequest request, int? page, int pageSize = 15)
        {
            var model = new TahakkukVM();
            string beyanId = "";
            var tahakkuklar = _tahakkukService.GetirSorguListe(request);

            foreach (var th in tahakkuklar)
            {
                beyanId = th.BEYAN_ID.ToString();
                var result = _beyanService.Getir(int.Parse(beyanId));
                if (result != null)
                {
                    th.BeyanNo = !string.IsNullOrEmpty(result.BeyanNo) ? result.BeyanNo : "-";

                    th.BeyanYil = result.BeyanYil.HasValue ? result.BeyanYil.Value : -1;
                }
            }

            model.PageNumber = page ?? 1;

            model.PageSize = pageSize;

            if (tahakkuklar != null)
            {
                model.OdemeDurumuSelectList = OdemeDurumuSelectList();
                model.Tahakkuklar = new StaticPagedList<GL_BORC>(tahakkuklar, model.PageNumber, model.PageSize, tahakkuklar.Count());
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