using System;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IKira_DurumDal : IEntityRepository<Kira_Durum>
    {
        Kira_Durum GetById(int id);
        Kira_Durum GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
