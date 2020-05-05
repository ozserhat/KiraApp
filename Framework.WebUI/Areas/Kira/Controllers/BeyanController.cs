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
        public static KiraBeyanEkleVM _beyanVM;
        private IBeyan_TurService _beyanTurService;
        private ISistemParametre_DetayService _parametreService;
        dynamic beyanDynamicModel;

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
        IBeyan_DosyaService beyanDosyaService
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
            beyanDynamicModel = new ExpandoObject();
        }
        #endregion
        // GET: Kira/Beyan
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var beyanlar = _kiraBeyanService.GetirListe();

            var model = new KiraBeyanVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (beyanlar != null)
            {
                model.Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count());
                model.TotalRecordCount = beyanlar.Count();
            }

            return View(model);
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

        #region SicilSorgulama

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

            beyanDynamicModel.Kiraci = _beyanVM.Kiraci;
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

            beyanDynamicModel.Gayrimenkul = _beyanVM.Gayrimenkul;
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
        public ActionResult KiraBeyanEkle(KiraBeyanEkleVM kiraBeyanModel)
        {
            ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme Sayfası Görüntülendi.");
            return View(_beyanVM);
        }

        #endregion
    }
}