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
    public class RolesController : Controller
    {
        // GET: Admin/Roles
        private IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var roller = _roleService.GetAll();

            var model = new RolPageVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (roller != null)
            {
                model.Roller = new StaticPagedList<Role>(roller, model.PageNumber, model.PageSize, roller.Count());
                model.TotalRecordCount = roller.Count();
            }

            return View(model);
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
        public JsonResult Ekle(RolEkleVM rol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Role role = new Role()
                    {
                        Guid = Guid.NewGuid(),
                        Name = rol.RolAdi,
                        Description = rol.Aciklama,
                        IsActive = true,
                        IsDeleted=false
                    };

                    var result = _roleService.Ekle(role);

                    if (result.Id > 0)
                        return Json(new { Message = "Rol Bilgisi Başarıyla Kaydedildi.", success = true }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { Message = "Rol Bilgisi Kaydedilemedi!!!", success = false }, JsonRequestBehavior.AllowGet);
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
            var model = new RolDuzenleVM();

            var rol = _roleService.GetByGuid(guid);

            if (rol != null)
            {
                model.RolId = rol.Id;
                model.Guid = rol.Guid;
                model.RolAdi = rol.Name;
                model.Aciklama = rol.Description;
                model.AktifMi = rol.IsActive.Value;
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
        public ActionResult Duzenle(RolDuzenleVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rol = _roleService.GetByGuid(model.Guid);

                    if (rol != null)
                    {
                        rol.Id = model.RolId;
                        rol.Guid = model.Guid;
                        rol.Name = model.RolAdi;
                        rol.Description = model.Aciklama;
                        rol.IsActive = model.AktifMi;
                        rol.IsDeleted = false;
                        rol = _roleService.Guncelle(rol);
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
            var rol = _roleService.GetById(Id); ;

            rol.IsDeleted = true;

            rol = _roleService.Guncelle(rol);

            if (rol.IsDeleted.HasValue && rol.IsDeleted.Value)
                return Json(new { success = true, Message = "Rol Bilgisi Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Message = "Rol Bilgisi Silinemedi!!!" }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}