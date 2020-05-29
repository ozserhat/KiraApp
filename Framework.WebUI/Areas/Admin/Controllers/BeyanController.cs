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

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
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

        public static KiraBeyanEkleVM _beyanVM;
        private IBeyan_TurService _beyanTurService;

        private IIlService _ilService;
        private IIlceService _ilceService;
        private IMahalleService _mahalleService;
        private IUserService _userService;
        private IPersonel_BeyanService _personelBeyanService;


        public BeyanController(IGayrimenkulService gayrimenkulservice,
            IBeyanService beyanService,
            IKira_BeyanService kiraBeyanService,
            KiraBeyanEkleVM beyanVM,
            IKira_DurumService kiraDurumService,
            IOdemePeriyotTurService odemePeriyotService,
        IBeyan_TurService beyanTurService,
        IBeyanDosya_TurService dosyaService,
        IBeyan_DosyaService beyanDosyaService,
        IIlService ilService,
        IIlceService ilceService,
        IMahalleService mahalleService,
        IUserService userService,
        IPersonel_BeyanService personelBeyanService
        )
        {
            _gayrimenkulservice = gayrimenkulservice;
            _beyanService = beyanService;
            _kiraBeyanService = kiraBeyanService;
            _beyanVM = beyanVM;
            _dosyaService = dosyaService;
            _beyanTurService = beyanTurService;
            _odemePeriyotService = odemePeriyotService;
            _kiraDurumService = kiraDurumService;
            _beyanDosyaService = beyanDosyaService;
            _ilService = ilService;
            _ilceService = ilceService;
            _mahalleService = mahalleService;
            _userService = userService;
            _personelBeyanService = personelBeyanService;
        }
        #endregion
        // GET: Admin/Beyan

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
                model.KullaniciSelectList = KullaniciSelectList();
                model.Beyanlar = new StaticPagedList<Kira_Beyan>(beyanlar, model.PageNumber, model.PageSize, beyanlar.Count());
                model.TotalRecordCount = beyanlar.Count();
            }

            return View(model);

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

        [HttpPost]
        public JsonResult PersonelBeyanEkle(string[] kullaniciYetki, string KullaniciId)
        {
            Kira_Beyan kiraBeyan = new Kira_Beyan();
            if (kullaniciYetki == null)
                return Json(new { success = false, Message = "Lütfen Beyan Seçiniz" }, JsonRequestBehavior.AllowGet);
            if (string.IsNullOrEmpty(KullaniciId))
                return Json(new { success = false, Message = "Lütfen Kullanıcı Seçiniz" }, JsonRequestBehavior.AllowGet);

            foreach (string item in kullaniciYetki)
            {
                kiraBeyan = _kiraBeyanService.Getir(Convert.ToInt32(item));
                kiraBeyan.SorumluPersonelId = Convert.ToInt32(KullaniciId);
                //var result = _personelBeyanService.BeyanSil(int.Parse(item));
                //personelBeyan = new PersonelBeyan()
                //{

                //    Beyan_Id = int.Parse(item),
                //    Personel_Id = int.Parse(KullaniciId),
                //    GuncellenmeTarihi = DateTime.Now,
                //    GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                //    AktifMi = true,
                //    OlusturulmaTarihi = DateTime.Now,
                //};
                kiraBeyan = _kiraBeyanService.Guncelle(kiraBeyan);

            }

            if (kiraBeyan.Id > 0)
                return Json(new { success = true, Message = "Personel Beyan Eklendi!!!" }, JsonRequestBehavior.AllowGet);

            else
                return Json(new { success = false, Message = "Personel Beyan Eklenemedi!!!" }, JsonRequestBehavior.AllowGet);


        }

        public SelectList KullaniciSelectList()
        {
            var roller = _userService.GetAll().Select(x => new { Id = x.Id, Ad = x.UserName }).ToList();

            return new SelectList(roller, "Id", "Ad");
        }

        public SelectList IlceSelectList()
        {
            var ilceler = _ilceService.GetirListe().Where(a => a.Il_Id == 6).Select(x => new { Id = x.Id, Ad = x.Ad }).ToList();

            return new SelectList(ilceler, "Id", "Ad");
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
    }
}