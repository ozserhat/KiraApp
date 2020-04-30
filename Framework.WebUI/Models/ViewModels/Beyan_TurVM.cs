using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;


namespace Framework.WebUI.Models.ViewModels
{
  
        public class Beyan_TurVM : PagingVMBase
        {
            [Display(Name = "Id")]
            public int Id { get; set; }

            [Display(Name = "Guid")]
            public Guid Guid { get; set; }

            [Display(Name = "Beyan Türü Adı")]
            public string BeyanTurAd { get; set; }

            [Display(Name = "Oluşturan Kullanıcı")]
            public int? OlusturanKullanici_Id { get; set; }

            [Display(Name = "Güncelleyen Kullanıcı")]
            public int? GuncelleyenKullanici_Id { get; set; }

            [Display(Name = "Oluşturulma Tarihi")]
            public DateTime? OlusturulmaTarihi { get; set; }

            [Display(Name = "Güncellenme Tarihi")]
            public DateTime? GuncellenmeTarihi { get; set; }

            [Display(Name = "Aktif Mi")]
            public bool? AktifMi { get; set; }

            public IPagedList<Beyan_Tur> Beyan_Tur { get; set; }
        }

        public class Beyan_TurEkleVM : VMBase
    {
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Beyan Tür Adı")]
        public string BeyanAdi { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }

    public class Beyan_TurDuzenleVM : VMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Display(Name = "Beyan Tür Adı")]
        public string BeyanAdi { get; set; }

        [Display(Name = "Güncelleyen Kullanıcı")]
        public int? GuncelleyenKullanici_Id { get; set; }

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? GuncellenmeTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }
    }
    }
