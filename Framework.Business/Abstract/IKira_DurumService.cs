using System;
using Framework.Entities.Concrete;
using System.Collections.Generic;


namespace Framework.Business.Abstract
{
    public interface IKira_DurumService
    {
        IEnumerable<Kira_Durum> GetirListe();

        Kira_Durum Getir(int id);

        Kira_Durum GetirGuid(Guid guid);

        Kira_Durum Ekle(Kira_Durum kiriDurum);

        Kira_Durum Guncelle(Kira_Durum kiriDurum);

        bool Sil(int id);
    }
}
