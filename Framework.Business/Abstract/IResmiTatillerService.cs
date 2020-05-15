using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IResmiTatillerService
    {
        IEnumerable<ResmiTatiller> GetirListe();
        IEnumerable<ResmiTatiller> GetirListeAktif();
        ResmiTatiller Getir(int id);

        ResmiTatiller Ekle(ResmiTatiller tur);
        int TarihAraligiGunSayisi(DateTime baslangicTarihi, DateTime bitisTarihi);

        DateTime TatilGunuKontrol(DateTime tarih);

        int TatilGunuKontrol(DateTime baslangic,DateTime bitisTarihi,string[] tarihler,bool? resmiTatilVarmi);

        ResmiTatiller Guncelle(ResmiTatiller tur);

        bool Sil(int id);
    }
}