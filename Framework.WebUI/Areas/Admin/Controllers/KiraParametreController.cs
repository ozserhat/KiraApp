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
    public class KiraParametreController : Controller
    {
        #region Constructor

        private IKiraParametreService _service;
        private ISistemParametre_DetayService  _parametreService;

        public KiraParametreController(IKiraParametreService service, ISistemParametre_DetayService parametreService)
        {
            _service = service;
            _parametreService = parametreService;
        }
        #endregion

        // GET: Admin/GayrimenkulTur
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new KiraParametreVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.BeyanYiliSelectList = BeyanYiliSelectList();
                model.KiraParametre = new StaticPagedList<KiraParametre>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            return View(model);
        }

        public SelectList BeyanYiliSelectList()
        {
            var turler = _parametreService.GetirListe(1).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new KiraParametreEkleVM();
            model.BeyanYiliSelectList = BeyanYiliSelectList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(KiraParametreEkleVM parametreModel)
        {
            try
            {
              
                if (ModelState.IsValid)
                {
                    KiraParametre kiraParametre = new KiraParametre()
                    {
                        Guid = Guid.NewGuid(),
                        BeyanYili = parametreModel.BeyanYili,
                        KiraTarifeKodu = parametreModel.KiraTarifeKodu,
                        OtoparkTarifeKodu = parametreModel.OtoparkTarifeKodu,
                        EcrimisilTarifeKodu = parametreModel.EcrimisilTarifeKodu,
                        DamgaTarifeKodu = parametreModel.DamgaTarifeKodu,
                        TeminatTarifeKodu = parametreModel.TeminatTarifeKodu,
                        KararHarciTarifeKodu = parametreModel.KararHarciTarifeKodu,
                        KapatmaTarifeKodu = parametreModel.KapatmaTarifeKodu,
                        KdvTarifeKodu = parametreModel.KdvTarifeKodu,
                        DamgaOran = decimal.Parse(parametreModel.DamgaOran),
                        TeminatOran = decimal.Parse(parametreModel.TeminatOran),
                        KararHarciOran = decimal.Parse(parametreModel.KararHarciOran),
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(kiraParametre);

                    if (result.Id > 0)
                        return Json(new { Message = kiraParametre.BeyanYili.ToString()+ "Parametreleri Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Parametreler Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new KiraParametreDuzenleVM();

            var tur = _service.GetirGuid(guid);

            if (tur != null)
            {
                model.Id = tur.Id;
                model.Guid = tur.Guid;
                model.BeyanYili = tur.BeyanYili;
                model.KiraTarifeKodu = tur.KiraTarifeKodu;
                model.OtoparkTarifeKodu = tur.OtoparkTarifeKodu;
                model.EcrimisilTarifeKodu = tur.EcrimisilTarifeKodu;
                model.DamgaTarifeKodu = tur.DamgaTarifeKodu;
                model.TeminatTarifeKodu = tur.TeminatTarifeKodu;
                model.KararHarciTarifeKodu = tur.KararHarciTarifeKodu;
                model.KapatmaTarifeKodu = tur.KapatmaTarifeKodu;
                model.KdvTarifeKodu = tur.KdvTarifeKodu;
                model.DamgaOran = tur.DamgaOran;
                model.TeminatOran = tur.TeminatOran;
                model.KararHarciOran = tur.KararHarciOran;
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
        public ActionResult Duzenle(KiraParametreDuzenleVM model)
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
                        tur.BeyanYili = model.BeyanYili;
                        tur.KiraTarifeKodu = model.KiraTarifeKodu;
                        tur.OtoparkTarifeKodu = model.OtoparkTarifeKodu;
                        tur.EcrimisilTarifeKodu = model.EcrimisilTarifeKodu;
                        tur.DamgaTarifeKodu = model.DamgaTarifeKodu;
                        tur.TeminatTarifeKodu = model.TeminatTarifeKodu;
                        tur.KararHarciTarifeKodu = model.KararHarciTarifeKodu;
                        tur.KapatmaTarifeKodu = model.KapatmaTarifeKodu;
                        tur.KdvTarifeKodu = model.KdvTarifeKodu;
                        tur.DamgaOran = model.DamgaOran;
                        tur.TeminatOran = model.TeminatOran;
                        tur.KararHarciOran = model.KararHarciOran;
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
                return Json(new { success = true, Message = "Parametreler Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Parametreler Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}