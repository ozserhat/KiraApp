using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Framework.WebUI.Models.ViewModels
{
    public class KiraBeyanVM : PagingVMBase
    {
        public IPagedList<Kira_Beyan> Beyanlar { get; set; }
    }

    public class KiraBeyanEkleVM : VMBase
    {

        [Display(Name = "Kiraci_Id")]
        public int Kiraci_Id { get; set; }

        [Display(Name = "Gayrimenkul_Id")]
        public int Gayrimenkul_Id { get; set; }

        [Display(Name = "Beyan_Id")]
        public int Beyan_Id { get; set; }

        public KiraciEkleVM Kiraci { get; set; }

        public GayrimenkulEkleVM Gayrimenkul { get; set; }

        public BeyanEkleVM Beyan { get; set; }

        public KiraBeyanVM KiraBeyan { get; set; }

        [Display(Name = "Oluşturan Kullanıcı")]
        public int? OlusturanKullanici_Id { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime? OlusturulmaTarihi { get; set; }

        [Display(Name = "Aktif Mi")]
        public bool AktifMi { get; set; }

    }
}