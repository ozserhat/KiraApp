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

namespace Framework.WebUI.Areas.Kira.Controllers
{
    public class AnasayfaController : Controller
    {
        #region Constructor

        private readonly IDuyuruService _service;

        private readonly IDuyuru_BildirimService _bildirimService;

        public IDuyuruService Service => _service;

        public IDuyuru_BildirimService BildirimService => _bildirimService;
       
        private readonly ITahakkukService _tahakkukService;

        public AnasayfaController(IDuyuruService service,
            IDuyuru_BildirimService bildirimService, ITahakkukService tahakkukService)
        {
            _service = service;
            _tahakkukService = tahakkukService;
            _bildirimService = bildirimService;
        }

        #endregion

        // GET: Emlak/Anasayfa
        #region Listeleme
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HatirlatmaListesi()
        {
            HatirlatmaVm hatirlatma = new HatirlatmaVm()
            {
                ArtisiGelenSayisi = _tahakkukService.GetirArtisiGelenTahakkuklar().Count,
                GecikenTahakkukSayisi = _tahakkukService.GetirOdemesiGecikenTahakkuklar().Count,
                SozlemesiBitenSayisi = _tahakkukService.GetirSozlemesiBitenBeyanlar().Count,
                OdemesiGelenTahakkukSayisi = _tahakkukService.GetirOdemesiGelenTahakkuklar().Count
            };

            //TempData["HatirlatmaListesi"] = hatirlatma;
            return PartialView("~/Views/Shared/_Hatirlatmalar.cshtml",hatirlatma);
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