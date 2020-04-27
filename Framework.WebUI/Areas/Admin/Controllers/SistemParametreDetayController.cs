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
    public class SistemParametreDetayController : Controller
    {
        #region Constructor

        private ISistemParametreleriService _parametreService;
        private ISistemParametre_DetayService _detayService;

        public SistemParametreDetayController(ISistemParametreleriService parametreService, ISistemParametre_DetayService detayService)
        {
            _parametreService = parametreService;
            _detayService = detayService;
        }

        #endregion

        #region Listeleme
        public ActionResult Index(int? ParametreId, int? page, int pageSize = 15)
        {
            var parametreDetay = _detayService.GetirListe(ParametreId);

            var model = new SistemParametre_DetayVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (parametreDetay != null)
            {
                model.ParametreDetay = new StaticPagedList<SistemParametre_Detay>(parametreDetay, model.PageNumber, model.PageSize, parametreDetay.Count());
                model.TotalRecordCount = parametreDetay.Count();
            }

            ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Bilgisi Görüntülendi.");
            return View(model);
        }

        public SelectList SelectListSistemParametresi()
        {
            int parametreId;
            if (Request.QueryString["ParametreId"] != null)
                Int32.TryParse(Request.QueryString["ParametreId"], out parametreId);

            var sistemParametreleri = _parametreService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(sistemParametreleri, "Id", "Ad", Request.QueryString["ParametreId"]);
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new SistemParametre_DetayEkleVM();
            model.SelectListSistemParametresi = SelectListSistemParametresi();
            ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Ekleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(SistemParametre_DetayEkleVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SistemParametre_Detay parametre = new SistemParametre_Detay()
                    {
                        Guid = Guid.NewGuid(),
                        SistemParametre_Id = model.SistemParametre_Id,
                        Ad = model.Ad,
                        Deger = model.Deger,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _detayService.Ekle(parametre);

                    if (result.Id > 0)
                    {
                        ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Bilgisi Bilgisi Kaydedildi.");
                        return Json(new { Message = "Sistem Parametre Detay Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);
                    }
                }

                ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = "Sistem Parametre Detay Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Bilgisi Bilgisi Kaydedilemedi.");
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(int parametreId)
        {
            var model = new SistemParametre_DetayDuzenleVM();
            var parametre = _detayService.Getir(parametreId);

            if (parametre != null)
            {
                model.Id = parametre.Id;
                model.Guid = parametre.Guid;
                model.SistemParametre_Id = parametre.SistemParametre_Id;
                model.Ad = parametre.Ad;
                model.Deger = parametre.Deger;
                model.GuncelleyenKullanici_Id = parametre.GuncelleyenKullanici_Id;
                model.GuncellenmeTarihi = parametre.GuncellenmeTarihi;
                model.AktifMi = parametre.AktifMi.Value;
                model.SelectListSistemParametresi = SelectListSistemParametresi();
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }
            ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Düzenleme Sayfası Görüntülendi.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(SistemParametre_DetayDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var parametre = _detayService.GetirGuid(model.Guid);

                    if (parametre != null)
                    {
                        parametre.Id = model.Id;
                        parametre.Guid = model.Guid;
                        parametre.SistemParametre_Id = model.SistemParametre_Id;
                        parametre.Ad = model.Ad;
                        parametre.Deger = model.Deger;
                        parametre.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        parametre.GuncellenmeTarihi = DateTime.Now;
                        parametre.AktifMi = model.AktifMi;
                        parametre = _detayService.Guncelle(parametre);
                    }

                    ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Bilgisi Düzenlendi.");
                    return RedirectToAction("Index", "SistemParametreDetay", new { ParametreId = parametre.SistemParametre_Id });
                }
                catch (Exception ex)
                {
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Bilgisi Düzenlenemedi.");
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
            var tur = _detayService.Getir(Id);

            tur.AktifMi = false;

            tur = _detayService.Guncelle(tur);

            if (tur.AktifMi.HasValue && !tur.AktifMi.Value)
            {
                ModelState.AddModelError("LogMessage", "Sistem Parametre Detay Bilgisi Bilgisi Silindi.");

                return Json(new { success = true, Message = "Sistem Parametre Detay Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Sistem Parametre DetayBilgisi Bilgisi Silinemedi.");
                return Json(new { success = false, Message = "Sistem Parametre Detay Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
    }
}