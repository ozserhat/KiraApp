using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
   public interface IIlService
    {
        IEnumerable<Il> GetirListe();

        Il Getir(int id);

        Il Ekle(Il Il);

        Il Guncelle(Il Il);

        bool Sil(int id);
    }
}
