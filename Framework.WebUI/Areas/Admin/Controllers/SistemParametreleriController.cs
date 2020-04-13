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
    public class SistemParametreleriController : Controller
    {
        #region Constructor

        private ISistemParametreleriService _service;

        public SistemParametreleriController(ISistemParametreleriService service)
        {
            _service = service;
        }
        #endregion

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var parametreler = _service.GetirListe();

            var model = new SistemParametreleriVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (parametreler != null)
            {
                model.Parametreler = new StaticPagedList<SistemParametreleri>(parametreler, model.PageNumber, model.PageSize, parametreler.Count());
                model.TotalRecordCount = parametreler.Count();
            }

            return View(model);
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new SistemParametresiEkleVM();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(SistemParametresiEkleVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SistemParametreleri parametre = new SistemParametreleri()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = model.Ad,
                        Deger= model.Deger,
                        BaslangicTarihi=model.BaslangicTarihi.Value,
                        BitisTarihi=model.BitisTarihi.Value,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = false
                    };

                    var result = _service.Ekle(parametre);

                    if (result.Id > 0)
                        return Json(new { Message = "Sistem Parametre Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Sistem Parametre  Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new SistemParametresiDuzenleVM();
            var parametre = _service.GetirGuid(guid);

            if (parametre != null)
            {
                model.Id = parametre.Id;
                model.Guid = parametre.Guid;
                model.Ad = parametre.Ad;
                model.Deger = parametre.Deger;
                model.BaslangicTarihi = parametre.BaslangicTarihi.Value;
                model.BitisTarihi = parametre.BitisTarihi.Value;
                model.GuncelleyenKullanici_Id = parametre.GuncelleyenKullanici_Id;
                model.GuncellenmeTarihi = parametre.GuncellenmeTarihi;
                model.AktifMi = parametre.AktifMi.Value;
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
        public ActionResult Duzenle(SistemParametresiDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var parametre = _service.GetirGuid(model.Guid);

                    if (parametre != null)
                    {
                        parametre.Id = model.Id;
                        parametre.Guid = model.Guid;
                        parametre.Ad = model.Ad;
                        parametre.Deger = model.Deger;
                        parametre.BaslangicTarihi = model.BaslangicTarihi;
                        parametre.BitisTarihi = model.BitisTarihi;
                        parametre.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        parametre.GuncellenmeTarihi = DateTime.Now;
                        parametre.AktifMi = model.AktifMi;
                        parametre = _service.Guncelle(parametre);
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
                return Json(new { success = true, Message = "Sistem Parametre Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Sistem Parametre Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}