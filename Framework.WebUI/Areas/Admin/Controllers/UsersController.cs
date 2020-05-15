using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Models;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        // GET: Admin/Users  
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var kullanicilar = _userService.GetAll();

            var model = new KullaniciPageVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (kullanicilar != null)
            {
                model.Kullanicilar = new StaticPagedList<User>(kullanicilar, model.PageNumber, model.PageSize, kullanicilar.Count());
                model.TotalRecordCount = kullanicilar.Count();
            }

            return View(model);
        }
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new KullaniciEkleVM();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(KullaniciEkleVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var encryptpass = _userService.CreatePasswordHash(model.Sifre);

                    User kullanici = new User()
                    {
                        Guid = Guid.NewGuid(),
                        UserName = model.KullaniciAdi,
                        Password = encryptpass,
                        PasswordSalt = "b14ca5898a4e4133bbce2ea2315a1916",
                        FirstName = model.Ad,
                        LastName = model.Soyad,
                        Email = model.EMail,
                        IsActive = true,
                        IsDeleted = false
                    };

                    var result = _userService.Ekle(kullanici);

                    if (result.Id > 0)
                        return Json(new { Message = "Kullanıcı Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Kullanıcı Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new KullaniciDuzenleVM();

            var kullanici = _userService.GetByGuid(guid);

            if (kullanici != null)
            {
                model.KullaniciId = kullanici.Id;
                model.Guid = kullanici.Guid;
                model.KullaniciAdi = kullanici.UserName;
                model.Sifre = kullanici.Password;
                model.Ad = kullanici.FirstName;
                model.Soyad = kullanici.LastName;
                model.EMail = kullanici.Email;
                model.AktifMi = kullanici.IsActive.Value;
            }
            else
            {
                model.Errors.Add(VMErrors.NotFound);
                model.HideContent = true;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(KullaniciDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kullanici = _userService.GetByGuid(model.Guid);

                    var encryptpass = _userService.CreatePasswordHash(model.Sifre);

                    if (kullanici != null)
                    {
                        kullanici.Id = model.KullaniciId;
                        kullanici.Guid = model.Guid;
                        kullanici.UserName = model.KullaniciAdi;
                        kullanici.Password = encryptpass;
                        kullanici.PasswordSalt = "b14ca5898a4e4133bbce2ea2315a1916";
                        kullanici.FirstName = model.Ad;
                        kullanici.LastName = model.Soyad;
                        kullanici.Email = model.EMail;
                        kullanici.IsActive = model.AktifMi;
                        kullanici.IsDeleted = false;
                        kullanici = _userService.Guncelle(kullanici);
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
            var user = _userService.GetById(Id);

            user.IsDeleted = true;

            user = _userService.Guncelle(user);
      
            if (user.IsDeleted.HasValue && user.IsDeleted.Value)
            
                return Json(new { success = true, Message = "Kullanıcı Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Kullanıcı Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}