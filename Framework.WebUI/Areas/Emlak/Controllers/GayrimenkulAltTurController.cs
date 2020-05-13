using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Helpers;
using Framework.WebUI.Models;
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
    public class GayrimenkulAltTurController : Controller
    {
        #region Constructor

        private IGayrimenkulTurService _turservice;
        private IGayrimenkulAlt_TurService _service;

        public GayrimenkulAltTurController(IGayrimenkulTurService turservice, IGayrimenkulAlt_TurService service)
        {
            _service = service;
            _turservice = turservice;
        }
        #endregion

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new GayrimenkulAlt_TurVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.GayrimenkulAltTur = new StaticPagedList<GayrimenkulAlt_Tur>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            return View(model);
        }

        public SelectList TurSelectList()
        {
            var turler = _turservice.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new GayrimenkulAlt_TurEkleVM();
            model.TurSelectList = TurSelectList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(GayrimenkulAlt_TurEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GayrimenkulAlt_Tur GayrimenkulAlt_Tur = new GayrimenkulAlt_Tur()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = tur.AltTurAdi,
                        GayrimenkulTur_Id = tur.GayrimenkulTur_Id,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(GayrimenkulAlt_Tur);

                    if (result.Id > 0)
                        return Json(new { Message = "Gayrimenkul Alt Tür Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Gayrimenkul Alt Tür Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new GayrimenkulAlt_TurDuzenleVM();
            model.TurSelectList = TurSelectList();
            var tur = _service.GetirGuid(guid);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.Guid = tur.Guid;
                model.GayrimenkulTur_Id = tur.GayrimenkulTur_Id;
                model.AltTurAdi = tur.Ad;
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
        public ActionResult Duzenle(GayrimenkulAlt_TurDuzenleVM model)
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
                        tur.GayrimenkulTur_Id = model.GayrimenkulTur_Id;
                        tur.Ad = model.AltTurAdi;
                        tur.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        tur.GuncellenmeTarihi = DateTime.Now;
                        tur.AktifMi = model.AktifMi;
                        tur.GayrimenkulTur = null;
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

            if (tur.AktifMi.HasValue && !tur.AktifMi.Value)
                return Json(new { success = true, Message = "Gayrimenkul Alt Tür Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Gayrimenkul Alt Tür Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}