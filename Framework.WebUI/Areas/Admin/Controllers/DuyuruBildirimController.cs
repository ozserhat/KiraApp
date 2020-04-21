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

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class DuyuruBildirimController : Controller
    {
        #region Constructor

        private IDuyuruService _service;

        private IDuyuru_BildirimService _bildirimService;

        public DuyuruBildirimController(IDuyuruService service,
            IDuyuru_BildirimService bildirimService)
        {
            _service = service;
            _bildirimService = bildirimService;
        }

        #endregion

        // GET: Admin/DuyuruBildirim
        public ActionResult Index(int? page, int pageSize = 15)
        {
            return View();
        }

        public ActionResult BildirimListesi(int? page, int pageSize = 15)
        {
            int kullaniciId = 0;

            if (User.GetUserPropertyValue("UserId") != null)
                kullaniciId = int.Parse(User.GetUserPropertyValue("UserId"));

            var bildirimListe = _bildirimService.GetirKullaniciMesajlari(kullaniciId);

            var model = new DuyuruBildirimVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (bildirimListe != null)
            {
                model.MesajSayisi = _bildirimService.OkunmamisMesajSayisi(kullaniciId);
                model.DuyuruBildirimleri = new StaticPagedList<Duyuru_Bildirim>(bildirimListe, model.PageNumber, model.PageSize, bildirimListe.Count());
                model.TotalRecordCount = bildirimListe.Count();
            }

            ModelState.AddModelError("LogMessage", "Duyuru Bildirim Mesajları Getirildi.");

            return View(model);
        }

    }
}