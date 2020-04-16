using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IGayrimenkulDal : IEntityRepository<Gayrimenkul>
    {
        IEnumerable<Gayrimenkul> GetirListe();
        Gayrimenkul GetById(int id);
        Gayrimenkul GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
