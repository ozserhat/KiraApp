using System;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IKiraciTurDal : IEntityRepository<KiraciTur>
    {
        KiraciTur GetById(int id);
        KiraciTur GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
