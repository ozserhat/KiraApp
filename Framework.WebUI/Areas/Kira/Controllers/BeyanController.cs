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
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using HtmlAgilityPack;
using Framework.Entities.Enums;

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
        private ITahakkukDisServis _tahakkukDisServis;
        public static KiraBeyanEkleVM _beyanVM;
        private IBeyan_TurService _beyanTurService;
        private ISistemParametre_DetayService _parametreService;
        private IGayrimenkulTurService _gayrimenkulTurService;
        private IIlService _ilService;
        private IIlceService _ilceService;
        private IMahalleService _mahalleService;
        private IUserService _userService;
        private IBeyan_UfeOranService _ufeOranService;

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
        IResmiTatillerService resmiTatilService,
        ITahakkukDisServis tahakkukDisServis,
        IGayrimenkulTurService gayrimenkulTurService,
        IUserService userService,
        IBeyan_UfeOranService ufeOranService
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
            _tahakkukDisServis = tahakkukDisServis;
            _gayrimenkulTurService = gayrimenkulTurService;
            //_tahakkukDisServis = tahakkukDisServis;
            _userService = userService;
            _ufeOranService = ufeOranService;
        }
        #endregion

        #region Listeleme

        public ActionResult GayrimenkulSorgula(GayrimenkulBeyanRequest request, int? page, int pageSize = 15)
        {
            var model = new GayrimenkulBeyanVM();

            var beyanlar = _kiraBeyanService.GetirSorguListeGayrimenkul(request);


            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (beyanlar != null)
            {
                model.IlceSelectList = IlceSelectList();
                model.IlSelectList = IlSelectList();
                model.GayrimenkulTuruSelectList = GayrimenkulTuruSelectList();

                model.KiraBeyanVm = new KiraBeyanVM();
                model.KiraBeyanVm.Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count());
                model.TotalRecordCount = model.KiraBeyanVm.Beyanlar.Count();
            }

            return View(model);
        }

        public ActionResult SicilSorgula(SicilBeyanRequest request, int? page, int pageSize = 15)
        {
            var model = new SicilBeyanVM();

            var beyanlar = _kiraBeyanService.GetirSorguListeSicil(request);


            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (beyanlar != null)
            {
                model.IlceSelectList = IlceSelectList();
                model.IlSelectList = IlSelectList();
                model.KiraBeyanVm = new KiraBeyanVM();
                model.KiraBeyanVm.Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count());
                model.TotalRecordCount = model.KiraBeyanVm.Beyanlar.Count();
            }

            return View(model);
        }

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

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle(Guid? id)
        {
            GetirSelectList();
            if (id != null)
            {
                _beyanVM = BeyanGuncelleVeriGetir((Guid)id);
                ViewBag.GuidId = id;
                return View(_beyanVM);
            }

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
            int AySayisi;

            bool beyanEkleSonuc = false;

            #region Beyan Pasife Alma İşlemi
            if (kiraBeyanModel.Beyan.Id > 0)
            {
                //Tahakkuk kaydı varsa ödenen güncellenmesine izin verilmeyecek.
                BeyanPasifeAl(kiraBeyanModel);
                //Beyan Pasif,Tahakkuklar,KiraBeyanEkle pasife alınacak.
            }
            #endregion

            #region Gayrimenkul,Sicil,Beyan Ekleme İşlemleri

            kiraBeyanModel.Gayrimenkul_Id = kiraBeyanModel.Gayrimenkul.GayrimenkulId;

            kiraBeyanModel.Kiraci_Id = BeyanSicilEkle(kiraBeyanModel.Kiraci);

            kiraBeyanModel.Beyan.AktifMi = (int)EnmIslemDurumu.Aktif;

            kiraBeyanModel.Beyan_Id = BeyanEkle(kiraBeyanModel.Beyan);

            #endregion

            if (kiraBeyanModel.Beyan_Id > 0 && kiraBeyanModel.Kiraci_Id > 0 && kiraBeyanModel.Gayrimenkul_Id > 0)
            {
                BeyanDosyaEkle(kiraBeyanModel.Beyan_Id, kiraBeyanModel.BeyanDosyalar);

                kiraBeyanModel.KiraBeyan_Id = KiraBeyanEkle(kiraBeyanModel.Beyan_Id, kiraBeyanModel.Kiraci_Id, kiraBeyanModel.Gayrimenkul_Id);

                #region Beyan Parametreleri Doldurma

                AySayisi = GetirOdemeAySayisi(kiraBeyanModel.Beyan.OdemePeriyotTur_Id.Value);

                var kiraParametre = GetirParametreDetay(kiraBeyanModel.Beyan);

                #endregion

                if (AySayisi > 0)
                {
                    List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();

                    var standart = StandartTahakkukEkle(kiraBeyanModel.KiraBeyan_Id, kiraBeyanModel.Beyan, kiraParametre);

                    if (standart != null)
                        tahakkukListe.AddRange(standart);

                    if (tahakkukListe != null && tahakkukListe.Count > 0)
                    {
                        var tahakkuk = TahakkukSatirEkle(kiraBeyanModel.KiraBeyan_Id, AySayisi, kiraBeyanModel.Beyan, kiraBeyanModel.Gayrimenkul, kiraParametre);

                        if (tahakkuk != null)
                            tahakkukListe.AddRange(tahakkuk);

                        beyanEkleSonuc = _tahakkukService.Ekle(tahakkukListe);
                    }
                }

                if (beyanEkleSonuc)
                {
                    ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme İşlemi Gerçekleşti.");

                    return Json(new { Data = _beyanVM, Message = "Kira Beyan Ekleme İşlemi Başarıyla Getirildi.", success = true }, JsonRequestBehavior.AllowGet);
                }
            }

            ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme İşlemi Gerçekleştirilemedi!!!");

            return Json(new { Message = "Kira Beyan Ekleme İşlemi Gerçekleştirilemedi.", success = false }, JsonRequestBehavior.AllowGet);
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
                model.EkTahakkukOranlari = EkTahakkukOranlariSelectList();
                model.KiraDurumSelectList = KiraDurumFullSelectList();
                model.UfeOranlari = UfeOranSelectList();
                model.ArtisTuruSelectList = ArtisTuruSelectList();
            }

            return View(model);
        }

        #endregion

        #region EkTahakkukOlustur
        [HttpPost]
        public JsonResult EkTahakkukOlustur(TahakkukEkleVM tahakkuk)
        {
            DateTime vadeTarih = new DateTime();
            var kiraParametre = _kiraParametreService.GetirBeyanYil(tahakkuk.BeyanYil.Value);
            vadeTarih = tahakkuk.VadeTarihi.Value.AddMonths(1);
            string tarih = "07" + "." + vadeTarih.Month + "." + vadeTarih.Year;
            vadeTarih = Convert.ToDateTime(tarih);
            vadeTarih = _resmiTatilService.TatilGunuKontrol(vadeTarih);
            decimal kdvTutar = 0;

            decimal tahakkukTutar = Convert.ToDecimal(tahakkuk.Tutar);

            if (tahakkuk.KdvAlinacakMi)
                kdvTutar = (tahakkukTutar * tahakkuk.KdvOrani / 100);

            List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();

            Tahakkuk ekTahakkuk = new Tahakkuk()
            {
                Guid = Guid.NewGuid(),
                KiraBeyan_Id = tahakkuk.KiraBeyan_Id.Value,
                KiraParametre_Id = kiraParametre.Id,
                ServisSonucTahakkukId = null,
                BeyanYil = tahakkuk.BeyanYil.Value,
                KiraParametreKodu = kiraParametre.KiraTarifeKodu.Value,
                TahakkukTarihi = DateTime.Now,
                VadeTarihi = vadeTarih,
                TaksitSayisi = tahakkuk.TaksitNo,
                Tutar = tahakkukTutar,
                KalanBorcTutari = null,
                OdemeDurumu = false,
                KdvAlinacakMi = tahakkuk.KdvAlinacakMi,
                EkTahakkukKdvOran = tahakkuk.EkTahakkukKdvOran,
                EkTahakkukMu = true,
                Aciklama = tahakkuk.Aciklama,
                ServisAciklama = kiraParametre.KiraTarifeAciklama,
                OlusturulmaTarihi = DateTime.Now,
                OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                AktifMi = (int)EnmIslemDurumu.Aktif
            };

            tahakkukListe.Add(ekTahakkuk);

            if (tahakkuk.KdvAlinacakMi)
            {
                Tahakkuk ekTahakkukKdv = new Tahakkuk()
                {
                    Guid = Guid.NewGuid(),
                    KiraBeyan_Id = tahakkuk.KiraBeyan_Id.Value,
                    KiraParametre_Id = kiraParametre.Id,
                    ServisSonucTahakkukId = null,
                    BeyanYil = tahakkuk.BeyanYil.Value,
                    KiraParametreKodu = kiraParametre.KdvTarifeKodu.Value,
                    TahakkukTarihi = DateTime.Now,
                    VadeTarihi = vadeTarih,
                    TaksitSayisi = tahakkuk.TaksitNo,
                    Tutar = kdvTutar,
                    KalanBorcTutari = null,
                    OdemeDurumu = false,
                    KdvAlinacakMi = tahakkuk.KdvAlinacakMi,
                    EkTahakkukKdvOran = tahakkuk.EkTahakkukKdvOran,
                    EkTahakkukMu = true,
                    Aciklama = tahakkuk.Aciklama,
                    ServisAciklama = kiraParametre.KdvTarifeAciklama,
                    OlusturulmaTarihi = DateTime.Now,
                    OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = (int)EnmIslemDurumu.Aktif
                };

                tahakkukListe.Add(ekTahakkukKdv);
            }


            if (ekTahakkuk != null)
            {
                var tahakkukSonuc = _tahakkukService.Ekle(tahakkukListe);

                if (tahakkukSonuc)
                {
                    ModelState.AddModelError("LogMessage", "Ek Tahakkuk Ekleme İşlemi Gerçekleşti.");

                    return Json(new { Data = _beyanVM, Message = "Ek Tahakkuk Ekleme İşlemi Başarıyla Gerçekleşti.", success = true }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Message = "Kira Beyan Ekleme İşlemi Gerçekleştirilemedi.", success = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BeyanArtis
        [HttpPost]
        public JsonResult KiraBeyanArtis(KiraArtisEkleVM beyanArtis)
        {
            bool artisSonuc = false;

            var kiraBeyan = _kiraBeyanService.Getir(beyanArtis.KiraBeyan_Id);

            BeyanEkleVM ekleVM = GetirBeyanEkleVM(beyanArtis.Beyan_Id);

            beyanArtis.Beyan = ekleVM;

            artisSonuc = BeyanPasifeAl(beyanArtis);

            if (artisSonuc)
            {
                ModelState.AddModelError("LogMessage", "Kira Artış İşlemi Başarıyla Gerçekleşti!!!");

                return Json(new { Message = "Kira Artış İşlemi Başarıyla Gerçekleştirildi.", success = true }, JsonRequestBehavior.AllowGet);
            }

            ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme İşlemi Gerçekleştirilemedi!!!");

            return Json(new { Message = "Kira Beyan Ekleme İşlemi Gerçekleştirilemedi.", success = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SelectLists

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

        public SelectList GayrimenkulTuruSelectList()
        {
            var gayrimenkulTuru = _gayrimenkulTurService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(gayrimenkulTuru, "Id", "Ad");
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
            var yillar = _parametreService.GetirListe(1).Where(a => a.AktifMi.Value).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(yillar, "Id", "Ad");
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

        public SelectList KiraDurumFullSelectList()
        {
            var iller = _kiraDurumService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList KiraDurumSelectList()
        {
            var iller = _kiraDurumService.GetirListe().Where(a => a.Id == 1).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList EkTahakkukOranlariSelectList()
        {
            var iller = _parametreService.GetirListe(11).Select(x => new { Id = x.Id, Ad = x.Ad }).OrderByDescending(a => a.Id).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList OdemePeriyotSelectList()
        {
            var iller = _odemePeriyotService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList OtoparkTatilGunuSelectList()
        {
            var iller = _parametreService.GetirListe(10).Select(x => new { Id = x.Deger, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList UfeOranSelectList()
        {
            var iller = _ufeOranService.GetirListeAktif().Select(x => new { Id = x.Id, Ad = x.Ad + " (%" + x.Oran + ")" }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList ArtisTuruSelectList()
        {
            var iller = _parametreService.GetirListe(9).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Tam Artış",
                                    Value="2"
                                  },
                                    new SelectListItem(){
                                    Text="Normal Artış",
                                    Value="1"
                                  }
            };

            return new SelectList(newList, "Value", "Text");
        }

        #endregion

        #region Metodlar

        private KiraBeyanEkleVM BeyanGuncelleVeriGetir(Guid id)
        {
            Kiraci kiraci = new Kiraci();
            var request = new KiraBeyanRequest() { Guid = id };
            var kiraBeyan = _kiraBeyanService.GetirSorguListe(request).FirstOrDefault();
            var model = new KiraBeyanEkleVM();
            if (kiraBeyan != null)
            {
                var beyanDosya = _beyanDosyaService.GetirListe().Where(a => a.Beyan_Id == kiraBeyan.Beyan_Id).ToList();
                if (kiraBeyan.Kiracilar.VergiNo != null)
                    kiraci = _kiraciService.GetirVergiNo(Convert.ToInt64(kiraBeyan.Kiracilar.VergiNo));
                else
                    kiraci = _kiraciService.GetirTcNo(Convert.ToInt64(kiraBeyan.Kiracilar.TcKimlikNo));

                model.Kiraci = _beyanVM.Kiraci = new KiraciEkleVM()
                {
                    SicilNo = Convert.ToInt64(kiraci.SicilNo),
                    VergiNo = Convert.ToInt64(kiraci.VergiNo),
                    Ad = kiraci.Ad,
                    Soyad = kiraci.Soyad,
                    Tanim = kiraci.Tanim,
                    IlAdi = kiraci.IlAdi,
                    IlceAdi = kiraci.IlceAdi,
                    MahalleAdi = kiraci.MahalleAdi,
                    AcikAdres = kiraci.AcikAdres,
                    VergiDairesi = kiraci.VergiDairesi,
                    Id = kiraci.Id,
                };

                model.Gayrimenkul = GetirGayrimenkul(kiraBeyan.Gayrimenkuller.GayrimenkulNo);
                model.Beyan = new BeyanEkleVM();
                model.Beyan = GetirBeyanEkleVM(kiraBeyan.Beyanlar.Id);
                model.Beyan.BeyanTurSelectList = BeyanTurSelectList();
                model.Beyan.BeyanYilSelectList = BeyanYilSelectList();
                model.Beyan.KiraDurumSelectList = KiraDurumSelectList();
                model.Beyan.OdemePeriyotSelectList = OdemePeriyotSelectList();
                model.Beyan.KdvOraniSelectList = KdvOraniSelectList();
                model.Beyan.DamgaVergisiDurumSelectList = DamgaVergisiDurumSelectList();
                model.Beyan.DosyaTurleri = _dosyaService.GetirListe();
                model.Beyan.OtoparkTatilGunuSelectList = OtoparkTatilGunuSelectList();

                model.BeyanDosyalar = GetirBeyanDosyalar(kiraBeyan.Beyanlar.Id);
            }
            return model;

        }

        private BeyanEkleVM GetirBeyanEkleVM(int id)
        {
            BeyanEkleVM ekleVm = new BeyanEkleVM();
            var beyanBilgi = _kiraBeyanService.GetirBeyan(id).Beyanlar;
            CultureInfo cultures = new CultureInfo("en-US");

            ekleVm.BeyanTur_Id = beyanBilgi.BeyanTur_Id;
            ekleVm.BeyanYil = beyanBilgi.BeyanYil;
            ekleVm.KiraDurum_Id = beyanBilgi.KiraDurum_Id;
            ekleVm.BeyanNo = beyanBilgi.BeyanNo;
            ekleVm.NoterSozlesmeNo = beyanBilgi.NoterSozlesmeNo.ToString();
            ekleVm.EncumenKararNo = beyanBilgi.EncumenKararNo;
            ekleVm.BeyanTarihi = beyanBilgi.BeyanTarihi;
            ekleVm.IhaleEncumenTarihi = beyanBilgi.IhaleEncumenTarihi;
            ekleVm.KiraBaslangicTarihi = beyanBilgi.KiraBaslangicTarihi;
            ekleVm.SozlesmeTarihi = beyanBilgi.SozlesmeTarihi;
            ekleVm.SozlesmeBitisTarihi = beyanBilgi.SozlesmeBitisTarihi;
            ekleVm.TeminatTarihi = beyanBilgi.TeminatTarihi;
            ekleVm.BeyanKapatmaTarihi = beyanBilgi.BeyanKapatmaTarihi;
            ekleVm.TeminatNo = beyanBilgi.TeminatNo.ToString();
            ekleVm.KullanimAlani = Convert.ToDecimal(beyanBilgi.KullanimAlani, cultures);
            ekleVm.OdemePeriyotTur_Id = beyanBilgi.OdemePeriyotTur_Id;
            ekleVm.SozlesmeSuresi = beyanBilgi.SozlesmeSuresi;
            ekleVm.MusadeliGunSayisi = beyanBilgi.MusadeliGunSayisi;
            ekleVm.IhaleTutari = beyanBilgi.IhaleTutari.ToString();
            ekleVm.KiraTutari = beyanBilgi.KiraTutari.ToString();
            ekleVm.Kdv = beyanBilgi.Kdv;
            ekleVm.BaslangicTaksitNo = beyanBilgi.BaslangicTaksitNo;
            ekleVm.KalanAy = beyanBilgi.KalanAy;
            ekleVm.DamgaAlinsinMi = (beyanBilgi.DamgaAlinsinMi == true ? "1" : "0");
            ekleVm.BeyanAciklama = beyanBilgi.Aciklama;
            ekleVm.Id = beyanBilgi.Id;


            return ekleVm;

        }

        private int GetirOdemeAySayisi(int periyotTurId)
        {
            int aySayisi = 0;
            var odemePeriyorTur = _odemePeriyotService.Getir(periyotTurId);

            if (odemePeriyorTur.OdemeAySayisi.HasValue)
                Int32.TryParse(odemePeriyorTur.OdemeAySayisi.ToString(), out aySayisi);
            return aySayisi;
        }

        private KiraParametreDetay GetirParametreDetay(BeyanEkleVM beyan)
        {
            int beyanKod = 0;
            var beyanTur = _beyanTurService.Getir(beyan.BeyanTur_Id);

            if (beyanTur != null && beyanTur.Kod.Equals(1))
                Int32.TryParse(beyanTur.Kod.ToString(), out beyanKod);
            else if (beyanTur != null && beyanTur.Kod.Equals(2))
                Int32.TryParse(beyanTur.Kod.ToString(), out beyanKod);
            else
                Int32.TryParse(beyanTur.Kod.ToString(), out beyanKod);

            var beyanYil = _parametreService.Getir(beyan.BeyanYil.Value);
            var kiraParametre = _kiraParametreService.GetirBeyanYil(Convert.ToInt32(beyanYil.Ad));

            KiraParametreDetay parametreDetay = new KiraParametreDetay();
            parametreDetay.KiraParametre = kiraParametre;
            if (beyanKod > 0 && beyanKod.Equals(1))
            {
                parametreDetay.BeyanTurKod = beyanKod;
                parametreDetay.ParametreKod = kiraParametre.KiraTarifeKodu.Value;
                parametreDetay.ParametreAciklama = kiraParametre.KiraTarifeAciklama;
            }
            else if (beyanKod > 0 && beyanKod.Equals(2))
            {
                parametreDetay.BeyanTurKod = beyanKod;

                parametreDetay.ParametreKod = kiraParametre.OtoparkTarifeKodu.Value;
                parametreDetay.ParametreAciklama = kiraParametre.OtoparkTarifeAciklama;
            }
            else
            {
                parametreDetay.BeyanTurKod = beyanKod;
                parametreDetay.ParametreKod = kiraParametre.EcrimisilTarifeKodu.Value;
                parametreDetay.ParametreAciklama = kiraParametre.EcrimisilTarifeAciklama;
            }


            parametreDetay.Hesap = HesapDetay(beyan, parametreDetay);

            return parametreDetay;
        }

        private KiraParametreHesapDetay HesapDetay(BeyanEkleVM beyan, KiraParametreDetay parametreDetay)
        {
            KiraParametreHesapDetay detay = new KiraParametreHesapDetay();
            parametreDetay.Hesap.KararHarciTutar = (decimal.Parse(beyan.IhaleTutari.Replace('.', ',')) * parametreDetay.KiraParametre.KararHarciOran.Value);
            parametreDetay.Hesap.TeminatTutar = (decimal.Parse(beyan.IhaleTutari.Replace('.', ',')) * parametreDetay.KiraParametre.TeminatOran.Value);
            parametreDetay.DamgaAlinsinMi = (beyan.DamgaAlinsinMi == "1" ? true : false);
            if (beyan.DamgaAlinsinMi == "1")
                parametreDetay.Hesap.DamgaVergisiTutar = (decimal.Parse(beyan.IhaleTutari.Replace('.', ',')) * parametreDetay.KiraParametre.DamgaOran.Value);
            else
                parametreDetay.Hesap.DamgaVergisiTutar = 0;

            parametreDetay.Hesap.KdvTutar = ((beyan.Kdv.HasValue && beyan.Kdv.Value > 0) ? (decimal.Parse(beyan.KiraTutari.Replace('.', ',')) * beyan.Kdv.Value / 100) : 0);
            parametreDetay.Hesap.KiraTutar = decimal.Parse(beyan.KiraTutari);
            return detay;
        }

        private List<Tahakkuk> StandartTahakkukEkle(int KiraBeyan_Id, BeyanEkleVM beyan, KiraParametreDetay parametreDetay)
        {
            List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();
            #region StandartTahakkukVeri

            DateTime vadeTarih = _resmiTatilService.TatilGunuKontrol(beyan.KiraBaslangicTarihi.Value.AddDays(beyan.MusadeliGunSayisi.Value));

            Tahakkuk kararHarci = new Tahakkuk()
            {
                Guid = Guid.NewGuid(),
                KiraBeyan_Id = KiraBeyan_Id,
                KiraParametre_Id = parametreDetay.KiraParametre.Id,
                ServisSonucTahakkukId = null,
                BeyanYil = beyan.BeyanYil.Value,
                KiraParametreKodu = parametreDetay.KiraParametre.KararHarciTarifeKodu.Value,
                TahakkukTarihi = DateTime.Now,
                VadeTarihi = vadeTarih,
                TaksitSayisi = 1,
                Tutar = parametreDetay.Hesap.KararHarciTutar,
                KalanBorcTutari = null,
                OdemeDurumu = false,
                EkTahakkukMu = false,
                Aciklama = parametreDetay.KiraParametre.KararHarciTarifeAciklama,
                OlusturulmaTarihi = DateTime.Now,
                OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                AktifMi = (int)EnmIslemDurumu.Aktif
            };

            //tutari = kararHarci.FirstOrDefault().Tutar.ToString();

            //TahakkukEkleVm tahakkukEkleVm = new TahakkukEkleVm()
            //{
            //    GelirId = kiraParametre.KararHarciTarifeKodu.Value,
            //    SicilNo = int.Parse(sicil),
            //    TaksitNo = 1,
            //    Aciklama = kararHarci.Aciklama,
            //    ModulGrup = 5,
            //    Yil = kiraBeyanModel.Beyan.BeyanYil.Value,
            //    Tutar = double.Parse(tutari),
            //    SonOdemeTarihi = kararHarci.VadeTarihi
            //};

            //var result1 = _tahakkukDisServis.TahakkukOlustur(tahakkukEkleVm);
            //tahakkuk1.ServisSonucTahakkukId = result1.TahakkukId;
            tahakkukListe.Add(kararHarci);

            Tahakkuk teminatTarife = new Tahakkuk()
            {
                Guid = Guid.NewGuid(),
                KiraBeyan_Id = KiraBeyan_Id,
                KiraParametre_Id = parametreDetay.KiraParametre.Id,
                ServisSonucTahakkukId = null,
                BeyanYil = beyan.BeyanYil.Value,
                KiraParametreKodu = parametreDetay.KiraParametre.TeminatTarifeKodu.Value,
                TahakkukTarihi = DateTime.Now,
                VadeTarihi = vadeTarih,
                TaksitSayisi = 1,
                Tutar = parametreDetay.Hesap.TeminatTutar,
                KalanBorcTutari = null,
                OdemeDurumu = false,
                EkTahakkukMu = false,
                Aciklama = parametreDetay.KiraParametre.TeminatTarifeAciklama,
                OlusturulmaTarihi = DateTime.Now,
                OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                AktifMi = (int)EnmIslemDurumu.Aktif
            };
            //tutari = teminatTarife.FirstOrDefault().Tutar.ToString();

            //TahakkukEkleVm tahakkukEkleVm2 = new TahakkukEkleVm()
            //{
            //    GelirId = kiraParametre.TeminatTarifeKodu.Value,
            //    SicilNo = int.Parse(sicil),
            //    TaksitNo = 1,
            //    Aciklama = teminatTarife.Aciklama,
            //    ModulGrup = 5,
            //    Yil = kiraBeyanModel.Beyan.BeyanYil.Value,
            //    Tutar = double.Parse(tutari),
            //    SonOdemeTarihi = teminatTarife.VadeTarihi
            ////};
            //var result2 = _tahakkukDisServis.TahakkukOlustur(tahakkukEkleVm2);
            //tahakkuk2.ServisSonucTahakkukId = result2.TahakkukId;
            tahakkukListe.Add(teminatTarife);

            if (parametreDetay.DamgaAlinsinMi)
            {

                Tahakkuk damgaTarife = new Tahakkuk()
                {
                    Guid = Guid.NewGuid(),
                    KiraBeyan_Id = KiraBeyan_Id,
                    KiraParametre_Id = parametreDetay.KiraParametre.Id,
                    ServisSonucTahakkukId = null,
                    BeyanYil = beyan.BeyanYil.Value,
                    KiraParametreKodu = parametreDetay.KiraParametre.DamgaTarifeKodu.Value,
                    TahakkukTarihi = DateTime.Now,
                    VadeTarihi = vadeTarih,
                    TaksitSayisi = 1,
                    Tutar = parametreDetay.Hesap.DamgaVergisiTutar,
                    KalanBorcTutari = null,
                    OdemeDurumu = false,
                    EkTahakkukMu = false,
                    Aciklama = parametreDetay.KiraParametre.DamgaTarifeAciklama,
                    OlusturulmaTarihi = DateTime.Now,
                    OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = (int)EnmIslemDurumu.Aktif
                };

                //tutari = damgaTarife.FirstOrDefault().Tutar.ToString();

                //TahakkukEkleVm tahakkukEkleVm3 = new TahakkukEkleVm()
                //{
                //    GelirId = kiraParametre.DamgaTarifeKodu.Value,
                //    SicilNo = int.Parse(sicil),
                //    TaksitNo = 1,
                //    Aciklama = damgaTarife.Aciklama,
                //    ModulGrup = 5,
                //    Yil = kiraBeyanModel.Beyan.BeyanYil.Value,
                //    Tutar = double.Parse(tutari),
                //    SonOdemeTarihi = damgaTarife.VadeTarihi
                //};
                //var result3 = _tahakkukDisServis.TahakkukOlustur(tahakkukEkleVm3);
                //tahakkuk3.ServisSonucTahakkukId = result3.TahakkukId;
                tahakkukListe.Add(damgaTarife);
            }

            #endregion
            return tahakkukListe;
        }

        private List<Tahakkuk> TahakkukSatirEkle(int KiraBeyan_Id, int OdemeAySayisi, BeyanEkleVM beyan, Beyan_GayrimenkulEkleVM gayrimenkul, KiraParametreDetay parametreDetay)
        {
            bool standartOtoparkMi = true;
            decimal otoparkTutar, otoparkGunSayisi, gunAraligi;

            List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();


            if (gayrimenkul.AracKapasitesi.HasValue && gayrimenkul.AracKapasitesi.Value > 0)
                standartOtoparkMi = false;

            var dateSon = _resmiTatilService.TatilGunuKontrol(beyan.KiraBaslangicTarihi.Value.AddDays(beyan.MusadeliGunSayisi.Value));

            for (int i = 0; i < OdemeAySayisi; i++)
            {
                if (parametreDetay.BeyanTurKod.Equals(2))
                {
                    gunAraligi = _resmiTatilService.TarihAraligiGunSayisi(beyan.KiraBaslangicTarihi.Value.AddMonths(i), beyan.KiraBaslangicTarihi.Value.AddMonths(i + 1));

                    otoparkGunSayisi = _resmiTatilService.TatilGunuKontrol(beyan.KiraBaslangicTarihi.Value.AddMonths(i), beyan.KiraBaslangicTarihi.Value.AddMonths(i + 1),
                                                                           beyan.OtoparkTatilGun.Split(','), beyan.ResmiTatilVarmi);

                    otoparkTutar = (parametreDetay.KiraParametre.OtoparkBirimFiyat.Value * gayrimenkul.AracKapasitesi.Value * (gunAraligi - otoparkGunSayisi));

                    if (standartOtoparkMi)
                        otoparkTutar = parametreDetay.Hesap.KiraTutar;
                    else
                        otoparkTutar = (parametreDetay.KiraParametre.OtoparkBirimFiyat.Value * gayrimenkul.AracKapasitesi.Value * (gunAraligi - otoparkGunSayisi));


                    Tahakkuk otoparkTahakkuk = new Tahakkuk()
                    {
                        Guid = Guid.NewGuid(),
                        KiraBeyan_Id = KiraBeyan_Id,
                        KiraParametre_Id = parametreDetay.KiraParametre.Id,
                        ServisSonucTahakkukId = null,
                        BeyanYil = beyan.BeyanYil.Value,
                        KiraParametreKodu = parametreDetay.ParametreKod,
                        TahakkukTarihi = DateTime.Now,
                        VadeTarihi = dateSon,
                        TaksitSayisi = i + 1,
                        Tutar = otoparkTutar,
                        KalanBorcTutari = null,
                        OdemeDurumu = false,
                        EkTahakkukMu = false,
                        Aciklama = parametreDetay.ParametreAciklama,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = (int)EnmIslemDurumu.Aktif
                    };

                    tahakkukListe.Add(otoparkTahakkuk);
                }
                else
                {
                    Tahakkuk kiraTahakkuk = new Tahakkuk()
                    {
                        Guid = Guid.NewGuid(),
                        KiraBeyan_Id = KiraBeyan_Id,
                        KiraParametre_Id = parametreDetay.KiraParametre.Id,
                        ServisSonucTahakkukId = null,
                        BeyanYil = beyan.BeyanYil.Value,
                        KiraParametreKodu = parametreDetay.ParametreKod,
                        TahakkukTarihi = DateTime.Now,
                        VadeTarihi = dateSon,
                        TaksitSayisi = i + 1,
                        Tutar = parametreDetay.Hesap.KiraTutar,
                        KalanBorcTutari = null,
                        OdemeDurumu = false,
                        EkTahakkukMu = false,
                        Aciklama = parametreDetay.ParametreAciklama,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = (int)EnmIslemDurumu.Aktif
                    };


                    tahakkukListe.Add(kiraTahakkuk);
                }

                //TahakkukEkleVm kiraTahakkukEkleVm = new TahakkukEkleVm()
                //{
                //    GelirId = beyan.KararHarciTarifeKodu.Value,
                //    SicilNo = int.Parse(sicil),
                //    TaksitNo = 1,
                //    Aciklama = kiraTahakkuk.Aciklama,
                //    ModulGrup = 5,
                //    Yil = beyanBeyanYil.Value,
                //    Tutar = double.Parse(tutari),
                //    SonOdemeTarihi = kiraTahakkuk.VadeTarihi
                //};
                //var servisSonuc1 = _tahakkukDisServis.TahakkukOlustur(kiraTahakkukEkleVm);
                //kiraTahakkuk.ServisSonucTahakkukId = servisSonuc1.TahakkukId;

                if (parametreDetay.Hesap.KdvTutar > 0)
                {
                    Tahakkuk kdvTahakkuk = new Tahakkuk()
                    {
                        Guid = Guid.NewGuid(),
                        KiraBeyan_Id = KiraBeyan_Id,
                        KiraParametre_Id = parametreDetay.KiraParametre.Id,
                        ServisSonucTahakkukId = null,
                        BeyanYil = beyan.BeyanYil.Value,
                        KiraParametreKodu = parametreDetay.KiraParametre.KdvTarifeKodu.Value,
                        TahakkukTarihi = DateTime.Now,
                        VadeTarihi = dateSon,
                        TaksitSayisi = i + 1,
                        Tutar = parametreDetay.Hesap.KdvTutar,
                        KalanBorcTutari = null,
                        OdemeDurumu = false,
                        EkTahakkukMu = false,
                        Aciklama = parametreDetay.KiraParametre.KdvTarifeAciklama,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = (int)EnmIslemDurumu.Aktif
                    };

                    //TahakkukEkleVm kdvTahakkukEkleVm = new TahakkukEkleVm()
                    //{
                    //    GelirId = beyan.KararHarciTarifeKodu.Value,
                    //    SicilNo = int.Parse(sicil),
                    //    TaksitNo = 1,
                    //    Aciklama = kiraTahakkuk.Aciklama,
                    //    ModulGrup = 5,
                    //    Yil = beyanBeyanYil.Value,
                    //    Tutar = double.Parse(tutari),
                    //    SonOdemeTarihi = kiraTahakkuk.VadeTarihi
                    //};

                    //var servisSonuc2 = _tahakkukDisServis.TahakkukOlustur(kdvTahakkukEkleVm);
                    //kdvTahakkuk.ServisSonucTahakkukId = servisSonuc2.TahakkukId;

                    tahakkukListe.Add(kdvTahakkuk);
                }

                if (OdemeAySayisi == 4)
                    dateSon = _resmiTatilService.TatilGunuKontrol(dateSon.AddMonths(3));
                else
                    dateSon = _resmiTatilService.TatilGunuKontrol(dateSon.AddMonths(1));
            }

            return tahakkukListe;
        }

        private void BeyanPasifeAl(KiraBeyanEkleVM kiraBeyanModel)
        {
            var tahakkukListesi = _tahakkukService.GetirListe(kiraBeyanModel.Beyan.Id);

            if (tahakkukListesi.Where(x => x.OdemeDurumu == true).ToList().Count > 0)
            {
                ModelState.AddModelError("LogMessage", "Beyana ait ödenmiş veri bulunmaktadır. Beyanı güncelleyemezsiniz.");

                return;
            }
            //Beyan Pasife Alınır
            var beyan = kiraBeyanModel.Beyan;
            beyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            BeyanEkle(kiraBeyanModel.Beyan);

            //Tahakkuklar Pasife alınır.
            foreach (var item in tahakkukListesi)
            {
                item.AktifMi = (int)EnmIslemDurumu.Pasif;
                _tahakkukService.Guncelle(item);
            }

            //Kira Beyan Sayfası Pasife Alınır.
            var kiraBeyan = _kiraBeyanService.Getir(kiraBeyanModel.Beyan.Id, kiraBeyanModel.Gayrimenkul.Id, kiraBeyanModel.Kiraci.Id);
            kiraBeyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            _kiraBeyanService.Guncelle(kiraBeyan);

        }

        private bool BeyanPasifeAl(KiraArtisEkleVM artisModel)
        {
            bool sonuc = false;

            var tahakkukListesi = _tahakkukService.GetirListe(artisModel.Beyan_Id);

            if (tahakkukListesi.Where(x => x.OdemeDurumu == true).ToList().Count > 0)
            {
                ModelState.AddModelError("LogMessage", "Beyana ait ödenmiş veri bulunmaktadır. Beyanı güncelleyemezsiniz.");

                return false;
            }
            //Beyan Pasife Alınır
            artisModel.Beyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            BeyanEkle(artisModel.Beyan);

            //Tahakkuklar Pasife alınır.
            foreach (var item in tahakkukListesi)
            {
                item.AktifMi = (int)EnmIslemDurumu.Pasif;
                _tahakkukService.Guncelle(item);
            }

            //Kira Beyan Sayfası Pasife Alınır.
            var kiraBeyan = _kiraBeyanService.Getir(artisModel.Beyan_Id, artisModel.Gayrimenkul_Id, artisModel.Kiraci_Id);
            kiraBeyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            var kirabeyanSonuc = _kiraBeyanService.Guncelle(kiraBeyan);

            if (kirabeyanSonuc.AktifMi == (int)EnmIslemDurumu.Aktif)
                sonuc = true;

            return sonuc;
        }

        public ActionResult BeyanSil(Guid guidId, int kiraciId, int gayrimenkulId)
        {
            var beyan = _beyanService.GetirGuid(guidId);
            var tahakkukListesi = _tahakkukService.GetirListe(beyan.Id);
            beyan.AktifMi = (int)EnmIslemDurumu.Silindi;
            _beyanService.Guncelle(beyan);

            //Tahakkuklar Silinir
            foreach (var item in tahakkukListesi)
            {
                item.AktifMi = (int)EnmIslemDurumu.Silindi;
                _tahakkukService.Guncelle(item);
            }

            // Kira Beyan Sayfası Silinir Alınır.
            var kiraBeyan = _kiraBeyanService.Getir(beyan.Id, gayrimenkulId, kiraciId);
            kiraBeyan.AktifMi = (int)EnmIslemDurumu.Silindi;
            _kiraBeyanService.Guncelle(kiraBeyan);
            return RedirectToAction("Index");

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
                        AracKapasitesi = gayrimenkul.AracKapasitesi,
                        Id = gayrimenkul.Id
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
                        OlusturanKullanici = _userService.GetById(beyan.OlusturanKullanici_Id.Value).UserName,
                        SorumluPersonel = (beyan.SorumluPersonelId.HasValue ? _userService.GetById(beyan.SorumluPersonelId.Value).UserName : null),
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

        /// <summary>
        ///Beyan Dosyaları Ekleme Metodu
        ///Beyan Dosya Ekleme İşlemleri Bu Metodun İçerisinde Gerçekleşmektedir.
        /// </summary>
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

        /// <summary>
        ///Sicil (Kiracı) Ekleme Metodu
        ///Kiracı Ekleme İşlemi Bu Metodda Gerçekleşir
        /// </summary>
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

        /// <summary>
        ///Beyan Ekleme Metodu
        ///Beyan Ekleme İşlemleri Bu Metodun İçerisinde Gerçekleşmektedir.
        /// </summary>
        [HttpGet]
        private int BeyanEkle(BeyanEkleVM beyanBilgi)
        {
            Beyan result;
            if (beyanBilgi != null)
            {
                CultureInfo cultures = new CultureInfo("en-US");


                Beyan beyan = new Beyan()
                {
                    Id = beyanBilgi.Id,
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
                    OtoparkTatilGun = beyanBilgi.OtoparkTatilGun,
                    ResmiTatilVarmi = beyanBilgi.ResmiTatilVarmi,
                    OlusturulmaTarihi = DateTime.Now,
                    OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = beyanBilgi.AktifMi
                };
                if (beyanBilgi.Id == 0)
                    result = _beyanService.Ekle(beyan);
                else
                    result = _beyanService.Guncelle(beyan);
                if (result.Id > 0)
                    return result.Id;
            }

            return 0;
        }

        /// <summary>
        ///Kira Beyan Ekleme Metodu
        ///Kira Beyan Ekleme İşlemleri Bu Metodun İçerisinde Gerçekleşmektedir.
        /// </summary>
        [HttpGet]
        private int KiraBeyanEkle(int Beyan_Id, int Kiraci_Id, int Gayrimenkul_Id)
        {
            Kira_Beyan kiraBeyan = new Kira_Beyan()
            {
                Beyan_Id = Beyan_Id,
                Gayrimenkul_Id = Gayrimenkul_Id,
                Kiraci_Id = Kiraci_Id,
                OlusturulmaTarihi = DateTime.Now,
                AktifMi = (int)EnmIslemDurumu.Aktif,
                OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null)
            };

            var result = _kiraBeyanService.Ekle(kiraBeyan);

            if (result.Id > 0)
                return result.Id;

            return 0;
        }

        #endregion

        #region DigerIslemMetodları

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
            _beyanVM.Beyan.OtoparkTatilGunuSelectList = OtoparkTatilGunuSelectList();

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
        [ValidateInput(false)]
        public FileContentResult BeyanCiktiAl(string html)
        {
            HtmlNode.ElementsFlags["img"] = HtmlElementFlag.Closed;
            HtmlNode.ElementsFlags["input"] = HtmlElementFlag.Closed;
            HtmlDocument doc = new HtmlDocument();
            doc.OptionFixNestedTags = true;
            doc.LoadHtml(html);
            html = doc.DocumentNode.OuterHtml;

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();

                return File(stream.ToArray(), "application/pdf", "BeyanDetay.pdf");
                //return new FileContentResult(stream.ToArray(), MimeMapping.GetMimeMapping("BeyanDetay.pdf"));
            }
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

        [HttpGet]
        public JsonResult GetirGayrimenkulChart()
        {
            var liste = _gayrimenkulservice.GetirListe().GroupBy(a => a.GayrimenkulTur.Ad)
                         .Select(g => new { g.Key, Count = g.Count() });

            List<GayrimenkulChartModel> modelList = new List<GayrimenkulChartModel>();
            foreach (var item in liste)
            {
                modelList.Add(new GayrimenkulChartModel() { y = item.Count, name = item.Key });
            }
            return Json(modelList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult KiralikTasinmazlarChart()
        {
            var liste = _kiraBeyanService.GetirListe().Where(x => x.Gayrimenkuller.Ilce_Id != null).ToList();
            var data = liste.GroupBy(a => a.Gayrimenkuller.Ilceler.Ad)
                          .Select(g => new { g.Key, Count = g.Count() }).ToList();

            List<KiralikTasinmazlarChart> modelList = new List<KiralikTasinmazlarChart>();
            foreach (var item in data)
            {
                modelList.Add(new KiralikTasinmazlarChart() { y = item.Count, label = item.Key });
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(modelList);

            return Json(modelList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}