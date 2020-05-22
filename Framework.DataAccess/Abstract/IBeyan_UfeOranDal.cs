using System;
using Framework.Core.DataAccess;
using Framework.Entities.Concrete;

namespace Framework.DataAccess.Abstract
{
    public interface IBeyan_UfeOranDal : IEntityRepository<Beyan_UfeOran>
    {
        Beyan_UfeOran GetById(int id);
        Beyan_UfeOran GetByGuid(Guid guid);
        bool Delete(int id);
    }
}
