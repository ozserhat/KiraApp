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

namespace Framework.WebUI.Areas.Kira.Controllers
{
    [CustomAuthorize(Roles = "Kira")]
    public class BeyanController : Controller
    {
        #region Constructor

        private IIlService _ilService;
        private IIlceService _ilceService;
        private IMahalleService _mahalleService;
        private IGayrimenkulTurService _turService;
        private IGayrimenkulService _gayrimenkulservice;
        private IGayrimenkulDosya_TurService _dosyaTurService;
        private IBeyanService _beyanService;
        private IKira_BeyanService _kiraBeyanService;
        private ISicilService _sicilService;
        public BeyanController(IGayrimenkulService gayrimenkulservice, IGayrimenkulTurService turService,
            IIlService ilService, IIlceService ilceService,
            IMahalleService mahalleService,
            IGayrimenkulDosya_TurService dosyaTurService,
            IBeyanService beyanService,
            IKira_BeyanService kiraBeyanService,
            ISicilService sicilService)
        {
            _ilService = ilService;
            _ilceService = ilceService;
            _mahalleService = mahalleService;
            _turService = turService;
            _gayrimenkulservice = gayrimenkulservice;
            _dosyaTurService = dosyaTurService;
            _beyanService = beyanService;
            _kiraBeyanService = kiraBeyanService;
            _sicilService = sicilService;
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

        public SelectList TurSelectList()
        {
            var turler = _turService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(turler, "Id", "Ad");
        }

        public SelectList IlSelectList()
        {
            var iller = _ilService.GetirListe().Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(iller, "Id", "Ad");
        }

        [HttpPost]
        public JsonResult IlceSelectList(int ilId)
        {
            var ilceler = _ilceService.GetirListe().Where(a => a.Il_Id == ilId).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return Json(new { Data = ilceler, success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult MahalleSelectList(int ilceId)
        {
            var mahalleler = _mahalleService.GetirListe().Where(a => a.Ilce_Id == ilceId).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return Json(new { Data = mahalleler, success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SicilSorgulama

        [HttpPost]
        public ActionResult GetirSicilBilgisi(string TcVergiNo)
        {
            string tcNo = "";
            string vergiNo = "";

            if (!string.IsNullOrEmpty(TcVergiNo))
            {
                if (TcVergiNo.Length > 10)
                    tcNo = TcVergiNo;
                else
                    vergiNo = TcVergiNo;

                var sicilBilgisi = _sicilService.GetirSicilBilgisi(vergiNo, tcNo);

                KiraBeyanEkleVM beyanVM=new KiraBeyanEkleVM();
                beyanVM.Kiraci = new KiraciEkleVM() { 
                SicilNo=long.Parse(sicilBilgisi.SicilNo),
                VergiNo= long.Parse(sicilBilgisi.VergiNo),
                Ad=sicilBilgisi.Ad,
                Soyad=sicilBilgisi.Soyad,
                Tanim=sicilBilgisi.Tanim,
                IlAdi=sicilBilgisi.Il,
                IlceAdi=sicilBilgisi.Ilce,
                MahalleAdi=sicilBilgisi.Mahalle,
                AcikAdres=sicilBilgisi.AcikAdres,
                VergiDairesi=sicilBilgisi.VergiDairesi,
                };

                return View("Ekle", beyanVM);

            }

            return Json(new { Message = "Sicil Bilgisi Bulunamadı.", success = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new KiraBeyanEkleVM();
            ModelState.AddModelError("LogMessage", "Kira Beyan Ekleme Sayfası Görüntülendi.");
            return View(model);
        }


        #endregion
    }
}