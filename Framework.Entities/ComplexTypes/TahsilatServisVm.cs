using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.ComplexTypes
{
    public class TahsilatServisVm
    {
        public int MakbuzNo { get; set; }
        public int TahakkukNo { get; set; }
        public int VezneNo { get; set; }
        public decimal? OdenenTutar { get; set; }
        public decimal? TahakukTutar { get; set; }
        public decimal? GenelToplam { get; set; }
        public int? Artan { get; set; }
        public int? Azalan { get; set; }
        public DateTime? OdemeTarihi { get; set; }
        public DateTime? TahakkukTarihi { get; set; }
        public string Sonuc { get; set; }
    }
}
