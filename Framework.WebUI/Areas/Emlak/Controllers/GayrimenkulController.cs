﻿using System;
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
using System.IO;

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
        private IGayrimenkul_DosyaService _gayrimenkulDosyaService;
        private IKira_DurumService _gayrimenkuldurumservice;
        public static GayrimenkulEkleVM _gayrimenkulVM;

        public GayrimenkulController(IGayrimenkulService gayrimenkulservice, IGayrimenkulTurService turService,
             IIlceService ilceService, IMahalleService mahalleService, IGayrimenkulDosya_TurService dosyaTurService, IKira_DurumService gayrimenkuldurumservice, GayrimenkulEkleVM gayrimenkulVM, IGayrimenkul_DosyaService gayrimenkulDosyaService)
        {

            _ilceService = ilceService;
            _mahalleService = mahalleService;
            _turService = turService;
            _gayrimenkulservice = gayrimenkulservice;
            _dosyaTurService = dosyaTurService;
            _gayrimenkuldurumservice = gayrimenkuldurumservice;
            _gayrimenkulVM = gayrimenkulVM;
            _gayrimenkulDosyaService = gayrimenkulDosyaService;
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
            var ilceler = _ilceService.GetirListe().Where(a => a.Il_Id == 6).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(ilceler, "Id", "Ad");
        }

        public SelectList MahalleSelectListGetir(int ilceId)
        {
            var mahalleler = _mahalleService.GetirListe().Where(a => a.Ilce_Id == ilceId).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(mahalleler, "Id", "Ad");
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

            gayrimenkulNo += yil;
            gayrimenkulNo += DateTime.Now.Month + "-";
            model.GayrimenkulNo = gayrimenkulNo + "-" + _gayrimenkulservice.GayrimenkulNoUret(yil);
            model.TurSelectList = TurSelectList();
            model.IlceSelectList = IlceSelectList();
            model.DosyaTurleri = _dosyaTurService.GetirListe();
            model.GayrimenkulDurumSelectList = GayrimenkulDurumSelectList();
            return View(model);
        }


        [HttpGet]
        private int GayrimenkulDosyaEkle(int gayrimenkulId, List<Gayrimenkul_DosyaVM> dosyalar)
        {
            Gayrimenkul_Dosya dosya = new Gayrimenkul_Dosya();
            string dosyaAdi2 = "";
            string filePath = Server.MapPath("~/Dosyalar/Gayrimenkul/");

            if (dosyalar != null)
            {
                foreach (var item in dosyalar)
                {
                    Guid guidDosya = Guid.NewGuid();
                    dosyaAdi2 = item.DosyaAdi.Split('.').Last();
                    dosya.Guid = guidDosya;
                    dosya.GayrimenkulDosyaTur_Id = item.GayrimenkulDosyaTur_Id;
                    dosya.Gayrimenkul_Id = gayrimenkulId;
                    dosya.Ad = guidDosya.ToString() + "." + dosyaAdi2;
                    dosya.OlusturulmaTarihi = DateTime.Now;
                    dosya.OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null);
                    dosya.AktifMi = true;

                    var result = _gayrimenkulDosyaService.Ekle(dosya);

                    if (result.Id > 0)
                    {
                        byte[] fileBytes = Convert.FromBase64String(item.GayrimenkulDosya);
                        System.IO.File.WriteAllBytes(filePath + dosya.Ad, fileBytes);
                    }
                }

                if (dosya.Id > 0)
                    return dosya.Id;
            }

            return 0;
        }

        [Route("ControllerName/GetAgreementToReview/{fileName?}")]
        public ActionResult PdfGoruntule(string DosyaAdi, string DosyaTipi)
        {
            //ECM_FileManager fileManager = new ECM_FileManager();
            //byte[] bytes = fileManager.FileDisplay(null, DosyaAdi);

            string path = Path.Combine(Server.MapPath(@"~/Dosyalar/Gayrimenkul"), DosyaAdi);

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            string resultFileName = string.Format(DosyaAdi, DosyaTipi);

            Response.AppendHeader("Content-Disposition", "inline; filename=" + resultFileName);

            return new FileContentResult(bytes, MimeMapping.GetMimeMapping(path));
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

                    var result = _gayrimenkulservice.Guncelle(gayrimenkul);
                    GayrimenkulDosyaEkle(result.Id, model.GayrimenkulDosyalar);


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
                if (gayrimenkul.Ilce_Id != null)
                    model.MahalleSelectList = MahalleSelectListGetir(Convert.ToInt32(gayrimenkul.Ilce_Id));

                model.Id = gayrimenkul.Id;
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
                        gayrimenkul.Id = gayrimenkul.Id;
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
                    if (gayrimenkul != null)
                        GayrimenkulDosyaEkle(gayrimenkul.Id, model.GayrimenkulDosyalar);

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

        #region Detay
        [HttpGet]
        public ActionResult Detay(Guid guid)
        {
            var gayrimenkul = _gayrimenkulservice.GetirGuid(guid);
            var model = new GayrimenkulDetayVM();
            var turListesi = TurSelectList();
            var ilceListesi = IlceSelectList();
            var mahalleListesi = MahalleSelectListGetir(Convert.ToInt32(gayrimenkul.Ilce_Id));
            var gayrimenkulDurumListesi = GayrimenkulDurumSelectList();
            if (gayrimenkul != null)
            {
                model.Id = gayrimenkul.Id;
                model.Guid = Guid.NewGuid();
                model.GayrimenkulAdi = gayrimenkul.Ad;
                model.GayrimenkulTuru = turListesi.Where(x => x.Value == gayrimenkul.GayrimenkulTur_Id.ToString()).FirstOrDefault().Text;
                model.IlceAdi = ilceListesi.Where(x => x.Value == gayrimenkul.Ilce_Id.ToString()).FirstOrDefault().Text;
                model.MahalleAdi = mahalleListesi.Where(x => x.Value == gayrimenkul.Mahalle_Id.ToString()).FirstOrDefault().Text; ;
                model.GayrimenkulDurum = gayrimenkulDurumListesi.Where(x => x.Value == gayrimenkul.GayrimenkulDurum_Id.ToString()).FirstOrDefault().Text; ;
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
                model.GayrimenkulDosyalar = DosyaVmGetir(gayrimenkul.Id);
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            ModelState.AddModelError("LogMessage", "Gayrimenkul Düzenleme Sayfası Görüntülendi.");
            return View(model);
        }

        private List<Gayrimenkul_DosyaVM> DosyaVmGetir(int gayrimenkulId)
        {
            List<Gayrimenkul_DosyaVM> list = new List<Gayrimenkul_DosyaVM>();

            var gayrimekulDosya = _gayrimenkulDosyaService.GetirGayrimenkulId(gayrimenkulId);
            var dosyaTurListesi = _dosyaTurService.GetirListe();

            foreach (var item in gayrimekulDosya)
            {
                list.Add(new Gayrimenkul_DosyaVM()
                {
                    GayrimenkulDosyaTur = dosyaTurListesi.Where(x => x.Id == item.GayrimenkulDosyaTur_Id).FirstOrDefault().Ad,
                    DosyaAdi = item.Ad,
                    Guid = item.Guid,
                });
            }
            return list;
        }
        #endregion
    }
}