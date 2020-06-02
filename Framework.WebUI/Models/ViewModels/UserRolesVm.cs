using Framework.Entities.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Models.ViewModels
{
    public class UserRolesVm : VMBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class KullaniciRolEkleVM : VMBase
    {

        [Required(ErrorMessage = "Rol Adı Boş Bırakılamaz!!")]
        [Display(Name = "Rol Adı")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "Açıklama Bilgisi Boş Bırakılamaz!!")]
        [Display(Name = "Açıklama")]
        public int UserId { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool? AktifMi { get; set; }
    }

    public class UserRolesPageVm : PagingVMBase
    {
        [Display(Name = "Roller")]
        public SelectList SelectListRoller { get; set; }

        [Display(Name = "Kullanıcılar")]
        public SelectList SelectListKullanicilar { get; set; }

        public IPagedList<User_Role> UserList { get; set; }
    }
}