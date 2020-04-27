using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
   public interface IMahalleService
    {
        IEnumerable<Mahalle> GetirListe();

        Mahalle Getir(int id);

        Mahalle Ekle(Mahalle Mahalle);

        Mahalle Guncelle(Mahalle Mahalle);

        bool Sil(int id);
    }
}
