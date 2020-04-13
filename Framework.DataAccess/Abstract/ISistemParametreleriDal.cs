using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface ISistemParametreleriDal : IEntityRepository<SistemParametreleri>
    {
        SistemParametreleri GetById(int id);
        SistemParametreleri GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
