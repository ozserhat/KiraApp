using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IDuyuru_BildirimDal : IEntityRepository<Duyuru_Bildirim>
    {
        IEnumerable<Duyuru_Bildirim> GetirListe();
        Duyuru_Bildirim GetById(int id);
        bool Delete(int id);
    }
}
