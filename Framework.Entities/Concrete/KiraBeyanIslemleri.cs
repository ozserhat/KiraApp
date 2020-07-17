using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.Concrete
{
    public class KiraBeyanIslemleri
    {
        public KiraBeyanModel PasifeAlinanlar { get; set; }
        public KiraBeyanModel Kapananlar { get; set; }
        public KiraBeyanModel Eklenenler { get; set; }

    }

    public class KiraBeyanModel
    {
        public bool ArtisMi { get; set; }
        public Kiraci Kiraci { get; set; }

        public Gayrimenkul Gayrimenkul { get; set; }

        public Beyan Beyan { get; set; }

        public Kira_Beyan KiraBeyan { get; set; }

        public List<Beyan_Dosya> BeyanDosyalar { get; set; }
        //public List<Tahakkuk> Tahakkuklar { get; set; }

        public List<GL_BORC> Tahakkuklar { get; set; }

    }
}
