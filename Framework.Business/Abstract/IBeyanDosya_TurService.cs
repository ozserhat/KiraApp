using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Abstract
{
    public interface IBeyanDosya_TurService
    {
        IEnumerable<BeyanDosya_Tur> GetirListe();

        BeyanDosya_Tur Getir(int id);

        BeyanDosya_Tur GetirGuid(Guid guid);

        BeyanDosya_Tur Ekle(BeyanDosya_Tur tur);

        BeyanDosya_Tur Guncelle(BeyanDosya_Tur tur);

        bool Sil(int id);
    }
}
