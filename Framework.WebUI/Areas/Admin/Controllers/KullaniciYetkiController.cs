using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Helpers;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class KullaniciYetkiController : Controller
    {

        #region Constructor
        private IUserService _userService;
        private IUserPermissionsService _service;
        private IControllerActionService _controlleService;
        public KullaniciYetkiController(IUserPermissionsService service, IUserService userService, IControllerActionService controlleService)
        {
            _service = service;
            _userService = userService;
            _controlleService = controlleService;
        }
        #endregion

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var kullanicilar = _userService.GetAll();

            var model = new KullaniciYetkiVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (kullanicilar != null)
            {
                model.Kullanici = new StaticPagedList<User>(kullanicilar, model.PageNumber, model.PageSize, kullanicilar.Count());
                model.TotalRecordCount = kullanicilar.Count();
            }

            return View(model);
        }

        public ActionResult KullaniciYetkileri(int KullaniciId, int? page, int pageSize = 15)
        {
            var yetki = _service.GetUserByPermissions(KullaniciId);

            var idList = yetki.Select(x => x.ControllerAction_Id).ToList();

            var sayfalListe = _controlleService.GetAll().Where(a => !idList.Contains(a.Id));

            var model = new KullaniciYetkiVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (yetki != null)
            {
                model.Sayfalar = new StaticPagedList<ControllerAction>(sayfalListe, model.PageNumber, model.PageSize, sayfalListe.Count());
                model.KullaniciYetki = new StaticPagedList<User_Permission>(yetki, model.PageNumber, model.PageSize, yetki.Count());
                model.TotalRecordCount = yetki.Count();
            }

            return View(model);
        }

       

        #endregion

        #region KullaniciYetkiEklemeKaldirma

        [HttpPost]
        public JsonResult YetkiKaldir(string[] kullaniciYetki, string KullaniciId)
        {
            User_Permission permission = new User_Permission();

            foreach (string item in kullaniciYetki)
            {
                var yetki = _service.GetById(int.Parse(item));

                yetki.AktifMi = false;
                yetki.GuncellenmeTarihi = DateTime.Now;
                yetki.GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null);
                permission = _service.Guncelle(yetki);
            }

            if (permission.AktifMi.HasValue && !permission.AktifMi.Value)
                return Json(new { success = true, Message = "Kullanıcı Yetkileri Başarıyla Kaldırıldı." }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Kullanıcı Yetkileri Kaldırılamadı!!!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult YetkiEkle(string[] kullaniciYetki, string KullaniciId)
        {
            User_Permission permission = new User_Permission();

            foreach (string item in kullaniciYetki)
            {
                permission = new User_Permission()
                {
                    ControllerAction_Id = int.Parse(item),
                    User_Id = int.Parse(KullaniciId),
                    GuncellenmeTarihi = DateTime.Now,
                    GuncelleyenKullanici_Id = int.Parse(!string.IsNullOrEmpty(User.GetUserPropertyValue("UserId")) ? User.GetUserPropertyValue("UserId") : null),
                    AktifMi = true,
                    YetkiliMi = true
                };

                permission = _service.Ekle(permission);
            }

            if (permission.AktifMi.HasValue && permission.AktifMi.Value)
                return Json(new { success = true, Message = "Kullanıcı Yetkileri Eklendi." }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Kullanıcı Yetkileri Eklenemedi!!!" }, JsonRequestBehavior.AllowGet);
        }
    
        #endregion
    }
}