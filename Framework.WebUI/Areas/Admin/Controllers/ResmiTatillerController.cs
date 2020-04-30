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

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class ResmiTatillerController : Controller
    {
        #region Constructor

        private IResmiTatillerService _service;

        public ResmiTatillerController(IResmiTatillerService service)
        {
            _service = service;
        }
        #endregion

        // GET: Admin/ResmiTatiller
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new ResmiTatillerVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.ResmiTatiller = new StaticPagedList<ResmiTatiller>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            return View(model);
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new ResmiTatillerEkleVM();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(ResmiTatillerEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResmiTatiller resmiTatiller = new ResmiTatiller()
                    {
                        Tarih = tur.Tarih,
                        ResmiTatilAdi = tur.ResmiTatilAdi,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(resmiTatiller);

                    if (result.Id > 0)
                        return Json(new { Message = "Resmi Tatil Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Resmi Tatil Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            var model = new ResmiTatillerDuzenleVM();

            var tur = _service.Getir(id);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.ResmiTatilAdi = tur.ResmiTatilAdi;
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
        public ActionResult Duzenle(ResmiTatillerDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tur = _service.Getir(model.Id);

                    if (tur != null)
                    {
                        tur.Id = model.Id;
                        tur.ResmiTatilAdi = model.ResmiTatilAdi;
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
                return Json(new { success = true, Message = "Resmi Tatil Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Resmi Tatil Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}