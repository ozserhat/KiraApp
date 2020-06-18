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
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;
using System.Web.Mvc.Html;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    public class RaporlarController : Controller
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
        public RaporlarController(IGayrimenkulService gayrimenkulservice,
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

        #region SelectList
        public SelectList OdemeDurumuSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Tahsil",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Vade",
                                    Value="0"
                                    }
            };

            return new SelectList(newList, "Value", "Text");
        }
        public SelectList GayrimenKulSelectList()
        {
            var gayrimenKuller = _gayrimenkulservice.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(gayrimenKuller, "Id", "Ad");
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


        #endregion
        // GET: Kira/Raporlar
        #region Beyan Raporlama
        public ActionResult Beyan(KiraBeyanRequest request, int? page, int pageSize = 15)
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




        public string GridExportToExcelBeyanRapor(KiraBeyanRequest kiraBeyanRequest)
        {
            var model = new KiraBeyanVM();
            DataSet dataSet = new DataSet("dataSet");
            dataSet.Namespace = "NetFrameWork";

            if (kiraBeyanRequest.BeyanYil.HasValue)
            {
                var yillist = _parametreService.Getir(kiraBeyanRequest.BeyanYil.Value);

                kiraBeyanRequest.BeyanYil = int.Parse(yillist.Deger);
            }

            if (kiraBeyanRequest.Kdv.HasValue)
            {
                var kdvlist = _parametreService.Getir(kiraBeyanRequest.Kdv.Value);

                kiraBeyanRequest.Kdv = int.Parse(kdvlist.Ad);
            }

            var beyanlar = _kiraBeyanService.GetirSorguListeAktif(kiraBeyanRequest);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[29] {
                        new DataColumn("Beyan No"),
                        new DataColumn("Beyan Yıl"),
                        new DataColumn("Encümen Karar No"),
                        new  DataColumn("Noter Sözleşme No"),
                        new DataColumn("Teminat No"),
                        new DataColumn("İhale Tutarı"),
                        new DataColumn("Başlangıç Taksit No"),
                        new DataColumn("Kalan Ay"),
                        new DataColumn("Kullanim Alanı"),
                        new DataColumn("Sözleşme Süresi"),
                        new DataColumn("Kira Tutarı"),
                        new DataColumn("Damga Alınsın Mı"),
                        new DataColumn("Damga Karar Artış Türü"),
                        new DataColumn("Teminat Artış Türü"),
                        new DataColumn("Müsadeli Gün Sayısı"),
                        new DataColumn("KDV"),
                        new DataColumn("Otopark Tatil Gün"),
                        new DataColumn("Resmi Tatil Var Mı"),
                        new DataColumn("Beyan Tarihi"),
                        new DataColumn("İhale Encümen Tarihi"),
                        new DataColumn("Sözleşme Tarihi"),
                        new DataColumn("Sözleşme Bitiş Tarihi"),
                        new DataColumn("Kira Yenileme Periyot"),
                        new DataColumn("Teminat Tarihi"),
                        new DataColumn("Kira Başlangıç Tarihi"),
                        new DataColumn("Beyan Türü"),
                        new DataColumn("Kira Durumu"),
                        new DataColumn("Ödeme Periyot Türü"),
                        new DataColumn("Beyan Kapatma Tarihi")});
            var odemePeriyotListesi = _odemePeriyotService.GetirListe();
            var beyanTurListesi = _beyanTurService.GetirListe();
            var kiraDurumListesi = _kiraDurumService.GetirListe();
            foreach (var item in beyanlar)
            {
                var damgaAlinsinMi = item.Beyanlar.DamgaAlinsinMi == true ? "Evet" : "Hayır";
                var damgaKararArtisTuru = item.Beyanlar.DamgaKararArtisTuru == 1 ? "Üfe Oranı" : "Tüfe Oranı";
                var teminatArtisTuru = item.Beyanlar.TeminatArtisTuru == 1 ? "Tam Al" : "Fark Al";
                var resmiTatilVarmi = item.Beyanlar.ResmiTatilVarmi == true ? "Evet" : "Hayır";
                var beyanTur = beyanTurListesi.Where(x => x.Id == item.Beyanlar.BeyanTur_Id).FirstOrDefault().Ad;
                var odemePeriyoTur = odemePeriyotListesi.Where(x => x.Id == item.Beyanlar.OdemePeriyotTur_Id).FirstOrDefault().Ad;
                var kiraDurumu = kiraDurumListesi.Where(x => x.Id == item.Beyanlar.KiraDurum_Id).FirstOrDefault().Ad;

                dt.Rows.Add(item.Beyanlar.BeyanNo, item.Beyanlar.BeyanYil, item.Beyanlar.EncumenKararNo, item.Beyanlar.NoterSozlesmeNo, item.Beyanlar.TeminatNo,
                    item.Beyanlar.IhaleTutari, item.Beyanlar.BaslangicTaksitNo, item.Beyanlar.KalanAy, item.Beyanlar.KullanimAlani, item.Beyanlar.SozlesmeSuresi,
                    item.Beyanlar.KiraTutari, damgaAlinsinMi, damgaKararArtisTuru, teminatArtisTuru, item.Beyanlar.MusadeliGunSayisi,
                    item.Beyanlar.Kdv, item.Beyanlar.OtoparkTatilGun, resmiTatilVarmi, item.Beyanlar.BeyanTarihi, item.Beyanlar.IhaleEncumenTarihi, item.Beyanlar.SozlesmeTarihi,
                    item.Beyanlar.SozlesmeBitisTarihi, item.Beyanlar.KiraYenilemePeriyot, item.Beyanlar.TeminatTarihi, item.Beyanlar.KiraBaslangicTarihi,
                    item.Beyanlar.BeyanTur.Ad, kiraDurumu, odemePeriyoTur, item.Beyanlar.BeyanKapatmaTarihi);
            }
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);

            return json;
        }
        #endregion

        #region Artış Yapılan Beyan
        public ActionResult ArtisYapilanBeyan(KiraBeyanRequest request, int? page, int pageSize = 15)
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

            var beyanlar = _kiraBeyanService.GetirSorguListe(request).Where(x => x.Beyanlar.DamgaKararArtisTuru > 0 || x.Beyanlar.DamgaKararArtisTuru != null && (x.Beyanlar.OncekiBeyanId != null)).ToList();

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






        public string GridExportToExcelArtisRapor(KiraBeyanRequest kiraBeyanRequest)
        {
            var model = new KiraBeyanVM();
            DataSet dataSet = new DataSet("dataSet");
            dataSet.Namespace = "NetFrameWork";

            if (kiraBeyanRequest.BeyanYil.HasValue)
            {
                var yillist = _parametreService.Getir(kiraBeyanRequest.BeyanYil.Value);

                kiraBeyanRequest.BeyanYil = int.Parse(yillist.Deger);
            }

            if (kiraBeyanRequest.Kdv.HasValue)
            {
                var kdvlist = _parametreService.Getir(kiraBeyanRequest.Kdv.Value);

                kiraBeyanRequest.Kdv = int.Parse(kdvlist.Ad);
            }

            var beyanlar = _kiraBeyanService.GetirSorguListeAktif(kiraBeyanRequest).Where(x => x.Beyanlar.DamgaKararArtisTuru > 0 || x.Beyanlar.DamgaKararArtisTuru != null && (x.Beyanlar.OncekiBeyanId != null)).ToList(); ;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[29] {
                        new DataColumn("Beyan No"),
                        new DataColumn("Beyan Yıl"),
                        new DataColumn("Encümen Karar No"),
                        new  DataColumn("Noter Sözleşme No"),
                        new DataColumn("Teminat No"),
                        new DataColumn("İhale Tutarı"),
                        new DataColumn("Başlangıç Taksit No"),
                        new DataColumn("Kalan Ay"),
                        new DataColumn("Kullanim Alanı"),
                        new DataColumn("Sözleşme Süresi"),
                        new DataColumn("Kira Tutarı"),
                        new DataColumn("Damga Alınsın Mı"),
                        new DataColumn("Damga Karar Artış Türü"),
                        new DataColumn("Teminat Artış Türü"),
                        new DataColumn("Müsadeli Gün Sayısı"),
                        new DataColumn("KDV"),
                        new DataColumn("Otopark Tatil Gün"),
                        new DataColumn("Resmi Tatil Var Mı"),
                        new DataColumn("Beyan Tarihi"),
                        new DataColumn("İhale Encümen Tarihi"),
                        new DataColumn("Sözleşme Tarihi"),
                        new DataColumn("Sözleşme Bitiş Tarihi"),
                        new DataColumn("Kira Yenileme Periyot"),
                        new DataColumn("Teminat Tarihi"),
                        new DataColumn("Kira Başlangıç Tarihi"),
                        new DataColumn("Beyan Türü"),
                        new DataColumn("Kira Durumu"),
                        new DataColumn("Ödeme Periyot Türü"),
                        new DataColumn("Beyan Kapatma Tarihi")});
            var odemePeriyotListesi = _odemePeriyotService.GetirListe();
            var beyanTurListesi = _beyanTurService.GetirListe();
            var kiraDurumListesi = _kiraDurumService.GetirListe();
            foreach (var item in beyanlar)
            {
                var damgaAlinsinMi = item.Beyanlar.DamgaAlinsinMi == true ? "Evet" : "Hayır";
                var damgaKararArtisTuru = item.Beyanlar.DamgaKararArtisTuru == 1 ? "Üfe Oranı" : "Tüfe Oranı";
                var teminatArtisTuru = item.Beyanlar.TeminatArtisTuru == 1 ? "Tam Al" : "Fark Al";
                var resmiTatilVarmi = item.Beyanlar.ResmiTatilVarmi == true ? "Evet" : "Hayır";
                var beyanTur = beyanTurListesi.Where(x => x.Id == item.Beyanlar.BeyanTur_Id).FirstOrDefault().Ad;
                var odemePeriyoTur = odemePeriyotListesi.Where(x => x.Id == item.Beyanlar.OdemePeriyotTur_Id).FirstOrDefault().Ad;
                var kiraDurumu = kiraDurumListesi.Where(x => x.Id == item.Beyanlar.KiraDurum_Id).FirstOrDefault().Ad;

                dt.Rows.Add(item.Beyanlar.BeyanNo, item.Beyanlar.BeyanYil, item.Beyanlar.EncumenKararNo, item.Beyanlar.NoterSozlesmeNo, item.Beyanlar.TeminatNo,
                    item.Beyanlar.IhaleTutari, item.Beyanlar.BaslangicTaksitNo, item.Beyanlar.KalanAy, item.Beyanlar.KullanimAlani, item.Beyanlar.SozlesmeSuresi,
                    item.Beyanlar.KiraTutari, damgaAlinsinMi, damgaKararArtisTuru, teminatArtisTuru, item.Beyanlar.MusadeliGunSayisi,
                    item.Beyanlar.Kdv, item.Beyanlar.OtoparkTatilGun, resmiTatilVarmi, item.Beyanlar.BeyanTarihi, item.Beyanlar.IhaleEncumenTarihi, item.Beyanlar.SozlesmeTarihi,
                    item.Beyanlar.SozlesmeBitisTarihi, item.Beyanlar.KiraYenilemePeriyot, item.Beyanlar.TeminatTarihi, item.Beyanlar.KiraBaslangicTarihi,
                    item.Beyanlar.BeyanTur, kiraDurumu, odemePeriyoTur, item.Beyanlar.BeyanKapatmaTarihi);
            }
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);

            return json;
        }

        #endregion


        #region Beyan Yapılan Gayrimenkuller

        public ActionResult BeyanYapilanGayrimenkul(KiraBeyanRequest request, int? page, int pageSize = 15)
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

            var beyanlar = _kiraBeyanService.GetirSorguListeAktif(request).Where(x => x.AktifMi == (int)EnmIslemDurumu.Aktif);

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


        public string GridExportToExcelBeyanYapilanGayrimenkulRapor(KiraBeyanRequest kiraBeyanRequest)
        {
            var model = new KiraBeyanVM();
            DataSet dataSet = new DataSet("dataSet");
            dataSet.Namespace = "NetFrameWork";

            if (kiraBeyanRequest.BeyanYil.HasValue)
            {
                var yillist = _parametreService.Getir(kiraBeyanRequest.BeyanYil.Value);

                kiraBeyanRequest.BeyanYil = int.Parse(yillist.Deger);
            }

            if (kiraBeyanRequest.Kdv.HasValue)
            {
                var kdvlist = _parametreService.Getir(kiraBeyanRequest.Kdv.Value);

                kiraBeyanRequest.Kdv = int.Parse(kdvlist.Ad);
            }

            var beyanlar = _kiraBeyanService.GetirSorguListeAktif(kiraBeyanRequest);


            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[10] {
                        new DataColumn("Beyan No"),
                        new DataColumn("Sicil No"),
                        new DataColumn("Dosya No"),
                        new DataColumn("Gayrimenkul No"),
                             new DataColumn("Kiracı Bilgisi"),
                        new DataColumn("Sorumlu Personel"),
                        new DataColumn("İl"),
                        new DataColumn("İlçe"),
                        new DataColumn("Mahalle"),
                        new DataColumn("Durum"),
                       });
            var mahalleListesi = _mahalleService.GetirListe();
            var ilceListesi = _ilceService.GetirListe();
            foreach (var item in beyanlar)
            {
                var durum = "Aktif";
                var sorumluPersonel = item.SorumluPersoneller.UserName ?? "";
                var ilAdi = "Ankara";
                var ilceAdi = ilceListesi.Where(x => x.Id == item.Gayrimenkuller.Ilce_Id).FirstOrDefault().Ad;
                var mahalleAdi = mahalleListesi.Where(x => x.Id == item.Gayrimenkuller.Mahalle_Id).FirstOrDefault().Ad;

                dt.Rows.Add(item.Beyanlar.BeyanNo, item.Kiracilar.SicilNo, item.Gayrimenkuller.DosyaNo,
                    item.Gayrimenkuller.GayrimenkulNo, item.Kiracilar.Ad, sorumluPersonel, ilAdi, ilceAdi, mahalleAdi, durum);
            }
            string json = JsonConvert.SerializeObject(dt, Formatting.Indented);

            return json;
        }
        #endregion

        #region GayrimenKule Özel Raporlar


        public ActionResult GayrimenkuleOzelBeyan(KiraBeyanRequest request, int? page, int pageSize = 15)
        {
            var model = new KiraBeyanVM();


            var beyanlar = _kiraBeyanService.GetirSorguListeAktif(request).Where(x => x.AktifMi == (int)EnmIslemDurumu.Aktif);

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (beyanlar != null)
            {
                model.GayrimenkulSelectList = GayrimenkulSelectList();

                model.Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count());
                model.TotalRecordCount = beyanlar.Count();
            }

            return View(model);
        }



        #endregion

        #region Tahsilat Raporu
        public ActionResult TahsilatRaporu(TahakkukRequest request, int? page, int pageSize = 15)
        {
            var model = new TahakkukVM();

            var tahakkuklar = _tahakkukService.GetirSorguListe(request);

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (tahakkuklar != null)
            {
                model.OdemeDurumuSelectList = OdemeDurumuSelectList();
                model.GayrimenKulSelectList = GayrimenKulSelectList();
                model.Tahakkuklar = new StaticPagedList<Tahakkuk>(tahakkuklar, model.PageNumber, model.PageSize, tahakkuklar.Count());
                model.TotalRecordCount = tahakkuklar.Count();
            }

            return View(model);
        }
        #endregion
    }
}