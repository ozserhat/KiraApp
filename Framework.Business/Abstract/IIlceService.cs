using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
   public  interface IIlceService
    {
        IEnumerable<Ilce> GetirListe();

        Ilce Getir(int id);

        Ilce Ekle(Ilce Il);

        Ilce Guncelle(Ilce Il);

        bool Sil(int id);
    }
}
