using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IDuyuru_BildirimDal : IEntityRepository<Duyuru_Bildirim>
    {
        IEnumerable<Duyuru_Bildirim> GetirListe();
        IEnumerable<Duyuru_Bildirim> GetirKullaniciMesajlari(int KullaniciId);
        Duyuru_Bildirim GetById(int id);
        bool Add(IEnumerable<Duyuru_Bildirim> entities);
        bool Delete(int id);
    }
}
