using System;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IDuyuru_TurDal : IEntityRepository<Duyuru_Tur>
    {
        Duyuru_Tur GetById(int id);
        Duyuru_Tur GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
