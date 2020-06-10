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
using Framework.WebUI.Models.ComplexType;
using iTextSharp.text.pdf.qrcode;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    [CustomAuthorize(Roles = "Kira")]
    public class BeyanController : Controller
    {
        #region Constructor

        private readonly IGayrimenkulService _gayrimenkulservice;
        private readonly IBeyanService _beyanService;
        private readonly IBeyanDosya_TurService _dosyaService;
        private readonly IBeyan_DosyaService _beyanDosyaService;
        private readonly IKira_BeyanService _kiraBeyanService;
        private readonly IKira_DurumService _kiraDurumService;
        private readonly IOdemePeriyotTurService _odemePeriyotService;
        private readonly ISicilService _sicilService;
        private readonly IKiraciService _kiraciService;
        private readonly ITahakkukService _tahakkukService;
        private readonly IKiraParametreService _kiraParametreService;
        private readonly ITahakkukDisServis _tahakkukDisServis;
        public static KiraBeyanEkleVM _beyanVM;
        private readonly IBeyan_TurService _beyanTurService;
        private readonly IMahalleService _mahalleService;
        private readonly IUserService _userService;
        private readonly IResmiTatillerService _resmiTatilService;
        private readonly IIlService _ilService;
        private readonly IIlceService _ilceService;
        private readonly BeyanSelectListsVm _selectLists;
        private readonly ISistemParametre_DetayService _parametreService;

        private readonly IBeyan_UfeOranService _ufeOranService;
        private readonly IGayrimenkulTurService _gayrimenkulTurService;

        private readonly IKiraDurum_DosyaTurService _kiraDurumDosyaTurService;


        public string DosyaYolu = ConfigurationManager.AppSettings["DosyaYolu"].ToString();


        public BeyanController(IGayrimenkulService gayrimenkulservice,
            IBeyanService beyanService,
            ITahakkukService tahakkukService,
            IKira_BeyanService kiraBeyanService,
            ISicilService sicilService,
            KiraBeyanEkleVM beyanVM,
            IKira_DurumService kiraDurumService,
            IOdemePeriyotTurService odemePeriyotService,
        IBeyan_TurService beyanTurService,
        IBeyanDosya_TurService dosyaService,
        IBeyan_DosyaService beyanDosyaService,
        IKiraciService kiraciService,
        IMahalleService mahalleService,
        IKiraParametreService kiraParametreService,
        ITahakkukDisServis tahakkukDisServis,
        IGayrimenkulTurService gayrimenkulTurService,
        IUserService userService,
        IResmiTatillerService resmiTatilService,
        IIlService ilService,
        IIlceService ilceService,
        ISistemParametre_DetayService parametreService,
        BeyanSelectListsVm selectLists,
        IKiraDurum_DosyaTurService kiraDurumDosyaTurService,
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
            _beyanDosyaService = beyanDosyaService;
            _odemePeriyotService = odemePeriyotService;
            _kiraDurumService = kiraDurumService;
            _mahalleService = mahalleService;
            _ilService = ilService;
            _ilceService = ilceService;
            _kiraciService = kiraciService;
            _tahakkukService = tahakkukService;
            _tahakkukDisServis = tahakkukDisServis;
            _kiraParametreService = kiraParametreService;
            _userService = userService;
            _selectLists = selectLists;
            _resmiTatilService = resmiTatilService;
            _parametreService = parametreService;
            _gayrimenkulTurService = gayrimenkulTurService;
            _ufeOranService = ufeOranService;
            _odemePeriyotService = odemePeriyotService;
            _beyanTurService = beyanTurService;
            _kiraParametreService = kiraParametreService;
            _kiraDurumDosyaTurService = kiraDurumDosyaTurService;

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

                model.KiraBeyanVm = new KiraBeyanVM
                {
                    Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count())
                };
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
                model.KiraBeyanVm = new KiraBeyanVM
                {
                    Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count())
                };
                model.TotalRecordCount = model.KiraBeyanVm.Beyanlar.Count();
            }

            return View(model);
        }

        public ActionResult Index(KiraBeyanRequest request, int? page, int pageSize = 15)
        {
            //TestWCF();
            var model = new KiraBeyanVM();

            if (request.BeyanYil.HasValue)
            {
                var yillist = _parametreService.Getir(request.BeyanYil.Value);

                request.BeyanYil = int.Parse(yillist.Deger);
            }

            if (request.Kdv.HasValue)
            {
                var kdvlist = _parametreService.Getir(request.Kdv.Value);

                request.Kdv = int.Parse(kdvlist.Ad);
            }

            var beyanlar = _kiraBeyanService.GetirSorguListeAktif(request);

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
            var iller = _ilService.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList IlceSelectList()
        {
            var ilceler = _ilceService.GetirListe().Where(a => a.Il_Id == 6).Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(ilceler, "Id", "Ad");
        }


        public SelectList GayrimenkulTuruSelectList()
        {
            var gayrimenkulTuru = _gayrimenkulTurService.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(gayrimenkulTuru, "Id", "Ad");
        }


        [HttpPost]
        public JsonResult MahalleSelectList(int ilceId)
        {
            var mahalleler = _mahalleService.GetirListe().Where(a => a.Ilce_Id == ilceId).Select(x => new { x.Id, x.Ad }).ToList();

            return Json(new { Data = mahalleler, success = true }, JsonRequestBehavior.AllowGet);
        }

        public SelectList GayrimenkulSelectList()
        {
            var iller = _gayrimenkulservice.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList BeyanTurSelectList()
        {
            var turler = _beyanTurService.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        public SelectList BeyanYilSelectList()
        {
            var yillar = _parametreService.GetirListe(1).Where(a => a.AktifMi.Value).Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(yillar, "Id", "Ad");
        }

        public SelectList KdvOraniSelectList()
        {
            var iller = _parametreService.GetirListe(9).Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList DamgaVergisiDurumSelectList()
        {
            var iller = _parametreService.GetirListe(9).Select(x => new { x.Id, x.Ad }).ToList();
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
            var iller = _kiraDurumService.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList KiraDurumSelectList()
        {
            var iller = _kiraDurumService.GetirListe().Where(a => a.Id == 1).Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList EkTahakkukOranlariSelectList()
        {
            var iller = _parametreService.GetirListe(11).Select(x => new { x.Id, x.Ad }).OrderByDescending(a => a.Id).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList OdemePeriyotSelectList()
        {
            var iller = _odemePeriyotService.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList OtoparkTatilGunuSelectList()
        {
            var iller = _parametreService.GetirListe(10).Select(x => new { x.Deger, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        public SelectList UfeOranSelectList()
        {
            var iller = _ufeOranService.GetirListeAktif().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
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
            KiraBeyanIslemleri islemler = new KiraBeyanIslemleri();
            islemler.Eklenenler = new KiraBeyanModel();


            #region Beyan Pasife Alma İşlemi
            if (kiraBeyanModel.Beyan.Id > 0)
            {
                islemler.PasifeAlinanlar = new KiraBeyanModel();
                //Tahakkuk kaydı varsa ödenen güncellenmesine izin verilmeyecek.
                BeyanPasifeAl(kiraBeyanModel, ref islemler);
                kiraBeyanModel.Beyan.Id = 0;
                //Beyan Pasif,Tahakkuklar,KiraBeyanEkle pasife alınacak.
            }
            #endregion

            #region Gayrimenkul,Sicil,Beyan Ekleme İşlemleri


            kiraBeyanModel.Gayrimenkul_Id = kiraBeyanModel.Gayrimenkul.GayrimenkulId;

            var kiraci = _kiraciService.GetirVergiNo(kiraBeyanModel.Kiraci.VergiNo.Value);

            if (kiraci != null)
                kiraBeyanModel.Kiraci_Id = kiraci.Id;
            else
                kiraBeyanModel.Kiraci_Id = BeyanSicilEkle(kiraBeyanModel.Kiraci, ref islemler);

            kiraBeyanModel.Beyan.AktifMi = (int)EnmIslemDurumu.Aktif;

            kiraBeyanModel.Beyan_Id = BeyanEkle(kiraBeyanModel.Beyan, ref islemler);

            #endregion


            BeyanDosyaEkle(kiraBeyanModel.Beyan_Id, false, kiraBeyanModel.BeyanDosyalar, ref islemler);

            kiraBeyanModel.KiraBeyan_Id = KiraBeyanEkle(kiraBeyanModel.Beyan_Id, kiraBeyanModel.Kiraci_Id, kiraBeyanModel.Gayrimenkul_Id, ref islemler);

            #region Beyan Parametreleri Doldurma

            AySayisi = GetirOdemeAySayisi(kiraBeyanModel.Beyan.OdemePeriyotTur_Id.Value);

            var kiraParametre = GetirParametreDetay(false, kiraBeyanModel.Beyan, null);

            #endregion

            if (AySayisi > 0)
            {
                kiraBeyanModel.Gayrimenkul = GetirGayrimenkul(kiraBeyanModel.Gayrimenkul.GayrimenkulId);

                List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();

                var standart = StandartTahakkukEkle(kiraBeyanModel.KiraBeyan_Id, kiraBeyanModel.Beyan, kiraParametre);

                if (standart != null)
                    tahakkukListe.AddRange(standart);

                if (tahakkukListe != null && tahakkukListe.Count > 0)
                {
                    var tahakkuk = TahakkukSatirEkle(kiraBeyanModel.KiraBeyan_Id, AySayisi, kiraBeyanModel.Beyan, kiraBeyanModel.Gayrimenkul, kiraParametre);

                    if (tahakkuk != null)
                        tahakkukListe.AddRange(tahakkuk);

                    islemler.Eklenenler.Tahakkuklar = new List<Tahakkuk>();

                    islemler.Eklenenler.Tahakkuklar.AddRange(tahakkukListe);
                    //beyanEkleSonuc = _tahakkukService.Ekle(tahakkukListe);
                }
            }



            var result = _beyanService.KiraBeyanIslemleri(islemler);
            if (result)
            {
                ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme İşlemi Gerçekleşti.");
                return Json(new { Data = _beyanVM, Message = "Kira Beyan Ekleme İşlemi Başarıyla Getirildi.", success = true }, JsonRequestBehavior.AllowGet);
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
                var beyanDosya = _beyanDosyaService.GetirListe(false).Where(a => a.Beyan_Id == kiraBeyan.Beyan_Id).ToList();

                model.Kiraci = GetirSicil(kiraBeyan.Kiracilar.VergiNo.ToString());
                model.Gayrimenkul = GetirGayrimenkul(kiraBeyan.Gayrimenkuller.GayrimenkulNo);
                model.Beyan = GetirBeyan(kiraBeyan.Beyanlar.Id);
                model.BeyanDosyalar = GetirBeyanDosyalar(kiraBeyan.Beyanlar.Id);
                model.Tahakkuklar = _tahakkukService.GetirListe(kiraBeyan.Id);
                model.EkTahakkukOranlari = _selectLists.EkTahakkukOranlariSelectList();
                model.KiraDurumSelectList = _selectLists.KiraDurumFullSelectList();
                model.UfeOranlari = _selectLists.UfeOranSelectList();
                model.ArtisTuruSelectList = _selectLists.ArtisTuruSelectList();
                model.ArtisTipiSelectList = _selectLists.ArtisTipiSelectList();
                //model.KiraYenilemePeriyotSelectList = _selectLists.KiraYenilemePeriyotSelectList();

                model.Beyan_Id = kiraBeyan.Beyan_Id;
                model.Gayrimenkul_Id = model.Gayrimenkul.Id;
                model.Kiraci_Id = kiraBeyan.Kiracilar.Id;
                model.KiraBeyan_Id = kiraBeyan.Id;
            }

            return View(model);
        }

        #endregion

        #region EkTahakkukOlustur
        [HttpPost]
        public JsonResult EkTahakkukOlustur(TahakkukEkleVM tahakkuk)
        {
            var kiraParametre = _kiraParametreService.GetirBeyanYil(tahakkuk.BeyanYil.Value);
            DateTime vadeTarih = tahakkuk.VadeTarihi.Value.AddMonths(1);
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
            bool pasifDurum, tahakkukDurum = false;
            KiraBeyanIslemleri islemler = new KiraBeyanIslemleri();
            islemler.Eklenenler = new KiraBeyanModel();

            BeyanEkleVM ekleVM = GetirBeyanEkleVM(beyanArtis.Beyan_Id, true, beyanArtis);

            beyanArtis.Beyan = ekleVM;

            var tahakkuk = _tahakkukService.GetirListe(beyanArtis.KiraBeyan_Id);

            var kiraBeyan = _kiraBeyanService.Getir(beyanArtis.KiraBeyan_Id);

            beyanArtis.KiraParametre = GetirParametreDetay(true, null, beyanArtis);

            if (beyanArtis.KiraParametre.KiraParametre is null)
                return Json(new { Message = "Parametrelere Uygun Hesaplama Yöntemi Bulunmamaktadır!!!", success = false }, JsonRequestBehavior.AllowGet);


            var dosyalar = GetirBeyanDosyalar(beyanArtis.Beyan_Id);

            pasifDurum = BeyanPasifeAl(beyanArtis, ref islemler);


            //if (tahakkuk != null)
            //{
            //    foreach (var item in tahakkuk)
            //    {
            //        item.AktifMi = (int)EnmIslemDurumu.Kapandı;
            //        item.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
            //                                         User.GetUserPropertyValue("UserId") : null);
            //        item.GuncellenmeTarihi = DateTime.Now;

            //        _tahakkukService.Guncelle(item);
            //    }
            //}

            //if (kiraBeyan != null)
            //{
            //    kiraBeyan.AktifMi = (int)EnmIslemDurumu.Kapandı;
            //    kiraBeyan.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
            //                                     User.GetUserPropertyValue("UserId") : null);
            //    kiraBeyan.GuncellenmeTarihi = DateTime.Now;

            //    _kiraBeyanService.Guncelle(kiraBeyan);
            //}

            beyanArtis.Beyan.KiraArtisiMi = true;
            //beyanArtis.Beyan.BeyanYil = (beyanArtis.Beyan.BeyanYil.Value - 1);
            beyanArtis.Beyan_Id = BeyanEkle(beyanArtis.Beyan, ref islemler);

            KiraBeyanEkle(beyanArtis.Beyan_Id, beyanArtis.Kiraci_Id, beyanArtis.Gayrimenkul_Id, ref islemler);

            var ufeOrani = _ufeOranService.Getir(beyanArtis.UfeOran_Id);

            if (ufeOrani != null)
                beyanArtis.UfeOrani = (ufeOrani.Oran ?? 0);
            
            tahakkukDurum = TahakkukOlustur(beyanArtis, ref islemler);

            islemler.Eklenenler.ArtisMi = true;
            var result = _beyanService.KiraBeyanIslemleri(islemler);
            if (result)
            {
                BeyanDosyaEkle(beyanArtis.Beyan.Id, true, dosyalar.ToList(), ref islemler);
                ModelState.AddModelError("LogMessage", "Kira Artış İşlemi Başarıyla Gerçekleşti!!!");

                return Json(new { Message = "Kira Artış İşlemi Başarıyla Gerçekleştirildi.", success = true }, JsonRequestBehavior.AllowGet);
            }

            ModelState.AddModelError("LogMessage", "Kira Artış İşlemi Gerçekleştirilemedi!!!");

            return Json(new { Message = "Kira Artış İşlemi Gerçekleştirilemedi.", success = false }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region BeyanKapatma
        [HttpPost]
        public JsonResult KiraBeyanKapama(BeyanKapamaEkleVM beyanKapamaModel)
        {

            KiraBeyanIslemleri islemler = new KiraBeyanIslemleri();
            islemler.Kapananlar = new KiraBeyanModel();

            var beyan = _beyanService.Getir(beyanKapamaModel.Beyan_Id);

            var tahakkuk = _tahakkukService.GetirListe(beyanKapamaModel.KiraBeyan_Id);

            var kiraBeyan = _kiraBeyanService.Getir(beyanKapamaModel.KiraBeyan_Id);

            var gayrimenkul = _gayrimenkulservice.Getir(beyanKapamaModel.Gayrimenkul_Id);


            if (beyan != null)
            {
                beyan.KiraDurum_Id = beyanKapamaModel.KiraDurum_Id;
                beyan.AktifMi = (int)EnmIslemDurumu.Kapandı;
                beyan.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                                                 User.GetUserPropertyValue("UserId") : null);
                beyan.GuncellenmeTarihi = DateTime.Now;
                islemler.Kapananlar.Beyan = beyan;
                //_beyanService.Guncelle(beyan);
            }

            if (tahakkuk != null)
            {
                foreach (var item in tahakkuk)
                {
                    item.AktifMi = (int)EnmIslemDurumu.Kapandı;
                    item.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                                                     User.GetUserPropertyValue("UserId") : null);
                    item.GuncellenmeTarihi = DateTime.Now;

                    // _tahakkukService.Guncelle(item);
                }
                islemler.Kapananlar.Tahakkuklar = tahakkuk;

            }

            if (kiraBeyan != null)
            {
                kiraBeyan.AktifMi = (int)EnmIslemDurumu.Kapandı;
                kiraBeyan.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                                                 User.GetUserPropertyValue("UserId") : null);
                kiraBeyan.GuncellenmeTarihi = DateTime.Now;

                //  kiraBeyan = _kiraBeyanService.Guncelle(kiraBeyan);
                islemler.Kapananlar.KiraBeyan = kiraBeyan;
            }

            if (beyanKapamaModel.BeyanDosyalar != null)
                BeyanDosyaEkle(beyanKapamaModel.Beyan_Id, false, beyanKapamaModel.BeyanDosyalar, ref islemler);

            //if (kiraBeyan.Id > 0)
            //{
            if (gayrimenkul != null)
            {
                gayrimenkul.GayrimenkulDurum_Id = beyanKapamaModel.KiraDurum_Id;
                gayrimenkul.AktifMi = true;
                gayrimenkul.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                                                 User.GetUserPropertyValue("UserId") : null);
                gayrimenkul.GuncellenmeTarihi = DateTime.Now;
                islemler.Kapananlar.Gayrimenkul = gayrimenkul;
                //  _gayrimenkulservice.Guncelle(gayrimenkul);
            }

            var result = _beyanService.KiraBeyanIslemleri(islemler);

            if (result)
            {
                ModelState.AddModelError("LogMessage", "Beyan Kapama İşlemi Başarıyla Gerçekleştirildi!!!");

                return Json(new { Message = "Beyan Kapama İşlemi Başarıyla Gerçekleştirildi.", success = true }, JsonRequestBehavior.AllowGet);
            }

            ModelState.AddModelError("LogMessage", "Beyan Kapama İşlemi Gerçekleştirilemedi!!!");

            return Json(new { Message = "Beyan Kapama İşlemi Gerçekleştirilemedi.", success = false }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Metodlar

        [HttpPost]
        private KiraBeyanEkleVM BeyanGuncelleVeriGetir(Guid id)
        {
            Kiraci kiraci = new Kiraci();
            var request = new KiraBeyanRequest() { Guid = id };
            var kiraBeyan = _kiraBeyanService.GetirSorguListe(request).FirstOrDefault();
            var model = new KiraBeyanEkleVM();
            if (kiraBeyan != null)
            {
                var beyanDosya = _beyanDosyaService.GetirListe(false).Where(a => a.Beyan_Id == kiraBeyan.Beyan_Id).ToList();
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
                model.Beyan = GetirBeyanEkleVM(kiraBeyan.Beyanlar.Id, false, null);
                model.Beyan.BeyanTurSelectList = _selectLists.BeyanTurSelectList();
                model.Beyan.BeyanYilSelectList = _selectLists.BeyanYilSelectList();
                model.Beyan.KiraDurumSelectList = _selectLists.KiraDurumSelectList();
                model.Beyan.OdemePeriyotSelectList = _selectLists.OdemePeriyotSelectList();
                model.Beyan.KdvOraniSelectList = _selectLists.KdvOraniSelectList();
                model.Beyan.DamgaVergisiDurumSelectList = _selectLists.DamgaVergisiDurumSelectList();
                model.Beyan.DosyaTurleri = _dosyaService.GetirListe();
                model.Beyan.OtoparkTatilGunuSelectList = _selectLists.OtoparkTatilGunuSelectList();
                _beyanVM.Beyan.BeyanYilSelectList.First().Selected = true;
                _beyanVM.Beyan.KiraDurumSelectList.First().Selected = true;
                model.BeyanDosyalar = GetirBeyanDosyalar(kiraBeyan.Beyanlar.Id);
            }
            return model;

        }

        private BeyanEkleVM GetirBeyanEkleVM(int id, bool ArtisMi, KiraArtisEkleVM kiraArtis)
        {
            BeyanEkleVM ekleVm = new BeyanEkleVM();
            var beyanBilgi = _kiraBeyanService.GetirBeyan(id).Beyanlar;
            CultureInfo cultures = new CultureInfo("en-US");

            if (ArtisMi)
            {
                var ufeOrani = _ufeOranService.Getir(kiraArtis.UfeOran_Id);

                if (ufeOrani != null)
                    kiraArtis.UfeOrani = (ufeOrani.Oran ?? 0);

                decimal kiraTutar = ((beyanBilgi.KiraTutari * kiraArtis.UfeOrani / 100) + beyanBilgi.KiraTutari);

                #region BeyanArtis
                ekleVm.BeyanTur_Id = beyanBilgi.BeyanTur_Id;
                ekleVm.BeyanYil = (beyanBilgi.BeyanYil + 1);
                ekleVm.KiraDurum_Id = beyanBilgi.KiraDurum_Id;
                ekleVm.BeyanNo = beyanBilgi.BeyanNo;
                ekleVm.NoterSozlesmeNo = beyanBilgi.NoterSozlesmeNo.ToString();
                ekleVm.EncumenKararNo = int.Parse(kiraArtis.EncumenNo);
                ekleVm.IhaleEncumenTarihi = DateTime.Parse(kiraArtis.EncumenTarihi);
                ekleVm.BeyanTarihi = beyanBilgi.BeyanTarihi;
                ekleVm.KiraBaslangicTarihi = beyanBilgi.KiraBaslangicTarihi.Value.AddYears(1);
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
                ekleVm.OncekiKiraTutari = beyanBilgi.KiraTutari.ToString();
                ekleVm.KiraTutari = kiraTutar.ToString();
                ekleVm.Kdv = beyanBilgi.Kdv;
                ekleVm.BaslangicTaksitNo = beyanBilgi.BaslangicTaksitNo;
                ekleVm.KalanAy = beyanBilgi.KalanAy;
                ekleVm.DamgaAlinsinMi = (kiraArtis.DamgaKararAlinacakMi == true ? "1" : "0");
                ekleVm.BeyanAciklama = beyanBilgi.Aciklama;
                ekleVm.Id = beyanBilgi.Id;
                #endregion
            }
            else
            {
                #region NormalBeyan
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
                #endregion
            }

            return ekleVm;

        }
        private bool TahakkukOlustur(KiraArtisEkleVM artisModel, ref KiraBeyanIslemleri islemler)
        {
            bool beyanEkleSonuc = false;

            int AySayisi = GetirOdemeAySayisi(artisModel.Beyan.OdemePeriyotTur_Id.Value);
            if (AySayisi > 0)
            {
                var gayrimenkul = GetirGayrimenkul(artisModel.Gayrimenkul_Id);

                List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();

                var standart = StandartTahakkukEkle(artisModel.KiraBeyan_Id, artisModel.Beyan, artisModel.KiraParametre);

                if (standart != null)
                    tahakkukListe.AddRange(standart);

                if (tahakkukListe != null && tahakkukListe.Count > 0)
                {
                    var tahakkuk = TahakkukSatirEkle(artisModel.KiraBeyan_Id, AySayisi, artisModel.Beyan, gayrimenkul, artisModel.KiraParametre);

                    if (tahakkuk != null)
                        tahakkukListe.AddRange(tahakkuk);

                    // beyanEkleSonuc = _tahakkukService.Ekle(tahakkukListe);
                    islemler.Eklenenler.Tahakkuklar = new List<Tahakkuk>();
                    islemler.Eklenenler.Tahakkuklar = tahakkukListe;
                }
            }

            return beyanEkleSonuc;
        }

        private int GetirOdemeAySayisi(int periyotTurId)
        {
            int aySayisi = 0;
            var odemePeriyorTur = _odemePeriyotService.Getir(periyotTurId);

            if (odemePeriyorTur.OdemeAySayisi.HasValue)
                Int32.TryParse(odemePeriyorTur.OdemeAySayisi.ToString(), out aySayisi);
            return aySayisi;
        }

        private KiraParametreDetay GetirParametreDetay(bool ArtisMi, BeyanEkleVM beyan, KiraArtisEkleVM beyanArtis)
        {
            if (ArtisMi)
                beyan = beyanArtis.Beyan;


            var beyanTur = _beyanTurService.Getir(beyan.BeyanTur_Id);


            int beyanKod;
            if (beyanTur != null && beyanTur.Kod.Equals(1))
                Int32.TryParse(beyanTur.Kod.ToString(), out beyanKod);
            else if (beyanTur != null && beyanTur.Kod.Equals(2))
                Int32.TryParse(beyanTur.Kod.ToString(), out beyanKod);
            else
                Int32.TryParse(beyanTur.Kod.ToString(), out beyanKod);


            var beyanYil = _parametreService.Getir(beyan.BeyanYil.Value);

            int beyanYili;
            if (beyanYil != null && !ArtisMi)
                _ = Convert.ToInt32(beyanYil.Ad);
            if (beyanYil is null && ArtisMi)
                beyanYili = beyan.BeyanYil.Value - 1;
            else
                beyanYili = beyan.BeyanYil.Value;

            var kiraParametre = _kiraParametreService.GetirBeyanYil(beyanYili);

            KiraParametreDetay parametreDetay = new KiraParametreDetay
            {
                KiraParametre = kiraParametre
            };

            if (kiraParametre != null)
            {
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


                if (ArtisMi)
                    parametreDetay.Hesap = HesapDetay(beyanArtis, parametreDetay);
                else
                    parametreDetay.Hesap = HesapDetay(beyan, parametreDetay);

            }

            return parametreDetay;
        }

        private KiraParametreHesapDetay HesapDetay(BeyanEkleVM beyan, KiraParametreDetay parametreDetay)
        {
            decimal ihaleTutar, kiraTutar;

            KiraParametreHesapDetay detay = new KiraParametreHesapDetay();

            if (beyan.KiraTutari.Contains('.'))
                decimal.TryParse(beyan.KiraTutari, out kiraTutar);
            else
                decimal.TryParse(beyan.KiraTutari.Replace('.', ','), out kiraTutar);

            if (beyan.IhaleTutari.Contains('.'))
                decimal.TryParse(beyan.IhaleTutari, out ihaleTutar);
            else
                decimal.TryParse(beyan.IhaleTutari.Replace('.', ','), out ihaleTutar);

            detay.KararHarciTutar = ihaleTutar * parametreDetay.KiraParametre.KararHarciOran.Value;
            detay.TeminatTutar = ihaleTutar * parametreDetay.KiraParametre.TeminatOran.Value;
            detay.DamgaAlinsinMi = (beyan.DamgaAlinsinMi == "1" ? true : false);

            if (detay.DamgaAlinsinMi)
                detay.DamgaVergisiTutar = ihaleTutar * parametreDetay.KiraParametre.DamgaOran.Value;
            else
                detay.DamgaVergisiTutar = 0;

            detay.KdvTutar = ((beyan.Kdv.HasValue && beyan.Kdv.Value > 0) ? (kiraTutar * beyan.Kdv.Value / 100) : 0);
            detay.KiraTutar = kiraTutar;


            return detay;
        }

        private KiraParametreHesapDetay HesapDetay(KiraArtisEkleVM beyanArtis, KiraParametreDetay parametreDetay)
        {
            decimal kiraTutar, oncekiKiraTutar;

            BeyanEkleVM beyan = beyanArtis.Beyan;

            KiraParametreHesapDetay detay = new KiraParametreHesapDetay();

            int AySayisi = GetirOdemeAySayisi(beyan.OdemePeriyotTur_Id.Value);
            if (beyan.KiraTutari.Contains('.'))
                decimal.TryParse(beyan.KiraTutari, out kiraTutar);
            else
                decimal.TryParse(beyan.KiraTutari.Replace('.', ','), out kiraTutar);

            if (beyan.OncekiKiraTutari.Contains('.'))
                decimal.TryParse(beyan.OncekiKiraTutari, out oncekiKiraTutar);
            else
                decimal.TryParse(beyan.OncekiKiraTutari.Replace('.', ','), out oncekiKiraTutar);


            if (beyanArtis.DamgaKararAlinacakMi)
            {
                if (beyanArtis.DamgaKararArtisTuru > 0 && beyanArtis.DamgaKararArtisTuru > 1)
                {
                    detay.KararHarciTutar = ((kiraTutar * AySayisi) * parametreDetay.KiraParametre.KararHarciOran.Value);

                    detay.DamgaAlinsinMi = (beyan.DamgaAlinsinMi == "1" ? true : false);

                    if (detay.DamgaAlinsinMi)
                        detay.DamgaVergisiTutar = ((kiraTutar * AySayisi) * parametreDetay.KiraParametre.DamgaOran.Value);
                    else
                        detay.DamgaVergisiTutar = 0;
                }
                else
                {
                    detay.KararHarciTutar = ((kiraTutar - oncekiKiraTutar) * AySayisi * parametreDetay.KiraParametre.KararHarciOran.Value);

                    detay.DamgaAlinsinMi = (beyan.DamgaAlinsinMi == "1" ? true : false);

                    if (detay.DamgaAlinsinMi)
                        detay.DamgaVergisiTutar = ((kiraTutar - oncekiKiraTutar) * AySayisi * parametreDetay.KiraParametre.DamgaOran.Value);
                    else
                        detay.DamgaVergisiTutar = 0;
                }
            }

            if (beyanArtis.TeminatAlinacakMi)
                detay.TeminatTutar = ((kiraTutar * AySayisi) * parametreDetay.KiraParametre.TeminatOran.Value);
            else
                detay.TeminatTutar = ((kiraTutar - oncekiKiraTutar) * AySayisi * parametreDetay.KiraParametre.TeminatOran.Value);

            detay.KdvTutar = ((beyan.Kdv.HasValue && beyan.Kdv.Value > 0) ? (kiraTutar * beyan.Kdv.Value / 100) : 0);
            detay.KiraTutar = kiraTutar;

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
            string[] otoparkGun;

            if (beyan.OtoparkTatilGun != null)
                otoparkGun = beyan.OtoparkTatilGun.Split(',');
            else
                otoparkGun = null;

            bool standartOtoparkMi = true;
            List<Tahakkuk> tahakkukListe = new List<Tahakkuk>();


            if (gayrimenkul.AracKapasitesi.HasValue && gayrimenkul.AracKapasitesi.Value > 0)
                standartOtoparkMi = false;

            var dateSon = _resmiTatilService.TatilGunuKontrol(beyan.KiraBaslangicTarihi.Value.AddDays(beyan.MusadeliGunSayisi.Value));

            for (int i = 0; i < OdemeAySayisi; i++)
            {
                if (parametreDetay.BeyanTurKod.Equals(2))
                {
                    decimal otoparkTutar;

                    decimal gunAraligi = _resmiTatilService.TarihAraligiGunSayisi(beyan.KiraBaslangicTarihi.Value.AddMonths(i), beyan.KiraBaslangicTarihi.Value.AddMonths(i + 1));

                    decimal otoparkGunSayisi = _resmiTatilService.TatilGunuKontrol(beyan.KiraBaslangicTarihi.Value.AddMonths(i), beyan.KiraBaslangicTarihi.Value.AddMonths(i + 1),
                                                               otoparkGun, beyan.ResmiTatilVarmi);
                    _ = (parametreDetay.KiraParametre.OtoparkBirimFiyat.Value * gayrimenkul.AracKapasitesi.Value * (gunAraligi - otoparkGunSayisi));

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

        private void BeyanPasifeAl(KiraBeyanEkleVM kiraBeyanModel, ref KiraBeyanIslemleri islemler)
        {
            var tahakkukListesi = _tahakkukService.GetirListe(kiraBeyanModel.Beyan.Id);
            var kiraBeyan = _kiraBeyanService.Getir(kiraBeyanModel.Beyan.Id, kiraBeyanModel.Gayrimenkul.Id, kiraBeyanModel.Kiraci.Id);
            var beyan = _beyanService.Getir(kiraBeyanModel.Beyan.Id);

            islemler.PasifeAlinanlar.Tahakkuklar = new List<Tahakkuk>();
            islemler.PasifeAlinanlar.KiraBeyan = new Kira_Beyan();
            islemler.PasifeAlinanlar.Beyan = new Beyan();

            islemler.PasifeAlinanlar.Tahakkuklar.AddRange(tahakkukListesi);
            islemler.PasifeAlinanlar.KiraBeyan = kiraBeyan;
            islemler.PasifeAlinanlar.Beyan = beyan;

            //if (tahakkukListesi.Where(x => x.OdemeDurumu == true).ToList().Count > 0)
            //{
            //    ModelState.AddModelError("LogMessage", "Beyana ait ödenmiş veri bulunmaktadır. Beyanı güncelleyemezsiniz.");

            //    return;
            //}
            ////Beyan Pasife Alınır
            //var beyan = kiraBeyanModel.Beyan;
            //beyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            //kiraBeyanModel.Beyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            //BeyanEkle(kiraBeyanModel.Beyan);

            ////Tahakkuklar Pasife alınır.
            //foreach (var item in tahakkukListesi)
            //{
            //    item.AktifMi = (int)EnmIslemDurumu.Pasif;
            //    _tahakkukService.Guncelle(item);
            //}

            ////Kira Beyan Sayfası Pasife Alınır.

            //kiraBeyan.AktifMi = (int)EnmIslemDurumu.Pasif;
            //_kiraBeyanService.Guncelle(kiraBeyan);

        }

        private bool BeyanPasifeAl(KiraArtisEkleVM artisModel, ref KiraBeyanIslemleri islemler)
        {
            bool sonuc = false;
            islemler.Kapananlar = new KiraBeyanModel();
            islemler.Kapananlar.Tahakkuklar = new List<Tahakkuk>();

            var tahakkukListesi = _tahakkukService.GetirListe(artisModel.Beyan_Id);

            if (tahakkukListesi.Where(x => x.OdemeDurumu == true).ToList().Count > 0)
            {
                ModelState.AddModelError("LogMessage", "Beyana ait ödenmiş veri bulunmaktadır. Beyanı güncelleyemezsiniz.");

                return false;
            }
            //Beyan Pasife Alınır
            artisModel.Beyan.AktifMi = (int)EnmIslemDurumu.Kapandı;
            BeyanEkle(artisModel.Beyan, ref islemler);

            //Tahakkuklar Pasife alınır.
            foreach (var item in tahakkukListesi)
            {
                item.AktifMi = (int)EnmIslemDurumu.Kapandı;
                //_tahakkukService.Guncelle(item);
            }
            islemler.Kapananlar.Tahakkuklar = tahakkukListesi;
            //Kira Beyan Sayfası Pasife Alınır.
            var kiraBeyan = _kiraBeyanService.Getir(artisModel.Beyan_Id, artisModel.Gayrimenkul_Id, artisModel.Kiraci_Id);
            kiraBeyan.AktifMi = (int)EnmIslemDurumu.Kapandı;
            islemler.Kapananlar.KiraBeyan = kiraBeyan;

            //var kirabeyanSonuc = _kiraBeyanService.Guncelle(kiraBeyan);

            //if (kirabeyanSonuc.AktifMi == (int)EnmIslemDurumu.Kapandı)
            //    sonuc = true;
            sonuc = true;
            return sonuc;
        }

        public ActionResult BeyanSil(Guid guidId, int kiraciId, int gayrimenkulId)
        {
            var beyan = _beyanService.GetirGuid(guidId);
            int kiraBeyanId = _kiraBeyanService.GetirBeyan(beyan.Id).Id;
            var tahakkukListesi = _tahakkukService.GetirListe(kiraBeyanId);
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

                var kiraciBilgi = _kiraciService.GetirVergiNo(long.Parse(TcVergiNo));

                SicilServisVm sicilBilgisi = new SicilServisVm();

                if (kiraciBilgi is null)
                    sicilBilgisi = _sicilService.GetirSicilBilgisi(vergiNo, tcNo);

                if (kiraciBilgi != null && kiraciBilgi.SicilNo > 0)
                {
                    _beyanVM.Kiraci = new KiraciEkleVM()
                    {
                        Id = kiraciBilgi.Id,
                        SicilNo = kiraciBilgi.SicilNo,
                        VergiNo = kiraciBilgi.VergiNo,
                        Ad = kiraciBilgi.Ad,
                        Soyad = kiraciBilgi.Soyad,
                        Tanim = kiraciBilgi.Tanim,
                        IlAdi = kiraciBilgi.IlAdi,
                        IlceAdi = kiraciBilgi.IlceAdi,
                        MahalleAdi = kiraciBilgi.MahalleAdi,
                        AcikAdres = kiraciBilgi.AcikAdres,
                        VergiDairesi = kiraciBilgi.VergiDairesi,
                    };
                }
                else if (kiraciBilgi is null && sicilBilgisi != null && !string.IsNullOrEmpty(sicilBilgisi.SicilNo))
                {
                    _beyanVM.Kiraci = new KiraciEkleVM()
                    {
                        Id = _kiraciService.GetirVergiNo(long.Parse(sicilBilgisi.VergiNo)).Id,
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

        public Beyan_GayrimenkulEkleVM GetirGayrimenkul(int GayrimenkulId)
        {
            if (GayrimenkulId > 0)
            {
                GetirSelectList();

                var gayrimenkul = _gayrimenkulservice.Getir(GayrimenkulId);

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
                        Id = beyan.Beyanlar.Id,
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
                var dosyalar = _beyanDosyaService.GetirBeyanIdFull(BeyanId);


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
        private int BeyanDosyaEkle(int beyanId, bool artisMi, List<Beyan_DosyaVM> dosyalar, ref KiraBeyanIslemleri islemler)
        {
            List<Beyan_Dosya> dosyaList = new List<Beyan_Dosya>();
            Beyan_Dosya dosya = new Beyan_Dosya();
            string filePath = Server.MapPath("~/Dosyalar/Beyan/");

            if (dosyalar != null)
            {
                foreach (var item in dosyalar)
                {
                    Guid guidDosya = Guid.NewGuid();
                    string dosyaAdi2 = item.DosyaAdi.Split('.').Last();
                    dosya.Guid = guidDosya;
                    dosya.BeyanDosya_Tur_Id = item.BeyanDosyaTur_Id;
                    dosya.Beyan_Id = beyanId;
                    dosya.Ad = (!artisMi ? guidDosya.ToString() + "." + dosyaAdi2 : item.DosyaAdi);
                    dosya.OlusturulmaTarihi = DateTime.Now;
                    dosya.OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null);
                    dosya.AktifMi = true;
                    dosya.FilePath = filePath;
                    dosya.BeyanDosya = item.BeyanDosya;
                    dosyaList.Add(dosya);
                    var result = _beyanDosyaService.Ekle(dosya);

                    if (result.Id > 0 && !artisMi)
                    {
                        byte[] fileBytes = Convert.FromBase64String(item.BeyanDosya);
                        System.IO.File.WriteAllBytes(filePath + dosya.Ad, fileBytes);
                    }
                }

                islemler.Kapananlar.BeyanDosyalar = new List<Beyan_Dosya>();
                islemler.Kapananlar.BeyanDosyalar.AddRange(dosyaList);
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
        private int BeyanSicilEkle(KiraciEkleVM kiraciBilgi, ref KiraBeyanIslemleri kiraBeyanIslemleri)
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
                kiraBeyanIslemleri.Eklenenler.Kiraci = kiraci;
                //var result = _kiraciService.Ekle(kiraci);

                //if (result.Id > 0)
                //    return result.Id;
            }

            return 0;
        }

        /// <summary>
        ///Beyan Ekleme Metodu
        ///Beyan Ekleme İşlemleri Bu Metodun İçerisinde Gerçekleşmektedir.
        /// </summary>
        [HttpGet]
        private int BeyanEkle(BeyanEkleVM beyanBilgi, ref KiraBeyanIslemleri kiraBeyanIslemleri)
        {

            if (beyanBilgi != null)
            {
                CultureInfo cultures = new CultureInfo("en-US");
                var beyanParametre = _parametreService.Getir(beyanBilgi.BeyanYil.Value);
                if (beyanParametre != null)
                    beyanBilgi.BeyanYil = int.Parse(beyanParametre.Ad);

                Beyan beyan;
                if (beyanBilgi.KiraArtisiMi.HasValue && beyanBilgi.KiraArtisiMi.Value)
                {
                    beyan = new Beyan()
                    {
                        OncekiBeyanId = beyanBilgi.Id,
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
                        DamgaKararArtisTuru = beyanBilgi.DamgaKararArtisTuru,
                        TeminatArtisTuru = beyanBilgi.TeminatArtisTuru,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = (int)EnmIslemDurumu.Aktif
                    };

                    kiraBeyanIslemleri.Eklenenler.Beyan = beyan;
                }
                else
                {
                    beyan = new Beyan()
                    {
                        Id = beyanBilgi.Id,
                        OncekiBeyanId = beyanBilgi.Id,
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
                        DamgaKararArtisTuru = beyanBilgi.DamgaKararArtisTuru,
                        TeminatArtisTuru = beyanBilgi.TeminatArtisTuru,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = beyanBilgi.AktifMi
                    };

                    kiraBeyanIslemleri.Kapananlar.Beyan = beyan;
                }

                //if (!beyanBilgi.KiraArtisiMi.HasValue && beyanBilgi.Id == 0)
                //    result = _beyanService.Ekle(beyan);
                //else if (beyanBilgi.KiraArtisiMi.HasValue && beyanBilgi.KiraArtisiMi.Value && beyan.Id <= 0)
                //    result = _beyanService.Ekle(beyan);
                //else
                //    result = _beyanService.Guncelle(beyan);

                //if (result.Id > 0)
                //    return result.Id;
            }

            return 0;
        }


        [HttpGet]
        private int BeyanEkle(BeyanEkleVM beyanBilgi)
        {

            if (beyanBilgi != null)
            {
                CultureInfo cultures = new CultureInfo("en-US");
                var beyanParametre = _parametreService.Getir(beyanBilgi.BeyanYil.Value);
                if (beyanParametre != null)
                    beyanBilgi.BeyanYil = int.Parse(beyanParametre.Ad);

                Beyan beyan;
                if (beyanBilgi.KiraArtisiMi.HasValue && beyanBilgi.KiraArtisiMi.Value)
                {
                    beyan = new Beyan()
                    {
                        OncekiBeyanId = beyanBilgi.Id,
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
                        DamgaKararArtisTuru = beyanBilgi.DamgaKararArtisTuru,
                        TeminatArtisTuru = beyanBilgi.TeminatArtisTuru,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = (int)EnmIslemDurumu.Aktif
                    };
                }
                else
                {
                    beyan = new Beyan()
                    {
                        Id = beyanBilgi.Id,
                        OncekiBeyanId = beyanBilgi.Id,
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
                        DamgaKararArtisTuru = beyanBilgi.DamgaKararArtisTuru,
                        TeminatArtisTuru = beyanBilgi.TeminatArtisTuru,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = beyanBilgi.AktifMi
                    };
                }

                //if (!beyanBilgi.KiraArtisiMi.HasValue && beyanBilgi.Id == 0)
                //    result = _beyanService.Ekle(beyan);
                //else if (beyanBilgi.KiraArtisiMi.HasValue && beyanBilgi.KiraArtisiMi.Value && beyan.Id <= 0)
                //    result = _beyanService.Ekle(beyan);
                //else
                //    result = _beyanService.Guncelle(beyan);

                //if (result.Id > 0)
                //    return result.Id;
            }

            return 0;
        }

        /// <summary>
        ///Kira Beyan Ekleme Metodu
        ///Kira Beyan Ekleme İşlemleri Bu Metodun İçerisinde Gerçekleşmektedir.
        /// </summary>
        [HttpGet]
        private int KiraBeyanEkle(int Beyan_Id, int Kiraci_Id, int Gayrimenkul_Id, ref KiraBeyanIslemleri islemler)
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
            islemler.Eklenenler.KiraBeyan = kiraBeyan;
            //var result = _kiraBeyanService.Ekle(kiraBeyan);

            //if (result.Id > 0)
            //    return result.Id;

            return 0;
        }

        [HttpPost]
        public JsonResult GetirBeyanKapamaDosya(int kiraDurumId)
        {
            var model = new List<KiraDurum_DosyaTurKapamaVM>();

            var dosyaBilgi = _kiraDurumDosyaTurService.GetirKiraDurumListe(kiraDurumId);

            if (dosyaBilgi != null)
            {
                foreach (var item in dosyaBilgi)
                {
                    model.Add(new KiraDurum_DosyaTurKapamaVM()
                    {
                        Id = item.Id,
                        BeyanDosyaTur_Id = item.BeyanDosyaTur_Id,
                        KiraDurum_Id = item.KiraDurum_Id,
                        AktifMi = item.AktifMi,
                        TasinmazDurum = item.Kira_Durumlari.Ad,
                        DosyaAdi = item.BeyanDosya_Turleri.Ad
                    });
                }
            }

            return Json(new { data = model, success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DigerIslemMetodları

        void GetirSelectList()
        {
            _beyanVM.Beyan = new BeyanEkleVM();
            _beyanVM.Kiraci = new KiraciEkleVM();
            _beyanVM.Gayrimenkul = new Beyan_GayrimenkulEkleVM();
            _beyanVM.Beyan.BeyanTurSelectList = _selectLists.BeyanTurSelectList();
            _beyanVM.Beyan.BeyanYilSelectList = _selectLists.BeyanYilSelectList();
            _beyanVM.Beyan.KiraDurumSelectList = _selectLists.KiraDurumSelectList();
            _beyanVM.Beyan.BeyanYilSelectList.First().Selected = true;
            _beyanVM.Beyan.KiraDurumSelectList.First().Selected = true;
            _beyanVM.Beyan.OdemePeriyotSelectList = _selectLists.OdemePeriyotSelectList();
            _beyanVM.Beyan.KdvOraniSelectList = _selectLists.KdvOraniSelectList();
            _beyanVM.Beyan.DamgaVergisiDurumSelectList = _selectLists.DamgaVergisiDurumSelectList();
            _beyanVM.Beyan.DosyaTurleri = _dosyaService.GetirListe();
            _beyanVM.Beyan.OtoparkTatilGunuSelectList = _selectLists.OtoparkTatilGunuSelectList();

        }

        public ActionResult GetirBeyanTable(KiraBeyanRequest request, int? page, int pageSize = 15)
        {
            var model = new KiraBeyanVM();

            var beyanlar = _kiraBeyanService.GetirSorguListe(request);

            if (beyanlar != null)
            {
                model.PageNumber = page ?? 1;
                model.PageSize = pageSize;
                model.IlceSelectList = _selectLists.IlceSelectList();
                model.GayrimenkulSelectList = _selectLists.GayrimenkulSelectList();
                model.BeyanTurSelectList = _selectLists.BeyanTurSelectList();
                model.KiraDurumSelectList = _selectLists.KiraDurumSelectList();
                model.OdemePeriyotSelectList = _selectLists.OdemePeriyotSelectList();
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
            HtmlDocument doc = new HtmlDocument
            {
                OptionFixNestedTags = true
            };
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
            KiraBeyanVM model = new KiraBeyanVM
            {
                TahakkukDetay = new List<TahakkukVM>()
            };

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
                        OdemeDurumu = item.OdemeDurumu.Value,
                        AktifMi = (int)item.AktifMi
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