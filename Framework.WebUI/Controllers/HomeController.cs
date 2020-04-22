using Framework.WebUI.App_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
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

namespace Framework.WebUI.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor

        private IDuyuruService _service;

        private IDuyuru_BildirimService _bildirimService;

        public HomeController(IDuyuruService service,
            IDuyuru_BildirimService bildirimService)
        {
            _service = service;
            _bildirimService = bildirimService;
        }

        #endregion
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _bildirimListesi()
        {
            int kullaniciId = 0;

            if (User.GetUserPropertyValue("UserId") != null)
                kullaniciId = int.Parse(User.GetUserPropertyValue("UserId"));

            var bildirimListe = _bildirimService.GetirKullaniciMesajlari(kullaniciId);

            var model = new DuyuruBildirimVM();

            model.PageNumber = 1;
            model.PageSize = 15;

            if (bildirimListe != null)
            {
                model.MesajSayisi = _bildirimService.OkunmamisMesajSayisi(kullaniciId);
                model.DuyuruBildirimleri = new StaticPagedList<Duyuru_Bildirim>(bildirimListe, model.PageNumber, model.PageSize, bildirimListe.Count());
                model.TotalRecordCount = bildirimListe.Count();
            }

            ModelState.AddModelError("LogMessage", "Duyuru Bildirim Mesajları Getirildi.");
            return PartialView("~/Views/Shared/_bildirimListesi.cshtml", model);
        }
    }
}