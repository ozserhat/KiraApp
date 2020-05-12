﻿using Framework.Entities.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Models.ViewModels
{
    public class TahakkukVM: PagingVMBase
    {
        [Display(Name = "KiraBeyan_Id")]
        public int? KiraBeyan_Id { get; set; }

        [Display(Name = "Beyan Yıl")]
        public int BeyanYil { get; set; }

        [Display(Name = " Tahakkuk Tur")]
        public string TahakkukTur { get; set; }

        [Display(Name = "Taksit No")]
        public int TaksitNo { get; set; }

        [Display(Name = "Vade Tarihi")]
        public DateTime? VadeTarihi { get; set; }

        [Display(Name = "Tahakkuk Tarihi")]
        public DateTime? TahakkukTarihi { get; set; }

        [Display(Name = "Tutar")]
        public decimal? Tutar { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Ödeme Durumu")]
        public bool OdemeDurumu { get; set; }

        public IPagedList<Tahakkuk> Tahakkuklar { get; set; }


        [Display(Name = "Tahakkuk Türü")]
        public SelectList TahakkukTurSelectList { get; set; }

        [Display(Name = "Beyan Yıl")]
        public SelectList BeyanYilSelectList { get; set; }

        [Display(Name = "Ödeme Durumu")]
        public SelectList OdemeDurumuSelectList { get; set; }
    }

    public class TahakkukDetayVM
    {
        public List<TahakkukVM> TahakkukDetay { get; set; }
    }
}