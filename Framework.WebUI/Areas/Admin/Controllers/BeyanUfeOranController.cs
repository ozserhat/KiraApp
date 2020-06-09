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
using iTextSharp.text.pdf.qrcode;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class BeyanUfeOranController : Controller
    {
        #region Constructor

        private IBeyan_UfeOranService _service;
        private ISistemParametre_DetayService _parametreService;
        private IBeyan_UfeOranService _beyanUfeOranService;

        public BeyanUfeOranController(IBeyan_UfeOranService service, ISistemParametre_DetayService parametreService, IBeyan_UfeOranService beyanUfeOranService)
        {
            _service = service;
            _parametreService = parametreService;
            _beyanUfeOranService = beyanUfeOranService;
        }
        #endregion

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var ufeOranListe = _service.GetirListe();

            var model = new Beyan_UfeOranVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (ufeOranListe != null)
            {
                model.Beyan_UfeOranListe = new StaticPagedList<Beyan_UfeOran>(ufeOranListe, model.PageNumber, model.PageSize, ufeOranListe.Count());
                model.TotalRecordCount = ufeOranListe.Count();
            }

            return View(model);
        }

        public SelectList YilSelectList()
        {
            var yillar = _parametreService.GetirListe(1).Where(a => a.AktifMi.Value).Select(x => new { Deger = x.Deger, Ad = x.Ad }).ToList();

            return new SelectList(yillar, "Deger", "Ad");
        }

        public SelectList AySelectList()
        {
            var aylar = _parametreService.GetirListe(12).Select(x => new { Deger = x.Deger, Ad = x.Ad }).ToList();
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Ocak",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Şubat",
                                    Value="2"
                                  }
                                    ,
                                    new SelectListItem(){
                                    Text="Mart",
                                    Value="3"
                                  }
                                    ,
                                    new SelectListItem(){
                                    Text="Nisan",
                                    Value="4"
                                  }
                                    ,
                                    new SelectListItem(){
                                    Text="Mayıs",
                                    Value="5"
                                  },
                                    new SelectListItem(){
                                    Text="Haziran",
                                    Value="6"
                                  },
                                    new SelectListItem(){
                                    Text="Temmuz",
                                    Value="7"
                                  },
                                    new SelectListItem(){
                                    Text="Ağustos",
                                    Value="8"
                                  },
                                    new SelectListItem(){
                                    Text="Eylül",
                                    Value="9"
                                  },
                                    new SelectListItem(){
                                    Text="Ekim",
                                    Value="10"
                                  },
                                    new SelectListItem(){
                                    Text="Kasım",
                                    Value="11"
                                  }
            };
            return new SelectList(aylar, "Deger", "Ad");
        }

        public SelectList TurSelectList()
        {
            List<SelectListItem> newList = new List<SelectListItem>() {
                                  new SelectListItem(){
                                    Text="Üfe",
                                    Value="1"
                                  },
                                    new SelectListItem(){
                                    Text="Tüfe",
                                    Value="2"
                                    }
            };

            return new SelectList(newList, "Value", "Text");
        }

        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new Beyan_UfeOranEkleVM();
            model.YilSelectList = YilSelectList();
            model.AySelectList = AySelectList();
            model.TurSelectList = TurSelectList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(Beyan_UfeOranEkleVM ufeOran)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Beyan_UfeOran beyanOran = new Beyan_UfeOran()
                    {
                        Guid = Guid.NewGuid(),
                        Tur_Id = ufeOran.Tur_Id,
                        Ad = ufeOran.Adi,
                        Yil= ufeOran.Yil.Value,
                        Ay= ufeOran.Ay.Value,
                        Oran= ufeOran.Oran,
                        OlusturulmaTarihi = DateTime.Now,
                        OlusturanKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                        AktifMi = true
                    };

                    var result = _service.Ekle(beyanOran);

                    if (result.Id > 0)
                        return Json(new { Message = "Beyan Üfe Oranı Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Beyan Üfe Oranı Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new Beyan_UfeOranDuzenleVM();

            var oran = _service.GetirGuid(guid);

            if (oran != null)
            {
                model.Id = oran.Id;
                model.Guid = oran.Guid;
                model.Tur_Id = oran.Tur_Id;
                model.Adi = oran.Ad;
                model.Yil = oran.Yil.ToString();
                model.Ay = oran.Ay.ToString();
                model.Oran = oran.Oran;
                model.GuncelleyenKullanici_Id = oran.GuncelleyenKullanici_Id;
                model.GuncellenmeTarihi = oran.GuncellenmeTarihi;
                model.AktifMi = oran.AktifMi.Value;

                model.YilSelectList = YilSelectList();
                model.AySelectList = AySelectList();
                model.TurSelectList = TurSelectList();
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Duzenle(Beyan_UfeOranDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var oran = _service.GetirGuid(model.Guid);

                    if (oran != null)
                    {
                        oran.Id = model.Id;
                        oran.Guid = model.Guid;
                        oran.Tur_Id = model.Tur_Id;
                        oran.Ad = model.Adi;
                        oran.Yil = int.Parse(model.Yil);
                        oran.Ay = int.Parse(model.Ay);
                        oran.Oran = model.Oran;
                        oran.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ?
                        User.GetUserPropertyValue("UserId") : null);
                        oran.GuncellenmeTarihi = DateTime.Now;
                        oran.AktifMi = model.AktifMi;
                        oran = _service.Guncelle(oran);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Errors.Add(ex.Message);
                }
            }
            else
            {
                model.Errors = new List<string>();
                model.Errors.Add(VMErrors.ValidationError);
            }

            return View(model);
        }

        #endregion

        #region Sil 
        [HttpPost]
        public JsonResult Sil(int Id)
        {
            var oran = _service.Getir(Id);

            oran.AktifMi = false;

            oran = _service.Guncelle(oran);

            if (oran.AktifMi.HasValue && !oran.AktifMi.Value)
                return Json(new { success = true, Message = "Beyan Üfe Oran Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Beyan Üfe Oran Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}