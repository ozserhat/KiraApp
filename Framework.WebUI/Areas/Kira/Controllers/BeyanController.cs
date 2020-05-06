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

        public static KiraBeyanEkleVM _beyanVM;
        private IBeyan_TurService _beyanTurService;
        private ISistemParametre_DetayService _parametreService;

        private IIlService _ilService;
        private IIlceService _ilceService;
        private IMahalleService _mahalleService;


        public BeyanController(IGayrimenkulService gayrimenkulservice,
            IBeyanService beyanService,
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
        IMahalleService mahalleService
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
        }
        #endregion
        // GET: Kira/Beyan
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

            foreach (var item in dosyalar)
            {
                Guid guidDosya = Guid.NewGuid();
                dosyaAdi2 = item.DosyaAdi.Split('.').Last();
                dosya.Guid = guidDosya;
                dosya.BeyanDosya_Tur_Id = int.Parse(item.BeyanDosyaTur_Id);
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

            return 0;
        }

        [HttpGet]
        private int BeyanSicilEkle(KiraciEkleVM kiraciBilgi)
        {
            if (kiraciBilgi != null)
            {
                int IlId, IlceId, MahalleId;

                IlId = _ilService.GetirAdaGore(kiraciBilgi.IlAdi).Id;
                IlceId = _ilceService.GetirAdaGore(kiraciBilgi.IlceAdi).Id;
                MahalleId = _mahalleService.GetirAdaGore(kiraciBilgi.MahalleAdi).Id;

                Kiraci kiraci = new Kiraci()
                {
                    Guid = Guid.NewGuid(),
                    KiraciTur_Id = (kiraciBilgi.VergiNo != null ? 1 : 2),
                    Il_Id = IlId,
                    Ilce_Id = IlceId,
                    Mahalle_Id = MahalleId,
                    SicilNo = kiraciBilgi.SicilNo.Value,
                    VergiNo = kiraciBilgi.VergiNo,
                    TcKimlikNo = kiraciBilgi.TcKimlikNo,
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
            int sicilId, beyanId, gayrimenkulId, beyanDosyaId, kiraBeyanId;

            sicilId = BeyanSicilEkle(kiraBeyanModel.Kiraci);

            gayrimenkulId = kiraBeyanModel.Gayrimenkul.GayrimenkulId;

            beyanId = BeyanEkle(kiraBeyanModel.Beyan);

            beyanDosyaId = BeyanDosyaEkle(beyanId, kiraBeyanModel.BeyanDosyalar);

            kiraBeyanId = KiraBeyanEkle(beyanId, sicilId, gayrimenkulId);

            ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme İşlemi Gerçekleşti.");

            if (kiraBeyanId > 0)
                return Json(new { Data = _beyanVM, Message = "Kira Beyan Ekleme İşlemi Başarıyla Getirildi.", success = true }, JsonRequestBehavior.AllowGet);

            return Json(new { Message = "Kira Beyan Ekleme İşlemi Gerçekleştirilemedi.", success = false }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}