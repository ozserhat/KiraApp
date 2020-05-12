using System;
using PagedList;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Framework.WebUI.Models;
using Framework.WebUI.Helpers;
using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Models.ViewModels;
using Framework.DataAccess.Abstract;
using System.Dynamic;
using System.Web.Services.Description;
using System.IO;
using System.Globalization;
using System.Configuration;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    [CustomAuthorize(Roles = "Kira")]
    public class TahakkukController : Controller
    {

        // GET: Kira/Tahakkuk

        #region Constructor

        private ITahakkukService _tahakkukService;
        private IKira_BeyanService _kiraBeyanService;
        private ISistemParametre_DetayService _parametreService;
        public string DosyaYolu = ConfigurationManager.AppSettings["DosyaYolu"].ToString();
        public TahakkukController(ITahakkukService tahakkukService, IKira_BeyanService kiraBeyanService, ISistemParametre_DetayService parametreService)
        {
            _tahakkukService = tahakkukService;
            _kiraBeyanService = kiraBeyanService;

        }

        #endregion
        #region Listeleme

        public ActionResult Index(int? page, int pageSize = 15)
        {
            var model = new TahakkukVM();
            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;


            return View(model);
        }

        private SelectList OdemeDurumuSelectList()
        {
            var odemeler = _parametreService.GetirListe(1).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(odemeler, "Id", "OdemeDurumu");
        }

        private SelectList BeyanYilSelectList()
        {
            var beyanyil = _parametreService.GetirListe(1).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(beyanyil, "Id", "BeyanYil");
        }

        private SelectList TahakkukTurSelectList()
        {
            var tahakkuktur = _parametreService.GetirListe(1).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(tahakkuktur, "Id", "TahakkukTur");
        }

        #endregion

    }

}