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
    public class KiraciTurController : Controller
    {
        #region Constructor

        private IKiraciTurService _service;

        public KiraciTurController(IKiraciTurService service)
        {
            _service = service;
        }

        #endregion
        // GET: Admin/KiraciTur
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new KiraciTurVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.KiraciTurleri = new StaticPagedList<KiraciTur>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Görüntülendi.");
            return View(model);
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new KiraciTurEkleVM();
            ModelState.AddModelError("LogMessage", "Kiracı Tür Ekleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(KiraciTurEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    KiraciTur kiraciTur = new KiraciTur()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = tur.KiraciTurAd,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(kiraciTur);

                    if (result.Id > 0)
                        return Json(new { Message = "Kiracı Tür Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);
                    ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Bilgisi Kaydedildi.");
                }

                ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = "Kiracı Tür Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(Guid guid)
        {
            var model = new KiraciTurDuzenleVM();

            var tur = _service.GetirGuid(guid);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.Guid = tur.Guid;
                model.KiraciTurAd = tur.Ad;
                model.GuncelleyenKullanici_Id = tur.OlusturanKullanici_Id;
                model.GuncellenmeTarihi = tur.OlusturulmaTarihi;
                model.AktifMi = tur.AktifMi.Value;
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            ModelState.AddModelError("LogMessage", "Kiracı Tür Düzenleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(KiraciTurDuzenleVM model)
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
                        tur.Ad = model.KiraciTurAd;
                        tur.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        tur.GuncellenmeTarihi = DateTime.Now;
                        tur.AktifMi = model.AktifMi;
                        tur = _service.Guncelle(tur);
                    }

                    ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Düzenlendi.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Düzenlenirken Hata Oluştu.");
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Düzenlenemedi.");
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
                ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Bilgisi Silindi.");

                return Json(new { success = true, Message = "Kiracı Tür Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Kiracı Tür Bilgisi Bilgisi Silinemedi.");
                return Json(new { success = false, Message = "Kiracı Tür Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}