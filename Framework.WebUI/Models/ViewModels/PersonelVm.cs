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
    public class PersonelVm : PagingVMBase
    {
        // GET: PersonelVm
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }
        public IPagedList<Personel> Personeller{ get; set; }
    }

    public class PersonelEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Required(ErrorMessage = "Personel Adı Boş Bırakılamaz!!")]
        [Display(Name = "Personel Adı")]
        public string PersonelAdi { get; set; }


        [Required(ErrorMessage = "Personel Soyadı Boş Bırakılamaz!!")]
        [Display(Name = "Personel Soyadı")]
        public string PersonelSoyadi { get; set; }
    }
}