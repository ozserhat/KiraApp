using Framework.Core.DataAccess;
using Framework.Entities.Concrete;
using System;

namespace Framework.DataAccess.Abstract
{
    public interface IBeyan_TurDal : IEntityRepository<Beyan_Tur>
    {
        Beyan_Tur GetById(int id);
        Beyan_Tur GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
