using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IIcraDurumService
    {
        IEnumerable<IcraDurum> GetirListe();
        IEnumerable<IcraDurum> GetirListeAktif();
        IcraDurum Getir(int id);

        IcraDurum Ekle(IcraDurum tur);

        IcraDurum Guncelle(IcraDurum tur);

        bool Sil(int id);
    }
}
