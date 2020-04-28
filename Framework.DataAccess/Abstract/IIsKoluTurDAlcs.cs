using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;

namespace Framework.DataAccess.Abstract
{
    public interface IIsKoluTurDAl : IEntityRepository<IsKoluTur>
    {
        IsKoluTur GetById(int id);
        IsKoluTur GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
