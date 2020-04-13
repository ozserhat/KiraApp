using System;
using PagedList;
using System.Web;
using System.Linq;
using Framework.WebUI.Models;
using Framework.WebUI.Helpers;
using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Models.ViewModels;

using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class GayrimenkulDosyaTurController : Controller
    {
        #region Constructor

        private IGayrimenkulService _gayrimenkulservice;
        private IGayrimenkulDosya_TurService _service;

        public GayrimenkulDosyaTurController(IGayrimenkulService gayrimenkulservice, IGayrimenkulDosya_TurService service)
        {
            _service = service;
            _gayrimenkulservice = gayrimenkulservice;
        }
        #endregion
        // GET: Admin/GayrimenkulDosyaTur
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new GayrimenkulDosya_TurVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.GayrimenkulDosyaTur = new StaticPagedList<GayrimenkulDosya_Tur>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            return View(model);
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new GayrimenkulDosya_TurEkleVM();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(GayrimenkulDosya_TurEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GayrimenkulDosya_Tur GayrimenkulAlt_Tur = new GayrimenkulDosya_Tur()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = tur.DosyaTurAdi,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = false
                    };

                    var result = _service.Ekle(GayrimenkulAlt_Tur);

                    if (result.Id > 0)
                        return Json(new { Message = "Gayrimenkul Dosya Tür Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Gayrimenkul Dosya Tür Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new GayrimenkulDosya_TurDuzenleVM();
            var tur = _service.GetirGuid(guid);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.Guid = tur.Guid;
                model.DosyaTurAdi = tur.Ad;
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
        public ActionResult Duzenle(GayrimenkulDosya_TurDuzenleVM model)
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
                        tur.Ad = model.DosyaTurAdi;
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

            if (tur.AktifMi.HasValue && !tur.AktifMi.Value)
                return Json(new { success = true, Message = "Gayrimenkul Dosya Tür Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Gayrimenkul Dosya Tür Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}