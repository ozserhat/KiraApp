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
using Framework.DataAccess.Abstract;

namespace Framework.WebUI.Areas.Emlak.Controllers
{
    [CustomAuthorize(Roles = "Emlak")]
    public class GayrimenkulController : Controller
    {
        #region Constructor

        private IIlceService _ilceService;
        private IMahalleService _mahalleService;
        private IGayrimenkulTurService _turService;
        private IGayrimenkulService _gayrimenkulservice;
        private IGayrimenkulDosya_TurService _dosyaTurService;
        private IKira_DurumService _gayrimenkuldurumservice;
        public static GayrimenkulEkleVM _gayrimenkulVM;

        public GayrimenkulController(IGayrimenkulService gayrimenkulservice, IGayrimenkulTurService turService,
             IIlceService ilceService, IMahalleService mahalleService, IGayrimenkulDosya_TurService dosyaTurService, IKira_DurumService gayrimenkuldurumservice, GayrimenkulEkleVM gayrimenkulVM)
        {
        
            _ilceService = ilceService;
            _mahalleService = mahalleService;
            _turService = turService;
            _gayrimenkulservice = gayrimenkulservice;
            _dosyaTurService = dosyaTurService;
            _gayrimenkuldurumservice = gayrimenkuldurumservice;
            _gayrimenkulVM = gayrimenkulVM;
        }
        #endregion
        // GET: Emlak/Gayrimenkul
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var gayrimenkul = _gayrimenkulservice.GetirListeAktif();

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

        public SelectList IlceSelectList()
        {
            var ilceler = _ilceService.GetirListe().Where(a=>a.Il_Id==6).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(ilceler, "Id", "Ad");
        }

     
        [HttpPost]

        public JsonResult MahalleSelectList(int ilceId)
        {
            var mahalleler = _mahalleService.GetirListe().Where(a => a.Ilce_Id == ilceId).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return Json(new { Data = mahalleler, success = true }, JsonRequestBehavior.AllowGet);
        }


