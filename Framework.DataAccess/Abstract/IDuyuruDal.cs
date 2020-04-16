using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IDuyuruDal : IEntityRepository<Duyuru>
    {
        IEnumerable<Duyuru> GetirListe();
        Duyuru GetById(int id);
        Duyuru GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
