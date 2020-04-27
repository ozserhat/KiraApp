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
        #region Listeleme
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
        #endregion

        #region Detay 

        [HttpPost]
        public JsonResult BildirimDetay(int Id)
        {
            try
            {
                var detay = _service.Getir(Id); 
                ModelState.AddModelError("LogMessage", "Duyuru Detay Bilgisi Görüntülendi.");
                return Json(new { Data = detay, success = true, Message = "Duyuru Detay Bilgisi Görüntülendi." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogMessage", "Duyuru Detay Bilgisi Görüntüleme Esnasında Hata Oluştu!!!");
                return Json(new { success = false, Message = "Duyuru Detay Bilgisi Görüntüleme Esnasında Hata Oluştu!!!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}