using System;
using System.Collections.Generic;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
namespace Framework.DataAccess.Abstract
{
    public interface IKiraciDal : IEntityRepository<Kiraci>
    {
        Kiraci GetById(int id);
        Kiraci GetByGuid(Guid guid);
        bool Delete(int id);
        Kiraci GetirTcKimlikNo(long TcKimlikNo);
        Kiraci GetirVergiNo(long vergiNo);
    }
}
