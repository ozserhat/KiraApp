using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Helpers;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Emlak.Controllers
{
    [CustomAuthorize(Roles = "Emlak")]
    public class AnasayfaController : Controller
    {
        #region Constructor

        private readonly IDuyuruService _service;

        private readonly IDuyuru_BildirimService _bildirimService;

        public IDuyuruService Service => _service;

        public IDuyuru_BildirimService BildirimService => _bildirimService;

        public AnasayfaController(IDuyuruService service,
            IDuyuru_BildirimService bildirimService)
        {
            _service = service;
            _bildirimService = bildirimService;
        }

        #endregion

        // GET: Emlak/Anasayfa
        #region Listeleme
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BildirimListesi()
        {
            int kullaniciId = 0;

            if (User.GetUserPropertyValue("UserId") != null)
                kullaniciId = int.Parse(User.GetUserPropertyValue("UserId"));

            var bildirimListe = BildirimService.GetirKullaniciMesajlari(kullaniciId);

            DuyuruBildirimVM model = new DuyuruBildirimVM
            {
                PageNumber = 1,
                PageSize = 15
            };

            if (bildirimListe != null)
            {
                TempData["MesajSayisi"] = BildirimService.OkunmamisMesajSayisi(kullaniciId);
                model.MesajSayisi = BildirimService.OkunmamisMesajSayisi(kullaniciId);
                model.DuyuruBildirimleri = new StaticPagedList<Duyuru_Bildirim>(bildirimListe, model.PageNumber, model.PageSize, bildirimListe.Count());
                model.TotalRecordCount = bildirimListe.Count();
            }

            ModelState.AddModelError("LogMessage", "Duyuru Bildirim Mesajları Getirildi.");
            return PartialView("~/Views/Shared/_bildirimListesi.cshtml", model);
        }
        #endregion
    }
}