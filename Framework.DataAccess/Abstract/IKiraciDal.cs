using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
   public interface IKiraciDal : IEntityRepository<Kiraci>
    {
        IEnumerable<Kiraci> GetirListe();
        Kiraci GetById(int id);
        Kiraci GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
