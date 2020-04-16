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
    public class DuyuruController : Controller
    {
        #region Constructor

        private IDuyuruService _service;
        private IDuyuru_TurService _duyuruTurService;
        public DuyuruController(IDuyuruService service, IDuyuru_TurService duyuruTurService)
        {
            _service = service;
            _duyuruTurService = duyuruTurService;
        }

        #endregion
        // GET: Admin/Duyuru
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new DuyuruVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.DuyuruTurSelectList = DuyuruTurSelectList();
                model.Duyurular = new StaticPagedList<Duyuru>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Görüntülendi.");
            return View(model);
        }

        public SelectList DuyuruTurSelectList()
        {
            var duyuruTur = _duyuruTurService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(duyuruTur, "Id", "Ad");
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new DuyuruEkleVM();
            model.DuyuruTurSelectList = DuyuruTurSelectList();
            ModelState.AddModelError("LogMessage", "Duyuru  Ekleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(DuyuruEkleVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Duyuru Duyuru = new Duyuru()
                    {
                        Guid = Guid.NewGuid(),
                        DuyuruTur_Id = model.TurId,
                        Ad = model.DuyuruAd,
                        Aciklama=model.Aciklama,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true,
                        Duyuru_Turleri = null,
                        Duyuru_Bildirimleri=null
                    };

                    var result = _service.Ekle(Duyuru);

                    if (result.Id > 0)
                        return Json(new { Message = "Duyuru Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);
                    ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Bilgisi Kaydedildi.");
                }

                ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = "Duyuru Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(Guid guid)
        {
            var model = new DuyuruDuzenleVM();

            var duyuru = _service.GetirGuid(guid);

            if (duyuru != null)
            {
                model.Id = duyuru.Id;
                model.Guid = duyuru.Guid;
                model.Id = duyuru.Id;
                model.DuyuruAd = duyuru.Ad;
                model.Aciklama = duyuru.Aciklama;
                model.GuncelleyenKullanici_Id = duyuru.GuncelleyenKullanici_Id;
                model.GuncellenmeTarihi = duyuru.GuncellenmeTarihi;
                model.AktifMi = duyuru.AktifMi.Value;
                model.DuyuruTurSelectList = DuyuruTurSelectList();
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            ModelState.AddModelError("LogMessage", "Duyuru Düzenleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(DuyuruDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var duyuru = _service.GetirGuid(model.Guid);

                    if (duyuru != null)
                    {
                        duyuru.Id = model.Id;
                        duyuru.DuyuruTur_Id = model.TurId;
                        duyuru.Guid = model.Guid;
                        duyuru.Ad = model.DuyuruAd;
                        duyuru.Aciklama = model.Aciklama;
                        duyuru.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        duyuru.GuncellenmeTarihi = DateTime.Now;
                        duyuru.AktifMi = model.AktifMi;
                        duyuru = _service.Guncelle(duyuru);
                    }

                    ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Düzenlendi.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Düzenlenirken Hata Oluştu.");
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Düzenlenemedi.");
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
                ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Bilgisi Silindi.");

                return Json(new { success = true, Message = "Duyuru Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Duyuru Bilgisi Bilgisi Silinemedi.");
                return Json(new { success = false, Message = "Duyuru Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
    }
}