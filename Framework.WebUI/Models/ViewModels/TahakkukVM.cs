using Framework.Entities.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models.ViewModels
{
    public class TahakkukVM
    {
        public int? KiraBeyan_Id { get; set; }
        public int BeyanYil { get; set; }
        public int TaksitNo { get; set; }
        public DateTime? VadeTarihi { get; set; }
        public decimal? Tutar { get; set; }
        public string Aciklama { get; set; }
        public bool OdemeDurumu { get; set; }
    }

    public class TahakkukDetayVM
    {
        public List<TahakkukVM> TahakkukDetay { get; set; }
    }
}