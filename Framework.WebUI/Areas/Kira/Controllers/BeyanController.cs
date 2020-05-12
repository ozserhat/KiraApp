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
using System.Dynamic;
using System.Web.Services.Description;
using System.IO;
using System.Globalization;
using System.Configuration;
using Framework.Entities.ComplexTypes;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    [CustomAuthorize(Roles = "Kira")]
    public class BeyanController : Controller
    {
        #region Constructor

        private IGayrimenkulService _gayrimenkulservice;
        private IBeyanService _beyanService;
        private IBeyanDosya_TurService _dosyaService;
        private IBeyan_DosyaService _beyanDosyaService;
        private IKira_BeyanService _kiraBeyanService;
        private IKira_DurumService _kiraDurumService;
        private IOdemePeriyotTurService _odemePeriyotService;
        private ISicilService _sicilService;
        private IKiraciService _kiraciService;
        private ITahakkukService _tahakkukService;
        private IKiraParametreService _kiraParametreService;
        private IResmiTatillerService _resmiTatilService;
        //private ITahakkukDisServis _tahakkukDisServis;
        public static KiraBeyanEkleVM _beyanVM;
        private IBeyan_TurService _beyanTurService;
        private ISistemParametre_DetayService _parametreService;

        private IIlService _ilService;
        private IIlceService _ilceService;
        private IMahalleService _mahalleService;
        public string DosyaYolu = ConfigurationManager.AppSettings["DosyaYolu"].ToString();


        public BeyanController(IGayrimenkulService gayrimenkulservice,
            IBeyanService beyanService,
            ITahakkukService tahakkukService,
            IKira_BeyanService kiraBeyanService,
            ISicilService sicilService,
            KiraBeyanEkleVM beyanVM,
            ISistemParametre_DetayService parametreService,
            IKira_DurumService kiraDurumService,
            IOdemePeriyotTurService odemePeriyotService,
        IBeyan_TurService beyanTurService,
        IBeyanDosya_TurService dosyaService,
        IBeyan_DosyaService beyanDosyaService,
        IKiraciService kiraciService,
        IIlService ilService,
        IIlceService ilceService,
        IMahalleService mahalleService,
        IKiraParametreService kiraParametreService,
        IResmiTatillerService resmiTatilService
        )
        {
            _gayrimenkulservice = gayrimenkulservice;
            _beyanService = beyanService;
            _kiraBeyanService = kiraBeyanService;
            _sicilService = sicilService;
            _beyanVM = beyanVM;
            _dosyaService = dosyaService;
            _beyanTurService = beyanTurService;
            _parametreService = parametreService;
            _odemePeriyotService = odemePeriyotService;
            _kiraDurumService = kiraDurumService;
            _beyanDosyaService = beyanDosyaService;
            _ilService = ilService;
            _ilceService = ilceService;
            _mahalleService = mahalleService;
            _kiraciService = kiraciService;
            _tahakkukService = tahakkukService;
            _kiraParametreService = kiraParametreService;
            _resmiTatilService = resmiTatilService;
            //_tahakkukDisServis = tahakkukDisServis;
        }
        #endregion

        #region Listeleme


        public ActionResult Index(KiraBeyanRequest request, int? page, int pageSize = 15)
        {
            var model = new KiraBeyanVM();

            var beyanlar = _kiraBeyanService.GetirSorguListe(request);


            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (beyanlar != null)
            {
                model.IlceSelectList = IlceSelectList();
                model.GayrimenkulSelectList = GayrimenkulSelectList();
                model.BeyanTurSelectList = BeyanTurSelectList();
                model.KiraDurumSelectList = KiraDurumSelectList();
                model.OdemePeriyotSelectList = OdemePeriyotSelectList();
                model.BeyanYilSelectList = BeyanYilSelectList();
                model.KdvOraniSelectList = KdvOraniSelectList();
                model.DamgaVergisiDurumSelectList = DamgaVergisiDurumSelectList();
                model.GayrimenkulSelectList = GayrimenkulSelectList();
                model.OdemePeriyotSelectList = OdemePeriyotSelectList();

                model.Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count());
                model.TotalRecordCount = beyanlar.Count();
            }

            return View(model);
        }

        public SelectList IlSelectList()
        {
            var iller = _ilService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList IlceSelectList()
        {
            var ilceler = _ilceService.GetirListe().Where(a => a.Il_Id == 6).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(ilceler, "Id", "Ad");
        }

        [HttpPost]
        public JsonResult MahalleSelectList(int ilceId)
        {
            var mahalleler = _mahalleService.GetirListe().Where(a => a.Ilce_Id == ilceId).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return Json(new { Data = mahalleler, success = true }, JsonRequestBehavior.AllowGet);
        }

        public SelectList GayrimenkulSelectList()
        {
            var iller = _gayrimenkulservice.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList BeyanTurSelectList()
        {
            var turler = _beyanTurService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        public SelectList BeyanYilSelectList()
        {
            var iller = _parametreService.GetirListe(1).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList KdvOraniSelectList()
        {
            var iller = _parametreService.GetirListe(9).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList DamgaVergisiDurumSelectList()
        {
            var iller = _parametreService.GetirListe(9).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Evet",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Hayır",
                                    Value="0"
                                  }
            };

            return new SelectList(newList, "Value", "Text");
        }

        public SelectList KiraDurumSelectList()
        {
            var iller = _kiraDurumService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList OdemePeriyotSelectList()
        {
            var iller = _odemePeriyotService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        #endregion

        #region Metodlar

        void GetirSelectList()
        {
            _beyanVM.Beyan = new BeyanEkleVM();
            _beyanVM.Kiraci = new KiraciEkleVM();
            _beyanVM.Gayrimenkul = new Beyan_GayrimenkulEkleVM();
            _beyanVM.Beyan.BeyanTurSelectList = BeyanTurSelectList();
            _beyanVM.Beyan.BeyanYilSelectList = BeyanYilSelectList();
            _beyanVM.Beyan.KiraDurumSelectList = KiraDurumSelectList();
            _beyanVM.Beyan.OdemePeriyotSelectList = OdemePeriyotSelectList();
            _beyanVM.Beyan.KdvOraniSelectList = KdvOraniSelectList();
            _beyanVM.Beyan.DamgaVergisiDurumSelectList = DamgaVergisiDurumSelectList();
            _beyanVM.Beyan.DosyaTurleri = _dosyaService.GetirListe();
        }

        public ActionResult GetirBeyanTable(KiraBeyanRequest request, int? page, int pageSize = 15)
        {
            var model = new KiraBeyanVM();

            var beyanlar = _kiraBeyanService.GetirSorguListe(request);

            if (beyanlar != null)
            {
                model.PageNumber = page ?? 1;
                model.PageSize = pageSize;
                model.IlceSelectList = IlceSelectList();
                model.GayrimenkulSelectList = GayrimenkulSelectList();
                model.BeyanTurSelectList = BeyanTurSelectList();
                model.KiraDurumSelectList = KiraDurumSelectList();
                model.OdemePeriyotSelectList = OdemePeriyotSelectList();
                model.Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count());
                model.TotalRecordCount = beyanlar.Count();
            }

            return PartialView("_tablePartial", model);
        }

        public ActionResult GetirSicilBilgi(string TcVergiNo)
        {
            string tcNo = "";
            string vergiNo = "";

            if (!string.IsNullOrEmpty(TcVergiNo))
            {
                if (TcVergiNo.Length > 10)
                    tcNo = TcVergiNo;
                else
                    vergiNo = TcVergiNo;

                GetirSelectList();

                var sicilBilgisi = _sicilService.GetirSicilBilgisi(vergiNo, tcNo);

                if (sicilBilgisi != null && sicilBilgisi.SicilNo != null)
                {
                    _beyanVM.Kiraci = new KiraciEkleVM()
                    {
                        SicilNo = long.Parse(sicilBilgisi.SicilNo),
                        VergiNo = long.Parse(sicilBilgisi.VergiNo),
                        Ad = sicilBilgisi.Ad,
                        Soyad = sicilBilgisi.Soyad,
                        Tanim = sicilBilgisi.Tanim,
                        IlAdi = sicilBilgisi.Il,
                        IlceAdi = sicilBilgisi.Ilce,
                        MahalleAdi = sicilBilgisi.Mahalle,
                        AcikAdres = sicilBilgisi.AcikAdres,
                        VergiDairesi = sicilBilgisi.VergiDairesi,
                    };
                }
                else
                {
                    _beyanVM.Kiraci.Errors.Add("Sicil Bilgisi Bulunamadı!!!");
                    ViewData["SicilHata"] = "Sicil Bilgisi Bulunamadı!!!";
                    ModelState.AddModelError("SicilHata", @"Sicil Bilgisi Bulunamadı!!!");
                }
            }

            //return Json(new { Data = _beyanVM, Message = "Sicil Bilgisi Başarıyla Getirildi.", success = true }, JsonRequestBehavior.AllowGet);
            return PartialView("_SicilPartial", _beyanVM.Kiraci);
        }

        public ActionResult GetirGayrimenkulBilgi(string GayrimenkulNo)
        {
            if (!string.IsNullOrEmpty(GayrimenkulNo))
            {
                GetirSelectList();

                var gayrimenkul = _gayrimenkulservice.GetirGayrimenkul(GayrimenkulNo);

                if (gayrimenkul != null)
                {

                    _beyanVM.Gayrimenkul = new Beyan_GayrimenkulEkleVM()
                    {
                        GayrimenkulId = gayrimenkul.Id,
                        GayrimenkulNo = gayrimenkul.GayrimenkulNo,
                        BinaKimlikNo = gayrimenkul.BinaKimlikNo,
                        NumaratajKimlikNo = gayrimenkul.NumaratajKimlikNo,
                        AdresNo = gayrimenkul.AdresNo,
                        DosyaNo = gayrimenkul.DosyaNo,
                        GayrimenkulAdi = gayrimenkul.Ad,
                        GayrimenkulTur_Id = gayrimenkul.GayrimenkulTur_Id,
                        GayrimenkulTur = gayrimenkul.GayrimenkulTur.Ad,
                        Il = gayrimenkul.Mahalleler.Ilceler.Iller.Ad,
                        Ilce = gayrimenkul.Mahalleler.Ilceler.Ad,
                        Mahalle = gayrimenkul.Mahalleler.Ad,
                        Il_Id = gayrimenkul.Il_Id,
                        Ilce_Id = gayrimenkul.Ilce_Id,
                        Mahalle_Id = gayrimenkul.Mahalle_Id,
                        Sokak = gayrimenkul.Sokak,
                        IcKapiNo = gayrimenkul.IcKapiNo,
                        DisKapiNo = gayrimenkul.DisKapiNo,
                        Koordinat = gayrimenkul.Koordinat,
                        Ada = gayrimenkul.Ada,
                        Pafta = gayrimenkul.Pafta,
                        Parsel = gayrimenkul.Parsel,
                        AcikAdres = gayrimenkul.AcikAdres,
                        Metrekare = gayrimenkul.Metrekare,
                        AracKapasitesi = gayrimenkul.AracKapasitesi
                    };

                }
                else
                {
                    ViewData["GayrimenkulHata"] = "Gayrimenkul Bilgisi Bulunamadı!!!";
                    _beyanVM.Gayrimenkul.Errors.Add("Gayrimenkul Bilgisi Bulunamadı!!!");
                    ModelState.AddModelError("GayrimenkulHata", @"Gayrimenkul Bilgisi Bulunamadı!!!");
                }
            }

            return PartialView("_GayrimenkulPartial", _beyanVM.Gayrimenkul);

            //return View("Ekle", _beyanVM);
            //return Json(new { Data = _beyanVM, Message = "Gayrimenkul Bilgisi Başarıyla Getirildi.", success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetirBeyanBilgi()
        {
            GetirSelectList();

            return PartialView("_BeyanPartial", _beyanVM);
        }

        public ActionResult GetirDosyaBilgi()
        {
            GetirSelectList();

            return PartialView("_DosyaPartial", _beyanVM);
        }

        [HttpGet]
        private int BeyanDosyaEkle(int beyanId, List<Beyan_DosyaVM> dosyalar)
        {
            Beyan_Dosya dosya = new Beyan_Dosya();
            string dosyaAdi2 = "";
            string filePath = Server.MapPath("~/Dosyalar/Beyan/");

            if (dosyalar != null)
            {
                foreach (var item in dosyalar)
                {
                    Guid guidDosya = Guid.NewGuid();
                    dosyaAdi2 = item.DosyaAdi.Split('.').Last();
                    dosya.Guid = guidDosya;
                    dosya.BeyanDosya_Tur_Id = item.BeyanDosyaTur_Id;
                    dosya.Beyan_Id = beyanId;
                    dosya.Ad = guidDosya.ToString() + "." + dosyaAdi2;
                    dosya.OlusturulmaTarihi = DateTime.Now;
                    dosya.OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null);
                    dosya.AktifMi = true;

                    var result = _beyanDosyaService.Ekle(dosya);

                    if (result.Id > 0)
                    {
                        byte[] fileBytes = Convert.FromBase64String(item.BeyanDosya);
                        System.IO.File.WriteAllBytes(filePath + dosya.Ad, fileBytes);
                    }
                }

                if (dosya.Id > 0)
                    return dosya.Id;
            }

            return 0;
        }

        [HttpGet]
        private int BeyanSicilEkle(KiraciEkleVM kiraciBilgi)
        {
            if (kiraciBilgi != null)
            {
                int IlId = 0;
                int IlceId = 0;
                int MahalleId = 0;

                var il = _ilService.GetirAdaGore(kiraciBilgi.IlAdi);
                var ilce = _ilceService.GetirAdaGore(kiraciBilgi.IlceAdi);
                var mahalle = _mahalleService.GetirAdaGore(kiraciBilgi.MahalleAdi);

                if (il != null)
                    IlId = il.Id;
                if (ilce != null)
                    IlceId = ilce.Id;

                if (mahalle != null)
                    MahalleId = mahalle.Id;

                Kiraci kiraci = new Kiraci()
                {
                    Guid = Guid.NewGuid(),
                    KiraciTur_Id = (kiraciBilgi.VergiNo != null ? 1 : 2),
                    Il_Id = IlId,
                    Ilce_Id = IlceId,
                    Mahalle_Id = MahalleId,
                    SicilNo = kiraciBilgi.SicilNo.Value,
                    VergiNo = (kiraciBilgi.VergiNo.HasValue ? kiraciBilgi.VergiNo : null),
                    TcKimlikNo = (kiraciBilgi.TcKimlikNo.HasValue ? kiraciBilgi.TcKimlikNo : null),
                    Ad = kiraciBilgi.Ad,
                    Soyad = kiraciBilgi.Soyad,
                    Tanim = kiraciBilgi.Tanim,
                    IlAdi = kiraciBilgi.IlAdi,
                    IlceAdi = kiraciBilgi.IlceAdi,
                    MahalleAdi = kiraciBilgi.MahalleAdi,
                    AcikAdres = kiraciBilgi.AcikAdres,
                    VergiDairesi = kiraciBilgi.VergiDairesi,
                    OlusturulmaTarihi = DateTime.Now,
                    OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = true
                };

                var result = _kiraciService.Ekle(kiraci);

                if (result.Id > 0)
                    return result.Id;
            }

            return 0;
        }

        [HttpGet]
        private int BeyanEkle(BeyanEkleVM beyanBilgi)
        {
            if (beyanBilgi != null)
            {
                CultureInfo cultures = new CultureInfo("en-US");


                Beyan beyan = new Beyan()
                {
                    Guid = Guid.NewGuid(),
                    BeyanTur_Id = beyanBilgi.BeyanTur_Id,
                    KiraDurum_Id = beyanBilgi.KiraDurum_Id.Value,
                    OdemePeriyotTur_Id = beyanBilgi.OdemePeriyotTur_Id.Value,
                    BeyanNo = beyanBilgi.BeyanNo,
                    TeminatNo = int.Parse(beyanBilgi.TeminatNo),
                    EncumenKararNo = beyanBilgi.EncumenKararNo.Value,
                    NoterSozlesmeNo = int.Parse(beyanBilgi.NoterSozlesmeNo),
                    BaslangicTaksitNo = beyanBilgi.BaslangicTaksitNo.Value,
                    BeyanYil = beyanBilgi.BeyanYil.Value,
                    TeminatTarihi = beyanBilgi.TeminatTarihi,
                    BeyanKapatmaTarihi = beyanBilgi.BeyanKapatmaTarihi,
                    BeyanTarihi = beyanBilgi.BeyanTarihi,
                    IhaleEncumenTarihi = beyanBilgi.IhaleEncumenTarihi,
                    KiraBaslangicTarihi = beyanBilgi.KiraBaslangicTarihi,
                    SozlesmeBitisTarihi = beyanBilgi.SozlesmeBitisTarihi,
                    SozlesmeTarihi = beyanBilgi.SozlesmeTarihi,
                    SozlesmeSuresi = beyanBilgi.SozlesmeSuresi,
                    Aciklama = beyanBilgi.BeyanAciklama,
                    IhaleTutari = Convert.ToDecimal(beyanBilgi.IhaleTutari, cultures),
                    KiraTutari = Convert.ToDecimal(beyanBilgi.KiraTutari, cultures),
                    KalanAy = beyanBilgi.KalanAy.Value,
                    MusadeliGunSayisi = beyanBilgi.MusadeliGunSayisi,
                    KullanimAlani = beyanBilgi.KullanimAlani.Value,
                    Kdv = beyanBilgi.Kdv.Value,
                    DamgaAlinsinMi = (beyanBilgi.DamgaAlinsinMi == "1" ? true : false),
                    OlusturulmaTarihi = DateTime.Now,
                    OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = true
                };

                var result = _beyanService.Ekle(beyan);

                if (result.Id > 0)
                    return result.Id;
            }

            return 0;
        }

        [HttpGet]
        private List<Tahakkuk> TahakkukStandart()
        {
            List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();
            return tahakkukListe;
        }

        [HttpGet]
        private int KiraBeyanEkle(int Beyan_Id, int Kiraci_Id, int Gayrimenkul_Id)
        {
            Kira_Beyan kiraBeyan = new Kira_Beyan()
            {
                Beyan_Id = Beyan_Id,
                Gayrimenkul_Id = Gayrimenkul_Id,
                Kiraci_Id = Kiraci_Id,
                OlusturulmaTarihi = DateTime.Now,
                OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null)
            };

            var result = _kiraBeyanService.Ekle(kiraBeyan);

            if (result.Id > 0)
                return result.Id;

            return 0;
        }

        [Route("ControllerName/GetAgreementToReview/{fileName?}")]
        public ActionResult PdfGoruntule(string DosyaAdi, string DosyaTipi)
        {
            //ECM_FileManager fileManager = new ECM_FileManager();
            //byte[] bytes = fileManager.FileDisplay(null, DosyaAdi);

            string path = Path.Combine(Server.MapPath(DosyaYolu), DosyaAdi);

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            string resultFileName = string.Format(DosyaAdi, DosyaTipi);

            Response.AppendHeader("Content-Disposition", "inline; filename=" + resultFileName);

            return new FileContentResult(bytes, MimeMapping.GetMimeMapping(path));
        }

        [HttpPost]
        public JsonResult GetirDetayTable(int BeyanId)
        {
            var model = new KiraBeyanVM();

            model.TahakkukDetay = new List<TahakkukVM>();

            int kiraBeyanId = _kiraBeyanService.GetirBeyan(BeyanId).Id;
            var tahakkukBilgi = _tahakkukService.GetirListe(kiraBeyanId);

            if (tahakkukBilgi != null)
            {
                #region Tahakkuk
                foreach (var item in tahakkukBilgi)
                {
                    model.TahakkukDetay.Add(new TahakkukVM()
                    {
                        KiraBeyan_Id = item.KiraBeyan_Id,
                        TaksitNo = item.TaksitSayisi.Value,
                        Tutar = item.Tutar.Value,
                        VadeTarihi = item.VadeTarihi.Value,
                        Aciklama = item.Aciklama,
                        OdemeDurumu = item.OdemeDurumu.Value
                    });
                }
                #endregion
            }

            return Json(new { data = model.TahakkukDetay, success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            GetirSelectList();

            string beyanNo = "BYN-";
            int yil = DateTime.Now.Year;

            beyanNo += yil;
            beyanNo += DateTime.Now.Month;
            beyanNo += DateTime.Now.Day;

            _beyanVM.Beyan.BeyanNo = beyanNo + "-" + _beyanService.BeyanNoUret(yil);

            ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme Sayfası Görüntülendi.");
            return View(_beyanVM);
        }

        [HttpPost]
        public JsonResult KiraBeyanEkle(KiraBeyanEkleVM kiraBeyanModel)
        {

            int sicilId, beyanId, gayrimenkulId, beyanDosyaId, kiraBeyanId, AySayisi;

            sicilId = BeyanSicilEkle(kiraBeyanModel.Kiraci);

            gayrimenkulId = kiraBeyanModel.Gayrimenkul.GayrimenkulId;

            beyanId = BeyanEkle(kiraBeyanModel.Beyan);

            beyanDosyaId = BeyanDosyaEkle(beyanId, kiraBeyanModel.BeyanDosyalar);

            kiraBeyanId = KiraBeyanEkle(beyanId, sicilId, gayrimenkulId);


            var odemePeriyorTur = _odemePeriyotService.Getir(kiraBeyanModel.Beyan.OdemePeriyotTur_Id.Value);
            var beyanTur = _beyanTurService.Getir(kiraBeyanModel.Beyan.BeyanTur_Id);

            var kiraParametre = _kiraParametreService.GetirBeyanYil(kiraBeyanModel.Beyan.BeyanYil.Value);

            int parametreKod = 0;
            string parametreAciklama = "";

            if (beyanTur != null && beyanTur.Kod.Equals(1))
            {
                parametreKod = kiraParametre.KiraTarifeKodu.Value;
                parametreAciklama = kiraParametre.KiraTarifeAciklama;
            }

            if (beyanTur != null && beyanTur.Kod.Equals(2))
            {
                parametreKod = kiraParametre.OtoparkTarifeKodu.Value;
                parametreAciklama = kiraParametre.OtoparkTarifeAciklama;
            }

            if (beyanTur != null && beyanTur.Kod.Equals(3))
            {
                parametreKod = kiraParametre.EcrimisilTarifeKodu.Value;
                parametreAciklama = kiraParametre.EcrimisilTarifeAciklama;
            }

            if (odemePeriyorTur.OdemeAySayisi.HasValue)
            {
                Int32.TryParse(odemePeriyorTur.OdemeAySayisi.ToString(), out AySayisi);

                List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();

                decimal kiraTutar, karakHarciTutar, teminatTutar, damgaVergisiTutar, kdvTutar;

                karakHarciTutar = (decimal.Parse(kiraBeyanModel.Beyan.IhaleTutari.Replace('.', ',')) * kiraParametre.KararHarciOran.Value);
                teminatTutar = (decimal.Parse(kiraBeyanModel.Beyan.IhaleTutari.Replace('.', ',')) * kiraParametre.TeminatOran.Value);
                damgaVergisiTutar = (decimal.Parse(kiraBeyanModel.Beyan.IhaleTutari.Replace('.', ',')) * kiraParametre.DamgaOran.Value);
                kdvTutar = ((kiraBeyanModel.Beyan.Kdv.HasValue && kiraBeyanModel.Beyan.Kdv.Value > 0) ? (decimal.Parse(kiraBeyanModel.Beyan.KiraTutari.Replace('.', ',')) * kiraBeyanModel.Beyan.Kdv.Value / 100) : 0);
                kiraTutar = decimal.Parse(kiraBeyanModel.Beyan.KiraTutari);

                #region StandartTahakkukVeri
                DateTime vadeTarih = _resmiTatilService.TatilGunuKontrol(kiraBeyanModel.Beyan.KiraBaslangicTarihi.Value.AddDays(kiraBeyanModel.Beyan.MusadeliGunSayisi.Value));

                tahakkukListe.Add(
                        new Tahakkuk()
                        {
                            Guid = Guid.NewGuid(),
                            KiraBeyan_Id = kiraBeyanId,
                            KiraParametre_Id = kiraParametre.Id,
                            ServisSonucTahakkukId = null,
                            KiraParametreKodu = kiraParametre.KararHarciTarifeKodu.Value,
                            TahakkukTarihi = DateTime.Now,
                            VadeTarihi = vadeTarih,
                            TaksitSayisi = 1,
                            Tutar = karakHarciTutar,
                            KalanBorcTutari = null,
                            OdemeDurumu = false,
                            Aciklama = kiraParametre.KararHarciTarifeAciklama,
                            OlusturulmaTarihi = DateTime.Now,
                            OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                            AktifMi = true
                        }
                        );
                TahakkukEkleVm tahakkukEkleVm = new TahakkukEkleVm()
                {
                    GelirId = kiraParametre.KararHarciTarifeKodu.Value,
                    SicilNo = int.Parse(tahakkukListe.FirstOrDefault().Kira_Beyani.Kiracilar.SicilNo.ToString()),


                };

                //var result1 = _tahakkukDisServis.TahakkukOlustur(tahakkukEkleVm);
                tahakkukListe.Add(
                new Tahakkuk()
                {
                    Guid = Guid.NewGuid(),
                    KiraBeyan_Id = kiraBeyanId,
                    KiraParametre_Id = kiraParametre.Id,
                    ServisSonucTahakkukId = null,
                    KiraParametreKodu = kiraParametre.TeminatTarifeKodu.Value,
                    TahakkukTarihi = DateTime.Now,
                    VadeTarihi = vadeTarih,
                    TaksitSayisi = 1,
                    Tutar = teminatTutar,
                    KalanBorcTutari = null,
                    OdemeDurumu = false,
                    Aciklama = kiraParametre.TeminatTarifeAciklama,
                    OlusturulmaTarihi = DateTime.Now,
                    OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = true
                }
                );

                tahakkukListe.Add(
                new Tahakkuk()
                {
                    Guid = Guid.NewGuid(),
                    KiraBeyan_Id = kiraBeyanId,
                    KiraParametre_Id = kiraParametre.Id,
                    ServisSonucTahakkukId = null,
                    KiraParametreKodu = kiraParametre.DamgaTarifeKodu.Value,
                    TahakkukTarihi = DateTime.Now,
                    VadeTarihi = vadeTarih,
                    TaksitSayisi = 1,
                    Tutar = damgaVergisiTutar,
                    KalanBorcTutari = null,
                    OdemeDurumu = false,
                    Aciklama = kiraParametre.DamgaTarifeAciklama,
                    OlusturulmaTarihi = DateTime.Now,
                    OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = true
                });
                #endregion

                var dateSon = _resmiTatilService.TatilGunuKontrol(kiraBeyanModel.Beyan.KiraBaslangicTarihi.Value.AddDays(kiraBeyanModel.Beyan.MusadeliGunSayisi.Value));

                for (int i = 0; i < AySayisi; i++)
                {
                    tahakkukListe.Add(new Tahakkuk()
                    {
                        Guid = Guid.NewGuid(),
                        KiraBeyan_Id = kiraBeyanId,
                        KiraParametre_Id = kiraParametre.Id,
                        ServisSonucTahakkukId = null,
                        KiraParametreKodu = parametreKod,
                        TahakkukTarihi = DateTime.Now,
                        VadeTarihi = dateSon,
                        TaksitSayisi = i + 1,
                        Tutar = kiraTutar,
                        KalanBorcTutari = null,
                        OdemeDurumu = false,
                        Aciklama = parametreAciklama,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    });

                    tahakkukListe.Add(new Tahakkuk()
                    {
                        Guid = Guid.NewGuid(),
                        KiraBeyan_Id = kiraBeyanId,
                        KiraParametre_Id = kiraParametre.Id,
                        ServisSonucTahakkukId = null,
                        KiraParametreKodu = kiraParametre.KdvTarifeKodu.Value,
                        TahakkukTarihi = DateTime.Now,
                        VadeTarihi = dateSon,
                        TaksitSayisi = i + 1,
                        Tutar = kdvTutar,
                        KalanBorcTutari = null,
                        OdemeDurumu = false,
                        Aciklama = kiraParametre.KdvTarifeAciklama,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    });


                    if (AySayisi == 4)
                        dateSon = _resmiTatilService.TatilGunuKontrol(dateSon.AddMonths(3));
                    else
                        dateSon = _resmiTatilService.TatilGunuKontrol(dateSon.AddMonths(1));
                }

                if (tahakkukListe != null && tahakkukListe.Count > 0)
                    _tahakkukService.Ekle(tahakkukListe);
            }

            ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme İşlemi Gerçekleşti.");

            if (kiraBeyanId > 0)
                return Json(new { Data = _beyanVM, Message = "Kira Beyan Ekleme İşlemi Başarıyla Getirildi.", success = true }, JsonRequestBehavior.AllowGet);

            return Json(new { Message = "Kira Beyan Ekleme İşlemi Gerçekleştirilemedi.", success = false }, JsonRequestBehavior.AllowGet);
        }

        public KiraciEkleVM GetirSicil(string TcVergiNo)
        {
            string tcNo = "";
            string vergiNo = "";

            if (TcVergiNo != null)
            {
                if (TcVergiNo.Length > 10)
                    tcNo = TcVergiNo;
                else
                    vergiNo = TcVergiNo;

                GetirSelectList();

                var sicilBilgisi = _sicilService.GetirSicilBilgisi(vergiNo, tcNo);

                if (sicilBilgisi != null && sicilBilgisi.SicilNo != null)
                {
                    _beyanVM.Kiraci = new KiraciEkleVM()
                    {
                        SicilNo = long.Parse(sicilBilgisi.SicilNo),
                        VergiNo = long.Parse(sicilBilgisi.VergiNo),
                        Ad = sicilBilgisi.Ad,
                        Soyad = sicilBilgisi.Soyad,
                        Tanim = sicilBilgisi.Tanim,
                        IlAdi = sicilBilgisi.Il,
                        IlceAdi = sicilBilgisi.Ilce,
                        MahalleAdi = sicilBilgisi.Mahalle,
                        AcikAdres = sicilBilgisi.AcikAdres,
                        VergiDairesi = sicilBilgisi.VergiDairesi,
                    };
                }
                else
                {
                    _beyanVM.Kiraci.Errors.Add("Sicil Bilgisi Bulunamadı!!!");
                    ViewData["SicilHata"] = "Sicil Bilgisi Bulunamadı!!!";
                    ModelState.AddModelError("SicilHata", @"Sicil Bilgisi Bulunamadı!!!");
                }
            }

            //return Json(new { Data = _beyanVM, Message = "Sicil Bilgisi Başarıyla Getirildi.", success = true }, JsonRequestBehavior.AllowGet);
            return _beyanVM.Kiraci;
        }

        public Beyan_GayrimenkulEkleVM GetirGayrimenkul(string GayrimenkulNo)
        {
            if (!string.IsNullOrEmpty(GayrimenkulNo))
            {
                GetirSelectList();

                var gayrimenkul = _gayrimenkulservice.GetirGayrimenkul(GayrimenkulNo);

                if (gayrimenkul != null)
                {
                    _beyanVM.Gayrimenkul = new Beyan_GayrimenkulEkleVM()
                    {
                        GayrimenkulId = gayrimenkul.Id,
                        GayrimenkulNo = gayrimenkul.GayrimenkulNo,
                        BinaKimlikNo = gayrimenkul.BinaKimlikNo,
                        NumaratajKimlikNo = gayrimenkul.NumaratajKimlikNo,
                        AdresNo = gayrimenkul.AdresNo,
                        DosyaNo = gayrimenkul.DosyaNo,
                        GayrimenkulAdi = gayrimenkul.Ad,
                        GayrimenkulTur_Id = gayrimenkul.GayrimenkulTur_Id,
                        GayrimenkulTur = gayrimenkul.GayrimenkulTur.Ad,
                        Il = gayrimenkul.Mahalleler.Ilceler.Iller.Ad,
                        Ilce = gayrimenkul.Mahalleler.Ilceler.Ad,
                        Mahalle = gayrimenkul.Mahalleler.Ad,
                        Il_Id = gayrimenkul.Il_Id,
                        Ilce_Id = gayrimenkul.Ilce_Id,
                        Mahalle_Id = gayrimenkul.Mahalle_Id,
                        Sokak = gayrimenkul.Sokak,
                        IcKapiNo = gayrimenkul.IcKapiNo,
                        DisKapiNo = gayrimenkul.DisKapiNo,
                        Koordinat = gayrimenkul.Koordinat,
                        Ada = gayrimenkul.Ada,
                        Pafta = gayrimenkul.Pafta,
                        Parsel = gayrimenkul.Parsel,
                        AcikAdres = gayrimenkul.AcikAdres,
                        Metrekare = gayrimenkul.Metrekare,
                        AracKapasitesi = gayrimenkul.AracKapasitesi
                    };
                }
                else
                {
                    ViewData["GayrimenkulHata"] = "Gayrimenkul Bilgisi Bulunamadı!!!";
                    _beyanVM.Gayrimenkul.Errors.Add("Gayrimenkul Bilgisi Bulunamadı!!!");
                    ModelState.AddModelError("GayrimenkulHata", @"Gayrimenkul Bilgisi Bulunamadı!!!");
                }
            }

            return _beyanVM.Gayrimenkul;
        }

        public BeyanDetayVM GetirBeyan(int BeyanId)
        {
            if (BeyanId > 0)
            {
                var beyan = _kiraBeyanService.GetirBeyan(BeyanId);

                if (beyan != null)
                {
                    _beyanVM.BeyanDetay = new BeyanDetayVM()
                    {
                        BeyanTur_Id = beyan.Beyanlar.BeyanTur_Id,
                        KiraDurum_Id = beyan.Beyanlar.KiraDurum_Id,
                        OdemePeriyotTur_Id = beyan.Beyanlar.OdemePeriyotTur_Id,
                        BeyanTuru = beyan.Beyanlar.BeyanTur.Ad,
                        BeyanNo = beyan.Beyanlar.BeyanNo,
                        TeminatNo = beyan.Beyanlar.TeminatNo.ToString(),
                        EncumenKararNo = beyan.Beyanlar.EncumenKararNo,
                        NoterSozlesmeNo = beyan.Beyanlar.NoterSozlesmeNo.ToString(),
                        BaslangicTaksitNo = beyan.Beyanlar.BaslangicTaksitNo,
                        BeyanAciklama = beyan.Beyanlar.Aciklama,
                        BeyanKapatmaTarihi = beyan.Beyanlar.BeyanKapatmaTarihi,
                        BeyanTarihi = beyan.Beyanlar.BeyanTarihi,
                        BeyanYil = beyan.Beyanlar.BeyanYil,
                        IhaleEncumenTarihi = beyan.Beyanlar.IhaleEncumenTarihi,
                        TeminatTarihi = beyan.Beyanlar.TeminatTarihi,
                        KiraBaslangicTarihi = beyan.Beyanlar.KiraBaslangicTarihi,
                        SozlesmeBitisTarihi = beyan.Beyanlar.SozlesmeBitisTarihi,
                        SozlesmeTarihi = beyan.Beyanlar.SozlesmeTarihi,
                        IhaleTutari = beyan.Beyanlar.IhaleTutari.ToString(),
                        KiraTutari = beyan.Beyanlar.KiraTutari.ToString(),
                        SozlesmeSuresi = beyan.Beyanlar.SozlesmeSuresi,
                        Kdv = beyan.Beyanlar.Kdv,
                        MusadeliGunSayisi = beyan.Beyanlar.MusadeliGunSayisi,
                        KalanAy = beyan.Beyanlar.KalanAy,
                        KullanimAlani = beyan.Beyanlar.KullanimAlani,
                        OdemePeriyotu = beyan.Beyanlar.OdemePeriyotTur.Ad,
                        KiraDurumu = beyan.Beyanlar.KiraDurum.Ad,
                        DamgaAlinsinMi = (beyan.Beyanlar.DamgaAlinsinMi == true ? "Evet" : "Hayır"),
                        AktifMi = beyan.Beyanlar.AktifMi.Value,
                    };
                }
                else
                {
                    ViewData["BeyanHata"] = "Beyan Bilgisi Bulunamadı!!!";
                    _beyanVM.Beyan.Errors.Add("Beyan Bilgisi Bulunamadı!!!");
                    ModelState.AddModelError("Beyan", @"Gayrimenkul Bilgisi Bulunamadı!!!");
                }
            }

            return _beyanVM.BeyanDetay;
        }

        public List<Beyan_DosyaVM> GetirBeyanDosyalar(int BeyanId)
        {
            if (BeyanId > 0)
            {
                var dosyalar = _beyanDosyaService.GetirBeyanId(BeyanId);


                _beyanVM.BeyanDetayDosyalar = new List<Beyan_DosyaVM>();


                if (dosyalar != null)
                {
                    foreach (var item in dosyalar)
                    {
                        _beyanVM.BeyanDetayDosyalar.Add(new Beyan_DosyaVM()
                        {
                            Id = item.Id,
                            Beyan_Id = item.Beyan_Id,
                            BeyanDosyaTur_Id = item.BeyanDosyaTurleri.Id,
                            BeyanDosyaTur = item.BeyanDosyaTurleri.Ad,
                            DosyaAdi = item.Ad,
                        });
                    }
                }
                else
                {
                    ViewData["BeyanHata"] = "Beyan Dosya Bilgisi Bulunamadı!!!";
                    _beyanVM.Beyan.Errors.Add("Beyan Bilgisi Bulunamadı!!!");
                    ModelState.AddModelError("Beyan", @"Gayrimenkul Bilgisi Bulunamadı!!!");
                }
            }

            return _beyanVM.BeyanDetayDosyalar;
        }


        #endregion

        #region BeyanDetay
        [HttpGet]
        public ActionResult BeyanDetay(KiraBeyanRequest request)
        {
            var model = new KiraBeyanDetayVM();

            var kiraBeyan = _kiraBeyanService.GetirSorguListe(request).FirstOrDefault();

            if (kiraBeyan != null)
            {
                var beyanDosya = _beyanDosyaService.GetirListe().Where(a => a.Beyan_Id == kiraBeyan.Beyan_Id).ToList();

                model.Kiraci = GetirSicil(kiraBeyan.Kiracilar.VergiNo.ToString());
                model.Gayrimenkul = GetirGayrimenkul(kiraBeyan.Gayrimenkuller.GayrimenkulNo);
                model.Beyan = GetirBeyan(kiraBeyan.Beyanlar.Id);
                model.BeyanDosyalar = GetirBeyanDosyalar(kiraBeyan.Beyanlar.Id);
                model.Tahakkuklar = _tahakkukService.GetirListe(kiraBeyan.Id);
            }

            return View(model);
        }
        #endregion
    }
}