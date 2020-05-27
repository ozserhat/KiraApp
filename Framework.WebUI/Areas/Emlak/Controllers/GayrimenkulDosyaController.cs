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


namespace Framework.WebUI.Areas.Emlak.Controllers
{
    public class GayrimenkulDosyaController : Controller
    {
        #region Constructor

        private IGayrimenkul_DosyaService _service;
        private IGayrimenkulDosya_TurService _dosyaTurService;
        public GayrimenkulDosyaController(IGayrimenkul_DosyaService service, IGayrimenkulDosya_TurService dosyaTurService)
        {
            _service = service;
            _dosyaTurService = dosyaTurService;
        }
        #endregion

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var turler = _service.GetirListe();

            var model = new Gayrimenkul_DosyaVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (turler != null)
            {
                model.Gayrimenkul_Dosya = new StaticPagedList<Gayrimenkul_Dosya>(turler, model.PageNumber, model.PageSize, turler.Count());
                model.TotalRecordCount = turler.Count();
            }

            return View(model);
        }

        public SelectList DosyaTurSelectList()
        {
            var roller = _dosyaTurService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(roller, "Id", "Ad");
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new Gayrimenkul_DosyaEkleVM();
            model.DosyaTur = DosyaTurSelectList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(Gayrimenkul_DosyaEkleVM tur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Gayrimenkul_Dosya gayrimenkul_Dosya = new Gayrimenkul_Dosya()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = tur.DosyaAdi,
                        GayrimenkulDosyaTur_Id = tur.GayrimenkulDosyaTur_Id,
                        GayrimenkulDosyaTurleri = null,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(gayrimenkul_Dosya);

                    if (result.Id > 0)
                        return Json(new { Message = "Gayrimenkul Dosya Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Gayrimenkul Dosya Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}