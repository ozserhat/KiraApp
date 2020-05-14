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
        DateTime TatilGunuKontrol(DateTime tarih);

        ResmiTatiller Guncelle(ResmiTatiller tur);

        bool Sil(int id);
    }
}