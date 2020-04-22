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
    public class KiraDurumController : Controller
    {
        #region Constructor

        private IKira_DurumService _service;

        public KiraDurumController(IKira_DurumService service)
        {
            _service = service;
        }

        #endregion

        // GET: Admin/KiraDurum
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var kiraDurum = _service.GetirListe();

            var model = new KiraDurumVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (kiraDurum != null)
            {
                model.KiraDurumlari = new StaticPagedList<Kira_Durum>(kiraDurum, model.PageNumber, model.PageSize, kiraDurum.Count());
                model.TotalRecordCount = kiraDurum.Count();
            }

            ModelState.AddModelError("LogMessage", "Kira Durum Bilgisi Görüntülendi.");
            return View(model);
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new KiraDurumEkleVM();
            ModelState.AddModelError("LogMessage", "Kira Durum Ekleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(KiraDurumEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Kira_Durum kiraDurum = new Kira_Durum()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = tur.KiraDurumAd,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(kiraDurum);

                    if (result.Id > 0)
                        return Json(new { Message = "Kira Durum Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);
                    ModelState.AddModelError("LogMessage", "Kira Durum Bilgisi Bilgisi Kaydedildi.");
                }

                ModelState.AddModelError("LogMessage", "Kira Durum Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = "Kira Durum Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new KiraDurumDuzenleVM();

            var tur = _service.GetirGuid(guid);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.Guid = tur.Guid;
                model.KiraDurumAd = tur.Ad;
                model.GuncelleyenKullanici_Id = tur.OlusturanKullanici_Id;
                model.GuncellenmeTarihi = tur.OlusturulmaTarihi;
                model.AktifMi = tur.AktifMi.Value;
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            ModelState.AddModelError("LogMessage", "Kira Durum Düzenleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(KiraDurumDuzenleVM model)
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
                        tur.Ad = model.KiraDurumAd;
                        tur.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        tur.GuncellenmeTarihi = DateTime.Now;
                        tur.AktifMi = model.AktifMi;
                        tur = _service.Guncelle(tur);
                    }

                    ModelState.AddModelError("LogMessage", "Kira Durum Bilgisi Düzenlendi.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LogMessage", "Kira Durum Bilgisi Düzenlenirken Hata Oluştu.");
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Kira Durum Bilgisi Düzenlenemedi.");
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
                ModelState.AddModelError("LogMessage", "Kira Durum Bilgisi Bilgisi Silindi.");

                return Json(new { success = true, Message = "Kira Durum Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Kira DurumBilgisi Bilgisi Silinemedi.");
                return Json(new { success = false, Message = "Kira Durum Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}