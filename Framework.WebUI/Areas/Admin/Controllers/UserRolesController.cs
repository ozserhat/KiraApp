using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
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
    public class UserRolesController : Controller
    {
        #region Constructor
        private IUserService _userService;
        private IRoleService _roleService;
        private IUserRoleService _userroleService;
        public UserRolesController(IUserService userService, IRoleService roleService, IUserRoleService userroleService)
        {
            _roleService = roleService;
            _userService = userService;
            _userroleService = userroleService;
        } 
        #endregion

        // GET: Admin/UserRoles
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var model = new UserRolesPageVm();

            model.SelectListKullanicilar = KullaniciSelectList();

            model.SelectListRoller = RolSelectList();

            var kullaniciRol = _userroleService.GetAll();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (model != null)
            {
                model.UserList = new StaticPagedList<User_Role>(kullaniciRol, model.PageNumber, model.PageSize, kullaniciRol.Count());
                model.TotalRecordCount = kullaniciRol.Count();
            }

            return View(model);
        }

        public SelectList RolSelectList()
        {
            var roller = _roleService.GetAll().Select(x => new { Id = x.Id, Ad = x.Name }).ToList();

            return new SelectList(roller, "Id", "Ad");
        }

        public SelectList KullaniciSelectList()
        {
            var roller = _userService.GetAll().Select(x => new { Id = x.Id, Ad = x.UserName }).ToList();

            return new SelectList(roller, "Id", "Ad");
        } 
        #endregion

        #region Ekle

        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new RolEkleVM();
            return View(model);
        }

        [HttpPost]
        public JsonResult Ekle(KullaniciRolEkleVM model)
        {
            try
            {
                bool sonuc = false;

                if (ModelState.IsValid)
                {
                    User_Role userRole = new User_Role()
                    {
                        User_Id = model.UserId,
                        Role_Id = model.RolId,
                        IsDeleted = false,
                        Users=null
                    };

                    sonuc = _userroleService.GetUserRoleExists(userRole);

                    if (!sonuc)
                    {
                        var result = _userroleService.Ekle(userRole);

                        if (result.Id > 0)
                            return Json(new { Message = "Rol Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { Message = "Kullanıcı Rolü Daha Önce Tanımlanmış!!!", success = false }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Message = "Rol Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Sil 
        [HttpPost]
        public JsonResult Sil(int Id)
        {
            var userRol = _userroleService.GetById(Id); ;

            userRol.IsDeleted = true;

            userRol = _userroleService.Guncelle(userRol);

            if (userRol.IsDeleted)
                return Json(new { success = true, Message = "Kullanıcı Rol Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Kullanıcı Rol Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}