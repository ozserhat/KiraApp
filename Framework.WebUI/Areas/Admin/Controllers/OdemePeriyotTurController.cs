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
    public class OdemePeriyotTurController : Controller
    {
        #region Constructor

        private IOdemePeriyotTurService _service;

        public OdemePeriyotTurController(IOdemePeriyotTurService service)
        {
            _service = service;
        }

        #endregion

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new OdemePeriyotTurVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.PeriyotTurleri = new StaticPagedList<OdemePeriyotTur>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            ModelState.AddModelError("LogMessage", "Duyuru Tür Bilgisi Görüntülendi.");
            return View(model);
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new OdemePeriyotTurEkleVM();
            ModelState.AddModelError("LogMessage", "Periyot Tür Ekleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(OdemePeriyotTurEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OdemePeriyotTur Duyuru_Tur = new OdemePeriyotTur()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = tur.PeriyotTurAd,
                        Aciklama=tur.PeriyotAciklama,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(Duyuru_Tur);

                    if (result.Id > 0)
                        return Json(new { Message = "Periyot Tür Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);
                    ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Bilgisi Kaydedildi.");
                }

                ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = "Periyot Tür Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(Guid guid)
        {
            var model = new OdemePeriyotTurDuzenleVM();

            var tur = _service.GetirGuid(guid);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.Guid = tur.Guid;
                model.PeriyotTurAd = tur.Ad;
                model.PeriyotAciklama = tur.Aciklama;
                model.GuncelleyenKullanici_Id = tur.OlusturanKullanici_Id;
                model.GuncellenmeTarihi = tur.OlusturulmaTarihi;
                model.AktifMi = tur.AktifMi.Value;
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            ModelState.AddModelError("LogMessage", "Periyot Tür Düzenleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(OdemePeriyotTurDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tur = _service.GetirGuid(model.Guid);

                    if (tur != null)
                    {
                        tur.Id = model.Id;
                        tur.Guid = model.Guid;
                        tur.Ad = model.PeriyotTurAd;
                        tur.Aciklama = model.PeriyotAciklama;
                        tur.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        tur.GuncellenmeTarihi = DateTime.Now;
                        tur.AktifMi = model.AktifMi;
                        tur = _service.Guncelle(tur);
                    }

                    ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Düzenlendi.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Düzenlenirken Hata Oluştu.");
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Düzenlenemedi.");
                model.Errors = new List<string>();
                model.Errors.Add(VMErrors.ValidationError);
            }

            return View(model);
        }

        #endregion

        #region Sil 
        [HttpPost]
        public JsonResult Sil(int Id)
        {
            var tur = _service.Getir(Id);

            tur.AktifMi = false;

            tur = _service.Guncelle(tur);

            if (tur.AktifMi.HasValue && !tur.AktifMi.Value)
            {
                ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Bilgisi Silindi.");

                return Json(new { success = true, Message = "Periyot Tür Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Periyot Tür Bilgisi Bilgisi Silinemedi.");
                return Json(new { success = false, Message = "Periyot Tür Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
    }
}