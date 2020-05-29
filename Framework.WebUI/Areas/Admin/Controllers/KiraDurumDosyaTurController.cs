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

namespace Framework.WebUI.Areas.Admin.Controllers
{
    public class KiraDurumDosyaTurController : Controller
    {
        #region Constructor

        private IKiraDurum_DosyaTurService _service;
        private IBeyanDosya_TurService _beyanDosyaTurService;
        private IKira_DurumService _kiraDurumService;

        public KiraDurumDosyaTurController(IKiraDurum_DosyaTurService service, IBeyanDosya_TurService beyanDosyaTurService, IKira_DurumService kiraDurumService)
        {
            _service = service;
            _beyanDosyaTurService = beyanDosyaTurService;
            _kiraDurumService = kiraDurumService;
        }
   
        #endregion

        #region Listeleme
       
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var dosyalar = _service.GetirListe();

            var model = new KiraDurum_DosyaTurVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (dosyalar != null)
            {
                model.Dosyalar = new StaticPagedList<KiraDurum_DosyaTur>(dosyalar, model.PageNumber, model.PageSize, dosyalar.Count());
                model.TotalRecordCount = dosyalar.Count();
            }

            return View(model);
        }

        public SelectList KiraDurumSelectList()
        {
            var kiraDurum = _kiraDurumService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(kiraDurum, "Id", "Ad");
        }

        public SelectList BeyanDosyaTurSelectList()
        {
            var duyuruTur = _beyanDosyaTurService.GetirKapamaListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(duyuruTur, "Id", "Ad");
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new KiraDurum_DosyaTurEkleVM();
            model.KiraDurumSelectList = KiraDurumSelectList();
            model.BeyanDosyaTurSelectList = BeyanDosyaTurSelectList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(KiraDurum_DosyaTurEkleVM dosyaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    KiraDurum_DosyaTur dosya = new KiraDurum_DosyaTur()
                    {
                        BeyanDosyaTur_Id= dosyaModel.BeyanDosyaTur_Id,
                        KiraDurum_Id= dosyaModel.KiraDurum_Id,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(dosya);

                    if (result.Id > 0)
                        return Json(new { Message = "Kira Durum Dosya Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Kira Durum Dosya Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(int Id)
        {
            var model = new KiraDurum_DosyaTurDuzenleVM();

            var tur = _service.Getir(Id);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.BeyanDosyaTur_Id = tur.BeyanDosyaTur_Id;
                model.KiraDurum_Id = tur.KiraDurum_Id;
                model.KiraDurumSelectList = KiraDurumSelectList();
                model.BeyanDosyaTurSelectList = BeyanDosyaTurSelectList();
                model.GuncelleyenKullanici_Id = tur.OlusturanKullanici_Id;
                model.GuncellenmeTarihi = tur.OlusturulmaTarihi;
                model.AktifMi = tur.AktifMi;
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(KiraDurum_DosyaTurDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tur = _service.Getir(model.Id);

                    if (tur != null)
                    {
                        tur.Id = model.Id;
                        tur.BeyanDosyaTur_Id = model.BeyanDosyaTur_Id;
                        tur.KiraDurum_Id = model.KiraDurum_Id;
                        tur.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        tur.GuncellenmeTarihi = DateTime.Now;
                        tur.AktifMi = model.AktifMi;
                        tur = _service.Guncelle(tur);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
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

            if (!tur.AktifMi)
                return Json(new { success = true, Message = "Dosya Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Dosya Tür Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}