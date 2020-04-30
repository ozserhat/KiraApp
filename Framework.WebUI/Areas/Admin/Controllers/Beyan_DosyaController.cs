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
    public class Beyan_DosyaController : Controller
    {
        #region Constructor

        private IBeyan_DosyaService _service;

        public Beyan_DosyaController(IBeyan_DosyaService service)
        {
            _service = service;
        }
        #endregion

        // GET: Admin/Beyan_Dosya

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new Beyan_DosyaVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.Beyan_Dosya = new StaticPagedList<Beyan_Dosya>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            return View(model);
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new Beyan_DosyaEkleVM();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(Beyan_DosyaEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Beyan_Dosya gayrimenkulTur = new Beyan_Dosya()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = tur.BeyanAdi,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(beyan_Dosya);

                    if (result.Id > 0)
                        return Json(new { Message = "Beyan Dosya Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Beyan Dosya Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(Guid guid)
        {
            var model = new Beyan_DosyaDuzenleVM();

            var tur = _service.GetirGuid(guid);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.Guid = tur.Guid;
                model.BeyanAdi = tur.Ad;
                model.GuncelleyenKullanici_Id = tur.OlusturanKullanici_Id;
                model.GuncellenmeTarihi = tur.OlusturulmaTarihi;
                model.AktifMi = tur.AktifMi.Value;
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
        public ActionResult Duzenle(Beyan_DosyaDuzenleVM model)
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
                        tur.Ad = model.BeyanAdi;
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

            if (tur.AktifMi.HasValue && tur.AktifMi.Value)
                return Json(new { success = true, Message = "Beyan Dosya Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Beyan Dosya Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}