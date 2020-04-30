using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;

namespace Framework.DataAccess.Abstract
{
    public interface IResmiTatillerDal : IEntityRepository<ResmiTatiller>
    {
        ResmiTatiller GetById(int id);
        bool Delete(int id);
    }
}
