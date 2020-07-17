using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models.ViewModels
{
    public class IcraIslemeVM
    {
        public string Beyan_Id { get; set; }
        public string IcraDurum_Id { get; set; }

        public string Sayi { get; set; }

        public string VerilisTarihi { get; set; }

        public string BaskanlikOlurTarihi { get; set; }

        public string Aciklama { get; set; }

    }

    public class IcraListesiVM
    {
        public int? Beyan_Id { get; set; }
        public int? IcraDurum_Id { get; set; }

        public int? Sayi { get; set; }

        public string BeyanNo { get; set; }

        public DateTime VerilisTarihi { get; set; }

        public DateTime BaskanlikOlurTarihi { get; set; }

        public string Aciklama { get; set; }

        public string IcraDurum { get; set; }

    }
}