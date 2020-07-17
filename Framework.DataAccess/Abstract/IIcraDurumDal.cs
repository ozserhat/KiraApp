using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;

namespace Framework.DataAccess.Abstract
{
    public interface IIcraDurumDal : IEntityRepository<IcraDurum>
    {
        IcraDurum GetById(int id);
        bool Delete(int id);
    }
}