        public SelectList GayrimenkulDurumSelectList()
        {
            var turler = _gayrimenkuldurumservice.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new GayrimenkulEkleVM();
            string gayrimenkulNo = "GY-";
            int yil = DateTime.Now.Year;

            gayrimenkulNo += yil ;
            gayrimenkulNo += DateTime.Now.Month+"-";
            model.GayrimenkulNo = gayrimenkulNo + "-"+_gayrimenkulservice.GayrimenkulNoUret(yil);
            model.TurSelectList = TurSelectList();
            model.IlceSelectList = IlceSelectList();
            model.DosyaTurleri = _dosyaTurService.GetirListe();
            model.GayrimenkulDurumSelectList = GayrimenkulDurumSelectList();
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
                        Ilce_Id = model.Ilce_Id,
                        Mahalle_Id = model.Mahalle_Id,
                        GayrimenkulDurum_Id = model.GayrimenkulDurum_Id,
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

        #region Duzenle

        [HttpGet]
        public ActionResult Duzenle(Guid guid)
        {
            var model = new GayrimenkulDuzenleVM();
            model.TurSelectList = TurSelectList();
            model.IlceSelectList = IlceSelectList();
            model.DosyaTurleri = _dosyaTurService.GetirListe();
            model.GayrimenkulDurumSelectList = GayrimenkulDurumSelectList();
            var gayrimenkul = _gayrimenkulservice.GetirGuid(guid);

            if (gayrimenkul != null)
            {
                model.Guid = Guid.NewGuid();
                model.GayrimenkulAdi = gayrimenkul.Ad;
                model.GayrimenkulTur_Id = gayrimenkul.GayrimenkulTur_Id;
                model.Ilce_Id = gayrimenkul.Ilce_Id;
                model.Mahalle_Id = gayrimenkul.Mahalle_Id;
                model.GayrimenkulDurum_Id = gayrimenkul.GayrimenkulDurum_Id;
                model.BinaKimlikNo = gayrimenkul.BinaKimlikNo;
                model.NumaratajKimlikNo = gayrimenkul.NumaratajKimlikNo;
                model.AdresNo = gayrimenkul.AdresNo;
                model.Cadde = gayrimenkul.Cadde;
                model.Sokak = gayrimenkul.Sokak;
                model.DisKapiNo = gayrimenkul.DisKapiNo;
                model.IcKapiNo = gayrimenkul.IcKapiNo;
                model.AcikAdres = gayrimenkul.AcikAdres;
                model.Koordinat = gayrimenkul.Koordinat;
                model.Ada = gayrimenkul.Ada;
                model.Pafta = gayrimenkul.Pafta;
                model.Parsel = gayrimenkul.Parsel;
                model.GayrimenkulNo = gayrimenkul.GayrimenkulNo;
                model.DosyaNo = gayrimenkul.DosyaNo;
                model.AracKapasitesi = gayrimenkul.AracKapasitesi;
                model.Metrekare = gayrimenkul.Metrekare;
                gayrimenkul.OlusturulmaTarihi = DateTime.Now;
                model.GuncelleyenKullanici_Id = gayrimenkul.OlusturanKullanici_Id;

            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            ModelState.AddModelError("LogMessage", "Gayrimenkul Düzenleme Sayfası Görüntülendi.");
            return View(model);
        }

       
        [HttpPost]
        public ActionResult Duzenle(GayrimenkulDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var gayrimenkul = _gayrimenkulservice.GetirGuid(model.Guid);

                    if (gayrimenkul != null)
                    {
                        gayrimenkul.Guid = Guid.NewGuid();
                        gayrimenkul.Ad = model.GayrimenkulAdi;
                        gayrimenkul.GayrimenkulTur_Id = model.GayrimenkulTur_Id;
                        gayrimenkul.Ilce_Id = model.Ilce_Id;
                        gayrimenkul.Mahalle_Id = model.Mahalle_Id;
                        gayrimenkul.GayrimenkulDurum_Id = model.GayrimenkulDurum_Id;
                        gayrimenkul.BinaKimlikNo = model.BinaKimlikNo;
                        gayrimenkul.NumaratajKimlikNo = model.NumaratajKimlikNo;
                        gayrimenkul.AdresNo = model.AdresNo;
                        gayrimenkul.Cadde = model.Cadde;
                        gayrimenkul.Sokak = model.Sokak;
                        gayrimenkul.DisKapiNo = model.DisKapiNo;
                        gayrimenkul.IcKapiNo = model.IcKapiNo;
                        gayrimenkul.AcikAdres = model.AcikAdres;
                        gayrimenkul.Koordinat = model.Koordinat;
                        gayrimenkul.Ada = model.Ada;
                        gayrimenkul.Pafta = model.Pafta;
                        gayrimenkul.Parsel = model.Parsel;
                        gayrimenkul.GayrimenkulNo = model.GayrimenkulNo;
                        gayrimenkul.DosyaNo = model.DosyaNo;
                        gayrimenkul.AracKapasitesi = model.AracKapasitesi;
                        gayrimenkul.Metrekare = model.Metrekare;
                        gayrimenkul.OlusturulmaTarihi = DateTime.Now;
                        gayrimenkul.OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null);
                        gayrimenkul = _gayrimenkulservice.Guncelle(gayrimenkul);
                        

                    }

                    ModelState.AddModelError("LogMessage", "Gayrimenkul Bilgisi Düzenlendi.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("LogMessage", "Gayrimenkul Bilgisi Düzenlenirken Hata Oluştu.");
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("LogMessage", "Gayrimenkul Bilgisi Düzenlenemedi.");
                model.Errors = new List<string>();
                model.Errors.Add(VMErrors.ValidationError);
            }

            return View(model);
        }

        #endregion


    }
}