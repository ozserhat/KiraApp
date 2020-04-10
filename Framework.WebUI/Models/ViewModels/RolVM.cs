using Framework.Entities.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models.ViewModels
{
    public class RolVM : VMBase
    {
        public List<RolSecItem> Roller { get; set; }
    }

    public class RolPageVM : PagingVMBase
    {
        [Display(Name = "Rol Id")]
        public int RolId { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        [Display(Name = "Açıklama")]
        [MaxLength]
        public string Aciklama { get; set; }


        [Display(Name = "Aktif Mi")]
        public bool? AktifMi { get; set; }

        public IPagedList<Role> Roller { get; set; }
    }

    public class RolSecItem
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string AreaName { get; set; }

        public string Aciklama { get; set; }
    }

    public class RolEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }
       
        [Required(ErrorMessage = "Rol Adı Boş Bırakılamaz!!")]
        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        [Required(ErrorMessage = "Açıklama Bilgisi Boş Bırakılamaz!!")]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool? AktifMi { get; set; }
    }

    public class RolDuzenleVM : VMBase
    {
        [Display(Name = "RolId")]
        public int RolId { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Required(ErrorMessage = "Rol Adı Boş Bırakılamaz!!")]
        [Display(Name = "Rol Adı")]
        public string RolAdi { get; set; }

        [Required(ErrorMessage = "Açıklama Bilgisi Boş Bırakılamaz!!")]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
}