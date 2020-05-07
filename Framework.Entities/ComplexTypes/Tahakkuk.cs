using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.ComplexTypes
{
    public class Tahakkuk
    {
    }

    public class TahakkukSorguSonucVm
    {
        public string BorcId { get; set; }

        public string Tip { get; set; }

        public string TurKod { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public int SicilNo { get; set; }

        public int MakbuzNo { get; set; }

        public int VezneNo { get; set; }

        public double Bakiye { get; set; }

        public double ArazTutar { get; set; }

        public double TahakkukTutar { get; set; }

        public double TahsilatToplam { get; set; }

        public int ModulGrup { get; set; }

        public string VadeTarihi { get; set; }
        public string TahsilatTarihi { get; set; }
        public string TahakkukTarihi { get; set; }

        public string HataKodu { get; set; }
    }

    public class TahakkukEkleSonucVm
    {
        public string HataKodu { get; set; }

        public string TahakkukId { get; set; }

        public string TahakkukKdvId { get; set; }

        public string TahakkukNo { get; set; }

        public bool Durum { get; set; }

        public string Aciklama { get; set; }
    }

    public class TahakkukEkleVm
    {
        public int GelirId { get; set; }

        public int KdvGelirId { get; set; }

        public int SicilNo { get; set; }

        public int Yil { get; set; }

        public int TaksitNo { get; set; }

        public double Tutar { get; set; }

        public int ModulGrup { get; set; }

        public string Aciklama { get; set; }

        public DateTime? SonOdemeTarihi { get; set; }
    }
}
