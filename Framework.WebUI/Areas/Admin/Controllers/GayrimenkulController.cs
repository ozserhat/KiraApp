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
    [CustomAuthorize(Roles = "Admin")]
    public class GayrimenkulController : Controller
    {
        #region Constructor

        private IIlService _ilService;
        private IIlceService _ilceService;
        private IMahalleService _mahalleService;
        private IGayrimenkulTurService _turService;
        private IGayrimenkulService _gayrimenkulservice;
        public GayrimenkulController(IGayrimenkulService gayrimenkulservice, IGayrimenkulTurService turService,
            IIlService ilService, IIlceService ilceService, IMahalleService mahalleService)
        {
            _ilService = ilService;
            _ilceService = ilceService;
            _mahalleService = mahalleService;
            _turService = turService;
            _gayrimenkulservice = gayrimenkulservice;
        }
        #endregion
        // GET: Emlak/Gayrimenkul
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var gayrimenkul = _gayrimenkulservice.GetirListe();

            var model = new GayrimenkulVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (gayrimenkul != null)
            {
                model.Gayrimenkuller = new StaticPagedList<Gayrimenkul>(gayrimenkul, model.PageNumber, model.PageSize, gayrimenkul.Count());
                model.TotalRecordCount = gayrimenkul.Count();
            }

            return View(model);
        }

        public SelectList TurSelectList()
        {
            var turler = _turService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        public SelectList IlSelectList()
        {
            var turler = _ilService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        public SelectList IlceSelectList()
        {
            var turler = _ilceService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }
        public SelectList MahalleSelectList()
        {
            var turler = _mahalleService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new GayrimenkulEkleVM();
            model.TurSelectList = TurSelectList();
            model.IlSelectList = IlSelectList();
            model.IlceSelectList = IlceSelectList();
            model.MahalleSelectList = MahalleSelectList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(GayrimenkulEkleVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Gayrimenkul gayrimenkul = new Gayrimenkul()
                    {
                        Guid = Guid.NewGuid(),
                        Ad = model.GayrimenkulAdi,
                        GayrimenkulTur_Id = model.GayrimenkulTur_Id,
                        Il_Id = model.Il_Id,
                        Ilce_Id = model.Ilce_Id,
                        Mahalle_Id = model.Mahalle_Id,
                        BinaKimlikNo = model.BinaKimlikNo,
                        NumaratajKimlikNo = model.NumaratajKimlikNo,
                        AdresNo = model.AdresNo,
                        Cadde = model.Cadde,
                        Sokak = model.Sokak,
                        DisKapiNo = model.DisKapiNo,
                        IcKapiNo = model.IcKapiNo,
                        AcikAdres = model.AcikAdres,
                        Koordinat = model.Koordinat,
                        Ada = model.Ada,
                        Pafta = model.Pafta,
                        Parsel = model.Parsel,
                        GayrimenkulNo = model.GayrimenkulNo,
                        DosyaNo = model.DosyaNo,
                        AracKapasitesi = model.AracKapasitesi,
                        Metrekare = model.Metrekare,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _gayrimenkulservice.Ekle(gayrimenkul);

                    if (result.Id > 0)
                        return Json(new { Message = "Gayrimenkul Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Gayrimenkul Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}