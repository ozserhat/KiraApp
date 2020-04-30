using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;

namespace Framework.DataAccess.Abstract
{
    public interface IKira_BeyanDal : IEntityRepository<Kira_Beyan>
    {
        Kira_Beyan GetById(int id);
        bool Delete(int id);
    }
}